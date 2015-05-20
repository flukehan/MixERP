#Sample Data

In MixERP, we do not create a large SQL and maintain it. This process is buggy, error-prone, and difficult to manage.
You might not have heard about SQL bundling before, but we do just that.

#What is SQL bundling?
SQL bundling is nothing more than a process where you will:

* Create many individual sql files
* Organize them logically in a directory structure
* Work on one thing at a time, focus on everything later
* Bundle them all together to create a giant SQL file

#How Does This Even Work?
Believe me, this works extremely well! I borrowed this concept from bundling and minifying stylesheets and javascripts, where you create a single
file to serve to the browser. But only the concept of creating one big file is borrowed, nothing else. So, in the bundling process,
we have:

* an organized directory structure where we store files
* SQL (*.sql) files
* Sample (*.sql.sample) files
* Optional (*.optional) files

#Wait, sqlbundle comes first!

If you did notice, we have a special tiny file **mixerp.sqlbundle** in the directory

``src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v1/``

and also

``src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v2/``

This file is YAML file

````markup
- default-language : 
- script-directory : db/beta-1/v1/src
- output-directory:db/beta-1/v1/
````
This file is used as a configuration file by the utility **MixERP.Net.Utility.SqlBundler**, which will create a bundled SQL file
for us.

##Syntax
**MixERP.Net.Utility.SqlBundler.exe [path/to/root] [path/to/sqlbundle-file] [include-optional=false] [include-sample=false]**

#SQL (*.sql) Files
Each of the .sql will be bundled to create a large sql-bundle file, which is nothing more than a plain sql file.
The .sql files have the primary priority to get included on a budled file.

#Sample (*.sql.sample) Files
These files will only be included when the flag **[include-sample]** is set to **true**. We use this flag for development purpose
to create a new sample file by uinsg it along with pg_dump to include us some data. This has the secondary priority to get included
on the bundled file. This file will not be included on releases.

#Optional (*.optional) Files
These files will only be included when the flag **[include-optional]** is set to **true**. An optional file would have a lower
priority. Please serch for existing *.optional file(s) to see how exactly we use this.

#Further Information

The following files would be useful for database developers to review:

* /src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v1/rebundle-db-with-sample.bat
* /src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v1/rebundle-db-without-sample.bat
* /src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v1/rebundle-and-rerun-with-sample.bat
* /src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v1/rebundle-and-rerun-without-sample.bat
* /src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v1/rebundle-and-rerun-with-sample-with-vacuum-analyze.bat
* /src/FrontEnd/MixERP.Net.FrontEnd/db/beta-1/v1/rebundle-and-rerun-without-sample-with-vacuum-analyze.bat


##Related Topics
* [Developer Documentation](../developer/index.md).
