using DomainEntity = RALProject.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RALProject.Infrastructure.EntityFramework;
using RALProject.Infrastructure.EntityFramework.RAL;

namespace RALProject.Web
{
    public class DomainMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<DomainEntity.UserServerDefaultEntity, UserServerDefult>().ReverseMap();
        }
    }
}
