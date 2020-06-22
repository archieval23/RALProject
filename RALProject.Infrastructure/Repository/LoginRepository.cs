using AutoMapper;
using RALProject.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DomainEntity = RALProject.Domain.Entities;
using LoginData = RALProject.Infrastructure.EntityFramework.RAL;
using System.Data.SqlClient;
using System.Data;
using System.Data.Odbc;

namespace RALProject.Infrastructure.Repository
{
    public class LoginRepository : ILoginRepository
    {
        OdbcConnection JDAContext;

        private IMapper _mapper;
        private LoginData.RAL_DevEntities _rALDbContext;

        public LoginRepository
        (
            IMapper mapper,
            LoginData.IRALUnitOfWork rAlUnitOfWork
        )
        {
            if (mapper == null) throw new ArgumentNullException("Mapper");
            if (rAlUnitOfWork == null) throw new ArgumentNullException("RALUnitOfWork");

            _mapper = mapper;
            _rALDbContext = rAlUnitOfWork.DbContext;
        }

        public IEnumerable<DomainEntity.LoginEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public DomainEntity.LoginEntity GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DomainEntity.LoginEntity> Find(Expression<Func<DomainEntity.LoginEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Add(DomainEntity.LoginEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Update(DomainEntity.LoginEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public bool GetByConnectionString(DomainEntity.LoginEntity entity)
        {
            try
            {
                using (JDAContext)
                {
                    if (DBOPEN(Function.getConnectionString(entity), JDAContext))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool DBOPEN(string ConnectionStr, OdbcConnection tmpJDAContext)
        {
            if (JDAContext == null)
            {
                JDAContext = new OdbcConnection();

                JDAContext.ConnectionString = ConnectionStr;
                try
                {
                    JDAContext.Open();
                    if (JDAContext != null) { return true; }
                }
                catch (Exception e)
                {
                    return false;
                    throw e;
                }
            }
            else
            {
                return true;
            }
            return false;
        }

    }
}
