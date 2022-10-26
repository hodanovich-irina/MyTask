using Task;
var filePath = @"..\..\..\Files";
var searchPattern = "*.txt";
var results = Reader.FindWordCount(filePath, searchPattern);
foreach (var result in results) 
{
    Console.WriteLine(result);
}
