using System;

using Lextm.SharpSnmpLib;

using NGSL.lib.Enums;

namespace NGSL.lib.Common
{
    public static class Extensions
    {
        public static VersionCode ToVersionCode(this SNMPVersion version) => version switch
        {
            SNMPVersion.V1 => VersionCode.V1,
            SNMPVersion.V2 => VersionCode.V2,
            SNMPVersion.V3 => VersionCode.V3,
            _ => throw new ArgumentOutOfRangeException($"{version} is out of supported range"),
        };
    }
}