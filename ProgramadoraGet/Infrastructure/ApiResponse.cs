using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ProgramadoraGet.Utils.ErrorMessagesHelper;

namespace ProgramadoraGet.Infrastructure
{
    public class ApiResponse<T>
    {
        public T data;
        public List<ErrorMessage> erros;
    }
}
