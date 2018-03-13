using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProgramadoraGet.Infrastructure;

namespace ProgramadoraGet.Features.Match
{
    [Route("api/[controller]")]
    public class MatchController : Controller
    {
        //TODO: ReadOne
        //TODO: ReadAll
        //TODO: Create
        //TODO: Update
        //TODO: Delete

        private Db db;

        public MatchController(Db db)
        {
            this.db = db;
        }

        //[HttpPost]


    }
}