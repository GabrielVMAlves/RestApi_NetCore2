using System;
using System.Collections.Generic;
using System.Linq;
using RestApi_NetCore2.Models;
using RestApi_NetCore2.Models.Context;

namespace RestApi_NetCore2.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private MySQLContext _db; 

        public UserRepository(MySQLContext mySQLContext)
        {
            _db = mySQLContext;
        }

        public User Create(User user)
        {
            try
            {
                _db.Add(user);
                _db.SaveChanges();
                return user;
            } catch(Exception e)
            {
                throw e;
            }
        }

        public void Delete(long Id)
        {
            try
            {
                if (!_db.Persons.Any(u => u.Id.Equals(Id)))
                {
                    throw new NullReferenceException();
                }
                var result = _db.Users.SingleOrDefault(u => u.ID.Equals(Id));

                _db.Users.Remove(result);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<User> FindAll()
        {
            try
            {
                return _db.Users.ToList();
            } catch(Exception e)
            {
                throw e;
            }
        }

        public User FindById(long Id)
        {
            try
            {
                return _db.Users.SingleOrDefault(u => u.ID.Equals(Id));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public User FindByUsername(string login)
        {
            try
            {
                return _db.Users.SingleOrDefault(u => u.Username.Equals(login));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public User Update(User user)
        {
            try
            {
                if(!_db.Users.Any(u => u.ID.Equals(user.ID)))
                {
                    return null;
                }
                var result = _db.Persons.SingleOrDefault(u => u.Id.Equals(user.ID));

                _db.Entry(result).CurrentValues.SetValues(user);
                _db.SaveChanges();

                return user;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
