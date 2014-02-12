/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0. 
If a copy of the MPL was not distributed  with this file, You can obtain one at 
http://mozilla.org/MPL/2.0/.
***********************************************************************************/

using System.Collections.ObjectModel;
using Npgsql;
using MixERP.Net.Common;
using System;

namespace MixERP.Net.WebControls.StockTransactionView.Data
{
    public static class SalesQuotation
    {
        [CLSCompliant(false)]
        public static NpgsqlCommand GetSalesQuotationViewCommand(Collection<int> ids)
        {
            if (ids == null)
            {
                return null;
            }

            //Crate an array object to store the parameters.
            var parameters = new string[ids.Count];

            //Iterate through the ids and create a parameter array.
            for (int i = 0; i < ids.Count; i++)
            {
                parameters[i] = "@Id" + Conversion.TryCastString(i);
            }

            string sql = @"SELECT
	                        transactions.non_gl_stock_master.value_date,
	                        transactions.non_gl_stock_master.party_id,
	                        core.parties.party_code,
	                        transactions.non_gl_stock_master.price_type_id,
	                        transactions.non_gl_stock_master.reference_number,
	                        core.items.item_code,
	                        core.items.item_name,
	                        transactions.non_gl_stock_details.quantity,
	                        core.units.unit_name,
	                        transactions.non_gl_stock_details.price,
	                        transactions.non_gl_stock_details.discount,
	                        transactions.non_gl_stock_details.tax_rate,
	                        transactions.non_gl_stock_details.tax,	
	                        transactions.non_gl_stock_master.statement_reference
                        FROM
                        transactions.non_gl_stock_master
                        INNER JOIN
                        transactions.non_gl_stock_details
                        ON transactions.non_gl_stock_master.non_gl_stock_master_id = transactions.non_gl_stock_details.non_gl_stock_master_id
                        INNER JOIN core.items
                        ON transactions.non_gl_stock_details.item_id = core.items.item_id
                        INNER JOIN core.units
                        ON transactions.non_gl_stock_details.unit_id = core.units.unit_id
                        INNER JOIN core.parties
                        ON transactions.non_gl_stock_master.party_id = core.parties.party_id
                        WHERE transactions.non_gl_stock_master.non_gl_stock_master_id IN(" + string.Join(",", parameters) + ");";

            //Create a PostgreSQL command object from the SQL string.
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                //Iterate through the IDs and add PostgreSQL parameters.
                for (int i = 0; i < ids.Count; i++)
                {
                    command.Parameters.Add("@Id" + Conversion.TryCastString(i), ids[i]);
                }

                return command;
            }
        }
    }
}
