using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using osu.Game.Rulesets;
using osu.Game.Rulesets.Catch;
using osu.Game.Rulesets.Mania;
using osu.Game.Rulesets.Osu;
using osu.Game.Rulesets.Taiko;
using osu.Game.Scoring;
using osu.Game.Scoring.Legacy;
using vault.Entities;
using DbContext = vault.Databases.DbContext;

namespace vault.Controllers
{
    [ApiController]
    [Route("/replays")]
    public class ReplayController : Controller
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public enum Sort { Timestamp, Score, MaxCombo, Miss }
        public enum SortDirection { Ascending, Descending }
        
        
        private static readonly Ruleset[] BuiltInRulesets =
        {
            new OsuRuleset(),
            new TaikoRuleset(),
            new CatchRuleset(),
            new ManiaRuleset()
        };
        
        private readonly DbContext dbContext;
        public ReplayController(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [FromQuery(Name = "order")] public Sort Order { get; set; } = Sort.Timestamp;
        [FromQuery(Name = "direction")] public SortDirection Direction { get; set; } = SortDirection.Descending;
        [FromQuery(Name = "page")] public int PageIndex { get; set; } = 0;
        [FromQuery(Name = "count")] public int PageCount { get; set; } = 50;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Replay>>> Recent(CancellationToken cancellationToken)
        {
            IQueryable<Replay> query = dbContext.Replays;
#pragma warning disable CS8524
            query = Direction switch
            {
                SortDirection.Descending => Order switch
                {
                    Sort.Timestamp => query.OrderByDescending(r => r.Timestamp),
                    Sort.Score => query.OrderByDescending(r => r.Score),
                    Sort.MaxCombo => query.OrderByDescending(r => r.MaxCombo),
                    Sort.Miss => query.OrderByDescending(r => r.CountMiss)
                },
                SortDirection.Ascending => Order switch
                {
                    Sort.Timestamp => query.OrderBy(r => r.Timestamp),
                    Sort.Score => query.OrderBy(r => r.Score),
                    Sort.MaxCombo => query.OrderBy(r => r.MaxCombo),
                    Sort.Miss => query.OrderBy(r => r.CountMiss)
                }
            };
#pragma warning restore CS8524
            
            var replays = await query
                .Skip(PageIndex * PageCount)
                .Take(PageCount)
                .Include(r => r.Beatmap)
                .ThenInclude(b => b!.Detail)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            return Json(
                replays
                    .Select(r =>
                    {
                        var score = new ScoreInfo
                        {
                            Ruleset = BuiltInRulesets[r.Mode].RulesetInfo
                        };
                        score.SetCount50(r.Count50);
                        score.SetCount100(r.Count100);
                        score.SetCount300(r.Count300);
                        score.SetCountGeki(r.CountGeki);
                        score.SetCountKatu(r.CountKatsu);
                        score.SetCountMiss(r.CountMiss);
                        LegacyScoreDecoder.PopulateAccuracy(score);
                        r.Accuracy = score.Accuracy * 100;
                        return r;
                    })
            );
        }
    }
}