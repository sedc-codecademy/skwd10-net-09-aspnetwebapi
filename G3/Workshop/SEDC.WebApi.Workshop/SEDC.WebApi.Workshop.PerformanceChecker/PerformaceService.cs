namespace SEDC.WebApi.Workshop.PerformanceChecker
{
    public class PerformaceService
    {
        private string? _url = null;

        public void SetUrl(string url) => _url = url;
        
        public void CheckPerformance()
        {
            using HttpClient client = new();
            var limit = 100;

            using HttpResponseMessage response = client.GetAsync(_url).Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;
            if(int.Parse(responseBody) > limit)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.WriteLine($"Performance: {responseBody} [Limit: {limit}]");
        }
    }
}
