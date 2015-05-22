#Translate MixERP

MixERP places the language files into the database, which means that you would have to first 
[set up your development environment](setting-up-your-development-environment.md).

Now, once you have the latest MixERP database, run the following query:

<pre>
SELECT * FROM localization.get_localization_table('fr');
</pre>

if you want to translate MixERP in French or improve the current translation. The result will be something like:

<table>
    <thead>
        <tr>
            <th>id</th>
            <th>resource_class</th>
            <th>key</th>
            <th>original</th>
            <th>translated</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>...</td>
            <td>...</td>
            <td>...</td>
            <td>...</td>
            <td>...</td>
        </tr>
        <tr>
            <td>5</td>
            <td>DbErrors</td>
            <td>P3007</td>
            <td>Invalid value date.</td>
            <td>Invalid date de valeur.</td>
        </tr>
        <tr>
            <td>...</td>
            <td>...</td>
            <td>...</td>
            <td>...</td>
            <td>...</td>
        </tr>
        <tr>
            <td>125</td>
            <td>Labels</td>
            <td>ThisFieldIsRequired</td>
            <td>This field is required.</td>
            <td>Ce champ est obligatoire.</td>
        </tr>
        <tr>
            <td>...</td>
            <td>...</td>
            <td>...</td>
            <td>...</td>
            <td>...</td>
        </tr>
    </tbody>
</table>


* [Click here on how to save the resultset to a file](https://duckduckgo.com/?q=pgadmin+query+yo+csv)
* Translate the column **translated** to your language.
* Discuss and send your translation to [community forums](http://mixerp.org/forum).


##Related Topics
* [Developer Documentation](index.md)
* [MixERP Documentation](../index.md)
* [MixERP User Guide](../user-guide/index.md)
