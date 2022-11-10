using ictweb5.Domain;
using ictweb5.Domain.Interfaces;
using ictweb5.Models;
using InnoTech_Solutions.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using static InnoTech_Solutions.Domain.IInnoTechDataRepository;

namespace InnoTech_Solutions.Domain
{
    public class InnoTechDataRepository : IInnoTechDataRepository
    {
        protected IICTDataRepository Repository;
        public InnoTechDataRepository(IConfiguration configuration)
        {
            this.Repository = new SQLICTDataRepository(configuration["ictdb"].ToString(), null);
            this.Common = new CommonItemSQLDataRepositoryClass(configuration["ictdb"].ToString(), null);
            this.User = new UserItemDataRepository(Repository);
            this.Counter = new CounterItemInnoTech(Repository);
        }
        public IItemDataRepository Counter { get; }
        public ICommonItemDataRepository Common { get; }
        public IAuthItemDataRepository User { get; }
    }

    public class InnoCRUDDataRepositoryItem : IBaseDataRepositoryItem
    {
        protected IICTDataRepository repository;
        public InnoCRUDDataRepositoryItem(IICTDataRepository Repository)
        {
            repository = Repository;
        }

        public virtual Object Data<T>(T Data, UserAccountClass user)
        {
            return default;
        }

        public virtual Object View<T>(T Data, IQueryCollection Params, UserAccountClass user)
        {
            return default;
        }
    }
    }
