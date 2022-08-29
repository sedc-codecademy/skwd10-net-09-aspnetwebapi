using NotesApp.PerformanceChecker;


var port = "7241";
var address = $"https://localhost:{port}/api/External/performance/getnote";

Console.WriteLine("Performance check started...");
Console.WriteLine("____________________________");

PerformanceService.SetAddress(address);
PerformanceService.CheckPerformance();

Console.ReadLine();
