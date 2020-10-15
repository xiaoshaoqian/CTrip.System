using System;
using System.Collections.Generic;
using System.Text;

namespace CTrip.System.Common.Helpers
{
    public class LogHelpers
    {
        public static string logWrite(string message)
        {
            return $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss:fff}] => {message}";
        }
    }
}
