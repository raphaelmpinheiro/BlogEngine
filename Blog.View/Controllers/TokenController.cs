using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Blog.Core.Domain;
using Blog.Core.Domain.Repositorios;
using Blog.View.Controllers.Base;
using Blog.View.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Blog.View.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : SecureController
    {
        private readonly IConfiguration _config;
        private readonly ITodosOsBlogueiros _todosOsBlogueiros;

        public TokenController(IConfiguration config, ITodosOsBlogueiros todosOsBlogueiros)
        {
            _config = config;
            _todosOsBlogueiros = todosOsBlogueiros;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateToken([FromBody] LoginModel login)
        {
            IActionResult response = Unauthorized();
            var blogueiro = Autenticar(login);

            if (blogueiro != null)
            {
                var tokenString = GerarToken(blogueiro);
                response = Ok(new {token = tokenString});
            }

            return response;
        }

        private Blogueiro Autenticar(LoginModel login)
        {            
            var blogueiro = _todosOsBlogueiros.ComEmail(login.Usuario);
            if (blogueiro.Autenticacao.Autenticado(login.Senha))
            {
                return blogueiro;
            }
            return null;
        }

        private string GerarToken(Blogueiro blogueiro)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);
            
            token.Payload.Add("nome", blogueiro.Nome);
            token.Payload.Add("autenticacao.email", blogueiro.Autenticacao.Email);            

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}