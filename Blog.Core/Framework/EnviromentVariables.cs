using System;

namespace Blog.Core.Framework
{
    public static class EnviromentVariables
    {
        public static string GetOrDefault(string key, string def)
        {
            var result = Environment.GetEnvironmentVariable(key);
            return string.IsNullOrEmpty(result) ? def : result;
        }
    }
}