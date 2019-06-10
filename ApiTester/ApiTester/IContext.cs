namespace ApiTester
{
    public interface IContext
    {
        void Log(string log);

        string GetValue(string key);

        void SetValue(string key, string t);

        void SetService<T>(string key, T obj);

        T GetService<T>(string key);
    }
}