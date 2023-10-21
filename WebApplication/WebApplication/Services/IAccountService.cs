using System.Collections.Generic;
using WebApplication.Models;

public interface IAccountService
{
    IEnumerable<object> GetListCaptions(string statementSql, object param);
    IEnumerable<object> GetListBussiness(string statementSql);
    IEnumerable<object> GetVersion();
    bool Login(string statementSql, UserDTO param);
}