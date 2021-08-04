using System;
using System.Net;
using System.Threading.Tasks;
using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;

using Microsoft.Extensions.Logging;

using NGSL.lib.Common;
using NGSL.lib.Enums;

namespace NGSL.lib
{
    public class NGSLEngine
    {
        private readonly Discoverer discoverer;

        private readonly ILogger? _logger;

        public event EventHandler<Objects.NGSLAsset>? OnNewAssetDiscovered;

        private void LogError(string message)
        {
            Log(LogLevel.Error, message);
        }

        private void LogDebug(string message)
        {
            Log(LogLevel.Debug, message);
        }

        private void Log(LogLevel logLevel, string message)
        {
            _logger?.Log(logLevel, message);
        }

        public NGSLEngine(ILogger? logger = null)
        {
            _logger = logger;

            discoverer = new Discoverer();

            LogDebug("NGSLEngine Object Initialized");
        }

        public async Task<bool> Start(SNMPVersion version = SNMPVersion.V1 | SNMPVersion.V2 | SNMPVersion.V3, int portNumber = Constants.PortNumber, int intervalMs = Constants.Interval)
        {
            try
            {
                discoverer.AgentFound -= Discoverer_AgentFound;
                discoverer.AgentFound += Discoverer_AgentFound;
                
                if (version.HasFlag(SNMPVersion.V1))
                {
                    await discoverer.DiscoverAsync(VersionCode.V1, new IPEndPoint(IPAddress.Broadcast, portNumber), new OctetString("public"), intervalMs);
                }

                if (version.HasFlag(SNMPVersion.V2))
                {
                    await discoverer.DiscoverAsync(VersionCode.V2, new IPEndPoint(IPAddress.Broadcast, portNumber), new OctetString("public"), intervalMs);
                }

                if (version.HasFlag(SNMPVersion.V3))
                {
                    await discoverer.DiscoverAsync(VersionCode.V3, new IPEndPoint(IPAddress.Broadcast, portNumber), new OctetString("public"), intervalMs);
                }

                return true;
            } catch (Exception ex)
            {
                LogError($"An unexpected exception occurred when starting the Engine: {ex}");

                return false;
            }
        }

        private void Discoverer_AgentFound(object? sender, AgentFoundEventArgs e)
        {
            var agent = new NGSL.lib.Objects.NGSLAsset
            {
                Address = e.Agent.Address,
                IPAddressFamily = e.Agent.AddressFamily,
                Data = e.Variable.Data.ToString()
            };

            OnNewAssetDiscovered?.Invoke(this, agent);
        }
    }
}
