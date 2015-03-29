#MixERP User Documentation

MixERP aims on being an efficient and cost-effective ERP solution. Before you proceed to the following chapters, you may want to take a quick note on the following topics:

##MixERP Is Desiged for Multi-Establishment

MixERP is a multi-establishment ERP software which facilitates you to run **any number of office** in a **centralized environment**. This means that if you have multiple branch offices of the same establishment, you **should not create** 
multiple companies (and databases) and host separate application for each branch. 
You **should**, instead, create a hierarchical office structure as shown in the image.

![Establishment](images/establishment.png)

<span class="secondary label">* transaction units.</span>

###Remember
* Chart of Accounts is establishment-neutral. The Accounts (GL Heads) are common to all offices.
* Parties (Customers or Suppliers) are also establishment-neutral and common to all offices.
* A GL Head can have different closing balance on different offices.
* A GL Head balance when viewed on a parent office means the sum of all child offices.
* A party has a payable or receivable balance according to office.
  A party balance when viewed on a parent office means the sum of all child offices.
* A stock item has an inventory balance according to office.
  A stock item balance when viewed on parent office means the sum of all child offices.
* You cannot post transactions to an office group (an office which has child offices).
* You can create General Ledgers (Accounts), Stock Items, Parties when logged in to any office. Remember these entities are global--shared by all offices. 



##Core Concepts
- [Date Expressions](core-concepts/date-expressions.md)
- [Understanding MixERP Menu](core-concepts/understanding-menu.md)
- [Understanding and Configuring Taxes](core-concepts/understanding-and-configuring-taxes.md)
- [Working with ScrudFactory](core-concepts/scrud-factory.md)
- [Flags](core-concepts/flags.md)
- [Transaction Posting Engine](core-concepts/transaction-posting-engine.md)
- [Voucher Verification](core-concepts/voucher-verification.md)
- [Policy Engine](core-concepts/policy-engine.md)
- [End of Day Operation](core-concepts/eod-operation.md)
- [Widgets](core-concepts/widgets.md)

## Introduction
 - [Account Management](account-management.md)
  - [User Profile Management](profile-management.md)
  - [Change Password](change-password.md)

##Modules
- [Sales](sales/index.md)
- [Purchase](purchase/index.md)
- [Inventory](inventory/index.md)
- [Finance](finance/index.md)
- [Production](production/index.md)
- [Customer Relationship Management](crm/index.md)
- [Back Office](back-office/index.md)
- [Point of Sales](point-of-sales/index.md)

##Related Topics
* [MixERP Documentation](../index.md)
* [Developer Documentation](../developer/index.md)