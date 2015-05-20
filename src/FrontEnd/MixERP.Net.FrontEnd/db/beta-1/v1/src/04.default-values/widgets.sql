--This table should not be localized.
INSERT INTO core.widgets(widget_name, widget_source, row_number, column_number)
SELECT 'SalesByGeographyWidget',                    '/Modules/Sales/Widgets/SalesByGeographyWidget.ascx',                   1, 1 UNION ALL
SELECT 'SalesByOfficeWidget',                       '/Modules/Sales/Widgets/SalesByOfficeWidget.ascx',                      2, 1 UNION ALL
SELECT 'CurrentOfficeSalesByMonthWidget',           '/Modules/Sales/Widgets/CurrentOfficeSalesByMonthWidget.ascx',          2, 2 UNION ALL
SELECT 'OfficeInformationWidget',                   '/Modules/BackOffice/Widgets/OfficeInformationWidget.ascx',             3, 1 UNION ALL
SELECT 'LinksWidget',                               '/Modules/BackOffice/Widgets/LinksWidget.ascx',                         3, 2 UNION ALL
SELECT 'WorkflowWidget',                            '/Modules/Finance/Widgets/WorkflowWidget.ascx',                         3, 4 UNION ALL
SELECT 'TopSellingProductOfAllTimeWidget',          '/Modules/Sales/Widgets/TopSellingProductOfAllTimeWidget.ascx',         4, 1 UNION ALL
SELECT 'TopSellingProductOfAllTimeCurrentWidget',   '/Modules/Sales/Widgets/TopSellingProductOfAllTimeCurrentWidget.ascx',  4, 2;