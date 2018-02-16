namespace Blog.View.Models
{
    public class PostRequest
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public string Etiquetas { get; set; }
    }
}