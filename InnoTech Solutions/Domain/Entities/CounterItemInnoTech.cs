using ictweb5.Domain;
using ictweb5.Domain.Interfaces;
using ictweb5.Models;
using ictweb5.ViewModels;
using InnoTech_Solutions.Models;
using InnoTech_Solutions.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using static InnoTech_Solutions.Domain.IInnoTechDataRepository;

namespace InnoTech_Solutions.Domain.Entities
{
    public class CounterItemInnoTech : InnoCRUDDataRepositoryItem, IItemDataRepository
    {
        public CounterItemInnoTech(IICTDataRepository repository):
            base(repository)
        {
        }
        //heat.objectCard.Data.Report
        public override object Data<T>(T Data, UserAccountClass user)
        {
            try
            {
                DataParamsView dataParams = Data as DataParamsView;
                List<RegionClass> regions = new List<RegionClass>();
                List<LocationClass> locations = new List<LocationClass>();
                List<BaseObjectClass> objects = new List<BaseObjectClass>();
                List<ReportViewClass> reports = new List<ReportViewClass>();
                List<EntityClass> counters = (from counter in dataParams.ListCountersID.Split(",") 
                                             select new EntityClass() { ID = Convert.ToInt32(counter)}).ToList();
                foreach (var counter in counters)
                {
                    reports.Add(new InnoTechObjectCardDataReportSQLDataRepository(repository)
                        .View(user, counter, new ConsumerClass(), false, ArchiveType.atCurrent, AccountingType.actNone,
                                dataParams.Device.DateFrom, dataParams.Device.DateTo) as ReportViewClass);
                    reports.Add(new InnoTechLocationWaterCardReportSQLDataRepository(repository)
                       .View(user, counter, new ConsumerClass(), false, ArchiveType.atCurrent, AccountingType.actNone,
                               dataParams.Device.DateFrom, dataParams.Device.DateTo) as ReportViewClass);
                }
                //reports.Add(repository.ReportEngine
                //    .Reports["ictweb5.Domain.Reports.Water.LocationWaterCardReportSQLDataRepositoryClass"]
                //    .View(user, regions, locations, objects
                //    , new ConsumerClass(), false,
                //    ArchiveType.atCurrent, AccountingType.actNone,
                //    dataParams.Device.DateFrom,
                //    dataParams.Device.DateTo) as ReportViewClass);
                //reports.Add(repository.ReportEngine
                //    .Reports["ictweb5.Domain.Reports.Heat.ObjectCardDataReportSQLDataRepositoryClass"]
                //    .View(user, regions, locations, objects
                //    , new ConsumerClass(), false,
                //    ArchiveType.atCurrent, AccountingType.actHotWater,
                //    dataParams.Device.DateFrom,
                //    dataParams.Device.DateTo) as ReportViewClass);
                CounterViewModel counterView =
                    new CounterViewModel(reports, dataParams.Device.Devices);
                
                return counterView.DeviceData;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public override Object View<T>(T Data, IQueryCollection Params, UserAccountClass user)
        {
            CounterParamsView counterParams = Data as CounterParamsView;
            string SQLStatement = "";
            if (counterParams.ListSerialNumbers != "")
            {
                SQLStatement = "select CounterSerialNum, CounterID, ObjectName, ObjectID from Objects " +
                "JOIN dbo.Devices ON Objects.ObjectID = Devices.FK_ObjectID " +
                "JOIN dbo.Counters ON Devices.DeviceID = Counters.FK_DeviceID " +
                "JOIN dbo.WebUserLocation ON Objects.FK_LocationID = WebUserLocation.FK_LocationID " +
                "Where CounterSerialNum IN(" + counterParams.ListSerialNumbers + ") AND FK_WebUserID = " + user.ID;
            }
            else
            {
                SQLStatement = "select CounterSerialNum, CounterID, ObjectName, ObjectID from Objects " +
                "JOIN dbo.Devices ON Objects.ObjectID = Devices.FK_ObjectID " +
                "JOIN dbo.Counters ON Devices.DeviceID = Counters.FK_DeviceID " +
                "JOIN dbo.WebUserLocation ON Objects.FK_LocationID = WebUserLocation.FK_LocationID " +
                "Where FK_WebUserID = " + user.ID;
            }
            DataTable table = repository.Common.OpenQuery(SQLStatement);
            var returnData = new { id_provider = "Indel", date = DateTime.Today, Devices = new List<Object>() };
            Object returnDataDevice = new Object();
            foreach (DataRow row in table.Rows)
            {
                returnDataDevice = new
                {
                    device_number = row["CounterSerialNum"],
                    device_id = row["CounterID"].ToString(),
                    radio_id = "",
                    address = row["ObjectName"],
                    unp = "",
                    manufacturer = "Indel",
                };
                returnData.Devices.Add(returnDataDevice);
            }
            return returnData;
        }
    }
}
