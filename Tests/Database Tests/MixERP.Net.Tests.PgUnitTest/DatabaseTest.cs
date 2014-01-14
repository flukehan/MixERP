/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using MixERP.Net.Tests.PgUnitTest.Helpers;

namespace MixERP.Net.Tests.PgUnitTest
{
    [TestFixture]
    public class DatabaseTest
    {
        [Test]
        public void RunDBTests()
        {
            DBTestRunner runner = new DBTestRunner();
            
            bool expected = true;
            bool actual = runner.RunTests();
            string message = runner.Message;

            if (expected != actual)
            {
                Assert.Fail(message);
            }
            else 
            {
                Assert.Pass(message);
            }
        }
    }
}