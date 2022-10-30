using System.Collections.Generic;
using WebApplication.Models;

public interface IAccountService
{
    IEnumerable<CaptionDTO> GetListCaptions(string statementSql, object param);
    IEnumerable<object> GetListBussiness(string statementSql);
    bool Login(string statementSql, User param);
}