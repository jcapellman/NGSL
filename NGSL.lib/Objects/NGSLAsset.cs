using System.Net;
using System.Net.Sockets;

namespace NGSL.lib.Objects
{
    public class NGSLAsset
    {
        public string Name { get; internal set;  }

        public IPAddress Address { get;internal set;}

        public AddressFamily IPAddressFamily { get; internal set; }
    }
}