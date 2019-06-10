namespace ApiTester
{
    public interface IConfig
    {
        bool IsFound(string key);
        string GetValue(string key);
    }
}