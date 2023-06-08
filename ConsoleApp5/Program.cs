using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    internal class Program
    {


        static void Main(string[] args)
        {
            var macAddr =
    (
        from nic in NetworkInterface.GetAllNetworkInterfaces()
        where nic.OperationalStatus == OperationalStatus.Up
        select nic.GetPhysicalAddress().ToString()
    ).FirstOrDefault();
            
            Console.WriteLine(GetMacAddress());
            PhysicalAddress physAddr = NetworkInterface.GetAllNetworkInterfaces()
    .Where(ni => ni.OperationalStatus == OperationalStatus.Up && ni.NetworkInterfaceType != NetworkInterfaceType.Loopback)
    .First()
    .GetPhysicalAddress();
            Console.WriteLine(BitConverter.ToString(physAddr.GetAddressBytes()));

            Console.WriteLine(Convert.ToString(GetDefaultGateway()));
        }
        static string GetMacAddress()
        {
            string macAddresses = "";
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                // Only consider Ethernet network interfaces, thereby ignoring any
                // loopback devices etc.
                if (nic.NetworkInterfaceType != NetworkInterfaceType.Ethernet) continue;
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    macAddresses += nic.GetPhysicalAddress().ToString();
                    break;
                }
            }
            return macAddresses;
        }

        public static IPAddress GetDefaultGateway()
        {
            var card = NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault();
            if (card == null) return null;
            var address = card.GetIPProperties().GatewayAddresses.FirstOrDefault();
            return address.Address;
        }
    }
}
