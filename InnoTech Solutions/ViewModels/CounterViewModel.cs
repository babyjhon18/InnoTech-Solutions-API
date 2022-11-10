using ictweb5.ViewModels;
using InnoTech_Solutions.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;

namespace InnoTech_Solutions.ViewModels
{
    public class CounterViewModel
    {
        public DeviceDataToSerialize DeviceData { get; set; }
        public CounterViewModel(ReportViewClass report, List<string> ListOfCounters)
        {
            DeviceDataToSerialize _DevicesData = new DeviceDataToSerialize();
            List<DataRowsClass> RowsDataDevice = new List<DataRowsClass>();
            List<int> CountersID = new List<int>();
            List<int> CountersIDForUser = new List<int>();
            foreach (DataRow row in report.Data.Rows)
            {
                var rowDataDevice = new DataRowsClass();
                rowDataDevice.CounterID = Convert.ToInt32(row["CounterID"]);
                rowDataDevice.Date_volume = DateTime.Parse(row["dtDateTime"].ToString());
                rowDataDevice.Volume = Convert.ToDouble(row["Volume"]);
                if (!CountersID.Contains(rowDataDevice.CounterID))
                    CountersID.Add(rowDataDevice.CounterID);
                RowsDataDevice.Add(rowDataDevice);
            };
            if (ListOfCounters.Count == 0)
            {
                foreach (var counterID in CountersID)
                {
                    CountersIDForUser.Add(Convert.ToInt32(counterID));
                }
            }
            else
            {
                foreach (var counterID in ListOfCounters)
                {
                    CountersIDForUser.Add(Convert.ToInt32(counterID));
                }
            }
            _DevicesData.Date = DateTime.Today;
            _DevicesData.id_provider = "Indel";
            Devices devices = new Devices();
            Values values = new Values();
            int index = 1;
            foreach (var CounterID in CountersIDForUser)
            {
                if (index == 999)
                {
                    DeviceData = _DevicesData;
                    break;
                }
                dynamic obj = new ExpandoObject();
                devices = new Devices();
                AddProperty(obj, "device_id" + index.ToString("000"), CounterID);
                foreach (var Volume in RowsDataDevice.Where(c => c.CounterID == CounterID))
                {
                    values = new Values();
                    values.Date_volume = Convert.ToDateTime(Volume.Date_volume + "+03:00");
                    values.Volume = Volume.Volume;
                    values.Back_volume = 0;
                    values.errors = "";
                    devices.Values.Add(values);
                }
                AddProperty(obj, "values", devices.Values);
                _DevicesData.Devices.Add(obj);
                index++;
            }
            DeviceData = _DevicesData;
        }
        private void AddProperty(ExpandoObject expando, string name, object value)
        {
            var exDict = expando as IDictionary<string, object>;
            if (exDict.ContainsKey(name))
                exDict[name] = value;
            else
                exDict.Add(name, value);
        }
    }
}
