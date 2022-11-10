using InnoTech_Solutions.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace InnoTech_Solutions.Domain
{
    public class CounterParamsView 
    {
        public string ListSerialNumbers { get; set; }
        public CounterParamsView(string Params)
        {
            Dictionary<string, string> dictionaryEntity = new Dictionary<string, string>();
            List<string> listEntity = new List<string>();
            try
            {
                dictionaryEntity = JsonConvert.DeserializeObject<Dictionary<string, string>>(Params.ToString());
                foreach (var value in dictionaryEntity.Values)
                {
                    listEntity.Add("'" + value.ToString() + "'");
                }
                ListSerialNumbers = string.Join(",", listEntity);
            }
            catch
            {
                listEntity = JsonConvert.DeserializeObject<List<string>>(Params.ToString());
                ListSerialNumbers = "'" + string.Join("','", listEntity) + "'";
            }
        }
    }
    public class DataParamsView
    {
        public DeviceDataList Device { get; set; }
        public string ListCountersID { get; set; }
        public DataParamsView(string Params)
        {
            DeviceDataDictionary dictionaryEntity = new DeviceDataDictionary();
            DeviceDataList listEntity = new DeviceDataList();
            try
            {
                dictionaryEntity = JsonConvert.DeserializeObject<DeviceDataDictionary>(Params.ToString());
                foreach (var value in dictionaryEntity.Devices.Values)
                {
                    listEntity.Devices.Add(value);
                }
                listEntity.DateFrom = dictionaryEntity.DateFrom;
                listEntity.DateTo = dictionaryEntity.DateTo;
            }
            catch
            {
                listEntity = JsonConvert.DeserializeObject<DeviceDataList>(Params.ToString());
            }
            ListCountersID = string.Join(",", listEntity.Devices);
            Device = listEntity;
        }
    }
}
