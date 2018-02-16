using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.View.Controllers.Base
{
    [Authorize]
    public class SecureController : Controller
    {
        protected string UsuarioLogado()
        {
            var claim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "autenticacao.email");
            if (claim != null)
            {
                return claim.Value;
            }
            throw new ArgumentException();
        }
    }
}