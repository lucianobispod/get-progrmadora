using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProgramadoraGet.Infrastructure;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProgramadoraGet.Features.Points
{
    [Route("api/[controller]")]
    public class PointsController : Controller
    {
        private Db db;

        public PointsController(Db db)
        {
            this.db = db;
        }

        [HttpPost]
        public async Task<string> AddPoint([FromBody] AddPoints.Model model)
        {
           return await new AddPoints.Services(db).SavePoints(model);
        }

    }
}
