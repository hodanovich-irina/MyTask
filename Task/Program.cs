using Task;

var filePath = @"..\..\..\File";
var searchPattern = "*.txt";
var results = await Reader.ReadDataWithTasksAsync(filePath, searchPattern);
foreach (var result in results)
{
    Console.WriteLine($"Слово '{result.Key}' встречается {result.Value} раз");
}
