using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace Task
{
    public static class Reader
    {
        private static ConcurrentDictionary<string, int> _allData = new ConcurrentDictionary<string, int>();

        public async static Task<ConcurrentDictionary<string, int>> ReadDataWithTasksAsync(string folderName, string searchPattern)
        {
            var allFiles = ReadAllFileName(folderName, searchPattern);
            var tasks = new System.Threading.Tasks.Task[allFiles.Count];
            for (int i = 0; i < allFiles.Count; i++)
            {
                tasks[i] = ReadFileAsync(allFiles[i]);
            }
            await System.Threading.Tasks.Task.WhenAll(tasks);

            return _allData;
        }

        public static void FindWordCountInLine(string line)
        {
            var pattern = @"\W";
            var regex = new Regex(pattern);

            var allWords = Regex.Split(line, pattern).Where(x => x != string.Empty)
                .GroupBy(x=>x.ToLower()).Select(x => new { Word = x.Key, Count = x.Count()});

            foreach (var word in allWords)
            {
                if (!_allData.ContainsKey(word.Word))
                {
                    _allData.TryAdd(word.Word, word.Count);
                }
                else
                {
                    _allData[word.Word] += word.Count;
                }

            }
        }

        private static List<string> ReadAllFileName(string folderName, string searchPattern)
        {
            if (!Directory.Exists(folderName)) 
            {
                throw new DirectoryNotFoundException($"Folder {folderName} not found.");
            }
            var fileList = Directory.EnumerateFiles(folderName, searchPattern);
            return fileList.ToList();
        }

        private static async System.Threading.Tasks.Task ReadFileAsync(string fileName)
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                string? line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    var res = line;
                    FindWordCountInLine(res);
                }
            }
        }
    }
}
