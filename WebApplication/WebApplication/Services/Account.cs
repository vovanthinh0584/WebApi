using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Services
{
    public class AccountService
    {
        IDao _dao;
       public AccountService(IDao dao)
        {
            _dao = dao;
        }


    }
}
