using My.Server.Core.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Server.Data.Infrastructure
{
    public class BasicUnitOfWork: IUnitOfWork
    {
        private IDbConnection db;
        private IDbTransaction dbTransaction = null;

        public BasicUnitOfWork(IDbConnection db, IDbTransaction dbTrans)
        {
            this.db = db;
            this.dbTransaction = dbTrans;
        }

        public int Commit()
        {
            try
            {
                dbTransaction.Commit();
                return 1;
            }
            catch (Exception ex)
            {

                return -1;
            }
        }

        public void RollbackChanges()
        {
            if (dbTransaction != null)
            {
                dbTransaction.Rollback();
            }
        }
    }
}
