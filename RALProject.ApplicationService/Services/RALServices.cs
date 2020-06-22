using RALProject.ApplicationService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using RALProject.Domain.Contracts;
using AutoMapper;
using RALProject.Domain.Entities;
using RALProject.Common.Logger;
using RALProject.Common.EmailHelper;
using RALProject.ApplicationService.ServiceContract;
using RALProject.Infrastructure.EntityFramework.RAL;

namespace RALProject.ApplicationService.Services
{
    public class RALServices : IRALServices
    {
        private readonly IMapper _mapper;
        private readonly IEmailManager _emailManager;
        private readonly IBusinessUnitRepository _businessUnitRepository;
        private readonly ILoginRepository _loginRepository;
        private readonly IPORepository _pORepository;
        private readonly IStoreRepository _storeRepository;
        private readonly IVendorRepository _vendorRepository;
        private readonly IRALUnitOfWork _rALUnitOfWork;
        private static readonly ILogCentral logCentral = LogCentral.GetLogger(typeof(RALServices));

        public RALServices
        (
            IBusinessUnitRepository businessUnitRepository,
            ILoginRepository loginRepository,
            IPORepository pORepository,
            IStoreRepository storeRepository,
            IVendorRepository vendorRepository,
            IRALUnitOfWork rALUnitOfWork,
            IMapper mapper, 
            IEmailManager emailManager
        )
        {
            if (businessUnitRepository == null) throw new ArgumentNullException("BusinessUnitRepository");
            if (loginRepository == null) throw new ArgumentNullException("LoginRepository");
            if (pORepository == null) throw new ArgumentNullException("PORepository");
            if (storeRepository == null) throw new ArgumentNullException("StoreRepository");
            if (vendorRepository == null) throw new ArgumentNullException("VendorRepository");
            if (rALUnitOfWork == null) throw new ArgumentNullException("RALUnitOfWork");
            if (mapper == null) throw new ArgumentNullException("Mapper");
            if (emailManager == null) throw new ArgumentNullException("EmailManager");

            _businessUnitRepository = businessUnitRepository;
            _loginRepository = loginRepository;
            _pORepository = pORepository;
            _storeRepository = storeRepository;
            _vendorRepository = vendorRepository;
            _rALUnitOfWork = rALUnitOfWork;
            _mapper = mapper;
            _emailManager = emailManager;
        }

        #region MyBusinessUnit Class definition  ++++++++++++++++++++++++++++++++++++++++++
        public IEnumerable<BusinessUnitDto> BusinessUnitAll()
        {
            try
            {
                return _mapper.Map<IEnumerable<BusinessUnitEntity>, IEnumerable<BusinessUnitDto>>
                    (_businessUnitRepository.GetAll().Where(a => a.jda_ip_address.Trim() != ""));
            }
            catch (Exception ex)
            {
                logCentral.Error("BusinessUnitAll", ex);
                throw;
            }
        }

        public BusinessUnitDto BusinessUnitById(int id)
        {
            try
            {
                return _mapper.Map<BusinessUnitEntity, BusinessUnitDto>
                    (_businessUnitRepository.GetById(id));
            }
            catch (Exception ex)
            {
                logCentral.Error("BusinessUnitById", ex);
                throw;
            }
        }
        #endregion

        #region MyLogin Class definition  ++++++++++++++++++++++++++++++++++++++++++
        public bool GetLoginByConnectionString(LoginDto logindto)
        {
            try
            {
                return _loginRepository.GetByConnectionString(_mapper.Map<LoginDto, LoginEntity>(logindto));
            }
            catch (Exception ex)
            {
                logCentral.Error("BusinessUnitById", ex);
                throw;
            }
        }
        #endregion

        #region MyPO Class definition  ++++++++++++++++++++++++++++++++++++++++++
        public IEnumerable<PODto> PODataAll(PODto podto)
        {
            try
            {
                POEntity poEntity = new POEntity
                {
                    login_entity = new LoginEntity
                    {
                        servername = podto.login_dto.servername,
                        username = podto.login_dto.username,
                        password = podto.login_dto.password,
                        dBname = podto.login_dto.dBname
                    },
                };

                return _mapper.Map<IEnumerable<POEntity>, IEnumerable<PODto>>
                    (_pORepository.GetPO(poEntity));
            }
            catch (Exception ex)
            {
                logCentral.Error("PODataAll", ex);
                throw;
            }
        }
        #endregion

        #region MyStore Class definition  ++++++++++++++++++++++++++++++++++++++++++
        public IEnumerable<StoreDto> GetStore(StoreDto storedto)
        {
            try
            {
                StoreEntity storeEntity = new StoreEntity
                {
                    login_entity = new LoginEntity
                    {
                        servername = storedto.login_dto.servername,
                        username = storedto.login_dto.username,
                        password = storedto.login_dto.password,
                        dBname = storedto.login_dto.dBname
                    },
                };

                return _mapper.Map<IEnumerable<StoreEntity>, IEnumerable<StoreDto>>
                    (_storeRepository.GetAllStore(storeEntity));
            }
            catch (Exception ex)
            {
                logCentral.Error("PODataAll", ex);
                throw;
            }
        }
        #endregion

        #region MyVendor Class definition  ++++++++++++++++++++++++++++++++++++++++++
        public IEnumerable<VendorDto> GetVendor(VendorDto vendordto)
        {
            try
            {
                VendorEntity vendorEntity = new VendorEntity
                {
                    login_entity = new LoginEntity
                    {
                        servername = vendordto.login_dto.servername,
                        username = vendordto.login_dto.username,
                        password = vendordto.login_dto.password,
                        dBname = vendordto.login_dto.dBname
                    },
                };

                return _mapper.Map<IEnumerable<VendorEntity>, IEnumerable<VendorDto>>
                    (_vendorRepository.GetAllVendor(vendorEntity));
            }
            catch (Exception ex)
            {
                logCentral.Error("PODataAll", ex);
                throw;
            }
        }
        #endregion

        public void Dispose()
        {
            _rALUnitOfWork.Dispose();
        }
    }
}
