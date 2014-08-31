<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PartyControl.ascx.cs" Inherits="MixERP.Net.FrontEnd.UserControls.PartyControl" %>
    <div class="grey form-inline" role="form">
        <div class="form-group form-group-sm">
            <label for="PartyDropDownList">Select Customer</label>

            <div class="form-group form-group-sm">
                <input type="text" id="PartyCodeTextBox" class="form-control input-sm" style="width: 100px;" />
            </div>

            <div class="input-group input-group-sm">
                <select id="PartyDropDownList" class="form-control">
                    <option>Test test test test test test test</option>
                </select>
                <span class="input-group-btn">
                    <button class="btn btn-primary" type="button">Go!</button>
                </span>
            </div>
        </div>
    </div>


    <ul class="nav nav-tabs" role="tablist">
        <li class="active"><a href="#home" role="tab" data-toggle="tab">Home</a></li>
        <li><a href="#party-summary" role="tab" data-toggle="tab">Party Summary</a></li>
        <li><a href="#transaction-summary" role="tab" data-toggle="tab">Transaction Summary</a></li>
        <li><a href="#addresses-and-contact-info" role="tab" data-toggle="tab">Addresses & Contact Info</a></li>
    </ul>

    <!-- Tab panes -->
    <div class="tab-content">
        <div class="tab-pane fade in active" id="home">
            
        </div>
        <div class="tab-pane fade" id="party-summary">
            <table class="table table-bordered table-hover">
                <tr>
                    <td style="width: 300px;">
                        <strong>Party Type</strong>
                    </td>
                    <td>Agent
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>Email Address</strong>
                    </td>
                    <td>email@address.com
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>PAN Number</strong>
                    </td>
                    <td>123234234
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>SST Number</strong>
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>CST Number</strong>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <strong>Credit Allowed</strong>
                    </td>
                    <td>Yes
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>Maximum Credit Period</strong>
                    </td>
                    <td>7 days
                    </td>
                </tr>
                <tr>
                    <td><strong>Maximum Credit Amount</strong>
                    </td>
                    <td>Rs. 30049
                    </td>
                </tr>
                <tr>
                    <td><strong>Interest Applicable</strong>
                    </td>
                    <td>Yes (7%, EOM)
                    </td>
                </tr>
                <tr>
                    <td><strong>GL Head Id</strong>
                    </td>
                    <td>10400 (Account Payables)
                    </td>
                </tr>

            </table>
        </div>
        <div class="tab-pane  fade" id="transaction-summary">
            <table class="table table-bordered table-hover">
                <tr>
                    <td style="width: 300px;">
                        <strong>Total Due Amount</strong>
                    </td>
                    <td>Rs. 394884
                    </td>
                </tr>
                <tr>
                    <td><strong>Accrued Interest</strong>
                    </td>
                    <td>Rs. 545
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>Last Payment Date</strong>
                    </td>
                    <td>6 months ago (1-25-2014)
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>Maximum Credit Period</strong>
                    </td>
                    <td>7 days
                    </td>
                </tr>
                <tr>
                    <td><strong>Total Transaction</strong>
                    </td>
                    <td>Rs. 30049
                    </td>
                </tr>
            </table>
        </div>
        <div class="tab-pane  fade" id="addresses-and-contact-info">
            <h4>Address</h4>

            <table class="table table-bordered table-hover">
                <tr>
                    <td style="width: 300px;">
                        <strong>Address
                        </strong>
                    </td>
                    <td>
                        <address>
                            <strong>Twitter, Inc.</strong><br>
                            795 Folsom Ave, Suite 600<br>
                            San Francisco, CA 94107<br>
                            <abbr title="Phone">P:</abbr>
                            (123) 456-7890
                        </address>
                    </td>
                </tr>
                <tr>
                    <td><strong>Shipping Addresses</strong>
                    </td>

                    <td>

                        <address>
                            <strong>Twitter, Inc.</strong><br>
                            795 Folsom Ave, Suite 600<br>
                            San Francisco, CA 94107<br>
                            <abbr title="Phone">P:</abbr>
                            (123) 456-7890
                        </address>
                        <hr />
                        <address>
                            <strong>Twitter, Inc.</strong><br>
                            795 Folsom Ave, Suite 600<br>
                            San Francisco, CA 94107<br>
                            <abbr title="Phone">P:</abbr>
                            (123) 456-7890
                        </address>
                        <hr />

                        <address>
                            <strong>Twitter, Inc.</strong><br>
                            795 Folsom Ave, Suite 600<br>
                            San Francisco, CA 94107<br>
                            <abbr title="Phone">P:</abbr>
                            (123) 456-7890
                        </address>

                    </td>
                </tr>
            </table>
        </div>
    </div>