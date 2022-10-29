using System.Collections.Generic;
using System.IO;

namespace WebApplication.Utils
{
    public class Message : IMessage
    {
        private IDictionary<string, string> _xmlDictionary;
        private string _path;
        private string _rootFolder;
        IAppSettings _appSettings;
        public Message(IAppSettings appSettings)
        {
            _appSettings = appSettings;
            _rootFolder = appSettings.RootFolder;
        }

        public void LoadStatements()
        {
            foreach (var message in _appSettings.ListMessages)
            {
                StatementCollection statements = null;
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(StatementCollection));
                var path =Path.Combine(_rootFolder,message.Path,".xml");
                using (StreamReader reader = new StreamReader(path))
                {
                    statements = (StatementCollection)serializer.Deserialize(reader);
                }
                _xmlDictionary = new Dictionary<string, string>();
                foreach (var item in statements.Statements)
                {
                    var code = item.Id + message.Code;
                    _xmlDictionary[code] = item.Text?.Trim();
                }

            }

        }
        public string GetMessage(string id,string lang)
        {
            var code = id + lang;
            return _xmlDictionary[code];
        }
    }
    [System.Diagnostics.DebuggerDisplay("{Id}")]
    public class SqlStatement
    {
        [System.Xml.Serialization.XmlAttribute("id")]
        public string Id { get; set; }

        [System.Xml.Serialization.XmlText]
        public string Text { get; set; }
    }


    [System.Xml.Serialization.XmlRoot("Messages")]
    public class StatementCollection
    {
        [System.Xml.Serialization.XmlElement("Message", typeof(SqlStatement))]
        public SqlStatement[] Statements { get; set; }
    }
}
