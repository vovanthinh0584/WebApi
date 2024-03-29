﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;
using WebApplication.Utils;

namespace WebApplication.Services
{
    public class AccountService : IAccountService
    {
        IDao _dao;
        ILogger<AccountService> _logger;
        IMessage _message;
        public AccountService(IDao dao, ILoggerFactory loggerFactory, IMessage message)
        {
            _dao = dao;
            _message = message;
            _logger = loggerFactory.CreateLogger<AccountService>();
        }
        public IEnumerable<object> GetListCaptions(string statementSql, object param)
        {
            string sql = _dao.GetSqlStatement(statementSql);
            IEnumerable<object> listCaptions = _dao.Query<object>(sql, param);
            return listCaptions;
        }
        public IEnumerable<object> GetListBussiness(string statementSql)
        {
            string sql = _dao.GetSqlStatement(statementSql);
            IEnumerable<object> listBussiness = _dao.Query<object>(sql, null);
            return listBussiness;
        }
        private List<string> GetPermission(DataTable dataTable)
        {
            var listPermission = new List<string>();
            var listRows = dataTable.Rows;
            for (int i = 0; i < listRows.Count; i++)
            {
                var row = listRows[i];
                listPermission.Add(row["FunctionID"].ToString());
            }
            return listPermission;
        }
        public Boolean Login(string statementSql, UserDTO user)
        {
            string sql = _dao.GetSqlStatement(statementSql);
            object userParam = new
            {
                UserID = user.UserID
            };
            var listUsers = _dao.Query<UserDTO>(sql, userParam);
           
            IDictionary<string, object> param = new Dictionary<string, object>();
            param["UserId"] = user.UserID;
            param["BUID"] = user.BusinessUnitID;
            param["Lang"] = user.Language;
            var permissons = _dao.ExecuteSP("UMS_tblUserAccount_Login", param);
            Boolean isCheckLogin = false;
            if (listUsers.Count()>0)
            {
                var detail = listUsers.FirstOrDefault();
                string passwordEncrypt = Encrypting.AesEncrypt(user.Password, user.Password, user.Password);
                
                user.Permissions = GetPermission(permissons);
                if (user.Password == detail.PasswordNotMahoa)
                {
                    isCheckLogin = true;
                }
            }
            if (isCheckLogin == true)
            {
                user.Message = _message.GetMessage("MBL00001", user.Language);
                return true;
            }
            else
            {
                user.Message = _message.GetMessage("MBL00002", user.Language);
                return false;
            }
            return true;
        }

        public IEnumerable<object> GetVersion()
        {
            string sql = _dao.GetSqlStatement("GetVersion");
            IEnumerable<object> versions = _dao.Query<object>(sql, null);
            return versions;
        }
    }
    
    
}
