using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RALProject.Infrastructure.EntityFramework.Common
{
    public interface ICommonUnitOfWork : IDisposable
    {
        CommonEntities DbContext { get; }
        int Save();
    }

    public class CommonUnitOfWork : ICommonUnitOfWork
    {
        private CommonEntities context;

        //public UnitOfWork(string connectionString)
        //{
        //    this.ConnectionString = connectionString;
        //}

        public CommonEntities DbContext
        {
            get
            {
                if (context == null)
                {
                    context = new CommonEntities();
                }
                return context;
            }
        }

        public int Save()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (context != null)
                {
                    context.Dispose();
                    context = null;
                }
            }
        }
    }
}
