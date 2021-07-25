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
        private Discoverer discoverer;

        private ILogger? _logger;

        public EventHandler<NGSL.lib.Objects.NGSLAsset> OnNewAssetDiscovered;

        public NGSLEngine(ILogger? logger = null)
        {
            _logger = logger;

            discoverer = new Discoverer();
        }

        public async void Start(SNMPVersion version = SNMPVersion.V1)
        {
            discoverer.AgentFound -= Discoverer_AgentFound;
            discoverer.AgentFound += Discoverer_AgentFound;

            await discoverer.DiscoverAsync(version.ToVersionCode(), new IPEndPoint(IPAddress.Broadcast, 161), new OctetString("public"), 6000);
        }

        private void Discoverer_AgentFound(object? sender, AgentFoundEventArgs e)
        {
            var agent = new NGSL.lib.Objects.NGSLAsset();

            OnNewAssetDiscovered?.Invoke(this, agent);
        }
    }
}
