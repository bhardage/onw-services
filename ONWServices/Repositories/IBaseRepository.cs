using MongoDB.Bson;
using ONWServices.Models;
using System.Collections.Generic;

namespace ONWServices.Repositories
{
    public interface IBaseRepository<T> where T : BaseDocument
    {
        List<T> FindAll();

        T FindById(ObjectId id);

        T Save(T document);

        void DeleteById(ObjectId id);
    }
}
