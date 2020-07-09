using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using DomainEntity = RALProject.Domain.Entities;
using System.Data.Odbc;

namespace RALProject.Infrastructure
{
    public class Function
    {
        public static string getConnectionString(DomainEntity.LoginEntity entity)
        {
            return $"Driver={{iSeries Access ODBC Driver}};DATABASE={entity.dBname};SYSTEM={entity.servername};UID={entity.username}; PWD={entity.password}; OPTION=0;";
        }
        public static string getStoreConnectionString(DomainEntity.LoginEntity entity)
        {
            return $"Driver={{iSeries Access ODBC Driver}};DATABASE=MM770RSC;SYSTEM=10.87.201.26;UID={entity.username}; PWD={entity.password}; OPTION=0;";
            //return $"Driver={{iSeries Access ODBC Driver}};DATABASE=MM770RSC;SYSTEM={entity.servername};UID={entity.username}; PWD={entity.password}; OPTION=0;";
        }
    }
}