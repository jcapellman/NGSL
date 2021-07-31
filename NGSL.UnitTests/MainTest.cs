using Microsoft.VisualStudio.TestTools.UnitTesting;

using NGSL.lib;

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
            var engine = new NGSLEngine();

            Assert.IsNotNull(engine);
        }
    }
}