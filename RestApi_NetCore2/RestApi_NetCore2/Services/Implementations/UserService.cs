using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.IdentityModel.Tokens;
using RestApi_NetCore2.Data.Converters;
using RestApi_NetCore2.Models;
using RestApi_NetCore2.Repository;
using RestApi_NetCore2.Security.Configuration;

namespace RestApi_NetCore2.Services.Implementations
{
    public class UserService : IUserService
    {
        private IUserRepository _repository;
        private SigningConfigurations _signingConfigurations;
        private TokenConfiguration _tokenConfiguration;

        public UserService(IUserRepository repository, SigningConfigurations signingConfigurations, TokenConfiguration tokenConfiguration)
        {
            _repository = repository;
            _signingConfigurations = signingConfigurations;
            _tokenConfiguration = tokenConfiguration;
        }

        public User Create(User user)
        {
            return _repository.Create(user);
        }

        public void Delete(long Id)
        {
            _repository.Delete(Id);
        }

        public List<User> FindAll()
        {

            return _repository.FindAll();
        }

        public User FindById(long Id)
        {
            return _repository.FindById(Id);
        }

        public object FindByUsername(User user)
        {
            bool credentialIsValid = false;
            if(!string.IsNullOrWhiteSpace(user.Username))
            {
                User baseUser = _repository.FindByUsername(user.Username);
                credentialIsValid = (baseUser != null && user.Username == baseUser.Username && user.AccessKey == baseUser.AccessKey);
            }
            if (credentialIsValid)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(user.Username, "Login"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                        new Claim("Profile", user.Profile)
                    }
                    );
                DateTime createdDate = DateTime.Now;
                DateTime expirationDate = createdDate + TimeSpan.FromSeconds(_tokenConfiguration.Seconds);
                var handler = new JwtSecurityTokenHandler();
                string token = CreateToken(identity, createdDate, expirationDate, handler);
                return SuccessObject(createdDate, expirationDate, token);
            }
            else
                return ExceptionObject(); 
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createdDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor() {
                Issuer = _tokenConfiguration.Issuer,
                Audience = _tokenConfiguration.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createdDate,
                Expires = expirationDate
            });

            var token = handler.WriteToken(securityToken);
            return token;
        }

        private object SuccessObject(DateTime createdDate, DateTime expirationDate, string token)
        {
            return new
            {
                autenticated = true,
                created = createdDate.ToString("yyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyy-MM-dd HH:mm:ss"),
                accessToken = token,
                message = "OK"
            };
        }

        private object ExceptionObject()
        {
            return new
            {
                autenticated = false,
                message = "Failed to autenticate"
            };
        }

        public User Update(User user)
        {
            return _repository.Update(user);
        }
    }
}
