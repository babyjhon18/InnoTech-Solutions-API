using ictweb5.ViewModels;
using InnoTech_Solutions.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;

namespace InnoTech_Solutions.ViewModels
{
    public class CounterViewModel
    {
        public DeviceDataToSerialize DeviceData { get; set; }
        public CounterViewModel(List<ReportViewClass> reports, List<string> ListOfCounters)
        {
            DeviceDataToSerialize _DevicesData = new DeviceDataToSerialize();
            List<DataRowsClass> RowsDataDevice = new List<DataRowsClass>();
            List<int> CountersID = new List<int>();
            List<int> CountersIDForUser = new List<int>();
            foreach (ReportViewClass report in reports) 
            {
                foreach (DataRow row in report.Data.Rows)
                {
                    var rowDataDevice = new DataRowsClass();
                    rowDataDevice.CounterID = Convert.ToInt32(row["CounterID"]);
                    if(row.Table.Columns.IndexOf("Energy1") < 0)
                    {
                        rowDataDevice.Date_volume = Convert.ToDateTime(row["dtDateTime"].Equals(DBNull.Value) ? 
                            DateTime.MinValue : row["dtDateTime"]);
                        if (rowDataDevice.Date_volume != DateTime.MinValue)
                        {
                            rowDataDevice.Volume1 = Convert.ToDouble(row["Volume"]);
                            rowDataDevice.Volume2 = 0;
                        }
                    }
                    else
                    {
                        rowDataDevice.Date_volume = Convert.ToDateTime(row["dtValuesTime"].Equals(DBNull.Value) ?
                            DateTime.MinValue : row["dtValuesTime"]);
                        if (rowDataDevice.Date_volume != DateTime.MinValue)
                        {
                            var CounterEnergoNum = row["CounterEnergoNum"].Equals(DBNull.Value) ? "" : Convert.ToString(row["CounterEnergoNum"]);
                            if (CounterEnergoNum != "")
                            {
                                switch (CounterEnergoNum)
                                {
                                    case "1":
                                        rowDataDevice.Volume1 = Convert.ToDouble(row["Volume1"]);
                                        rowDataDevice.Volume2 = 0;
                                        break;
                                    case "2":
                                        rowDataDevice.Volume1 = Convert.ToDouble(row["Volume2"]);
                                        rowDataDevice.Volume2 = 0;
                                    break;
                                }
                            }
                            else
                            {
                                rowDataDevice.Volume1 = Convert.ToDouble(row["Volume1"]);
                                rowDataDevice.Volume2 = Convert.ToDouble(row["Volume2"]);
                            }
                        }
                    }
                    if(rowDataDevice.Date_volume != DateTime.MinValue)
                    {
                        if (!CountersID.Contains(rowDataDevice.CounterID))
                            CountersID.Add(rowDataDevice.CounterID);
                        RowsDataDevice.Add(rowDataDevice);
                    }
                };
                
            }
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
                    values.Volume = Volume.Volume1;
                    values.Back_volume = Volume.Volume2;
                    //values.errors = "";
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
