using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;
using WebApplication.Utils;

namespace WebApplication.Services
{
    public class AccountService: IAccountService
    {
        IDao _dao;
        ILogger<AccountService> _logger;
        IMessage _message;
        public AccountService(IDao dao,ILoggerFactory loggerFactory,IMessage message)
        {
            _dao = dao;
            _message = message;
            _logger = loggerFactory.CreateLogger<AccountService>();
        }
        public IEnumerable<CaptionDTO> GetListCaptions(string statementSql, object param)
        {
            string sql = _dao.GetSqlStatement(statementSql);
            IEnumerable<CaptionDTO> listCaptions = _dao.Query<CaptionDTO>(sql, param);
            return listCaptions;
        }
        public IEnumerable<object> GetListBussiness(string statementSql)
        {
            string sql = _dao.GetSqlStatement(statementSql);
            IEnumerable<object> listBussiness = _dao.Query<object>(sql,null);
            return listBussiness;
        }
        public Boolean Login(string statementSql,User user)
        {
            string sql = _dao.GetSqlStatement(statementSql);
            var listUsers = _dao.Query<User>(sql,user);
            Boolean isCheckLogin =false;
            if(listUsers !=null)
            {
                var detail = listUsers.FirstOrDefault();
                string passwordEncrypt = Encrypting.AesEncrypt(user.Password, user.Password,user.Password);

                if (passwordEncrypt == detail.Password)
                {
                    isCheckLogin = true;
                }
            }
            if (isCheckLogin ==true)
            {
                user.Message = _message.GetMessage("MBL00001",user.Language);
                return true;
            }
            else
            {
                user.Message = _message.GetMessage("MBL00002", user.Language);
                return false;
            }
            return true;
        }

    }
    public interface IAccountService
    {
        IEnumerable<CaptionDTO> GetListCaptions(string statementSql, object param);
        IEnumerable<object> GetListBussiness(string statementSql);
        Boolean Login(string statementSql, User param);

    }
}
