using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Utils
{
    public class ErrorMessagesHelper
    {
        public static List<ErrorMessage> GetErrors(ModelStateDictionary modelState)
        {
            List<ErrorMessage> erros = new List<ErrorMessage>();

            foreach (var key in modelState.Keys)
            {
                ErrorMessage error = new ErrorMessage();
                error.fieldName = key;

                foreach (var e in modelState[key].Errors)
                {
                    error.errors.Add(e.ErrorMessage);
                }

                erros.Add(error);
            }

            return erros;
        }

        public class ErrorMessage
        {
            public string fieldName { get; set; }

            public List<string> errors { get; set; }

            public ErrorMessage()
            {
                errors = new List<string>();
            }
        }
    }
}
