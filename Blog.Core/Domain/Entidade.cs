using System;

namespace Blog.Core.Domain
{
    [Serializable]
    public abstract class Entidade
    {        
        public string Id { get; set; }

        protected Entidade()
        {
            Id = "";
        }

        public virtual bool IsNew()
        {
            return Id == "";
        }

        public override  bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() != GetType())
                return false;

            if (Id == "")
                return this == obj;

            var that = (Entidade)obj;
            return Id == that.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}