using ictweb5.Domain.Interfaces;
using ictweb5.Models;
using Microsoft.AspNetCore.Http;
using System;

namespace InnoTech_Solutions.Domain
{
    public interface IInnoTechDataRepository
    {
        public ICommonItemDataRepository Common { get; }
        public IAuthItemDataRepository User { get; }
        public IItemDataRepository Counter { get; }

        public interface IItemDataRepository : IBaseDataRepositoryItem
        {
        }

        public interface IBaseDataRepositoryItem
        {
            abstract Object View<T>(T Data, IQueryCollection Params, UserAccountClass user);
            abstract Object Data<T>(T Data, UserAccountClass user);
        }
    }
}
