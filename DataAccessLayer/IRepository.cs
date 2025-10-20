using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    interface IRepository<T>
    {
        void Add(T item);

        void Delete(T item);
        IEnumerable<T> ReadAll();
        T ReadByld(T repository);
        void Update(T item);
            
    }
}
