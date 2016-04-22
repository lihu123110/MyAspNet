using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Server.Core.Common
{
    public interface IUnitOfWork
    {
        int Commit();

        void RollbackChanges();
    }
}
