using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [FromQuery(Name = "page")] public int PageIndex { get; set; } = 0;

        [FromQuery(Name = "count")] public int PageCount { get; set; } = 50;

        [HttpGet]
        [Route("recent")]
        public async Task<ActionResult<IEnumerable<Replay>>> Recent(CancellationToken cancellationToken)
        {
            var replays = await dbContext.Replays
                .OrderByDescending(replay => replay.Timestamp)
                .Skip(PageIndex * PageCount)
                .Take(PageCount)
                .Include(r => r.Beatmap)
                .ThenInclude(b => b!.Detail)
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