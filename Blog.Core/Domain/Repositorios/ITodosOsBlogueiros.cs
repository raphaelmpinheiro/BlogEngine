namespace Blog.Core.Domain.Repositorios
{
    public interface ITodosOsBlogueiros
    {
        void Adicionar(Blogueiro blogueiro);
        Blogueiro ComEmail(string nome);
    }
}