using AutoMapper;
using RALProject.ApplicationService.DTOs;
using RALProject.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RALProject.Web
{
    public class WebMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<BusinessUnitDto, BusinessUnitModel>().ReverseMap();
            CreateMap<LoginDto, LoginModel>().ReverseMap();
            CreateMap<PODto, POModel>().ReverseMap();
            CreateMap<ReportDto, ReportModel>().ReverseMap();
            CreateMap<StoreDto, StoreModel>().ReverseMap();
            CreateMap<UserServerDefaultDto, UserServerDefaultModel>().ReverseMap();
            CreateMap<VendorDto, VendorModel>().ReverseMap();
            CreateMap<LastLoginDto, LastLoginModel>().ReverseMap();
        }
    }
}
