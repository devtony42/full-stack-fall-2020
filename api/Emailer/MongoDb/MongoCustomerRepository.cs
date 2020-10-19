using System;
using System.Collections.Generic;
using Emailer.Repo;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Emailer.MongoDb
{
    public class MongoCustomerRepository : IRepository<Customer>
    {
        private readonly ILogger<MongoCustomerRepository> _logger;
        private readonly IMongoCollection<Customer> _collection;
        
        public MongoCustomerRepository(IMongoDatabase db, ILogger<MongoCustomerRepository> logger)
        {
            _logger = logger;
            _collection = db.GetCollection<Customer>("Customer");
        }

        public List<Customer> Get()
        {
            try
            {
                var rval = _collection.Find(mongoRec => true).ToList();
                return rval;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error trying to find Customers", ex, null);
            }

            return new List<Customer>();
        }
        
        public Customer Get(string id)
        {
            try
            {
                var rval = _collection.Find(mongoRec => mongoRec.Id == id).FirstOrDefault();
                return rval;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error trying to find Customer", ex, id);
            }

            return new Customer();
        }

        public bool Save(Customer record)
        {
            bool rval;
            
            if (string.IsNullOrWhiteSpace(record.Id))
            {
                record.Id = new Guid().ToString();
                rval = Insert(record);
                return rval;
            }
            else
            {
                rval = Update(record);
                return rval;
            }
        }

        public bool Insert(Customer record)
        {
            bool rval;
            
            try
            {
                _collection.InsertOne(record);
                rval = true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error trying to insert Customer", ex, record);
                rval = false;
            }

            return rval;
        }

        public bool Update(Customer record)
        {
            bool rval;
            
            try
            {
                _collection.ReplaceOne(mongoRec => mongoRec.Id == record.Id, record);
                rval = true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error trying to update Customer", ex, record);
                rval = false;
            }

            return rval;
        }

        public bool Delete(string id)
        {
            bool rval;
            
            try
            {
                _collection.DeleteOne(mongoRec => mongoRec.Id == id);
                rval = true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error trying to delete Customer", ex, id);
                rval = false;
            }

            return rval;
        }

        public bool Delete(Customer record)
        {
            bool rval;
            
            try
            {
                _collection.DeleteOne(mongoRec => mongoRec.Id == record.Id);
                rval = true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error trying to delete Customer", ex, record);
                rval = false;
            }

            return rval;
        }
    }
}