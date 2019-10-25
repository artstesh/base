using System.Collections.Generic;
using artstesh.ru.Models;

namespace artstesh.ru.Utils
{
    public interface IMdIndexSearcher
    {
        List<SearchModel> Find(string searching);
    }
}