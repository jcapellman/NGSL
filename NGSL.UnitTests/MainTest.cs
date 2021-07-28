using Microsoft.VisualStudio.TestTools.UnitTesting;

using NGSL.lib;
using NLog;

namespace NGSL.UnitTests
{
    [TestClass]
    public class MainTest
    {
        [TestMethod]
        public void NGSLEngine_NullLogger()
        {
            var engine = new NGSLEngine(null);

            Assert.IsNotNull(engine);
        }

        [TestMethod]
        public void NGSLEngine_NotNullLogger()
        {
            var logger = LogManager.GetCurrentClassLogger();

            var engine = new NGSLEngine();

            Assert.IsNotNull(engine);
        }
    }
}