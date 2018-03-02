using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramadoraGet.Utils
{
    public static class UtilsExtensions
    {
        public static string ToBase64(this byte[] buffer) => Convert.ToBase64String(buffer);

        public static byte[] FromBase64(this string base64) => Convert.FromBase64String(base64);
    }
}
