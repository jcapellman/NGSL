using System;

namespace NGSL.lib.Enums
{
    [Flags]
    public enum SNMPVersion
    {
        V1 = 0x00,
        V2 = 0x01,
        V3 = 0x02
    }
}