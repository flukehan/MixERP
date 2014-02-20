/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

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