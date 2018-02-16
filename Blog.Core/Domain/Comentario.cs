using System;
using AssertLibrary;

namespace Blog.Core.Domain
{
    public class Comentario
    {
        public Comentario(string comento)
            : this(comento, DateTime.Now)
        {
        }

        public Comentario(string comento, DateTime momento)
        {
            Comento = comento;
            Momento = momento;
        }

        private string _comento;
        private DateTime _momento;

        public string Comento
        {
            get => _comento;
            private set
            {
                Assert.IsFalse(string.IsNullOrEmpty(value), "Comentário não pode ser vazio ou nulo");
                _comento = value;
            }
        }

        public DateTime Momento
        {
            get => _momento;
            private set
            {
                Assert.IsTrue(value > DateTime.MinValue);
                _momento = value;
            }
        }
    }
}