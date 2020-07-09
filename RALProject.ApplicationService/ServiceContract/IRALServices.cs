using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RALProject.ApplicationService.DTOs;

namespace RALProject.ApplicationService.ServiceContract
{
    public interface IRALServices : IDisposable
    {
        IEnumerable<BusinessUnitDto> BusinessUnitAll();
        BusinessUnitDto BusinessUnitById(int id);
        bool GetLoginByConnectionString(LoginDto logindto);
        IEnumerable<PODto> PODataAll(PODto podto);
        IEnumerable<StoreDto> GetStore(StoreDto storedto);
        IEnumerable<VendorDto> GetVendor(VendorDto vendordto);
        IEnumerable<LastLoginDto> LastLoginByUsername(string username);
        void AddLastLogin(LastLoginDto lastLoginDto);
    }
}
