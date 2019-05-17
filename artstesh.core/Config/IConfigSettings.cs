namespace artstesh.core.Config
{
    public interface IConfigSettings
    {
        string ConnectionString { get; set; }
        ApplicationKeys ApplicationKeys { get; set; }
        bool CheckPassword(string login, string password);
    }
}