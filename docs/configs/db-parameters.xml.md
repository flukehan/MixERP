#DbParameters.xml

This configuration file is related to the module [ScrudFactory](../user-guide/core-concepts/scrud-factory.md).
ScrudFactory uses this file to populate [DisplayFields](../../developer/scrud/display-fields.md).

```xml
<?xml version="1.0"?>
<configuration>
  <appSettings>
    <add key="?" value="?" />
  </appSettings>
</configuration>
```

##Example 1: Item Display Field

``<add key="ItemDisplayField" value="item_code + ' (' + item_name + ')'" />``

##Example 2: User Display Field

``<add key="UserDisplayField" value="user_name" />``


##Remember
DisplayField supports expression columns, as desbribed in MSDN

* [DataColumn.Expression Property](https://msdn.microsoft.com/en-us/library/system.data.datacolumn.expression%28v=vs.110%29.aspx)
* [Creating Expression Columns](https://msdn.microsoft.com/en-us/library/zwxk25bd%28v=vs.110%29.aspx)


##Related Topics
* [Web Configuration File](web.config.md)
* [Administrator Documentation](../admin.md)