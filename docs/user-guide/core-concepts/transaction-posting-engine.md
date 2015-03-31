#Transaction Posting Engine

Transaction Posting Engine (TPE) is core function of MixERP. It is not a module, but a collection of:

* application modules
* database functions and triggers

MixERP TPE's job is to validate and verify that the transaction posted on any level is valid and free of side-effects. TPE generally
restricts activities such as:

* entering a transaction in a past date.
* maker trying to approve her own transaction.
* posting transactions that would produce negative cash balance.
* posting transactions that would make an inventory item count negative.

Transaction Posting Engine can be thought of a collection of functions, features, and workflow that enforces 
data integration and validity for **finance, inventory, and manufacturing areas** across the entire MixERP application tiers.


#Further Reading
* [Voucher Verification](voucher-verification.md)



##Related Topics
* [Sales Module](../sales/index.md)
* [MixERP User Guide](../index.md)

