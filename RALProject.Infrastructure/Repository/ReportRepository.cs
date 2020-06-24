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
    public class ReportRepository : IReportRepository
    {
        OdbcConnection JDAContext;

        private IMapper _mapper;
        private LoginData.RAL_DevEntities _rALDbContext;

        public ReportRepository
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

        public IEnumerable<DomainEntity.ReportEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public DomainEntity.ReportEntity GetById(int id)
        {
            throw new NotImplementedException();
            //return _rALDbContext.ReportTables
            //        .Where(r => r.PONUMB == id)
            //        .Select(r => new DomainEntity.ReportEntity
            //        {
            //            id = r.id,
            //            pONumber = r.PONUMB,
            //            p
            //            //code = r.code,
            //            //name = r.name,
            //            //short_name = r.short_name,
            //            //jda_library = r.jda_library,
            //            //jda_ip_address = r.jda_ip_address,
            //            //jda_linked_server_catalog = r.jda_linked_server_catalog
            //        }).FirstOrDefault();
        }

        public IEnumerable<DomainEntity.ReportEntity> Find(Expression<Func<DomainEntity.ReportEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Add(DomainEntity.ReportEntity entity)
        {
            _rALDbContext.ReportTables.Add(_mapper.Map<DomainEntity.ReportEntity, LoginData.ReportTable>(entity));
        }

        public void AddReport(IEnumerable<DomainEntity.ReportEntity> entity)
        {
            try
            {
                entity.ToList().ForEach(a =>
                {
                    var report = new LoginData.ReportTable();

                    report.report_id = a.report_id;
                    report.PONUMB = a.pONumber;
                    report.POMRCV = a.rANumber;
                    report.ASAUTO = a.aSAuto;
                    report.POLOC = a.storeNumber;
                    report.STRNAM = a.storeName;
                    report.POVNUM = a.vendorCode;
                    report.ASNAME = a.vendorName;
                    report.POSDAT = a.receivingDate;
                    report.POCDAT = a.cancelDate;
                    report.IVPLTI = a.ti;
                    report.IVPLHI = a.hi; 
                    report.LOCATION = a.Location;
                    report.INUMBR = a.iNumber;
                    report.IDESCR = a.iDecription;
                    report.IUPC = a.upc;
                    report.POMUM = a.um;
                    report.ORDERQTY = a.orderQty;

                    _rALDbContext.ReportTables.Add(report);
                    _rALDbContext.SaveChanges();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Update(DomainEntity.ReportEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DomainEntity.ReportEntity> GetReport(DomainEntity.ReportEntity entity)
        {
            try
            {
                var reportList = new List<DomainEntity.ReportEntity>();

                string query = String.Empty;

                using (JDAContext)
                {
                    if (DBOPEN(Function.getConnectionString(entity.login_entity), JDAContext))
                    {
                        if (entity.pONumber != 0)
                        {
                            query = " AND a.PONUMB = " + entity.pONumber + " ";
                        }
                        else 
                        {
                            if (entity.storeNumber != 0 && entity.vendorCode == 0)
                            {
                                query = " AND a.POLOC = " + entity.storeNumber + " AND  d.POSDAT BETWEEN " + entity.receivingDate + " AND " + entity.cancelDate + "";
                            }

                            if (entity.storeNumber == 0 && entity.vendorCode != 0)
                            {
                                query = " AND a.POVNUM = " + entity.vendorCode + " AND  d.POSDAT BETWEEN " + entity.receivingDate + " AND " + entity.cancelDate + "";
                            }

                            if (entity.storeNumber != 0 && entity.vendorCode != 0)
                            {
                                query = " AND a.POVNUM = " + entity.vendorCode + " AND  d.POLOC = " + entity.storeNumber + " AND  d.POSDAT BETWEEN " + entity.receivingDate + " AND " + entity.cancelDate + "";
                            }
                        }

                        string querystring = "SELECT "
                                        + "a.PONUMB, "
                                        + "a.POMRCV, "
                                        + "case when b.ASAUTO = 'C' then 'CROSS DOCK' else 'STOCK OPERATION' end as ASAUTO , "
                                        + "a.POLOC, "
                                        + "c.STRNAM, "
                                        + "a.POVNUM, "
                                        + "b.ASNAME, "
                                        + "d.POSDAT, "
                                        + "d.POCDAT, "
                                        + "f.IVPLTI, "
                                        + "f.IVPLHI, "
                                        + "case when b.ASAUTO = 'C' then 'X-DOCK' else 'STOCK OP' end as LOCATION, "
                                        + "e.INUMBR, "
                                        + "f.IDESCR, "
                                        + "g.IUPC, "
                                        + "e.POMUM, "
                                        + "e.POMQTY/e.POMPK as ORDERQTY "
                                        + "FROM " + entity.login_entity.dBname + ".POMRCH a "
                                            + "INNER JOIN " + entity.login_entity.dBname + ".APSUPP b ON a.POVNUM = b.ASNUM "
                                            + "INNER JOIN " + entity.login_entity.dBname + ".TBLSTR c ON a.POLOC = c.STRNUM "
                                            + "INNER JOIN " + entity.login_entity.dBname + ".POMHDR d ON a.PONUMB = d.PONUMB "
                                            + "INNER JOIN " + entity.login_entity.dBname + ".POMRCD e ON a.PONUMB = e.PONUMB "
                                            + "INNER JOIN " + entity.login_entity.dBname + ".INVMST f ON e.INUMBR = f.INUMBR "
                                            + "INNER JOIN " + entity.login_entity.dBname + ".INVUPC g ON e.INUMBR = g.INUMBR "
                                        + "WHERE a.POSTAT = '3' and d.POSTAT = '3'" + query;

                        var JDACommand = new OdbcCommand(querystring, JDAContext);
                        JDACommand.CommandTimeout = 0;
                        var JDAReader = JDACommand.ExecuteReader();

                        while (JDAReader.Read())
                        {
                            var reportDetails = new DomainEntity.ReportEntity();
                            reportDetails.pONumber = JDAReader.GetInt32(JDAReader.GetOrdinal("PONUMB"));
                            reportDetails.rANumber = JDAReader.GetInt32(JDAReader.GetOrdinal("POMRCV"));
                            reportDetails.aSAuto = JDAReader.GetString(JDAReader.GetOrdinal("ASAUTO"));
                            reportDetails.storeNumber = JDAReader.GetInt32(JDAReader.GetOrdinal("POLOC"));
                            reportDetails.storeName = JDAReader.GetString(JDAReader.GetOrdinal("STRNAM"));
                            reportDetails.vendorCode = JDAReader.GetInt32(JDAReader.GetOrdinal("POVNUM"));
                            reportDetails.vendorName = JDAReader.GetString(JDAReader.GetOrdinal("ASNAME"));
                            reportDetails.receivingDate = JDAReader.GetString(JDAReader.GetOrdinal("POSDAT"));
                            reportDetails.cancelDate = JDAReader.GetString(JDAReader.GetOrdinal("POCDAT"));
                            reportDetails.ti = JDAReader.GetInt32(JDAReader.GetOrdinal("IVPLTI"));
                            reportDetails.hi = JDAReader.GetInt32(JDAReader.GetOrdinal("IVPLHI"));
                            reportDetails.Location = JDAReader.GetString(JDAReader.GetOrdinal("LOCATION"));
                            reportDetails.iNumber = JDAReader.GetInt32(JDAReader.GetOrdinal("INUMBR"));
                            reportDetails.iDecription = JDAReader.GetString(JDAReader.GetOrdinal("IDESCR"));
                            reportDetails.upc = JDAReader.GetString(JDAReader.GetOrdinal("IUPC"));
                            reportDetails.um = JDAReader.GetString(JDAReader.GetOrdinal("POMUM"));
                            reportDetails.orderQty = JDAReader.GetInt32(JDAReader.GetOrdinal("ORDERQTY"));
                            reportList.Add(reportDetails);
                        }

                        JDAReader.Close();
                        JDACommand.Dispose();
                    }
                }

                return reportList;
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
