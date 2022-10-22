namespace WebApplication
{
    public class AppSettings : IAppSettings
    {
        public string Database { get; set; }
        public string Secret { get; set; }

    }
    public interface IAppSettings
    {
       
        string Database { get; }
        string Secret { get; }
        

    }
}
