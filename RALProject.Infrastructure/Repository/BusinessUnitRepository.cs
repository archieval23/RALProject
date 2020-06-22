using AutoMapper;
using RALProject.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DomainEntity = RALProject.Domain.Entities;
using CommonData = RALProject.Infrastructure.EntityFramework.Common;
using System.Data;

namespace RALProject.Infrastructure.Repository
{
    public class BusinessUnitRepository : IBusinessUnitRepository
    {
        private IMapper _mapper;
        private CommonData.CommonEntities _commonDbContext;

        public BusinessUnitRepository
        (
            IMapper mapper,
            CommonData.ICommonUnitOfWork commonUnitOfWork
        )
        {
            if (mapper == null) throw new ArgumentNullException("Mapper");
            if (commonUnitOfWork == null) throw new ArgumentNullException("CommonUnitOfWork");

            _mapper = mapper;
            _commonDbContext = commonUnitOfWork.DbContext;
        }

        public IEnumerable<DomainEntity.BusinessUnitEntity> GetAll()
        {
            return _commonDbContext.business_units
                    .Select(r => new DomainEntity.BusinessUnitEntity
                    {
                        id = r.id,
                        code = r.code,
                        name = r.name,
                        short_name = r.short_name,
                        jda_library = r.jda_library,
                        jda_ip_address = r.jda_ip_address,
                        jda_linked_server_catalog = r.jda_linked_server_catalog
                    })
                    .ToList();
        }

        public DomainEntity.BusinessUnitEntity GetById(int id)
        {
            return _commonDbContext.business_units
                    .Where(r => r.id == id)
                    .Select(r => new DomainEntity.BusinessUnitEntity
                    {
                        id = r.id,
                        code = r.code,
                        name = r.name,
                        short_name = r.short_name,
                        jda_library = r.jda_library,
                        jda_ip_address = r.jda_ip_address,
                        jda_linked_server_catalog = r.jda_linked_server_catalog
                    }).FirstOrDefault();
        }

        public IEnumerable<DomainEntity.BusinessUnitEntity> Find(Expression<Func<DomainEntity.BusinessUnitEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Add(DomainEntity.BusinessUnitEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Update(DomainEntity.BusinessUnitEntity entity)
        {
            throw new NotImplementedException();
        }
        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
