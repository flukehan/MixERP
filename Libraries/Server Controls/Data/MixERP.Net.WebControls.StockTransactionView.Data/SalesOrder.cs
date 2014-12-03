/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/

using MixERP.Net.Common;
using Npgsql;
using System;

/********************************************************************************
Copyright (C) Binod Nepal, Mix Open Foundation (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/

using System.Collections.ObjectModel;

namespace MixERP.Net.WebControls.StockTransactionView.Data
{
    public static class SalesOrder
    {
        [CLSCompliant(false)]
        public static NpgsqlCommand GetSalesOrderViewCommand(Collection<int> ids)
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
                            transactions.non_gl_stock_details.shipping_charge,
                            core.get_sales_tax_code_by_sales_tax_id(transactions.non_gl_stock_details.sales_tax_id) as tax_code,
                            transactions.non_gl_stock_details.tax,
                            transactions.non_gl_stock_master.non_taxable,
                            transactions.non_gl_stock_master.salesperson_id,
                            transactions.non_gl_stock_master.shipper_id,
                            transactions.non_gl_stock_master.store_id,
                            core.get_shipping_address_code_by_shipping_address_id(transactions.non_gl_stock_master.store_id) AS shipping_address_code,
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
                        WHERE transactions.non_gl_stock_master.book = 'Sales.Order'
                        AND transactions.non_gl_stock_master.non_gl_stock_master_id IN(" + string.Join(",", parameters) + ");";

            //Create a PostgreSQL command object from the SQL string.
            using (NpgsqlCommand command = new NpgsqlCommand(sql))
            {
                //Iterate through the IDs and add PostgreSQL parameters.
                for (int i = 0; i < ids.Count; i++)
                {
                    command.Parameters.AddWithValue("@Id" + Conversion.TryCastString(i), ids[i]);
                }

                return command;
            }
        }
    }
}