using AutoMapper;
using RALProject.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DomainEntity = RALProject.Domain.Entities;
using LoginData = RALProject.Infrastructure.EntityFramework.RAL;
using System.Data;

namespace RALProject.Infrastructure.Repository
{
    public class BusinessUnitRepository : IBusinessUnitRepository
    {
        private IMapper _mapper;
        private LoginData.RAL_DevEntities _rALDbContext;

        public BusinessUnitRepository
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

        public IEnumerable<DomainEntity.BusinessUnitEntity> GetAll()
        {
            return _rALDbContext.Business_UnitsTable
                    .Select(r => new DomainEntity.BusinessUnitEntity
                    {
                        id = r.id,
                        code = r.code,
                        name = r.name,
                        jda_library = r.jda_library,
                        jda_ip_address = r.jda_ip_address,
                    })
                    .ToList();
        }

        public DomainEntity.BusinessUnitEntity GetById(int id)
        {
            return _rALDbContext.Business_UnitsTable
                    .Where(r => r.id == id)
                    .Select(r => new DomainEntity.BusinessUnitEntity
                    {
                        id = r.id,
                        code = r.code,
                        name = r.name,
                        jda_library = r.jda_library,
                        jda_ip_address = r.jda_ip_address,
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
