using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Services
{
    public class CaptionService: ICaptionService
    {
        IDao _dao;
        public CaptionService(IDao dao)
        {
            _dao = dao;
        }
        public IEnumerable<CaptionDTO> GetListCaptions(string statementSql, object param)
        {
            string sql = _dao.GetSqlStatement(statementSql);
            IEnumerable<CaptionDTO> listCaptions = _dao.Query<CaptionDTO>(sql, param);
            return listCaptions;
        }
    }
    public interface ICaptionService{
        IEnumerable<CaptionDTO> GetListCaptions(string statementSql, object param);
    }
}
