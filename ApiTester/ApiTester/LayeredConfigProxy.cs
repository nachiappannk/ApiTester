using System;

namespace ApiTester
{
    public class LayeredConfigProxy : IConfig
    {
        private readonly IConfig _primaryConfig;
        private readonly IConfig _fallbackConfig;

        public LayeredConfigProxy(IConfig primaryConfig, IConfig fallbackConfig)
        {
            _primaryConfig = primaryConfig;
            _fallbackConfig = fallbackConfig;
        }

        public bool IsFound(string key)
        {
            return _primaryConfig.IsFound(key) || _fallbackConfig.IsFound(key);
        }

        public string GetValue(string key)
        {
            if (_primaryConfig.IsFound(key)) return _primaryConfig.GetValue(key);
            if (_fallbackConfig.IsFound(key)) return _fallbackConfig.GetValue(key);
            throw new Exception("Value not found exception");
        }
    }
}