using ictweb5.Models;
using InnoTech_Solutions.Domain;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InnoTech_Solutions.AuthenticationMiddlewear
{
    public class AuthotizationMiddlewareService
    {
        private readonly RequestDelegate _next;
        private IInnoTechDataRepository _repository;
        public AuthotizationMiddlewareService(RequestDelegate next, IInnoTechDataRepository repository)
        {
            this._next = next;
            this._repository = repository;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(context.Request.Headers["Authorization"]);
                var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter)).Split(new[] { ':' }, 2);
                UserAccountClass user = _repository.User.Validate(credentials[0], credentials[1]) as UserAccountClass;
                if (user == null)
                    context.Response.StatusCode = 403;
                else
                {
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim("UserID", user.ID.ToString()));
                    claims.Add(new Claim("IsAdmin", user.Admin.ToString()));
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                       ClaimsIdentity.DefaultRoleClaimType);
                    context.User = new ClaimsPrincipal(new ClaimsIdentity(claims));
                    await _next.Invoke(context);
                }
            }
            catch(JsonReaderException e)
            {
                context.Response.StatusCode = 400;
            }
            catch (ArgumentNullException e)
            {
                context.Response.StatusCode = 400;
            }
            catch
            {
                context.Response.StatusCode = 403;
            }
        }
    }
}
