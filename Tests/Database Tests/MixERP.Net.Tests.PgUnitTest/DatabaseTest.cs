using NUnit.Framework;
using MixERP.Net.Tests.PgUnitTest.Helpers;

namespace MixERP.Net.Tests.PgUnitTest
{
    [TestFixture]
    public class DatabaseTest
    {
        [Test]
        public void RunDbTests()
        {
            DbTestRunner runner = new DbTestRunner();

            const bool expected = true;
            bool actual = runner.RunTests();
            string message = runner.Message;

            if (expected != actual)
            {
                Assert.Fail(message);
            }
        }
    }
}