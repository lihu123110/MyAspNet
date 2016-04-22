using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Server.Core.Caching
{
    public interface ICacheProvider
    {
        void Add(string key, object value);

        bool Remove(string key);

        void Clear();
    }
}
