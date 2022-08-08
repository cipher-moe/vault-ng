using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vault.Entities;
using Microsoft.EntityFrameworkCore;
using Pepper.Commons.Osu;
using DbContext = vault.Databases.DbContext;

namespace vault.Controllers
{
    [ApiController]
    [Route("/beatmaps")]
    public class BeatmapController : Controller
    {
        private readonly DbContext dbContext;
        private readonly OsuRestClient osuRestClient;

        public BeatmapController(DbContext dbContext, OsuRestClient osuRestClient)
        {
            this.dbContext = dbContext;
            this.osuRestClient = osuRestClient;
        }

        [FromQuery(Name = "page")] public int PageIndex { get; set; } = 0;
        [FromQuery(Name = "count")] public int PageCount { get; set; } = 50;

        [HttpGet]
        [Route("mostplayed")]
        public Task<MostPlayedMapsAggregateRecord[]> MostPlayed(CancellationToken cancellationToken)
        {
            var result = dbContext.AggregateDbSet
                .FromSqlRaw(
                    "SELECT * FROM (SELECT `beatmap_hash`, COUNT(`sha256`) as `count` FROM `replays` GROUP BY `beatmap_hash`) as t1 LEFT JOIN `beatmaps` ON `beatmaps`.`md5` = `t1`.`beatmap_hash`")
                .OrderByDescending(r => r.Count)
                .Skip(PageIndex * PageCount)
                .Take(PageCount)
                .Include(r => r.Detail)
                .ToArrayAsync(cancellationToken);
            return result;
        }
    }
}