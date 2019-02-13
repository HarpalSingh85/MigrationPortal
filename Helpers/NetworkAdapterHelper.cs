using MigrationPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;

namespace MigrationPortal.Helpers
{
    public static class NetworkAdapterHelper
    {
        public static List<Adapter> GetMac()
        {
            List<Adapter> NetworkAdapters = new List<Adapter>();
            NetworkAdapters = new List<Adapter>();
            IEnumerable<NetworkInterface> adapters = NetworkInterface
            .GetAllNetworkInterfaces()
            .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
            .Select(nic => nic);

            foreach (NetworkInterface item in adapters)
            {
                Adapter adapter = new Adapter();
                adapter.Name = item.Name;
                adapter.MacAddress = string.Join(":", (from rng in item.GetPhysicalAddress().GetAddressBytes() select rng.ToString("X2")).ToArray());
                NetworkAdapters.Add(adapter);
            }

            return NetworkAdapters;
        }


    }
}