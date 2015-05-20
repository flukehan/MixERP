#Verification Queue

Verification queue is a repository where transactions *awaiting* verification are stored. A verification queue can contain transactions
of [current date](current-date.md) or later. It should be noted that a past-dated transaction *cannot* be
entered into the system and if the queue has value dated transaction(s) prior to the current date, 
it usually means there is something wrong with your application and [you need help](http://mixerp.org/forum).

A future dated transaction is allowed and stored in the verification queue. However, you can 
[verify future dated transaction](transaction-posting-engine.md) in the future only, 
when the **current date** becomes the future **value date**.

##Related Topics

* [Current date](current-date.md)
* [EOD Operation](eod-operation.md)
* [Transaction Posting Engine](transaction-posting-engine.md)