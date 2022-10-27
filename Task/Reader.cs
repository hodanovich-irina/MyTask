using System.Text.RegularExpressions;

namespace Task
{
    public static class Reader
    {
        private static Dictionary<string, int> _allData = new Dictionary<string, int>();
        private static readonly object _lock = new object();
        public async static Task<Dictionary<string, int>> ReadDataWithTasksAsync(string folderName, string searchPattern)
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
                .Select(x => x.ToLower());

            foreach (var word in allWords)
            {
                var count = allWords.Where(x => x == word).Count();
                lock (_lock)
                {
                    if (!_allData.ContainsKey(word))
                    {
                        _allData.Add(word, 1);
                    }
                    else
                    {
                        _allData[word] += 1;
                    }
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
