using System.Collections.Generic;
using System.IO;
using System.Linq;
using Markdig;
using Microsoft.AspNetCore.Hosting;

namespace artstesh.ru.Utils
{
    public class MdIndexManager : IMdIndexManager
    {
        private readonly Dictionary<string, string> _indexDictionary;
        private readonly string _path;

        public MdIndexManager(IHostingEnvironment environment)
        {
            _path = Path.Combine(environment.WebRootPath, "wiki");
            _indexDictionary = CreateIndex();
        }

        public Dictionary<string, string> GetIndex()
        {
            return _indexDictionary;
        }

        public Dictionary<string, string> CreateIndex()
        {
            var result = new Dictionary<string, string>();
            var files = DirSearch(_path);
            foreach (var fileInfo in files)
                fileInfo.Value.ForEach(f => GetFileIndex(f, result, fileInfo));

            return result;
        }

        private static void GetFileIndex(string f, Dictionary<string, string> result, KeyValuePair<string, List<string>> fileInfo)
        {
            var name = f.Split('\\').Last();
            var text = File.ReadAllText(f);
            text = Markdown.ToPlainText(text);
            text = text.Replace("\r\n", " ").Replace("\n", " ");
            result.Add($"{fileInfo.Key}/{name}", text);
        }

        private static Dictionary<string, List<string>> DirSearch(string sDir, string path = "")
        {
            var result = new Dictionary<string, List<string>>();
            try
            {
                foreach (var d in Directory.GetDirectories(sDir))
                {
                    var directoryPath = path + $"/{d.Split('\\').Last()}";
                    var files = Directory.GetFiles(d).Where(f => Path.GetExtension(f) == ".md").ToList();
                    result.Add(directoryPath, files);
                    foreach (var keyValuePair in DirSearch(d, directoryPath))
                        result.Add(keyValuePair.Key, keyValuePair.Value);
                }
            }
            catch
            {
                // ignored
            }

            return result;
        }
    }
}