using System;
using System.Collections.Generic;

namespace InnoTech_Solutions.Models
{
    public class DeviceDataDictionary
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public Dictionary<string, string> Devices { get; set; }
        public DeviceDataDictionary()
        {
            Devices = new Dictionary<string, string>();
        }
    }

    public class DeviceDataList
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<String> Devices { get; set; }
        public DeviceDataList()
        {
            Devices = new List<string>();
        }
    }
}

