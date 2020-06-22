using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RALProject.Infrastructure.EntityFramework.RAL
{
    public interface IRALUnitOfWork : IDisposable
    {
        RAL_DevEntities DbContext { get; }
        int Save();
    }
    public class RALUnitOfWork : IRALUnitOfWork
    {
        private RAL_DevEntities context;

        //public UnitOfWork(string connectionString)
        //{
        //    this.ConnectionString = connectionString;
        //}

        public RAL_DevEntities DbContext
        {
            get
            {
                if (context == null)
                {
                    context = new RAL_DevEntities();
                }
                return context;
            }
        }

        public int Save()
        {
            //return context.SaveChanges();
            try
            {
                return context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }

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
