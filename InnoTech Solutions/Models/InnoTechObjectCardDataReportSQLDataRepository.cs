using ictweb5.Domain;
using ictweb5.Domain.Interfaces;
using ictweb5.Domain.Reports.Heat;
using ictweb5.Models;
using System.Collections.Generic;
using System;

namespace InnoTech_Solutions.Models
{
    public class InnoTechObjectCardDataReportSQLDataRepository : ObjectCardDataReportSQLDataRepositoryClass
    {
        public InnoTechObjectCardDataReportSQLDataRepository(IICTDataRepository repository)
            : base(repository)
        {
        }

        public Object View(UserAccountClass user, EntityClass Counter, ConsumerClass Consumer, 
            Boolean history, ArchiveType archiveType,
            AccountingType accountingType, DateTime dateFrom, DateTime dateTo)
        {
            Dictionary<String, Object> Parameters = new Dictionary<String, Object>();

            Parameters.Add("@CityID", null);
            Parameters.Add("@LocationID", null);
            Parameters.Add("@ObjectID", null);
            Parameters.Add("@CounterID", Counter.ID);
            Parameters.Add("@BeginTime", dateFrom);
            Parameters.Add("@EndTime", dateTo);
            Parameters.Add("@DateStr", "");

            Parameters.Add("@ArchiveType", archiveType);

            if (accountingType == ictweb5.Domain.AccountingType.actNone)
            {
                Parameters.Add("@AccountingType", null);
            }
            else
            {
                Parameters.Add("@AccountingType", accountingType);
            }

            Parameters.Add("@History", history);

            if (user.Admin)
                Parameters.Add("@UserID", null);
            else
                Parameters.Add("@UserID", user.ID);
            Parameters.Add("@CityList", "");
            Parameters.Add("@LocationList", "");
            Parameters.Add("@ObjectList", "");
            //потребитель
            if (Consumer.ID > 0)
                Parameters.Add("@ConsumerID", Consumer.ID);
            else
                Parameters.Add("@ConsumerID", null);

            return View(Parameters);
        }

    }
}
