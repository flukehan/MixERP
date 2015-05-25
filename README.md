#What Is MixERP?

MixERP is an ASP.net open source ERP Solution.

#Beta 2 Version Development Started

We have already started developing the next version. Beta 2 with MVC 5/Razor 3 will support Linux and OSX on ASP.net vNext. 

#We Are Accepting Feature Requests for CRM Module
Tell us what you like to see in the CRM Module! Not sure? Then what about reviewing the upcoming features of the CRM module? This is how it looks like:

[https://github.com/mixerp/mixerp/wiki/crm-requirements](https://github.com/mixerp/mixerp/wiki/crm-requirements)

##Demo Sites Updated

* [chamlang.mixerp.org](http://chamlang.mixerp.org)
* [nemjung.mixerp.org](http://nemjung.mixerp.org) 

##Beta 1 Version 2 (RC) is Coming Soon

Please stay tuned to updates and releases because we are planning to release the second version of the first Beta release with added features and improved functions within May end 2015. Case you find any issues with the project , please feel free to post on [MixERP forums](http://mixerp.org/forum). Let us know how we can help you.

[Beta 1 Version 2 (RC) Milestone](https://github.com/mixerp/mixerp/milestones/Beta%201%20Version%202%20%28RC%29)

##Upgrades and Compatibility

All transactions posted to the **v1** is supported and will be automatically updated to the v2 release, which means that installing a new release will automatically update your current installation of MixERP without any loss of data due to an update. This applies to every new release of MixERP henceforth.

#Where Can I Get Support?
Please create an account in [MixERP Forums](http://mixerp.org/forum) and post your questions there. We will be happy to help.

If you can afford to, please consider [making a donation](http://www.mixerp.org/donate) for continued development of MixERP project.

<a href="http://www.mixerp.org/donate"><img src="https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif"/></a>


#Where Is Documentation?
The documentation now lives in the "gh-pages" branch and is hosted on GitHub pages here:

[MixERP Documentation](http://docs.mixerp.org)

Similarly, the branch "gh-pages" is periodically merged into "master" branch. **Using firefox**, you can view the documentation locally by opening "index-ajax.html" on the root folder.

##Why Betas?

We will have many small, stable, and incremental releases to reach our milestone. This means that once we start Beta 2 version, Beta 1 should be considered stable even though we like to call it Beta.

##Why PostgreSQL Server?

We have been receiving tons of queries on why PostgreSQL? We chose PostgreSQL Server because:

* PostgreSQL is platform independent.
* We have plans to support Apache and nginx.
* We have plans to support *nix based operating system.
* PostgreSQL is free no matter how big your data grows, unlimited processors, unlimited cores, unlimited memory. You will never be forced to upgrade to higher version due to a limitation of database size or similar.
* PostgreSQL is an [excellent choice for enterprise application](http://www.computerweekly.com/feature/Hot-skills-PostgreSQL).
* PostgreSQL does have [paid support plans](http://www.infoworld.com/article/2617783/open-source-software/the-stealth-success-of-postgresql.html) for enterprises.
* PostgreSQL is [ahead of commercial database](http://www.infoworld.com/article/2608863/nosql/postgresql-ramps-up-its-nosql-game.html) vendors in terms of development. Also see [this](https://wiki.postgresql.org/wiki/What%27s_new_in_PostgreSQL_9.0), [this](https://wiki.postgresql.org/wiki/What%27s_new_in_PostgreSQL_9.1), [this](https://wiki.postgresql.org/wiki/What%27s_new_in_PostgreSQL_9.2), [this](https://wiki.postgresql.org/wiki/What%27s_new_in_PostgreSQL_9.3), and [this](https://wiki.postgresql.org/wiki/What%27s_new_in_PostgreSQL_9.4). And [this](http://www.postgresql.org/docs/9.4/static/release-9-4-1.html) as well.
* We have been SQL Server developers long before we even knew PostgreSQL Server existed. We love SQL Server, but PostgreSQL is a better fit for open source software.

![MixERP Dashboard](http://mixerp.org/images/features/mixerp-dashboard.png)

##You Dont Have to Bang Your Head to Learn or Implement MixERP

The first thing that we ever discussed when starting this project was simplicity. Designed from scratch, MixERP integrates most of the useful functionalities of an average ERP solution with extra emphasis on simplification of its modules. Switching to MixERP from your previous ERP solution will not be a nightmare unlike in most cases with other ERP Solutions.

##MixERP Disallows Side Effecting Functionality

Unlike other ERP solutions, MixERP restricts some side effecting functionality. For example, modification of past dated transactions is not allowed. This ensures that you cannot have two different balance sheets of the same date because of the modifications made. 

###MixERP Is a Pure
* multi-currency,
* multi-lingual, 
* and multi-establishment ERP Solution.

##MixERP Is Rich in Design

MixERP has a very wide range of features which are tightly integrated with each others. MixERP has a unique set of features that you will not find even in commercial ERP solution. Unlike other ERP solutions, MixERP simplifies business processes via automated financial tools such as:

* Central Database for Multiple Establishments.
* With a single instance of MixERP installation, you can operate multiple Office Groups and Offices under the hood. MixERP can consolidate and/or split reports on the fly--without needing a hacky lengthy procedure. 
* Policy engine for G/L access.
* Integrated workflow engine for transaction posting and verification.
* Automatic decisions by the system based upon your configuration.
* End of day (EOD) operation.


#Introduction
 - [Features](http://docs.mixerp.org/documentation/features)
 - [User Interface](http://docs.mixerp.org/documentation/user-interface)
 - [Account Management](http://docs.mixerp.org/documentation/account-management)
  - [User Profile Management](http://docs.mixerp.org/documentation/profile-management)
  - [Change Password](http://docs.mixerp.org/documentation/change-password)

##Core Concepts
- [Date Expressions](http://docs.mixerp.org/documentation/date-expressions)
- [Understanding MixERP Menu](http://docs.mixerp.org/documentation/understanding-menu)
- [Understanding and Configuring Taxes](http://docs.mixerp.org/documentation/understanding-and-configuring-taxes)
- [Flags](http://docs.mixerp.org/documentation/flags)
- [Transaction Governor](http://docs.mixerp.org/documentation/transaction-governor)
 - [Transaction Posting Engine](http://docs.mixerp.org/documentation/transaction-posting-engine)
 - [Policy Engine](http://docs.mixerp.org/documentation/policy-engine)
 - [Day Operations](http://docs.mixerp.org/documentation/day-operations)
    - [End of Day Operations](http://docs.mixerp.org/documentation/eod-operations)
- [Widgets](http://docs.mixerp.org/documentation/widgets)

##Modules
- [Sales](http://docs.mixerp.org/documentation/sales)
- [Purchase](http://docs.mixerp.org/documentation/purchase)
- [Inventory](http://docs.mixerp.org/documentation/inventory)
- [Finance](http://docs.mixerp.org/documentation/finance)
- [Production](http://docs.mixerp.org/documentation/production)
- [Customer Relationship Management](http://docs.mixerp.org/documentation/crm)
- [Back Office](http://docs.mixerp.org/documentation/back-office)
- [Point of Sales](http://docs.mixerp.org/documentation/point-of-sales)

##Related Topics
* [MixERP Documentation](http://docs.mixerp.org)
* [Technical Documentation](http://docs.mixerp.org/documentation/technical-documentation)
* <a href="http://demo.mixerp.org" target="_blank">MixERP Demo Website</a>
* [Contribution Guidelines](http://docs.mixerp.org/documentation/contribution-guidelines)
* <a href="http://mixerp.org/" target="_blank">Project Website</a>
* <a href="http://facebook.com/mixoferp/" target="_blank">Follow MixERP on Facebook</a>
* <a href="http://www.facebook.com/groups/183076085203506/" target="_blank">Facebook Discussions Group</a>
* <a href="http://mixerp.org/forum/" target="_blank">Community Forum</a>
* [Project Milestone](http://docs.mixerp.org/milestone)

##List of Supported Languages in Beta Version
* English
* Deutsch (Deutschland)
* español (España, alfabetización internacional)
* Filipino (Pilipinas)
* français (France)
* Bahasa Indonesia (Indonesia)
* 日本語 (日本)
* Bahasa Melayu (Malaysia)
* Nederlands (Nederland)
* português (Portugal)
* русский (Россия)
* svenska (Sverige)
* 中文(中华人民共和国)

##How Can I Support MixERP?

* Donate to MixERP project.
* Translate MixERP in your language.
* Support MixERP by providing ASP.net 4.5 and PostgreSQL hosting.
* Build and host MixERP on your development server.
* Join us by following this project.
* Report bugs and/or issues on github.
* Tell your friends about MixERP.

##Please Note
* MixERP comes with GNU-GPL Version 3 license.
* MixERP only supports PostgreSQL Server database and we do not have any plans to support other DBMS right now.