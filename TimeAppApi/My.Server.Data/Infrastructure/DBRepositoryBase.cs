using My.Server.Core.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using My.Server.Data.Common;
using ServiceStack.OrmLite;
using My.Server.Data.Model;

namespace My.Server.Data.Infrastructure
{
    public abstract class DBRepositoryBase: IRepository
    {
        private IUnitOfWork _unitOfWork;

        private IDbConnection _dbConnection;

        private IDbTransaction _dbTrans;

        public DBRepositoryBase() { }

        public DBRepositoryBase(string connectionConfig, bool isOpenTransaction)
        {
            if (_dbConnection == null)
            {
                _dbConnection = ConnectionHelper.OpenDbConnection(connectionConfig);

                if (isOpenTransaction)
                {
                    _dbTrans = _dbConnection.OpenTransaction();//开启事务
                }
            }
        }

        /// <summary>
        /// 数据连接对象
        /// </summary>
        public IDbConnection DBConnection
        {
            get
            {
                return _dbConnection;
            }
        }

        public IUnitOfWork UnitOfWork
        {
            get
            {
                if (_unitOfWork == null)
                {
                    _unitOfWork = new BasicUnitOfWork(this._dbConnection, _dbTrans);
                }
                return _unitOfWork;
            }
        }

        public void DisposeDB()
        {
            if (_dbConnection != null)
            {
                _dbConnection.Close();
                _dbConnection.Dispose();
            }
        }

        public T Single<T>(object anonType)
        {
            return DBConnection.Single<T>(anonType);
        }

        public T SingleById<T>(object idValue)
        {
            return DBConnection.SingleById<T>(idValue);
        }

        public bool Add<T>(T item) where T : class
        {
            return DBConnection.Insert<T>(item) > 0;
        }

        public void AddAll<T>(IEnumerable<T> objs)
        {
            DBConnection.InsertAll<T>(objs);
        }

        public bool UpdateAll<T>(IEnumerable<T> objs)
        {
            return DBConnection.UpdateAll<T>(objs) > 0;
        }

        public bool ExecuteSql(string sql, object dbParams)
        {
            return DBConnection.ExecuteSql(sql, dbParams) > 0;
        }

        public bool SqlProc(string procName, object inParams, int sqlCode)
        {
            IDbCommand dbCommand = DBConnection.SqlProc(procName, inParams);
            ExcuteProcModelData result = dbCommand.ConvertTo<ExcuteProcModelData>();
            return result.MessageId == sqlCode;
        }

        public T SqlProcConvertTo<T>(string procName, object inParams)
        {
            IDbCommand dbCommand = DBConnection.SqlProc(procName, inParams);
            return dbCommand.ConvertTo<T>();
        }

        public List<T> SqlProcConvertToList<T>(string procName, object inParams)
        {
            IDbCommand dbCommand = DBConnection.SqlProc(procName, inParams);
            return dbCommand.ConvertToList<T>();
        }

        public bool IsExist<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            return DBConnection.Exists<T>(expression);
        }

        public bool Update<T>(T item, System.Linq.Expressions.Expression<Func<T, bool>> where = null)
        {
            return DBConnection.Update<T>(item, where) > 0;
        }

        public bool UpdateOnly<T, TKey>(T item, System.Linq.Expressions.Expression<Func<T, TKey>> onlyFields = null)
        {
            return DBConnection.UpdateOnly<T, TKey>(item, onlyFields) > 0;
        }

        public bool UpdateOnly<T, TKey>(T item, System.Linq.Expressions.Expression<Func<T, TKey>> onlyFields = null, System.Linq.Expressions.Expression<Func<T, bool>> where = null)
        {
            return DBConnection.UpdateOnly<T, TKey>(item, onlyFields, where) > 0;
        }


        public List<T> Select<T>(Expression<Func<T, bool>> predicate)
        {
            return DBConnection.Select<T>(predicate);
        }

        public List<T> Select<T>()
        {
            return DBConnection.Select<T>();
        }

        public List<T> SelectByIds<T>(IEnumerable<T> ids)
        {
            return DBConnection.SelectByIds<T>(ids);
        }


        public List<T> Select<T>(string sql, object anonType)
        {
            return DBConnection.Select<T>(sql, anonType);
        }
    }
}
