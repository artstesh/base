using System;
using System.Collections.Generic;
using System.Linq;
using artstesh.ru.Models;

namespace artstesh.ru.Utils
{
    public class MdIndexSearcher : IMdIndexSearcher
    {
        private readonly IMdIndexManager _indexManager;
        private readonly int previewWidth = 100;

        public MdIndexSearcher(IMdIndexManager indexManager)
        {
            _indexManager = indexManager;
        }

        public List<SearchModel> Find(string searching)
        {
            var index = _indexManager.GetIndex()
                .Where(i => i.Value.Contains(searching, StringComparison.InvariantCultureIgnoreCase));
            return index.Select(indexElement => GetSearchModel(searching, indexElement)).ToList();
        }

        private SearchModel GetSearchModel(string searching, KeyValuePair<string, string> indexElement)
        {
            var indexPosition = indexElement.Value.IndexOf(searching);
            var startIndex = indexPosition > previewWidth ? indexPosition - 50 : 0;
            var totalLength = indexElement.Value.Length - 1;
            var previewLength = totalLength - startIndex > previewWidth
                ? previewWidth
                : totalLength - startIndex;
            var searchModel = new SearchModel
            {
                Link = indexElement.Key,
                Preview = $"...{indexElement.Value.Substring(startIndex, previewLength)}..."
            };
            return searchModel;
        }
    }
}