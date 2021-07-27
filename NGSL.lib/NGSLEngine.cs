using System;
using System.Net;

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

        public EventHandler<NGSL.lib.Objects.NGSLAsset> OnNewAssetDiscovered;

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

        public async void Start(SNMPVersion version = SNMPVersion.V1, int portNumber = Constants.PortNumber, int intervalMs = Constants.Interval)
        {
            discoverer.AgentFound -= Discoverer_AgentFound;
            discoverer.AgentFound += Discoverer_AgentFound;

            await discoverer.DiscoverAsync(version.ToVersionCode(), new IPEndPoint(IPAddress.Broadcast, portNumber), new OctetString("public"), intervalMs);
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
