using MixERP.Net.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MixERP.Net.TransactionGovernor.Autoverification
{
    public static class Autoverify
    {
        public static bool Pass(long transactionMasterId)
        {
            if (Switches.EnableAutoVerification())
            {
                return Data.Autoverification.Autoverify.Pass(transactionMasterId);
            }

            return false;
        }
    }
}