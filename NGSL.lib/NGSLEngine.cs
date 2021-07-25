﻿using System;
using System.Net;

using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;

using Microsoft.Extensions.Logging;

namespace NGSL.lib
{
    public class NGSLEngine
    {
        private Discoverer discoverer;

        private ILogger? _logger;

        public NGSLEngine(ILogger? logger = null)
        {
            _logger = logger;

            discoverer = new Discoverer();
        }

        public async void Start()
        {
            discoverer.AgentFound -= Discoverer_AgentFound;
            discoverer.AgentFound += Discoverer_AgentFound;

            await discoverer.DiscoverAsync(VersionCode.V1, new IPEndPoint(IPAddress.Broadcast, 161), new OctetString("public"), 6000);
        }

        private void Discoverer_AgentFound(object? sender, AgentFoundEventArgs e)
        {
            
        }
    }
}
