using AutoMapper;
using RALProject.ApplicationService.DTOs;
using RALProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RALProject.Web
{
    public class ServiceMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<BusinessUnitEntity, BusinessUnitDto>().ReverseMap();
            CreateMap<LoginEntity, LoginDto>().ReverseMap();
            CreateMap<POEntity, PODto>().ReverseMap();
            CreateMap<ReportEntity, ReportDto>().ReverseMap();
            CreateMap<StoreEntity, StoreDto>().ReverseMap();
            CreateMap<UserServerDefaultEntity, UserServerDefaultDto>().ReverseMap();
            CreateMap<VendorEntity, VendorDto>().ReverseMap();
            CreateMap<LastLoginEntity, LastLoginDto>().ReverseMap();

        }
    }
}
