#ScrudFactory

ScrudFactory is one of the various core modules of MixERP which facilitates rapid application development (RAD). ScrudFactory provides a common and consistent user interface, which helps you efficiently get you job done.

#GridView
The ScrudFactory grid displays information about an entity. In this example, [chart of accounts](../sales/setup/coa.md) is displayed. MixERP Scrud grid, by default, has the following default behavior:

* The rows are sorted by the key column.
* The rows are paged by the size of 10.
* The default grid page size of 10 is also known as a **compact view**.

![ScrudFactory Grid](images/scrud-grid.png)

##1. Buttons

<table>
	<thead>
		<tr>
			<th>Button</th>
			<th>Description</th>
		</tr>
	</thead>
	<tbody>
		<tr>
			<td>Show Compact</td>
			<td>Displays the compact view and resets the page to 1.</td>
		</tr>
		<tr>
			<td>Show All</td>
			<td>
				Displays an expanded view and resets the page to 1.
				<br/>
				<br/>
				**Warning**: If your entity is a huge table, MixERP will only
				display 1000 rows with paging.
			</td>
		</tr>
		<tr>
			<td>Add New</td>
			<td>Displays a form to add a new record.</td>
		</tr>
		<tr>
			<td>Edit Selected</td>
			<td>Displays a form with the selected record for editing. See selecting a record below.</td>
		</tr>
		<tr>
			<td>Delete Selected</td>
			<td>Deletes the selected record. See selecting a record below.</td>
		</tr>
		<tr>
			<td>Print</td>
			<td>Opens up a popup window with a printer-friendly view of the current grid.</td>
		</tr>
	</tbody>
</table>

![ScrudFactory Form](images/scrud-form.png)
![Scrud Item Selector](images/scrud-item-selector.png)


#Shortcuts