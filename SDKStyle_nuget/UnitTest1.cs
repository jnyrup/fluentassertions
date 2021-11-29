using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject1
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
