using MongoDB.Bson;
using MongoDB.Driver;
using ONWServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ONWServices.Repositories
{
    public abstract class BaseRepository<T> where T : BaseDocument
    {
        private readonly IOnwDbContext _context;
        private readonly string _collectionName;

        public BaseRepository(IOnwDbContext context, string collectionName)
        {
            _context = context;
            _collectionName = collectionName;
        }

        public List<T> FindAll()
        {
            return GetCollection()
                .Find(_ => true)
                .ToList();
        }

        public T FindById(ObjectId id)
        {
            return GetCollection()
                .Find(d => d.Id == id)
                .FirstOrDefault();
        }

        public T Save(T document)
        {
            if (document.Id == ObjectId.Empty)
            {
                GetCollection()
                    .InsertOne(document);
            }
            else
            {
                GetCollection()
                    .ReplaceOne(d => d.Id == document.Id, document);

                //TODO throw an exception if nothing was updated?
            }

            return document;
        }

        public void DeleteById(ObjectId id)
        {
            GetCollection()
                .DeleteOne(d => d.Id == id);
        }

        protected IMongoCollection<T> GetCollection()
        {
            return _context.GetDb().GetCollection<T>(_collectionName);
        }
    }
}
