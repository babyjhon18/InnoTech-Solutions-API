using ictweb5.Models;
using InnoTech_Solutions.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace InnoTech_Solutions.Controllers
{
    public class CustomController : ControllerBase
    {
        public CustomController(IInnoTechDataRepository InnoTechRepository)
        {
            this.InnoTechRepository = InnoTechRepository;
        }

        public IInnoTechDataRepository InnoTechRepository;

        public UserAccountClass CurrentUser
        {
            get
            {
                return new UserAccountClass(Convert.ToBoolean(HttpContext.User.Claims.Where(c => c.Type == "IsAdmin")
                    .Select(c => c.Value).SingleOrDefault().ToString()))
                {
                    ID = Convert.ToInt32(HttpContext.User.Claims.Where(c => c.Type == "UserID")
                    .Select(c => c.Value).SingleOrDefault().ToString()),
                };
            }
        }

        public object Status(dynamic dataItem)
        {
            if (dataItem is Boolean)
            {
                if (Convert.ToBoolean(dataItem))
                    Response.StatusCode = 200;
                else
                { 
                    Response.StatusCode = 403;
                    return new EmptyResult();
                }
            }
            else
            {
                if (dataItem == null)
                {
                    Response.StatusCode = 400;
                    return new EmptyResult();
                }
            }
            return dataItem;
        }
    }
}
