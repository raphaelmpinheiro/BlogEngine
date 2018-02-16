using Blog.Core.Framework;
using Xunit;

namespace Blog.Teste.Framework
{
    public class EnviromentTeste
    {
        [Fact]
        public void GetOrDefaultTeste()
        {
            var path = EnviromentVariables.GetOrDefault("PATH", "");
            Assert.False(string.IsNullOrEmpty(path));
            
            var system = EnviromentVariables.GetOrDefault("SYSTEM", "system");
            Assert.False(string.IsNullOrEmpty(system));
        }
    }
}