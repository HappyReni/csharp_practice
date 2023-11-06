using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodeTracker;

namespace CodeTracker.Tests
{
    [TestClass]
    public class CodeTracker_ValidDateTime
    {
        [TestMethod]
        public void ValidDateTime()
        {
            try
            {
                var result = Validation.ValidDateTime("2022-10-10 10:20:14");
            }
            catch 
            {
                Assert.Fail();
            }
        }
    }
}
