using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProgramadoraGet.Infrastructure;
using ProgramadoraGet.Utils;

namespace ProgramadoraGet.Features.AcademicQualification
{
    [Route("api/[controller]")]
    public class AcademicQualificationController : Controller
    {
        private Db db;

        public AcademicQualificationController(Db db)
        {
            this.db = db;
        }

        // TODO: update, read
        [HttpPost]
        public async Task<DefaultResponse<Domain.AcademicQualification>> Create ([FromBody]Create.Model model){
            var response = new DefaultResponse<Domain.AcademicQualification>();

            if (!ModelState.IsValid)
            {
                response.erros = ErrorMessagesHelper.GetErrors(ModelState);
                return response;
            }

            var services = new Create.Services(db);

            response.data = await services.Save(model);

            return response;
        }

        [HttpGet("{Id}")]
        public async Task<IList<Domain.AcademicQualification>> ReadOne(Read.Model model)
        {
            return await new Read.Services(db).One(model);
        }

        [HttpDelete("{Id}")]
        public async Task Delete(Delete.Model model)
        {
            await new Delete.Services(db).Trash(model);
        }

        [HttpPut("{Id}")]
        public async Task<DefaultResponse<Domain.AcademicQualification>> Update(Update.Model model)
        {
            var response = new DefaultResponse<Domain.AcademicQualification>();

            if (!ModelState.IsValid)
            {
                response.erros = ErrorMessagesHelper.GetErrors(ModelState);
                return response;
            }

            response.data = await new Update.Services(db).Save(model);
            return response;
        }

    }
}