using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SDKStyle_assembly
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            new System.Net.Http.HttpResponseMessage().Should().NotBeNull();
        }
    }
}
