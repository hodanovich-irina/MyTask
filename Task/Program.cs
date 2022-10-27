using System.Diagnostics;
using Task;

var filePath = @"..\..\..\Files";
var searchPattern = "*.txt";
var s = new Stopwatch();
s.Start();
var results = await Reader.ReadDataWithTasksAsync(filePath, searchPattern);
s.Stop();
Console.WriteLine(s.ElapsedMilliseconds.ToString());
foreach (var result in results)
{
    Console.WriteLine($"Слово '{result.Key}' встречается {result.Value} раз");
}
