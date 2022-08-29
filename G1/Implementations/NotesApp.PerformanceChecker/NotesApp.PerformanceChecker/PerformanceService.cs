using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.PerformanceChecker
{
    public static class PerformanceService
    {
        private static string _notesAddress;
        public static void SetAddress(string address) => _notesAddress = address;

        public static void CheckPerformance() 
        {
            HttpClient client = new HttpClient();
            var address = _notesAddress;
            int limit = 1;

            var response = client.GetAsync(address).Result;
            var responseBody = response.Content.ReadAsStringAsync().Result;

            Console.ForegroundColor = ConsoleColor.Green;

            if (int.Parse(responseBody) > limit) 
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }

            Console.WriteLine($"Performance: {responseBody} [Limit: {limit}]");
            Console.ResetColor();
        }
    }
}
