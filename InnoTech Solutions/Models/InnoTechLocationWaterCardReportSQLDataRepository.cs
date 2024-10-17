using ictweb5.Domain;
using ictweb5.Domain.Interfaces;
using ictweb5.Domain.Reports.Heat;
using ictweb5.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using ictweb5.Domain.Reports.Water;

namespace InnoTech_Solutions.Models
{
    public class InnoTechLocationWaterCardReportSQLDataRepository : LocationWaterCardReportSQLDataRepositoryClass
    {
        public InnoTechLocationWaterCardReportSQLDataRepository(IICTDataRepository repository)
            : base(repository)
        {
        }

        public Object View(UserAccountClass user, EntityClass Counter,
           ConsumerClass Consumer, Boolean history, ArchiveType archiveType,
           AccountingType accountingType, DateTime dateFrom, DateTime dateTo)
        {
            Dictionary<String, Object> Parameters = new Dictionary<String, Object>();

            Parameters.Add("@CityID", null);
            Parameters.Add("@LocationID", null);
            Parameters.Add("@ObjectID", null);
            Parameters.Add("@BeginTime", dateFrom);
            Parameters.Add("@EndTime", dateTo);
            Parameters.Add("@UserID", user.ID);
            Parameters.Add("@ArchiveType", archiveType);
            Parameters.Add("@History", history);
            Parameters.Add("@CityList", "");
            Parameters.Add("@LocationList", "");
            Parameters.Add("@ObjectList", "");
            //список счётчиков
            Parameters.Add("@CounterList", Counter.ID.ToString());
            //потребитель
            if (Consumer.ID > 0)
                Parameters.Add("@ConsumerID", Consumer.ID);
            else
                Parameters.Add("@ConsumerID", null);
            Parameters.Add("@ShowTotal", 0);

            return View(Parameters);
        }

    }
}
