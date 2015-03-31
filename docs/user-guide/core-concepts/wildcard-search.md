#Wildcard Search

If you are presented with a search form, the wildcard search is enabled for all text fields. The wildcard search enables you
to perform a pattern matching along with plain old text based searches.

| Existing value     | Pattern    | Matches |
|--------------------| -----------|---------|
| A quick brown fox. | fox        | No |
| A quick brown fox. | fox%       | No |
| A quick brown fox. | %fox       | Yes |
| A quick brown fox. | brown      | No |
| A quick brown fox. | %brown     | Yes |
| abc                | abc        | Yes |
| abc                | _b_        | Yes |
| abc                | %c         | Yes |
| abc                | a%         | Yes |


<div class="alert-box scrud radius">
    Please note that MixERP appends the character "%" on the start and the end of the search term.
</div>

##Related Topics
* [ScrudFactory](scrud-factory.md)