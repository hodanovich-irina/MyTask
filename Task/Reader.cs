using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Task
{
    public static class Reader
    {
        public static List<string> FindWordCount(string fileName, string searchPattern) 
        {
            var resList = new List<string>();
            var x = ReadFiles(fileName, searchPattern);

            var pattern = @"\W";
            var regex = new Regex(pattern);

            var allWords = Regex.Split(x, pattern).Where(res => res != string.Empty)
                .Select(res1 => res1.ToLower());

            foreach (var v in allWords)
            {
                var count = allWords.Where(x => x == v).Count();
                var res = $"Слово '{v}' встречается {count} раз";
                if (!resList.Contains(res))
                {
                    resList.Add(res);
                }
            }
            return resList;
        }
        private static List<string> ReadAllFileName(string folderName, string searchPattern)
        {            
            var fileList = Directory.EnumerateFiles(folderName);
            return fileList.ToList();
        }

        private static string ReadFiles(string folderName, string searchPattern) 
        {
            var allText = "";
            var files = ReadAllFileName(folderName, searchPattern);

            foreach (var file in files)
            {
                using (StreamReader reader = new StreamReader(file))
                {
                    string? line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        allText += line;
                    }
                }
            }
            return allText;
        }
    }
}
