using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace My.Server.Core.Common
{
    public interface IRepository
    {
        IUnitOfWork UnitOfWork { get; }

        void DisposeDB();

        /// <summary>
        /// 查询一个集合对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">条件表达式(x=> x.UserId=1001)</param>
        /// <returns></returns>
        List<T> Select<T>(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 查询一个集合对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        List<T> Select<T>();

        /// <summary>
        /// 可以查询视图，关联查询语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="anonType"></param>
        /// <returns></returns>
        List<T> Select<T>(string sql, object anonType);

        /// <summary>
        /// 查询一个集合对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ids">根据Id集合</param>
        /// <returns></returns>
        List<T> SelectByIds<T>(IEnumerable<T> ids);

        /// <summary>
        /// 查询一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="anonType">匿名类型：var anonType=new{x=1,xx=2,xxx=3}</param>
        /// <returns></returns>
        T Single<T>(object anonType);

        /// <summary>
        /// 根据一个Id查询一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="idValue">Id值</param>
        /// <returns></returns>
        T SingleById<T>(object idValue);

        /// <summary>
        /// 添加一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        bool Add<T>(T item) where T : class;

        /// <summary>
        /// 批量添加对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objs"></param>
        void AddAll<T>(IEnumerable<T> objs);

        /// <summary>
        /// 批量修改对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objs"></param>
        /// <returns></returns>
        bool UpdateAll<T>(IEnumerable<T> objs);

        /// <summary>
        /// 执行原生Sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dbParams"></param>
        /// <returns></returns>
        bool ExecuteSql(string sql, object dbParams);

        /// <summary>
        /// 执行一个存储过程,返回一个bool值
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="inParams"></param>
        /// <param name="sqlCode"></param>
        /// <returns></returns>
        bool SqlProc(string procName, object inParams, int sqlCode);

        /// <summary>
        /// 执行存储过程,返回一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procName"></param>
        /// <param name="inParams"></param>
        /// <returns></returns>
        T SqlProcConvertTo<T>(string procName, object inParams);

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procName"></param>
        /// <param name="inParams"></param>
        /// <returns></returns>
        List<T> SqlProcConvertToList<T>(string procName, object inParams);

        /// <summary>
        /// 对象是否存在
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        bool IsExist<T>(Expression<Func<T, bool>> expression);

        /// <summary>
        /// 更新一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        bool Update<T>(T item, Expression<Func<T, bool>> where = null);

        /// <summary>
        /// 仅更新对象的某一个字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="item"></param>
        /// <param name="onlyFields"></param>
        /// <returns></returns>
        bool UpdateOnly<T, TKey>(T item, Expression<Func<T, TKey>> onlyFields = null);

        /// <summary>
        /// 仅更新某一个对象的某一个字段，根据条件更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="item"></param>
        /// <param name="onlyFields"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        bool UpdateOnly<T, TKey>(T item, Expression<Func<T, TKey>> onlyFields = null, Expression<Func<T, bool>> where = null);
    }
}
