namespace SEDC.WebApi.Workshop.PerformanceChecker
{
    public class PChecker : IPChecker, ICheckPerformance
    {
        private string? _url = null;

        public ICheckPerformance SetUrl(string url)
        { 
            _url = url;
            return this;
        }

        public void CheckPerformance()
        {
            using HttpClient client = new();
            var limit = 100;

            using HttpResponseMessage response = client.GetAsync(_url).Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;
            if (int.Parse(responseBody) > limit)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.WriteLine($"Performance: {responseBody} [Limit: {limit}]");
        }
    }

    public interface IPChecker
    {
        ICheckPerformance SetUrl(string url);
    }

    public interface ICheckPerformance
    {
        void CheckPerformance();
    }


    public class Test
    {
        private readonly IPChecker _performaceCheker;

        public Test(IPChecker performaceCheker)
        {
            _performaceCheker = performaceCheker;
        }

        public void Teest()
        {
            _performaceCheker.SetUrl("").CheckPerformance();
        }
    }
}
