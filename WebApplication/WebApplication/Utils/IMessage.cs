using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Utils
{
    public interface IMessage
    {
        void LoadStatements();
        string GetMessage(string id,string lang);
    }
}
