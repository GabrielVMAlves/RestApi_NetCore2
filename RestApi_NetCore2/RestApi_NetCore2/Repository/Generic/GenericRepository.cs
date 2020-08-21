using Microsoft.EntityFrameworkCore;
using RestApi_NetCore2.Models.Base;
using RestApi_NetCore2.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi_NetCore2.Repository.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        private MySQLContext _db;
        private DbSet<T> dataset;

        public GenericRepository(MySQLContext mySQLContext)
        {
            _db = mySQLContext;
            dataset = _db.Set<T>();
        }

        public T Create(T item)
        {
            try
            {
                dataset.Add(item);
                _db.SaveChanges();
                return item;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Delete(long Id)
        {
            try
            {
                if (Exists(Id))
                    throw new NullReferenceException();
                var result = FindById(Id);

                dataset.Remove(result);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Exists(long? Id)
        {
            return dataset.Any(x => x.Id.Equals(Id));
        }

        public List<T> FindAll()
        {
            try
            {
                return dataset.ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public T FindById(long Id)
        {
            try
            {
                return dataset.SingleOrDefault(p => p.Id.Equals(Id));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public T Update(T item)
        {
            try
            {
                if (Exists(item.Id))
                    return null;
                var result = FindById(item.Id.Value);

                _db.Entry(result).CurrentValues.SetValues(item);
                _db.SaveChanges();

                return item;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
