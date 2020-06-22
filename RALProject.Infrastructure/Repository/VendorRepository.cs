using AutoMapper;
using RALProject.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DomainEntity = RALProject.Domain.Entities;
using LoginData = RALProject.Infrastructure.EntityFramework.RAL;
using System.Data.SqlClient;
using System.Data;
using System.Data.Odbc;

namespace RALProject.Infrastructure.Repository
{
    public sealed class VendorRepository : IVendorRepository
    {
        OdbcConnection JDAContext;

        private IMapper _mapper;
        private LoginData.RAL_DevEntities _rALDbContext;

        public VendorRepository
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

        public IEnumerable<DomainEntity.VendorEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public DomainEntity.VendorEntity GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DomainEntity.VendorEntity> Find(Expression<Func<DomainEntity.VendorEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Add(DomainEntity.VendorEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Update(DomainEntity.VendorEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DomainEntity.VendorEntity> GetAllVendor(DomainEntity.VendorEntity entity)
        {
            try
            {
                var vendorList = new List<DomainEntity.VendorEntity>();

                using (JDAContext)
                {
                    if (DBOPEN(Function.getConnectionString(entity.login_entity), JDAContext))
                    {
                        string querystring = "SELECT " + entity.login_entity.dBname + ".APSUPP.ASALPH, "
                                                + entity.login_entity.dBname + ".APSUPP.ASNUM, "
                                                + entity.login_entity.dBname + ".APSUPP.ASNAME, "
                                                + entity.login_entity.dBname + ".APADDR.AAPHON "
                                            + "FROM " + entity.login_entity.dBname + ".APSUPP "
                                                + "INNER JOIN " + entity.login_entity.dBname + ".APADDR "
                                            + "ON " + entity.login_entity.dBname + ".APSUPP.ASNUM "
                                            + "= " + entity.login_entity.dBname + ".APADDR.AANUM ";
                                            //+ "FETCH FIRST 50 ROWS ONLY";

                        var JDACommand = new OdbcCommand(querystring, JDAContext);
                        JDACommand.CommandTimeout = 0;
                        var JDAReader = JDACommand.ExecuteReader();

                        while (JDAReader.Read())
                        {
                            var vendorDetails = new DomainEntity.VendorEntity();
                            vendorDetails.mnemonic = JDAReader.GetString(JDAReader.GetOrdinal("ASALPH"));
                            vendorDetails.vendorNumber = JDAReader.GetInt32(JDAReader.GetOrdinal("ASNUM"));
                            vendorDetails.vendorName = JDAReader.GetString(JDAReader.GetOrdinal("ASNAME"));
                            vendorDetails.phoneNumber = JDAReader.GetString(JDAReader.GetOrdinal("AAPHON"));
                            vendorList.Add(vendorDetails);
                        }

                        JDAReader.Close();
                        JDACommand.Dispose();
                    }
                }

                return vendorList;
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
