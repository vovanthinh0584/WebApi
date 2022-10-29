using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using WebApplication.Models;

namespace WebApplication
{
    public class AppSettings: IAppSettings
    {

        public AppSettings(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        public string Database
        {
            get { return _configuration["AppSettings:Database"]; }
            set { }
        }
        public string Secret
        {
            get { return _configuration["AppSettings:Secret"]; }
            set { }
        }
        public string Queries_Folder
        {
            get { return _configuration["AppSettings:Queries_Folder"]; }
            set { }
        }
        public  List<Message> ListMessages
        {
            get { return GetMessage(); }
            set { }
        }
        private IConfiguration _configuration { get; set; }
        private List<Message> GetMessage()
        {
            var valuesSection = _configuration.GetSection("AppSettings:Messages");
            List<Message> listMessage = new List<Message>();
            foreach (IConfigurationSection section in valuesSection.GetChildren())
            {
                Message message = new Message();
                message.Path = section.GetValue<string>("Path");
                message.Code = section.GetValue<string>("Code");
                message.Language = section.GetValue<string>("Language");
                listMessage.Add(message);
            }
            return listMessage;
        }
        public string RootFolder { get { return Directory.GetCurrentDirectory(); } }
    }
    public interface IAppSettings
    {
       string RootFolder { get;}
        
        string Database { get; }
        string Secret { get; }

        List<Message> ListMessages { get; }

        string Queries_Folder { get; }
    }
}
