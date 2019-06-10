using System.Collections.Generic;

namespace ApiTester
{
    public class Config : IConfig
    {
        readonly Dictionary<string, string> _dictionary;

        public Config(Dictionary<string, string> seed)
        {
            _dictionary = seed;
        }

        public bool IsFound(string key)
        {
            return _dictionary.ContainsKey(key);
        }

        public string GetValue(string key)
        {
            return _dictionary[key];
        }
    }
}