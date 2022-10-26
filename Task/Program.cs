using Task;
var filePath = @"..\..\..\Files";
var results = Reader.FindWordCount(filePath);
foreach (var result in results) 
{
    Console.WriteLine(result);
}
