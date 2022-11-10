using ictweb5.Domain;
using ictweb5.Domain.Interfaces;
using ictweb5.Models;
using System;

namespace InnoTech_Solutions.Domain.Entities
{
    public class UserItemDataRepository : CRUDItemICTDataRepositoryClass , IAuthItemDataRepository
    {
        public UserItemDataRepository(IICTDataRepository Repository)
           : base(Repository)
        {
        }
        public bool ChangePwd(UserAccountClass User, string Password)
        {
            throw new NotImplementedException();
        }
        public bool HasAccess(UserAccountClass User, EntityClass Entity)
        {
            throw new NotImplementedException();
        }
        public bool Unique(string UserName)
        {
            throw new NotImplementedException();
        }
        public object Validate(string username, string password)
        {
            return repository.User.Validate(username, password);
        }
    }
}
