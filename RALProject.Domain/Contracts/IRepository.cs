using RALProject.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RALProject.Domain.Contracts
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IEnumerable<T> GetAll();

        /// <summary>
        /// Retrieve single record from the database
        /// </summary>
        /// <param name="id">Id(primary key) of the record</param>
        /// <returns>Single record</returns>
        T GetById(int id);

        /// <summary>
        /// Find single or multiple record/s in the database
        /// </summary>
        /// <param name="predicate">Where condition</param>
        /// <returns>Single or multiple record/s</returns>
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Persist single record to database
        /// </summary>
        /// <param name="entity">Data model class</param>
        void Add(T entity);

        /// <summary>
        /// Persist changes on single record to database
        /// </summary>
        /// <param name="entity">Data model class</param>
        void Update(T entity);

        /// <summary>
        /// Delete permanently single record to database
        /// </summary>
        /// <param name="id">Id(primary key) of the record</param>
        void Remove(int id);
    }
}
