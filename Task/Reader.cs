using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    public static class Reader
    {
        public static List<string> FindWordCount(string fileName) 
        {
            var resList = new List<string>();
            var x = Reader.ReadFiles(fileName);
            var allWorlds = x.Split('.', ',', '\n', ':', ';', '!', '?', '-', ' ', '\t').Where(res=>res != string.Empty);
            foreach (var v in allWorlds)
            {
                var count = allWorlds.Where(x => x == v).Count();
                var res = $"Слово '{v}' встречается {count} раз";
                if (!resList.Contains(res))
                {
                    resList.Add(res);
                }
            }
            return resList;
        }
        private static List<string> ReadAllFileName(string folderName)
        {
            var fileList = new List<string>();
            if (Directory.Exists(folderName))
            {
                string[] files = Directory.GetFiles(folderName);
                foreach (var s in files)
                {
                    fileList.Add(s);
                }
            }
            return fileList;
        }

        private static string ReadFiles(string folderName) 
        {
            var allText = "";
            var files = ReadAllFileName(folderName);
            foreach (var file in files)
            {
                using (StreamReader reader = new StreamReader(file))
                {
                    string text = reader.ReadToEnd();
                    allText += text;
                }
            }
            return allText;
        }
    }
}
