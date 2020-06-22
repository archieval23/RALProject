using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using DomainEntity = RALProject.Domain.Entities;

namespace RALProject.Infrastructure
{
    public class Function
    {
        public static string getConnectionString(DomainEntity.LoginEntity entity)
        {
            return $"Driver={{iSeries Access ODBC Driver}};DATABASE={entity.dBname};SYSTEM={entity.servername};UID={entity.username}; PWD={entity.password}; OPTION=0;";
        }
    }
}