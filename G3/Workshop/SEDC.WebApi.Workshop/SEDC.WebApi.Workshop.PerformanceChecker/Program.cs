namespace SEDC.WebApi.Workshop.PerformanceChecker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var url = @"http://localhost:5277/api/v1/performance/notes";

            Console.WriteLine("Performance ccheck started");
            Console.WriteLine("--------------------------");
            var service = new PerformaceService();
            service.SetUrl(url);
            service.CheckPerformance();

            Console.ReadLine();
        }
    }
}