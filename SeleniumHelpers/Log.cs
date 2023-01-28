using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumHelpers
{
    public static class Log
    {
        public static void Error(string message)
        {
            Console.WriteLine($"[Error: {Time()}] - {message}");
        }

        public static void Info(string message)
        {
            Console.WriteLine($"[Info: {Time()}] - {message}");
        }

        private static string Time()
        {
            return DateTime.UtcNow.AddHours(1).ToString("dd-MM-yyyy hh:mm:ss");
        }
    }
}
