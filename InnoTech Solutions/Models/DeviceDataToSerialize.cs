using System;
using System.Collections.Generic;

namespace InnoTech_Solutions.Models
{
    public class DeviceDataToSerialize
    {
        public string id_provider { get; set; }
        public DateTime Date { get; set; }
        public List<Object> Devices { get; set; }
        public DeviceDataToSerialize()
        {
            Devices = new List<Object>();
        }
    }

    public class Devices
    {
        public Dictionary<string, int> Device_id { get; set; }
        public List<Values> Values { get; set; }
        public Devices()
        {
            Device_id = new Dictionary<string, int>();
            Values = new List<Values>();
        }
    }

    public class Values
    {
        public Values()
        {
            errors = "OK";
        }
        public DateTime Date_volume { get; set; }
        public double Volume { get; set; }
        public double Back_volume { get; set; }
        public string errors { get; set; }
    }

    public class DataRowsClass
    {
        public int CounterID { get; set; }
        public DateTime Date_volume { get; set; }
        public double Volume1 { get; set; }
        public double Volume2 { get; set; }
    }
}
