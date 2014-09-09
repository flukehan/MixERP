using MixERP.Net.Tests.PgUnitTest.Helpers;
using NUnit.Framework;

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