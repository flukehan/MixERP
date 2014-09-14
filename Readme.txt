Hello there,

MixERP is a feature rich, easy to use, open source ERP solution which is in very alpha stage.

-------------------------------------------------------------------------------------------------------------------------------------------------
 Development Environment Setup
-------------------------------------------------------------------------------------------------------------------------------------------------
Before you run the solution in your IDE, go through this video tutorial on vimeo:

https://vimeo.com/102617800


-------------------------------------------------------------------------------------------------------------------------------------------------
 Installing the database:
-------------------------------------------------------------------------------------------------------------------------------------------------

MixERP uses the powerful, award-winning, highly stable, and objected oriented database PostgreSQL. PostgreSQL server is an open source 
database server, which can be downloaded from: 

http://postgresql.org


Please run the bundled script found here:

"/FrontEnd/MixERP.Net.FrontEnd/db/"


**Please Note:**
If you are using PostgreSQL 9.2 and below, you would have to make a very minor change to 
the SQL script "customer-sample.sql":

1. Find the word 'MATERIALIZED VIEW' and replace that with 'VIEW'.
2. Comment out all the lines starting with 'REFRESH MATERIALIZED VIEW'.


-------------------------------------------------------------------------------------------------------------------------------------------------
Non English Speaking Countries:
-------------------------------------------------------------------------------------------------------------------------------------------------
MixERP is a multilingual product by design. Instead of hardcoding everying, we maintain a central resource file respository 
on the directory "MixERP.Net.FrontEnd/App_GlobalResources".

Please find the following files:

Titles.resx
-Titles and only titles should be stored in this file, complying to the rules of capitalization.
-Resource keys: ProperCase.

Questions.resx
-Questions are stored in this file.
-Resource keys: ProperCase.

Labels.resx
-Field labels are stored here. Must be a complete sentence or meaningful phrase.
-Resource keys: ProperCase.

Warnings.resx
-Application warnings are stored here. Must be a complete sentence or meaningful phrase.
-Resource keys: use ProperCasing.

Setup.resx
-System resource.
-Resource keys: ProperCase.


ScrudResource.resx
-PostgreSQL columns are stored as resource keys. These are used on dynamically generated forms and reports. 
-Resource keys: lowercase_with_underscore_separator.

-------------------------------------------------------------------------------------------------------------------------------------------------


Watch out for more ...

MixERP team.