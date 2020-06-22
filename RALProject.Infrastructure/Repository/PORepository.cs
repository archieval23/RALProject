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
    public sealed class PORepository : IPORepository
    {
        OdbcConnection JDAContext;

        private IMapper _mapper;
        private LoginData.RAL_DevEntities _rALDbContext;

        public PORepository
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

        public IEnumerable<DomainEntity.POEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public DomainEntity.POEntity GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DomainEntity.POEntity> Find(Expression<Func<DomainEntity.POEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Add(DomainEntity.POEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Update(DomainEntity.POEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DomainEntity.POEntity> GetPO(DomainEntity.POEntity entity)
        {
            try
            {
                var poList = new List<DomainEntity.POEntity>();

                using (JDAContext)
                {
                    if (DBOPEN(Function.getConnectionString(entity.login_entity), JDAContext))
                    {
                        string querystring = "SELECT " + entity.login_entity.dBname + ".POMHDR.POVNUM, "
                                            + entity.login_entity.dBname + ".POMHDR.POMSPO, "
                                            + entity.login_entity.dBname + ".POMHDR.PONUMB, "
                                            + entity.login_entity.dBname + ".POMHDR.POEDAT, "
                                            + entity.login_entity.dBname + ".POMHDR.POBUYR, "
                                            + entity.login_entity.dBname + ".POMHDR.PODPT, "
                                            + entity.login_entity.dBname + ".POMHDR.POSDPT "
                                + "FROM " + entity.login_entity.dBname + ".POMHDR "
                                    + "INNER JOIN " + entity.login_entity.dBname + ".POMRCH "
                                    + "ON " + entity.login_entity.dBname + ".POMHDR.PONUMB " + "=" + entity.login_entity.dBname + ".POMRCH.PONUMB "
                                + "WHERE " + entity.login_entity.dBname + ".POMHDR.POSTAT = '3'"
                                    + "AND " + entity.login_entity.dBname + ".POMRCH.POSTAT = '3'"
                                    + "GROUP BY"
                                    + " " + entity.login_entity.dBname + ".POMHDR.POVNUM "
                                    + "," + entity.login_entity.dBname + ".POMHDR.POMSPO "
                                    + "," + entity.login_entity.dBname + ".POMHDR.PONUMB "
                                    + "," + entity.login_entity.dBname + ".POMHDR.POEDAT "
                                    + "," + entity.login_entity.dBname + ".POMHDR.POBUYR "
                                    + "," + entity.login_entity.dBname + ".POMHDR.PODPT "
                                    + "," + entity.login_entity.dBname + ".POMHDR.POSDPT ";
                                    //+ "FETCH FIRST 50 ROWS ONLY";

                        var JDACommand = new OdbcCommand(querystring, JDAContext);
                        JDACommand.CommandTimeout = 0;
                        var JDAReader = JDACommand.ExecuteReader();

                        while (JDAReader.Read())
                        {
                            var poDetails = new DomainEntity.POEntity();
                            poDetails.vendorNumber = JDAReader.GetInt32(JDAReader.GetOrdinal("POVNUM"));
                            poDetails.masterPONumber = JDAReader.GetInt32(JDAReader.GetOrdinal("POMSPO"));
                            poDetails.pONumber = JDAReader.GetInt32(JDAReader.GetOrdinal("PONUMB"));
                            poDetails.orderDate = JDAReader.GetInt32(JDAReader.GetOrdinal("POEDAT"));
                            poDetails.buyer = JDAReader.GetString(JDAReader.GetOrdinal("POBUYR"));
                            poDetails.dept = JDAReader.GetInt32(JDAReader.GetOrdinal("PODPT"));
                            poDetails.subDept = JDAReader.GetInt32(JDAReader.GetOrdinal("POSDPT"));
                            poList.Add(poDetails);
                        }

                        JDAReader.Close();
                        JDACommand.Dispose();
                    }
                }

                return poList;
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
