using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void WhenGeneratingClientIdAndClientSecretTheyAreGenerated()
        {
            var audience = AudiencesStore.AddAudience("Test");
        }
    }
}
