using AssertLibrary;

namespace Blog.Core.Domain
{
    public class Blogueiro : Entidade
    {
        public Blogueiro(string nome, Autenticacao autenticacao)
        {
            Nome = nome;
            Autenticacao = autenticacao;
        }

        private string _nome;
        private Autenticacao _autenticacao;

        public string Nome
        {
            get => _nome;
            private set
            {
                Assert.IsFalse(string.IsNullOrEmpty(value), "Nome não pode ser vazio.");
                _nome = value;
            }
        }

        public Autenticacao Autenticacao
        {
            get => _autenticacao;
            private set
            {
                Assert.IsNotNull(value);
                _autenticacao = value;
            }
        }
    }
}