using System.Collections.Generic;
using System.IO;

namespace WebApplication.Utils
{
    public class Message : IMessage
    {
        private IDictionary<string, string> _xmlDictionary;
        private string _path;
        public Message(string path)
        {
            _path = path;
        }

        public void LoadStatements()
        {
            StatementCollection statements = null;
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(StatementCollection));

            using (StreamReader reader = new StreamReader(_path))
            {
                statements = (StatementCollection)serializer.Deserialize(reader);
            }
            _xmlDictionary = new Dictionary<string, string>();
            foreach (var item in statements.Statements)
            {
                _xmlDictionary[item.Id] = item.Text?.Trim();
            }
			var x = _xmlDictionary;

		}
        public string GetMessage(string id)
        {
            return _xmlDictionary[id];
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
