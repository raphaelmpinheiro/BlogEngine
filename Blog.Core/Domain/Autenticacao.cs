using AssertLibrary;
using EmailValidation;

namespace Blog.Core.Domain
{
    public class Autenticacao
    {
        public Autenticacao(string email, string senha)
        {
            Email = email;
            Senha = senha;
        }

        private string _email;
        private string _senha;

        public string Email
        {
            get => _email;
            private set
            {
                Assert.IsFalse(string.IsNullOrEmpty(value), "E-mail não pode estar vazio.");
                Assert.IsTrue(EmailValidator.Validate(value), "E-mail não pode ser inválido.");
                _email = value;
            }
        }

        public string Senha
        {
            get => _senha;
            private set
            {
                Assert.IsFalse(string.IsNullOrEmpty(value), "Senha não pode ser vazia.");
                _senha = value;
            }
        }

        public bool Autenticado(string senha)
        {
            return Senha.Equals(senha);
        }
    }
}