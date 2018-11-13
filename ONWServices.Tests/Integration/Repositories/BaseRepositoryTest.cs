using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using ONWServices.Models;
using ONWServices.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace ONWServices.Tests.Integration.Repositories
{
    [TestClass]
    public class BaseRepositoryTest : BaseMongoIntegrationTest
    {
        private TestBaseRepository cut;

        private static MongoDbContext _dbContext;
        private static string _collectionName;
        private static IMongoCollection<TestDocument> _collection;

        [ClassInitialize]
        public static void BeforeAll(TestContext context)
        {
            _dbContext = CreateMongoDbContext();
            _collectionName = "test";
            _collection = _dbContext.Database.GetCollection<TestDocument>(_collectionName);
        }

        [TestInitialize]
        public void BeforeEach()
        {
            //Wipe out the collection before each test
            _dbContext.Database.DropCollection(_collectionName);

            cut = new TestBaseRepository(CreateOnwDbContext(_dbContext), _collectionName);
        }

        [TestMethod]
        public void TestFindAll()
        {
            _collection.InsertOne(new TestDocument { Id = ObjectId.GenerateNewId(), TestValue = "test1" });
            _collection.InsertOne(new TestDocument { Id = ObjectId.GenerateNewId(), TestValue = "test2" });
            _collection.InsertOne(new TestDocument { Id = ObjectId.GenerateNewId(), TestValue = "test3" });

            List<TestDocument> results = cut.FindAll();

            Assert.IsNotNull(results);
            Assert.AreEqual(3, results.Count);
            CollectionAssert.AreEquivalent(
                new List<string> { "test1", "test2", "test3" },
                results.Select(td => td.TestValue).ToList()
            );
        }

        [TestMethod]
        public void TestFindById()
        {
            ObjectId idToFind = ObjectId.GenerateNewId();
            _collection.InsertOne(new TestDocument { Id = ObjectId.GenerateNewId(), TestValue = "test1" });
            _collection.InsertOne(new TestDocument { Id = idToFind, TestValue = "test2" });
            _collection.InsertOne(new TestDocument { Id = ObjectId.GenerateNewId(), TestValue = "test3" });

            TestDocument result = cut.FindById(idToFind);

            Assert.IsNotNull(result);
            Assert.AreEqual(idToFind, result.Id);
            Assert.AreEqual("test2", result.TestValue);
        }

        [TestMethod]
        public void TestFindByIdForNonexistentId()
        {
            ObjectId idToFind = ObjectId.GenerateNewId();
            _collection.InsertOne(new TestDocument { Id = ObjectId.GenerateNewId(), TestValue = "test1" });
            _collection.InsertOne(new TestDocument { Id = ObjectId.GenerateNewId(), TestValue = "test2" });
            _collection.InsertOne(new TestDocument { Id = ObjectId.GenerateNewId(), TestValue = "test3" });

            TestDocument result = cut.FindById(idToFind);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestSaveCreatesNewDocument()
        {
            _collection.InsertOne(new TestDocument { Id = ObjectId.GenerateNewId(), TestValue = "test1" });
            _collection.InsertOne(new TestDocument { Id = ObjectId.GenerateNewId(), TestValue = "test2" });
            _collection.InsertOne(new TestDocument { Id = ObjectId.GenerateNewId(), TestValue = "test3" });

            TestDocument newDocument = new TestDocument { TestValue = "test4" };

            TestDocument result = cut.Save(newDocument);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            Assert.AreNotEqual(ObjectId.Empty, result.Id);
            Assert.AreEqual("test4", result.TestValue);

            //Make sure the document was actually created
            Assert.AreEqual(4, _collection.CountDocuments(_ => true));
        }

        [TestMethod]
        public void TestSaveUpdatesExistingDocument()
        {
            TestDocument documentToUpdate = new TestDocument { Id = ObjectId.GenerateNewId(), TestValue = "test1" };
            _collection.InsertOne(documentToUpdate);
            _collection.InsertOne(new TestDocument { Id = ObjectId.GenerateNewId(), TestValue = "test2" });
            _collection.InsertOne(new TestDocument { Id = ObjectId.GenerateNewId(), TestValue = "test3" });

            documentToUpdate.TestValue = "test4";

            TestDocument result = cut.Save(documentToUpdate);

            Assert.IsNotNull(result);
            Assert.AreEqual(documentToUpdate.Id, result.Id);
            Assert.AreEqual("test4", result.TestValue);

            //Make sure no new documents were actually created but one was updated
            List<TestDocument> newResults = _collection.Find(_ => true).ToList();
            Assert.AreEqual(3, newResults.Count);
            CollectionAssert.AreEquivalent(
                new List<string> { "test2", "test3", "test4" },
                newResults.Select(td => td.TestValue).ToList()
            );
        }

        [TestMethod]
        public void TestDeleteById()
        {
            ObjectId idToDelete = ObjectId.GenerateNewId();
            _collection.InsertOne(new TestDocument { Id = ObjectId.GenerateNewId(), TestValue = "test1" });
            _collection.InsertOne(new TestDocument { Id = idToDelete, TestValue = "test2" });
            _collection.InsertOne(new TestDocument { Id = ObjectId.GenerateNewId(), TestValue = "test3" });

            cut.DeleteById(idToDelete);

            //Make sure the document was actually deleted
            List<TestDocument> newResults = _collection.Find(_ => true).ToList();
            Assert.AreEqual(2, newResults.Count);
            CollectionAssert.AreEquivalent(
                new List<string> { "test1", "test3" },
                newResults.Select(td => td.TestValue).ToList()
            );
        }
    }

    //Create a dummy subclass of BaseRepository for testing
    public class TestBaseRepository : BaseRepository<TestDocument>
    {
        public TestBaseRepository(IOnwDbContext context, string collectionName) : base(context, collectionName)
        {
        }
    }

    public class TestDocument : BaseDocument
    {
        [BsonElement("testValue")]
        public string TestValue { get; set; }
    }
}
