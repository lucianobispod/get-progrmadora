using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProgramadoraGet.Infrastructure;
using ProgramadoraGet.Utils;

namespace ProgramadoraGet.Features.HistoricAcademic
{
    public class HistoricAcademicController : Controller
    {
        private Db db;

        public HistoricAcademicController(Db db)
        {
            this.db = db;
        }

        // TODO: Insert, update, delete, read
        [HttpPost]
        public async Task<DefaultResponse<Domain.HistoricAcademic>> Create ([FromBody]Create.Model model){
            var response = new DefaultResponse<Domain.HistoricAcademic>();

            if (!ModelState.IsValid)
            {
                response.erros = ErrorMessagesHelper.GetErrors(ModelState);
                return response;
            }

            var services = new Create.Services(db);

            response.data = await services.Save(model);

            return response;
        }
    

    }
}