using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestApi_NetCore2.Models;
using RestApi_NetCore2.Models.Context;

namespace RestApi_NetCore2.Repository.Implementation
{
    public class PersonRepository : IPersonRepository
    {
        private MySQLContext _db; 

        public PersonRepository(MySQLContext mySQLContext)
        {
            _db = mySQLContext;
        }

        public Person Create(Person person)
        {
            try
            {
                _db.Add(person);
                _db.SaveChanges();
                return person;
            } catch(Exception e)
            {
                throw e;
            }
        }

        public void Delete(long Id)
        {
            try
            {
                if (!_db.Persons.Any(p => p.Id.Equals(Id)))
                {
                    throw new NullReferenceException();
                }
                var result = _db.Persons.SingleOrDefault(p => p.Id.Equals(Id));

                _db.Persons.Remove(result);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Person> FindAll()
        {
            try
            {
                return _db.Persons.ToList();
            } catch(Exception e)
            {
                throw e;
            }
        }

        public Person FindById(long Id)
        {
            try
            {
                return _db.Persons.SingleOrDefault(p => p.Id.Equals(Id));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Person Update(Person person)
        {
            try
            {
                if(!_db.Persons.Any(p => p.Id.Equals(person.Id)))
                {
                    throw new NullReferenceException();
                }
                var result = _db.Persons.SingleOrDefault(p => p.Id.Equals(person.Id));

                _db.Entry(result).CurrentValues.SetValues(person);
                _db.SaveChanges();

                return person;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
