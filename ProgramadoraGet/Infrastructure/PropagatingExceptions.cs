using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Infrastructure
{
    public class HttpException : Exception
    {
        public int StatusCode { get; set; }

        public dynamic Body { get; set; }

        public HttpException(int statusCode)
        {
            this.StatusCode = statusCode;
        }

        public HttpException(int statusCode, dynamic body)
        {
            this.StatusCode = statusCode;
            this.Body = body;
        }
    }

    /// <summary>
    /// 404 status code, resource not found
    /// </summary>
    public class NotFoundException : HttpException
    {
        public NotFoundException() : base(404)
        {
        }
    }

    /// <summary>
    /// 401 status code, user must be logged in
    /// </summary>
    public class UnauthorizedException : HttpException
    {
        public UnauthorizedException() : base(401)
        {
        }
    }

    /// <summary>
    /// 403 status code, user have no permission
    /// </summary>
    public class ForbiddenException : HttpException
    {
        public ForbiddenException() : base(403)
        {
        }
    }
}
