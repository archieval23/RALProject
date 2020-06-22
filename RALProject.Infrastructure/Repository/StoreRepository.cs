using AutoMapper;
using RALProject.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DomainEntity = RALProject.Domain.Entities;
using LoginData = RALProject.Infrastructure.EntityFramework.RAL;
using System.Data;
using System.Data.SqlClient;
using System.Data.Odbc;

namespace RALProject.Infrastructure.Repository
{
    public sealed class StoreRepository : IStoreRepository
    {
        OdbcConnection JDAContext;

        private IMapper _mapper;
        private LoginData.RAL_DevEntities _rALDbContext;

        public StoreRepository
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

        public IEnumerable<DomainEntity.StoreEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public DomainEntity.StoreEntity GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DomainEntity.StoreEntity> Find(Expression<Func<DomainEntity.StoreEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Add(DomainEntity.StoreEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Update(DomainEntity.StoreEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<DomainEntity.StoreEntity> GetAllStore(DomainEntity.StoreEntity entity)
        {
            try
            {
                var storeList = new List<DomainEntity.StoreEntity>();

                using (JDAContext)
                {
                    if (DBOPEN(Function.getConnectionString(entity.login_entity), JDAContext))
                    {
                        string querystring = "SELECT " + entity.login_entity.dBname + ".TBLSTR.STRNUM, "
                                + entity.login_entity.dBname + ".TBLSTR.REGNUM, "
                                + entity.login_entity.dBname + ".TBLSTR.STRDST, "
                                + entity.login_entity.dBname + ".TBLSTR.STRNAM, "
                                + entity.login_entity.dBname + ".TBLSTR.STMNGR, "
                                + entity.login_entity.dBname + ".TBLSTR.STRWHS "
                                + "FROM " + entity.login_entity.dBname + ".TBLSTR ";
                                //+ "FETCH FIRST 50 ROWS ONLY";

                        var JDACommand = new OdbcCommand(querystring, JDAContext);
                        JDACommand.CommandTimeout = 0;
                        var JDAReader = JDACommand.ExecuteReader();
                        while (JDAReader.Read())
                        {
                            var storeDetails = new DomainEntity.StoreEntity();
                            storeDetails.store = JDAReader.GetInt32(JDAReader.GetOrdinal("STRNUM"));
                            storeDetails.region = JDAReader.GetInt32(JDAReader.GetOrdinal("REGNUM"));
                            storeDetails.district = JDAReader.GetInt32(JDAReader.GetOrdinal("STRDST"));
                            storeDetails.description = JDAReader.GetString(JDAReader.GetOrdinal("STRNAM"));
                            storeDetails.manager = JDAReader.GetString(JDAReader.GetOrdinal("STMNGR"));
                            storeDetails.warehouse = JDAReader.GetString(JDAReader.GetOrdinal("STRWHS"));
                            storeList.Add(storeDetails);
                        }
                        JDAReader.Close();
                        JDACommand.Dispose();
                    }
                }
                return storeList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                JDAContext.Close();
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
