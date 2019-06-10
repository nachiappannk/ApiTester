using System;
using System.Collections.Generic;

namespace ApiTester
{
    public class Context : IContext
    {
        private List<string> _logs = new List<string>();

        Dictionary<string, object> _serviceDictionary = new Dictionary<string, object>();

        Dictionary<string, string> _contextDictionary = new Dictionary<string, string>();

        private IConfig _config = new Config(new Dictionary<string, string>());

        public Context(params IConfig[] configs)
        {
            foreach (var config in configs)
            {
                var conf = new LayeredConfigProxy(_config, config);
                _config = conf;
            }
        }

        public void Log(string log)
        {
            _logs.Add(log);
        }

        public string GetValue(string key)
        {

            if (_contextDictionary.ContainsKey(key))
            {
                return _contextDictionary[key];
            }
            if (_config.IsFound(key))
            {
                return _config.GetValue(key);
            }
            throw new Exception();
        }

        public void SetValue(string key, string t)
        {
            if (_contextDictionary.ContainsKey(key))
            {
                _contextDictionary[key] = t;
            }
            _contextDictionary.Add(key, t);
        }

        public void SetService<T>(string key, T obj)
        {
            if (_serviceDictionary.ContainsKey(key))
            {
                _serviceDictionary[key] = obj;
            }
            else
            {
                _serviceDictionary.Add(key, obj);
            }
        }

        public T GetService<T>(string key)
        {
            if (_serviceDictionary.ContainsKey(key))
            {
                var obj = _serviceDictionary[key];
                if (obj is T variable) return variable;
                throw new Exception("The service of specified type is not available");
            }
            throw new Exception("The service is not found");
        }

        public List<string> GetLogs()
        {
            return _logs;
        }
    }
}