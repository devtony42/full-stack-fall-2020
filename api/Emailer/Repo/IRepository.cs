using System.Collections.Generic;

namespace Emailer.Repo
{
    public interface IRepository<T>
    {
        List<T> Get();
        T Get(string id);
        bool Save(T record);
        bool Insert(T record);
        bool Update(T record);
        bool Delete(string id);
        bool Delete(T record);
    }
}