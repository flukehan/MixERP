--This translation is originally a courtesy of Johann Schwarz
--https://github.com/Johann-Schwarz
SELECT localization.add_localized_resource('CommonResource', 'de', 'DateMustBeGreaterThan', 'Ungültiges Datum. Muss größer sein als "{0}".');--Invalid date. Must be greater than "{0}".
SELECT localization.add_localized_resource('CommonResource', 'de', 'DateMustBeLessThan', 'Ungültiges Datum. Muss kleiner sein als "{0}".');--Invalid date. Must be less than "{0}".
SELECT localization.add_localized_resource('CommonResource', 'de', 'InvalidDate', 'Ungültiges Datum.');--Invalid date.
SELECT localization.add_localized_resource('CommonResource', 'de', 'NoRecordFound', 'Sorry,Eintrag nicht gefunden.');--Sorry, no record found.
SELECT localization.add_localized_resource('CommonResource', 'de', 'RequiredField', 'Dies ist ein Pflichtfeld.');--This is a required field.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P1301', 'Zinsberechnung fehlgeschlagen. Die Anzahl der Tage im Jahr fehlen.');--Cannot calculate interest. The number of days in a year was not provided.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P1302', 'Kann Umsatz nicht buchen. Ungültige Kassakonto-ZUordnung zu diesem Geschäft');--Cannot post sales. Invalid cash account mapping on store.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P3000', 'Ungültige Daten.');--Invalid data.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P3001', 'Ungültiger Benutzername.');--Invalid user name.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P3005', 'Das Passwort darf nicht leer sein.');--Password cannot be empty.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P3006', 'Bitte geben Sie ein neues Kennwort ein.');--Please provide a new password.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P3007', 'Verbuchungsdatum (Valuta) ungültig');--Invalid value date.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P3008', 'Ungültiges Datum.');--Invalid date.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P3009', 'Falscher Zeitraum angegeben');--Invalid period specified.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P3010', 'Ungültige Office-ID.');--Invalid office id.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P3011', 'Ungültige Office.');--Invalid office.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P3012', 'Ungültige Laden.');--Invalid store.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P3013', 'Ungültiges Barwerte-Depot.');--Invalid cash repository.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P3050', 'Ungültige Partei.');--Invalid party.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P3051', 'Üngültiger Artikel.');--Invalid item.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P3052', 'Ungültige Einheit.');--Invalid unit.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P3053', 'Ungültige oder inkompatibel Einheit.');--Invalid or incompatible unit.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P3054', 'Die angegebene Einheit ist mit der Basiseinheit nicht kompatibel.');--The reorder unit is incompatible with the base unit.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P3055', 'Ungültiger Wechselkurs.');--Invalid exchange rate.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P3101', 'Ungültige Login-Id.');--Invalid LoginId.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P3105', 'Ihr aktuelles Passwort ist nicht korrekt.');--Your current password is incorrect.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P3201', 'Masseinheit passt nicht zum Artikel');--Item/unit mismatch.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P3202', 'Falsches Steuerformular');--Tax form mismatch.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P3301', 'Ungültige Menge.');--Invalid quantity.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P3302', 'Ungültige Transaktions-ID.');--Invalid transaction id.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P3501', 'Die Spalte Konto Id kann nicht null sein.');--The column account_id cannot be null.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P4010', 'Wechselkurs zwischen den Währungen, wurde nicht gefunden.');--Exchange rate between the currencies was not found.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P4020', 'Dieser Artikel ist dieser Transaktion nicht zugeordnet.');--This item is not associated with this transaction.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P4030', 'Keine Verifizierungs Richtlinie für diesen User vorhanden, ');--No verification policy found for this user.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P4031', 'Bitte fragen Sie eine andere Person, um die Buchung zu überprüfen.');--Please ask someone else to verify your transaction.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P5000', 'Die aufeinander verweisenden Seiten sind nicht gleich.');--Referencing sides are not equal.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P5001', 'Negative Lagerstände sind nicht erlaubt.');--Negative stock is not allowed.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P5002', 'Die Buchung hätte einen negativen Kassastand zur Folge');--Posting this transaction would produce a negative cash balance.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P5010', 'Zurückdatierte Buchungen sind nicht gestattet.');--Past dated transactions are not allowed.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P5100', 'Diese Konfiguration erlaubt keine Transaktions Buchung.');--This establishment does not allow transaction posting.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P5101', 'Im eingeschränkten Transaktionsmodus sind Transaktionsbuchungen unzulässig.');--Cannot post transaction during restricted transaction mode.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P5102', 'Der Tagesabschluss wurde bereits durchgeführt.');--End of day operation was already performed.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P5103', 'Zurückdatierte Transaktionen in der Verifizierungs- Warteschlange.');--Past dated transactions in verification queue.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P5104', 'Bitte Transaktionen prüfen, bevor der Tagesabschluss durchgeführt wird.');--Please verify transactions before performing end of day operation.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P5110', 'Sie können keine Umsatzsteuer Informationen für nicht steuerpflichtige Umsätze angeben.');--You cannot provide sales tax information for non taxable sales.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P5111', 'Bank-Transaktionsinformationen ungültig');--Invalid bank transaction information provided.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P5112', 'Ungültige Kreditkarteninformationen.');--Invalid payment card information.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P5113', 'Konto zur Verbuchung der Hanelsgebühren nicht gefunden');--Could not find an account to post merchant fee expenses.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P5201', 'Eine Lager Korrektur Buchung kann keine Soll Position (en) enthalten.');--A stock adjustment entry can not contain debit item(s).
SELECT localization.add_localized_resource('DbErrors', 'de', 'P5202', 'Ein Artikel kann nur einmal in einem Geschäft aufscheinen.');--An item can appear only once in a store.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P5203', 'Der zurückgegebene Menge kann nicht größer sein als die tatsächliche Menge.');--The returned quantity cannot be greater than actual quantity.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P5204', 'Der zurückgegebene Betrag kann nicht größersein als der tatsächliche Betrag.');--The returned amount cannot be greater than actual amount.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P5301', 'Ungültige oder zurückgewiesene Transaktion.');--Invalid or rejected transaction.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P5500', 'Unzureichende Artikelmenge.');--Insufficient item quantity.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P5800', 'Löschen einer Transaktion ist nicht erlaubt. Markieren Sie die Transaktion als verworfen');--Deleting a transaction is not allowed. Mark the transaction as rejected instead.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P5901', 'Bitte fragen Sie jemanden, ob die gebuchte Transaktion korrekt ist.');--Please ask someone else to verify the transaction you posted.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P5910', 'Die Möglichkeiten zur automatischen Überprüfungs sind erschöpft. Die Transaktion wurde nicht überprüft.');--Self verification limit exceeded. The transaction was not verified.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P5911', 'Die Möglichkeiten zur Prüfung der Umsätze wurden überschritten. Die Transaktion wurde nicht überprüft.');--Sales verification limit exceeded. The transaction was not verified.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P5912', 'Die Möglichkeiten zur Prüfung der Einkäufe wurde überschritten. Die Transaktion wurde nicht überprüft.');--Purchase verification limit exceeded. The transaction was not verified.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P5913', 'Hauptbuch Überprüfungsgrenze  überschritten. Die Transaktion wurde nicht überprüft.');--GL verification limit exceeded. The transaction was not verified.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P6010', 'Ungültige Konfiguration bei der Berechnung der  Kosten der verkauften Güter');--Invalid configuration: COGS method.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P8001', 'Gewinn und Verlustrechnungen von  Geschäftsstelle (n) mit unterschiedlichen Basiswährungen können nicht erstellt werden.');--Cannot produce P/L statement of office(s) having different base currencies.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P8002', 'Rohbilanzen von Geschäftsstellen mit unterschiedlichen Basiswährungen können nicht produziert werden.');--Cannot produce trial balance of office(s) having different base currencies.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P8003', 'Sie können nicht verschiedene Währungen im Sachkonto buchen.');--You cannot have a different currency on the mapped GL account.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P8101', 'Der Tagesabschluss wurde bereits gestartet');--EOD operation was already initialized.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P8501', 'Nur eine Spalte erforderlich.');--Only one column is required.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P8502', 'Die Spalte kann nicht aktualisiert werden');--Cannot update column.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P8990', 'Sie sind nicht berechtigt, Systemkonten zu ändern.');--You are not allowed to change system accounts.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P8991', 'Sie sind nicht berechtigt, Systemkonten hinzuzufügen.');--You are not allowed to add system accounts.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P8992', 'Ein sys User hat  kein Passwort.');--A sys user cannot have a password.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P9001', 'Zugriff  verweigert.');--Access is denied.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P9010', 'Zugriff wird verweigert. Sie sind nicht berechtigt, diese Transaktion durchzuführen.');--Access is denied. You are not authorized to post this transaction.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P9011', 'Zugriff wird verweigert. Sie haben ungültige Werte eingegeben.');--Access is denied. Invalid values supplied.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P9012', 'Zugriff verweigert! Eine Lager Korrektur Transaktion kann nicht Verweise auf mehrere Niederlassungen haben.');--Access is denied! A stock adjustment transaction cannot references multiple branches.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P9013', 'Zugriff verweigert! Eine Lager Journal Transaktion kann nicht Verweise auf mehrere Niederlassungen haben.');--Access is denied! A stock journal transaction cannot references multiple branches.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P9014', 'Zugriff wird verweigert. Sie können  eine Transaktion eines anderen Offices nicht überprüfen.');--Access is denied. You cannot verify a transaction of another office.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P9015', 'Zugriff wird verweigert. Sie können Rück oder Vordatierte Transaktionen nicht verifizieren.');--Access is denied. You cannot verify past or futuer dated transaction.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P9016', 'Zugriff wird verweigert. Sie haben kein Recht, die Transaktion zu überprüfen.');--Access is denied. You don''t have the right to verify the transaction.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P9017', 'Zugriff wird verweigert. Sie  haben kein Recht, die Transaktion zu widerrufen.');--Access is denied. You don''t have the right to withdraw the transaction.
SELECT localization.add_localized_resource('DbErrors', 'de', 'P9201', 'Zugriff verweigert. Sie können die "Transaktions Details" Tabelle nicht aktualisieren.');--Acess is denied. You cannot update the "transaction_details" table.
SELECT localization.add_localized_resource('DbResource', 'de', 'actions', 'Aktionen');--Actions
SELECT localization.add_localized_resource('DbResource', 'de', 'amount', 'Betrag');--Amount
SELECT localization.add_localized_resource('DbResource', 'de', 'currency', 'Währung');--Currency
SELECT localization.add_localized_resource('DbResource', 'de', 'flag_background_color', 'Marke Hintergrundfarbe');--Flag Background Color
SELECT localization.add_localized_resource('DbResource', 'de', 'flag_foreground_color', 'Marke Vordergrundfarbe');--Flag Foreground Color
SELECT localization.add_localized_resource('DbResource', 'de', 'id', 'ID');--ID
SELECT localization.add_localized_resource('DbResource', 'de', 'office', 'Office');--Office
SELECT localization.add_localized_resource('DbResource', 'de', 'party', 'Partei');--Party
SELECT localization.add_localized_resource('DbResource', 'de', 'reference_number', 'Referenznummer');--Reference Number
SELECT localization.add_localized_resource('DbResource', 'de', 'statement_reference', 'Beschreibung');--Statement Reference
SELECT localization.add_localized_resource('DbResource', 'de', 'transaction_ts', 'Transaktionszeitstempel');--Transaction Timestamp
SELECT localization.add_localized_resource('DbResource', 'de', 'user', 'Benutzer');--User
SELECT localization.add_localized_resource('DbResource', 'de', 'value_date', 'Buchungstag');--Value Date
SELECT localization.add_localized_resource('Errors', 'de', 'BothSidesCannotHaveValue', 'Soll und Haben Felder  können nicht beide Werte enthalten. ');--Both debit and credit cannot have values.
SELECT localization.add_localized_resource('Errors', 'de', 'CompoundUnitOfMeasureErrorMessage', 'Die Basiseinheit und Vergleichseinheit dürfen nicht identisch sein.');--Base unit id and compare unit id cannot be same.
SELECT localization.add_localized_resource('Errors', 'de', 'InsufficientStockWarning', 'Nur {0} {1} von {2} auf Lager.');--Only {0} {1} of {2} left in stock.
SELECT localization.add_localized_resource('Errors', 'de', 'InvalidSubTranBookPurchaseDelivery', 'Tochtergesellschaftsbuchung ungültig :  "Einkaufs Lieferung"');--Invalid SubTranBook 'Purchase Delivery'.
SELECT localization.add_localized_resource('Errors', 'de', 'InvalidSubTranBookPurchaseQuotation', 'Tochtergesellschaftsbuchung ungültig : "Einkaufs Kostenvoranschlag"');--Invalid SubTranBook 'Purchase Quotation'.
SELECT localization.add_localized_resource('Errors', 'de', 'InvalidSubTranBookPurchaseReceipt', 'Tochtergesellschaftsbuchung ungültig : "Kaufbeleg"');--Invalid SubTranBook 'Purchase Receipt'.
SELECT localization.add_localized_resource('Errors', 'de', 'InvalidSubTranBookSalesPayment', 'Tochtergesellschaftsbuchung ungültig : "Zahlungseingänge"');--Invalid SubTranBook 'Sales Payment'.
SELECT localization.add_localized_resource('Errors', 'de', 'InvalidUserId', 'Ungültige Benutzer-ID.');--Invalid user id.
SELECT localization.add_localized_resource('Errors', 'de', 'KeyValueMismatch', 'Die Anzahl der Schlüssel und Werte Elemente in dieser Liste stimmt nicht überein.');--There is a mismatching count of key/value items in this ListControl.
SELECT localization.add_localized_resource('Errors', 'de', 'NoTransactionToPost', 'Keine Transaktion zu buchen.');--No transaction to post.
SELECT localization.add_localized_resource('Errors', 'de', 'ReferencingSidesNotEqual', 'Die referenzierten Seiten sind nicht gleich.');--The referencing sides are not equal.
SELECT localization.add_localized_resource('Labels', 'de', 'AllFieldsRequired', 'Alle Felder sind erforderlich.');--All fields are required.
SELECT localization.add_localized_resource('Labels', 'de', 'CannotWithdrawNotValidGLTransaction', 'Kann die Transaktion nicht löschen. Dies ist eine ungültige Hauptbuch-Transaktion.');--Cannot withdraw transaction. This is a not a valid GL transaction.
SELECT localization.add_localized_resource('Labels', 'de', 'CannotWithdrawTransaction', 'Kann dieTransaktion nicht löschen.');--Cannot withdraw transaction.
SELECT localization.add_localized_resource('Labels', 'de', 'ClickHereToDownload', 'Klicken Sie hier zum Download.');--Click here to download.
SELECT localization.add_localized_resource('Labels', 'de', 'ConfirmedPasswordDoesNotMatch', 'Das bestätigte Kennwort stimmt nicht überein.');--The confirmed password does not match.
SELECT localization.add_localized_resource('Labels', 'de', 'DatabaseBackupSuccessful', 'Das Datenbank-Backup war erfolgreich.');--The database backup was successful.
SELECT localization.add_localized_resource('Labels', 'de', 'DaysLowerCase', 'Tage');--days
SELECT localization.add_localized_resource('Labels', 'de', 'EODBegunSaveYourWork', 'Schließen Sie dieses Fenster und speichern Sie Ihre Arbeit, bevor Sie sich automatisch ausgeloggt werden.');--Please close this window and save your existing work before you will be signed off automatically.
SELECT localization.add_localized_resource('Labels', 'de', 'EmailBody', '<h2> Hallo, </ h2> <p> Anbei finden Sie das beigefügte Dokument. </ p> <p> Danke. <br /> MixERP </ p>');--<h2>Hi,</h2><p>Please find the attached document.</p><p>Thank you.<br />MixERP</p>
SELECT localization.add_localized_resource('Labels', 'de', 'EmailSentConfirmation', 'Eine E-Mail an {0} gesendet.');--An email was sent to {0}.
SELECT localization.add_localized_resource('Labels', 'de', 'FlagLabel', 'Sie können diese Transaktion markieren, aber Sie werden nicht in der Lage sein, die Markierungen anderer Benutzern zu sehen.');--You can mark this transaction with a flag, however you will not be able to see the flags created by other users.
SELECT localization.add_localized_resource('Labels', 'de', 'GoToChecklistWindow', 'Zum Fenster Checkliste.');--Go to checklist window.
SELECT localization.add_localized_resource('Labels', 'de', 'GoToTop', 'Nach oben');--Go to top.
SELECT localization.add_localized_resource('Labels', 'de', 'JustAMomentPlease', 'Einen Augenblick bitte!');--Just a moment, please!
SELECT localization.add_localized_resource('Labels', 'de', 'NumRowsAffected', '{0} Zeilen betroffen.');--{0} rows affected.
SELECT localization.add_localized_resource('Labels', 'de', 'OpeningInventoryAlreadyEntered', 'Der Anfangsbestand für dieses Office ist bereits eingetragen.');--Opening inventory has already been entered for this office.
SELECT localization.add_localized_resource('Labels', 'de', 'PartyDescription', 'Parteien beziehen sich allgemein auf Lieferanten, Kunden, Agenten und Händler.');--Parties collectively refer to suppliers, customers, agents, and dealers.
SELECT localization.add_localized_resource('Labels', 'de', 'SelectAFlag', 'Wählen Sie eine Markierung');--Select a flag.
SELECT localization.add_localized_resource('Labels', 'de', 'TaskCompletedSuccessfully', 'Die Aufgabe wurde erfolgreich abgeschlossen.');--Task completed successfully.
SELECT localization.add_localized_resource('Labels', 'de', 'ThankYouForYourBusiness', 'Vielen Dank für Ihre Arbeit.');--Thank you for your business.
SELECT localization.add_localized_resource('Labels', 'de', 'ThisFieldIsRequired', 'Dieses Feld ist erforderlich.');--This field is required.
SELECT localization.add_localized_resource('Labels', 'de', 'TransactionApprovedDetails', 'Diese Transaktion wurde von {0} um {1} zugelassen.');--This transaction was approved by {0} on {1}.
SELECT localization.add_localized_resource('Labels', 'de', 'TransactionAutoApprovedDetails', 'Diese Transaktion wurde automatisch von {0} um {1} zugelassen.');--This transaction was automatically approved by {0} on {1}.
SELECT localization.add_localized_resource('Labels', 'de', 'TransactionAwaitingVerification', 'Diese Transaktion wartet auf die Bestätigung eines Administrators.');--This transaction is awaiting verification from an administrator.
SELECT localization.add_localized_resource('Labels', 'de', 'TransactionClosedDetails', 'Diese Transaktion wurde von {0} um {1} geschlossen. Grund: "{2}".');--This transaction was closed by {0} on {1}. Reason: "{2}".
SELECT localization.add_localized_resource('Labels', 'de', 'TransactionPostedSuccessfully', 'Die Transaktion wurde erfolgreich gebucht.');--The transaction was posted successfully.
SELECT localization.add_localized_resource('Labels', 'de', 'TransactionRejectedDetails', 'Diese Transaktion wurde von {0} um {1} abgelehnt. Grund: "{2}".');--This transaction was rejected by {0} on {1}. Reason: "{2}".
SELECT localization.add_localized_resource('Labels', 'de', 'TransactionWithdrawalInformation', 'Wenn Sie eine Transaktion zurückziehen, wird dies nicht an das Arbeitsablauf Modul weitergeleitet. Das bedeutet, dass Ihre zurückgezogenen Transaktionen verworfen sind und keiner weiteren Überprüfung bedürfen. Es ist jedoch nicht mehr möglich, das Zurückziehen dieser Transaktion zu einem späteren Zeitpunkt aufzuheben');--When you withdraw a transaction, it won't be forwarded to the workflow module. This means that your withdrawn transactions are rejected and require no further verification. However, you won't be able to unwithdraw this transaction later.
SELECT localization.add_localized_resource('Labels', 'de', 'TransactionWithdrawnDetails', 'Diese Transaktion wurde von {0} um {1} zurückgezogen. Grund: "{2}".');--This transaction was withdrawn by {0} on {1}. Reason: "{2}".
SELECT localization.add_localized_resource('Labels', 'de', 'TransactionWithdrawnMessage', 'Die Transaktion wurde erfolgreich zurückgezogen. Darüber hinaus wird diese Aktion alle nach dem "{0}" erstellten diesbezüglichen Berichte beeinflussen.');--The transaction was withdrawn successfully. Moreover, this action will affect the all the reports produced on and after "{0}".
SELECT localization.add_localized_resource('Labels', 'de', 'UserGreeting', 'Hallo {0}!');--Hi {0}!
SELECT localization.add_localized_resource('Labels', 'de', 'YourPasswordWasChanged', 'Ihr Kennwort wurde geändert-');--Your password was changed.
SELECT localization.add_localized_resource('Messages', 'de', 'AreYouSure', 'Sind Sie sicher ?');--Are you sure?
SELECT localization.add_localized_resource('Messages', 'de', 'CouldNotDetermineVirtualPathError', 'Der Pfad um ein Bild zu erstellen konnte nicht ermittelt werden.');--Could not determine virtual path to create an image.
SELECT localization.add_localized_resource('Messages', 'de', 'DuplicateFile', 'Duplikat Datei');--Duplicate File!
SELECT localization.add_localized_resource('Messages', 'de', 'EODDoNotCloseWindow', 'Bitte dieses Fenster nicht zu schliessen oder die Seite zu wechseln, während das Programm initialisiert wird');--Please do not close this window or navigate away from this page during initialization.
SELECT localization.add_localized_resource('Messages', 'de', 'EODElevatedPriviledgeCanLogIn', 'Während  der Tagesabschlusses durchgeführt wird sind nur Benutzer mit gehobenen Privilegien dazu berechtigt einzuloggen.');--During the day-end period, only users having elevated privilege are allowed to log-in.
SELECT localization.add_localized_resource('Messages', 'de', 'EODLogsOffUsers', 'Wenn Sie den Tagesabschluss starten werden allebereits eingeloggten Benutzer inklusive Sie für etwa 2 Minuten abgemeldet.');--When you initialize day-end operation, the already logged-in application users including you are logged off on 120 seconds.
SELECT localization.add_localized_resource('Messages', 'de', 'EODProcessIsIrreversible', 'Dieser Vorgang kann nicht rückgängig gemacht werden!');--This process is irreversible.
SELECT localization.add_localized_resource('Messages', 'de', 'EODRoutineTasks', 'Während des Tagesabschlusses werden Arbeiten wie Kalkulation von Zinsen, Kontenabrechnungen,  Abschlussrechnungen und Report Erstellung durchgeführt.');--During EOD operation, routine tasks such as interest calculation, settlements, and report generation are performed.
SELECT localization.add_localized_resource('Messages', 'de', 'EODTransactionPosting', 'Wenn Sie einen Tagesabschluß für ein bestimmtes Datum durchführen können keine Transaktionen für diesen oder einen früheren Tag geändert, getauscht oder gelöscht werden.');--When you perform EOD operation for a particular date, no transaction on that date or before can be altered, changed, or deleted.
SELECT localization.add_localized_resource('Messages', 'de', 'InvalidFile', 'Ungültige Datei!');--Invalid file!
SELECT localization.add_localized_resource('Messages', 'de', 'TempDirectoryNullError', 'Bilder können nicht erstellen, wenn kein "Temp"-Verzeichnis existiert..');--Cannot create an image when the temp directory is null.
SELECT localization.add_localized_resource('Messages', 'de', 'UploadFilesDeleted', 'Die hochgeladenen Dateien erfolgreich gelöscht.');--The uploaded files were successfully deleted.
SELECT localization.add_localized_resource('Questions', 'de', 'AreYouSure', 'Sind Sie sicher?');--Are you sure?
SELECT localization.add_localized_resource('Questions', 'de', 'CannotAccessAccount', 'Kein Zugriff auf Ihr Konto?');--Cannot access your account?
SELECT localization.add_localized_resource('Questions', 'de', 'ConfirmAnalyze', 'Dies wird den Zugriff auf die Klienten Datenbank während der Durchführung sperren. Sind Sie sicher, dass sie das Jetzt durchführen möchten?');--This will lock client database access during execution. Are you sure you want to execute this action right now?
SELECT localization.add_localized_resource('Questions', 'de', 'ConfirmVacuum', 'Dies wird den Zugriff auf die Klienten Datenbank während der Durchführung sperren. Sind Sie sicher, dass sie das Jetzt durchführen möchten?');--This will lock client database access during execution. Are you sure you want to execute this action right now?
SELECT localization.add_localized_resource('Questions', 'de', 'ConfirmVacuumFull', 'Dies wird den Zugriff auf die Klienten Datenbank während der Durchführung sperren. Sind Sie sicher, dass sie das Jetzt durchführen möchten?');--This will lock client database access during execution. Are you sure you want to execute this action right now?
SELECT localization.add_localized_resource('Questions', 'de', 'WhatIsYourHomeCurrency', 'Was ist Ihre Landeswährung?');--What Is Your Home Currency?
SELECT localization.add_localized_resource('Questions', 'de', 'WithdrawalReason', 'Warum möchten Sie diese Transaktion verwerfen?');--Why do you want to withdraw this transaction?
SELECT localization.add_localized_resource('ScrudResource', 'de', 'Select', 'Wähle');--Select
SELECT localization.add_localized_resource('ScrudResource', 'de', 'account', 'Konto');--Account
SELECT localization.add_localized_resource('ScrudResource', 'de', 'account_id', 'Konto ID');--Account Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'account_master', 'Kontenstamm');--Account Master
SELECT localization.add_localized_resource('ScrudResource', 'de', 'account_master_code', 'Kontenstamm Code');--Account Master Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'account_master_id', 'Kontenstamm ID');--Account Master Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'account_master_name', 'Kontenstamm Name');--Account Master Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'account_name', 'Kontoname');--Account Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'account_number', 'Kontonummer');--Account Number
SELECT localization.add_localized_resource('ScrudResource', 'de', 'address', 'Adresse');--Address
SELECT localization.add_localized_resource('ScrudResource', 'de', 'address_line_1', 'Adresszeile 1');--Address Line 1
SELECT localization.add_localized_resource('ScrudResource', 'de', 'address_line_2', 'Adresszeile 2');--Address Line 2
SELECT localization.add_localized_resource('ScrudResource', 'de', 'ageing_slab_id', 'Alterungs Tabelle ID');--Ageing Slab Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'ageing_slab_name', 'Alterungs Tabelle Name');--Ageing Slab Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'allow_credit', 'Kredit zulassen');--Allow Credit
SELECT localization.add_localized_resource('ScrudResource', 'de', 'allow_sales', 'Verkäufe zulassen');--Allow Sales
SELECT localization.add_localized_resource('ScrudResource', 'de', 'allow_transaction_posting', 'Transaktionen zulassen');--Allow Transaction Posting
SELECT localization.add_localized_resource('ScrudResource', 'de', 'amount', 'Betrag');--Amount
SELECT localization.add_localized_resource('ScrudResource', 'de', 'amount_from', 'Betrag von');--Amount From
SELECT localization.add_localized_resource('ScrudResource', 'de', 'amount_to', 'Betrag zu');--Amount To
SELECT localization.add_localized_resource('ScrudResource', 'de', 'analyze_count', 'Gegliederte Aufzählung');--Analyze Count
SELECT localization.add_localized_resource('ScrudResource', 'de', 'api_access_policy_id', 'API-Richtlinien Id');--API Access Policy Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'api_access_policy_uix', 'Doppelter Eintrag für API-Richtlinien');--Duplicate Entry for API Access Policy
SELECT localization.add_localized_resource('ScrudResource', 'de', 'applied_on_shipping_charge', 'Angewendet auf Frachtgebühren');--Applied on Shipping Charge
SELECT localization.add_localized_resource('ScrudResource', 'de', 'audit_ts', 'Buchhaltungs Zeitstempel');--Audit Timestamp
SELECT localization.add_localized_resource('ScrudResource', 'de', 'audit_user_id', 'Buchhaltungs Benutzer ID');--Audit User Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'auto_trigger_on_sales', 'Bei Verkäufen automatisch triggern');--Automatically Trigger on Sales
SELECT localization.add_localized_resource('ScrudResource', 'de', 'autoanalyze_count', 'Automatische Darnbankanalyxe Anzahl');--Autoanalyze Count
SELECT localization.add_localized_resource('ScrudResource', 'de', 'autovacuum_count', 'AutoVacuum Anzahl');--Autovacuum Count
SELECT localization.add_localized_resource('ScrudResource', 'de', 'background_color', 'Hintergrundfarbe');--Background Color
SELECT localization.add_localized_resource('ScrudResource', 'de', 'balance', 'Bilanz');--Balance
SELECT localization.add_localized_resource('ScrudResource', 'de', 'bank_account_number', 'Bank Kontonummer IBAN');--Bank Account Number
SELECT localization.add_localized_resource('ScrudResource', 'de', 'bank_account_type', 'Bank Kontotyp ');--Bank Account Type
SELECT localization.add_localized_resource('ScrudResource', 'de', 'bank_accounts_account_id_chk', 'Die Auswahl ist kein gültiges Bankkonto.');--The selected item is not a valid bank account.
SELECT localization.add_localized_resource('ScrudResource', 'de', 'bank_accounts_pkey', 'Bankkontenduplicat');--Duplicate bank account.
SELECT localization.add_localized_resource('ScrudResource', 'de', 'bank_address', 'Anschrift der Bank');--Bank Address
SELECT localization.add_localized_resource('ScrudResource', 'de', 'bank_branch', 'Zweigstelle');--Bank Branch
SELECT localization.add_localized_resource('ScrudResource', 'de', 'bank_contact_number', 'Bank Kontakt Tel. Nummer');--Bank Contact Number
SELECT localization.add_localized_resource('ScrudResource', 'de', 'bank_name', 'Name der Bank');--Bank Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'base_unit_id', 'Grundeinheit Id');--Base Unit Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'base_unit_name', 'Gundeinheit Name');--Base Unit Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'based_on_shipping_address', 'Basierend auf Versandadresse');--Based On Shipping Address
SELECT localization.add_localized_resource('ScrudResource', 'de', 'bonus_rate', 'Bonus Rate');--Bonus Rate
SELECT localization.add_localized_resource('ScrudResource', 'de', 'bonus_slab_code', 'Bonustafel Code');--Bonus Slab Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'bonus_slab_detail_id', 'Bonustafel Details');--Bonus Slab Detail Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'bonus_slab_details_amounts_chk', 'Das Feld "Betrag  zu" muss größer als "Betrag von" sein.');--The field "AmountTo" must be greater than "AmountFrom".
SELECT localization.add_localized_resource('ScrudResource', 'de', 'bonus_slab_id', 'Bonustafel ID');--Bonus Slab Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'bonus_slab_name', 'Bonustafel Name');--Bonus Slab Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'book', 'Buch');--Book
SELECT localization.add_localized_resource('ScrudResource', 'de', 'book_date', 'Buchungsdatum');--Book Date
SELECT localization.add_localized_resource('ScrudResource', 'de', 'brand', 'Marke');--Brand
SELECT localization.add_localized_resource('ScrudResource', 'de', 'brand_code', 'Marke Code');--Brand Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'brand_id', 'Marken ID');--Brand Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'brand_name', 'Markenname');--Brand Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'browser', 'Browser');--Browser
SELECT localization.add_localized_resource('ScrudResource', 'de', 'can_change_password', 'Darf Paswort ändern');--Can Change Password
SELECT localization.add_localized_resource('ScrudResource', 'de', 'can_self_verify', 'Darf Selbst Überprüfen');--Can Self Verify
SELECT localization.add_localized_resource('ScrudResource', 'de', 'can_verify_gl_transactions', 'Darf Sachkonten Transaktionen Überprüfen');--Can Verify Gl Transactions
SELECT localization.add_localized_resource('ScrudResource', 'de', 'can_verify_purchase_transactions', 'Darf Einkaufs Transaktionen Überprüfen');--Can Verify Purchase Transactions
SELECT localization.add_localized_resource('ScrudResource', 'de', 'can_verify_sales_transactions', 'Kann Verkaufs Transaktionen Überprüfen');--Can Verify Sales Transactions
SELECT localization.add_localized_resource('ScrudResource', 'de', 'card_type', 'Karten Art');--Card Type
SELECT localization.add_localized_resource('ScrudResource', 'de', 'card_type_code', 'Karten Art-Code');--Card Type Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'card_type_id', 'Karten Art -ID');--Card Type Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'card_type_name', 'Karten-Art-Name');--Card Type Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'cash_flow_heading', 'CashF Flow Überschrift');--Cash Flow Heading
SELECT localization.add_localized_resource('ScrudResource', 'de', 'cash_flow_heading_cash_flow_heading_type_chk', 'Ungültiger Cash Flow Überschrifts Typ. Erlaubte Werte: O, I, F');--Invalid Cashflow Heading Type. Allowed values: O, I, F.
SELECT localization.add_localized_resource('ScrudResource', 'de', 'cash_flow_heading_code', 'Cash Flow Überschrift Code');--Cash Flow Heading Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'cash_flow_heading_id', 'Cash Flow Überschrift ID');--Cash Flow Heading Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'cash_flow_heading_name', 'Cash Flow Überschrift Name');--Cash Flow Heading Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'cash_flow_heading_type', 'Cash Flow Überschrift Typ');--Cashflow Heading Type
SELECT localization.add_localized_resource('ScrudResource', 'de', 'cash_flow_master_code', 'Cash Flow Master Code');--Cash Flow Master Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'cash_flow_master_id', 'Cash Flow Master Id');--Cash Flow Master Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'cash_flow_master_name', 'Cash Flow Master Name');--Cash Flow Master Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'cash_flow_setup_id', 'Cashflow Setup Id');--Cashflow Setup Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'cash_repositories_cash_repository_code_uix', 'Doppelter Barwerte-Depot Code');--Duplicate Cash Repository Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'cash_repositories_cash_repository_name_uix', 'Doppelter Barwerte-Depot Name');--Duplicate Cash Repository Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'cash_repository', 'Barwerte-Depot');--Cash Repository
SELECT localization.add_localized_resource('ScrudResource', 'de', 'cash_repository_code', 'Barwerte-Depot Code');--Cash Repository Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'cash_repository_id', 'Barwerte-Depot Id');--Cash Repository Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'cash_repository_name', 'Barwerte-Depot Name');--Cash Repository Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'cell', 'Handy');--Cell
SELECT localization.add_localized_resource('ScrudResource', 'de', 'charge_interest', 'Zinsberechnung');--Charge Interest
SELECT localization.add_localized_resource('ScrudResource', 'de', 'check_nexus', 'Nexus prüfen');--Check Nexus
SELECT localization.add_localized_resource('ScrudResource', 'de', 'checking_frequency', 'Prüfungshäufigkeit');--Checking Frequency
SELECT localization.add_localized_resource('ScrudResource', 'de', 'checking_frequency_id', 'Prüfungshäüfigkeits Id');--Checking Frequency Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'city', 'Stadt');--City
SELECT localization.add_localized_resource('ScrudResource', 'de', 'collecting_account', 'SammelKonto');--Collecting Account
SELECT localization.add_localized_resource('ScrudResource', 'de', 'collecting_account_id', 'Sammelknto Identifier');--Collecting Account Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'collecting_tax_authority', 'Sammelkontp Steuerbehörde');--Collecting Tax Authority
SELECT localization.add_localized_resource('ScrudResource', 'de', 'collecting_tax_authority_id', 'Sammelkonto Steuerbehörde Id');--Collecting Tax Authority Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'commision_rate', 'Kommissionsquote');--Commission Rate
SELECT localization.add_localized_resource('ScrudResource', 'de', 'commission_rate', 'Kommissionsquote');--Commission Rate
SELECT localization.add_localized_resource('ScrudResource', 'de', 'company_name', 'Firmenname');--Company Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'compare_unit_id', 'Vergleichseinheit ID');--Compare Unit Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'compare_unit_name', 'Vergleichseinheit Name');--Compare Unit Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'compound_item', 'Kombi Artikel');--Compound Item
SELECT localization.add_localized_resource('ScrudResource', 'de', 'compound_item_code', 'Kombi  Artikel  Code');--Compound Item Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'compound_item_detail_id', 'Kombi Artikel Detail Identifier');--Compound Item Detail Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'compound_item_details_unit_chk', 'Ungültige Einheit angegeben');--Invalid unit provided.
SELECT localization.add_localized_resource('ScrudResource', 'de', 'compound_item_id', 'Kombi Aretikel Id');--Compound Item Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'compound_item_name', 'Kombi Artikel Name');--Compound Item Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'compound_unit_id', 'Kombi Einheit Id');--Compound Unit Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'compound_units_chk', 'Die Basiseinheit Id kann nicht gleich wie Vergleichseinheit Id sein.');--The base unit id cannot same as compare unit id.
SELECT localization.add_localized_resource('ScrudResource', 'de', 'compounding_frequency', 'Zusammenfassungsperiode');--Compounding Frequency
SELECT localization.add_localized_resource('ScrudResource', 'de', 'confidential', 'Vertraulich');--Confidential
SELECT localization.add_localized_resource('ScrudResource', 'de', 'contact_address_line_1', 'Kontakt Adresszeile 1');--Contact Address Line 1
SELECT localization.add_localized_resource('ScrudResource', 'de', 'contact_address_line_2', 'Kontakt Adresszeile 2');--Contact Address Line 2
SELECT localization.add_localized_resource('ScrudResource', 'de', 'contact_cell', 'Kontakt Handy');--Contact Cell
SELECT localization.add_localized_resource('ScrudResource', 'de', 'contact_city', 'Kontakt Stadt');--Contact City
SELECT localization.add_localized_resource('ScrudResource', 'de', 'contact_country', 'Kontakt Staat');--Contact Country
SELECT localization.add_localized_resource('ScrudResource', 'de', 'contact_email', 'Kontakt per EMail');--Contact Email
SELECT localization.add_localized_resource('ScrudResource', 'de', 'contact_number', 'Kontaktnummer');--Contact Number
SELECT localization.add_localized_resource('ScrudResource', 'de', 'contact_person', 'Gesprächspartner');--Contact Person
SELECT localization.add_localized_resource('ScrudResource', 'de', 'contact_phone', 'Kontakt Telefoon');--Contact Phone
SELECT localization.add_localized_resource('ScrudResource', 'de', 'contact_po_box', 'Kontakt  Po Box');--Contact Po Box
SELECT localization.add_localized_resource('ScrudResource', 'de', 'contact_state', 'Kontakt Bundesland');--Contact State
SELECT localization.add_localized_resource('ScrudResource', 'de', 'contact_street', 'Kontakt Straße');--Contact Street
SELECT localization.add_localized_resource('ScrudResource', 'de', 'cost_center_code', 'Kostenstelle Code');--Cost Center Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'cost_center_id', 'Kostenstelle Id');--Cost Center Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'cost_center_name', 'Kostenstelle Name');--Cost Center Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'cost_of_goods_sold_account_id', 'Konto Verkaufsartikel-Produktkosten Id');--COGS Account Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'cost_price', 'Kostenhöhe');--Cost Price
SELECT localization.add_localized_resource('ScrudResource', 'de', 'cost_price_includes_tax', 'Kosten inclusive Mwst.');--Cost Price Includes Tax
SELECT localization.add_localized_resource('ScrudResource', 'de', 'counter_code', 'KassaCode');--Counter Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'counter_id', 'Kassa Id');--Counter Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'counter_name', 'Kassa Name');--Counter Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'country', 'Staat');--Country
SELECT localization.add_localized_resource('ScrudResource', 'de', 'country_code', 'Staats Code');--Country Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'country_id', 'Staat Id');--Country Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'country_name', 'Staat Name');--Country Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'county', 'Bezirk');--County
SELECT localization.add_localized_resource('ScrudResource', 'de', 'county_code', 'Bezirk Code');--County Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'county_id', 'Bezirk Id');--County Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'county_name', 'Bezirk Name');--County Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'county_sales_tax', 'Bezirks Umsatzsteuer');--County Sales Tax
SELECT localization.add_localized_resource('ScrudResource', 'de', 'county_sales_tax_code', 'Betirks Umsatzsteuer Code');--County Sales Tax Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'county_sales_tax_id', 'Bezirks Umsatzsteuer  Id');--County Sales Tax Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'county_sales_tax_name', 'Bezirks Umsatzsteuer Name');--County Sales Tax Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'credit', 'Kredit');--Credit
SELECT localization.add_localized_resource('ScrudResource', 'de', 'cst_number', 'Zentralumsatzsteuernummer');--CST Number
SELECT localization.add_localized_resource('ScrudResource', 'de', 'culture', 'Kultur');--Culture
SELECT localization.add_localized_resource('ScrudResource', 'de', 'currency', 'Währung');--Currency
SELECT localization.add_localized_resource('ScrudResource', 'de', 'currency_code', 'Währungscode');--Currency Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'currency_name', 'Währungsbezeichnung');--Currency Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'currency_symbol', 'Währungssymbol');--Currency Symbol
SELECT localization.add_localized_resource('ScrudResource', 'de', 'customer_pays_fee', 'Gebühr trägt Kunde');--Customer Pays Fee
SELECT localization.add_localized_resource('ScrudResource', 'de', 'date_of_birth', 'Geburtsdatum');--Date Of Birth
SELECT localization.add_localized_resource('ScrudResource', 'de', 'debit', 'Soll');--Debit
SELECT localization.add_localized_resource('ScrudResource', 'de', 'default_cash_account_id', 'Standard Kassa Konto Id');--Default Cash Account Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'default_cash_repository_id', 'Standard Barwerte-Depot Id');--Default Cash Repository Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'department_code', 'Abteilungscode');--Department Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'department_id', 'Abteilung Id');--Department Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'department_name', 'Abteilung Name');--Department Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'description', 'Beschreibung');--Description
SELECT localization.add_localized_resource('ScrudResource', 'de', 'discount', 'Rabatt');--Discount
SELECT localization.add_localized_resource('ScrudResource', 'de', 'due_days', 'Laufzeit (Tage)');--Due Days
SELECT localization.add_localized_resource('ScrudResource', 'de', 'due_frequency', 'Fälligkeits Zeitraum');--Due Frequency
SELECT localization.add_localized_resource('ScrudResource', 'de', 'due_frequency_id', 'Fälligkeits Zeitraum Id');--Due Frequency Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'due_on_date', 'Fällig am');--Due on Date
SELECT localization.add_localized_resource('ScrudResource', 'de', 'effective_from', 'Gültig ab');--Effective From
SELECT localization.add_localized_resource('ScrudResource', 'de', 'elevated', 'Erhöht');--Elevated
SELECT localization.add_localized_resource('ScrudResource', 'de', 'email', 'EMail');--Email
SELECT localization.add_localized_resource('ScrudResource', 'de', 'ends_on', 'Endet am');--Ends On
SELECT localization.add_localized_resource('ScrudResource', 'de', 'entity_id', 'Körperschaft Id');--Entity Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'entity_name', 'Körperschaftsbezeichnung');--Entity Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'entry_ts', 'Eintrag Zeitstempel');--Entry Ts
SELECT localization.add_localized_resource('ScrudResource', 'de', 'er', 'Wechselkurs');--ER
SELECT localization.add_localized_resource('ScrudResource', 'de', 'exclude_from_purchase', 'Vom Einkauf ausschließen');--Exclude From Purchase
SELECT localization.add_localized_resource('ScrudResource', 'de', 'exclude_from_sales', 'Vom Vertrieb ausschließen');--Exclude From Sales
SELECT localization.add_localized_resource('ScrudResource', 'de', 'external_code', 'Externer Code');--External Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'factory_address', 'Fabriks Anschrift');--Factory Address
SELECT localization.add_localized_resource('ScrudResource', 'de', 'fax', 'Fax');--Fax
SELECT localization.add_localized_resource('ScrudResource', 'de', 'first_name', 'Vorname');--First Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'fiscal_year_code', 'Geschäftsjahr Code');--Fiscal Year Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'fiscal_year_name', 'Geschäftsjahr Bezeichnung');--Fiscal Year Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'flag_id', 'Marke Id');--Flag Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'flag_type_id', 'Marke Typ Id');--Flag Type Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'flag_type_name', 'Marke Typ Name');--Flag Type Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'flagged_on', 'Markiert');--Flagged On
SELECT localization.add_localized_resource('ScrudResource', 'de', 'foreground_color', 'Vordergrundfarbe');--Foreground Color
SELECT localization.add_localized_resource('ScrudResource', 'de', 'frequency_code', 'Häufigkeit Code');--Frequency Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'frequency_id', 'Häufigkeit Id');--Frequency Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'frequency_name', 'Häufigkeit Name');--Frequency Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'frequency_setup_code', 'Häufigkeit Setup Code');--Frequency Setup Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'frequency_setup_id', 'Häufigkeit Setup Id');--Frequency Setup Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'from_days', 'Beginnt mit');--From Days
SELECT localization.add_localized_resource('ScrudResource', 'de', 'full_name', 'Vollständiger Name');--Full Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'gl_head', 'Sachkonten');--GL Head
SELECT localization.add_localized_resource('ScrudResource', 'de', 'gl_verification_limit', 'Sachkonten Verifizierungs Limit ');--Gl Verification Limit
SELECT localization.add_localized_resource('ScrudResource', 'de', 'grace_period', 'Spielraum');--Grace Period
SELECT localization.add_localized_resource('ScrudResource', 'de', 'has_child', 'Hat Tochter');--Has Child
SELECT localization.add_localized_resource('ScrudResource', 'de', 'height_in_centimeters', 'Höhe in Zentimetern');--Height In Centimeters
SELECT localization.add_localized_resource('ScrudResource', 'de', 'hot_item', 'Hot Item');--Hot item
SELECT localization.add_localized_resource('ScrudResource', 'de', 'http_action_code', 'HTTP Aktionscode');--HTTP Action Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'hundredth_name', 'Bezeichnung Untereinheit');--Hundredth Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'id', 'Identifier');--Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'includes_tax', 'Inklusive Steuern');--Includes Tax
SELECT localization.add_localized_resource('ScrudResource', 'de', 'income_tax_rate', 'Einkommensteuersatz');--Income Tax Rate
SELECT localization.add_localized_resource('ScrudResource', 'de', 'industry_id', 'Industie Id');--Industry Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'industry_name', 'Industrie Name');--Industry Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'interest_compounding_frequency_id', 'Aufzinsungs Periode Id');--Interest Compounding Frequency Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'interest_rate', 'Zinssatz');--Interest Rate
SELECT localization.add_localized_resource('ScrudResource', 'de', 'inventory_account_id', 'Bestandskonto Id');--Inventory Account Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'ip_address', 'IP Adresse');--IP Address
SELECT localization.add_localized_resource('ScrudResource', 'de', 'is_active', 'Ist Aktiv');--Is Active
SELECT localization.add_localized_resource('ScrudResource', 'de', 'is_added', 'Ist Hinzugefügt');--Is Added
SELECT localization.add_localized_resource('ScrudResource', 'de', 'is_admin', 'Ist Admin');--Is Admin
SELECT localization.add_localized_resource('ScrudResource', 'de', 'is_cash', 'Ist Barzahlung');--Is Cash
SELECT localization.add_localized_resource('ScrudResource', 'de', 'is_debit', 'Ist Lastschrift');--Is Debit
SELECT localization.add_localized_resource('ScrudResource', 'de', 'is_employee', 'Ist Mitarbeiter');--Is Employee
SELECT localization.add_localized_resource('ScrudResource', 'de', 'is_exempt', 'Ist Ausnahme');--Is Exempt
SELECT localization.add_localized_resource('ScrudResource', 'de', 'is_exemption', 'Ist Ausnahme Rgelung');--Is Exemption
SELECT localization.add_localized_resource('ScrudResource', 'de', 'is_flat_amount', 'Ist Pauschae');--Is Flat Amount
SELECT localization.add_localized_resource('ScrudResource', 'de', 'is_merchant_account', 'Ist Händler Konto');--Is Merchant Account
SELECT localization.add_localized_resource('ScrudResource', 'de', 'is_party', 'Ist Partei');--Is Party
SELECT localization.add_localized_resource('ScrudResource', 'de', 'is_purchase', 'Ist Einkauf');--Is Purchase
SELECT localization.add_localized_resource('ScrudResource', 'de', 'is_rectangular', 'Ist Rechteckig');--Is Rectangular
SELECT localization.add_localized_resource('ScrudResource', 'de', 'is_sales', 'Ist Verkauf');--Is Sales
SELECT localization.add_localized_resource('ScrudResource', 'de', 'is_summary', 'Ist Zusammenfassung');--Is Summary
SELECT localization.add_localized_resource('ScrudResource', 'de', 'is_supplier', 'Ist Lieferant');--Is Supplier
SELECT localization.add_localized_resource('ScrudResource', 'de', 'is_system', 'Ist System');--Is System
SELECT localization.add_localized_resource('ScrudResource', 'de', 'is_transaction_node', 'Ist Transaction Node');--Is Transaction Node
SELECT localization.add_localized_resource('ScrudResource', 'de', 'is_vat', 'Ist Umsatzsteuer');--Is Vat
SELECT localization.add_localized_resource('ScrudResource', 'de', 'item', 'Artikel');--Item
SELECT localization.add_localized_resource('ScrudResource', 'de', 'item_code', 'Artikel Code');--Item Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'item_cost_price_id', 'Artikelkosten Id');--Item Cost Price Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'item_cost_prices_unit_chk', 'Ungültige Einheit eingegeben');--Invalid unit provided.
SELECT localization.add_localized_resource('ScrudResource', 'de', 'item_group', 'Artikelgruppe');--Item Group
SELECT localization.add_localized_resource('ScrudResource', 'de', 'item_group_code', 'Artikel Gruppen Code');--Item Group Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'item_group_id', 'Artikel Gruppen Id');--Item Group Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'item_group_name', 'Artikelgruppen Name');--Item Group Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'item_id', 'Artikel-Nummer');--Item Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'item_name', 'Artikelname');--Item Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'item_opening_inventory_unit_chk', 'Ungültige Einheit eingegeben');--Invalid unit provided.
SELECT localization.add_localized_resource('ScrudResource', 'de', 'item_selling_price_id', 'Artikel Verkaufspreis Id');--Item Selling Price Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'item_selling_prices_unit_chk', 'Ungültige Einheit eingegeben');--Invalid unit provided.
SELECT localization.add_localized_resource('ScrudResource', 'de', 'item_type_code', 'Artikel Typ-Code');--Item Type Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'item_type_id', 'Artikel Typ Id');--Item Type Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'item_type_name', 'Artikel Typ Name');--Item Type Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'items_item_code_uix', 'Doppelter Artikelcode');--Duplicate item code.
SELECT localization.add_localized_resource('ScrudResource', 'de', 'items_item_name_uix', 'Doppelter Artikelname');--Duplicate item name.
SELECT localization.add_localized_resource('ScrudResource', 'de', 'items_reorder_quantity_chk', 'Die Bestellmenge  muß größer oder gleich als der Mindestbestand sein');--The reorder quantity must be great than or equal to the reorder level.
SELECT localization.add_localized_resource('ScrudResource', 'de', 'last_analyze', 'Letzte Datenbankanalyse am');--Last Analyze On
SELECT localization.add_localized_resource('ScrudResource', 'de', 'last_autoanalyze', 'Letzte Automatische Aanalyse am');--Last Autoanalyze On
SELECT localization.add_localized_resource('ScrudResource', 'de', 'last_autovacuum', 'Letztes Auto Vakuum  am');--Last Autovacuum On
SELECT localization.add_localized_resource('ScrudResource', 'de', 'last_name', 'Nachname');--Last Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'last_vacuum', 'Letztes Vacuum am');--Last Vacuum On
SELECT localization.add_localized_resource('ScrudResource', 'de', 'late_fee', 'Säumniszuschlag');--Late Fee
SELECT localization.add_localized_resource('ScrudResource', 'de', 'late_fee_code', 'Säumniszuschlag code');--Late Fee Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'late_fee_id', 'Säumniszuschlag Id');--Late Fee Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'late_fee_name', 'Säumniszuschlag Name');--Late Fee Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'late_fee_posting_frequency', 'Säumniszuschlag Buchungsperiode');--Late Fee Posting Frequency
SELECT localization.add_localized_resource('ScrudResource', 'de', 'late_fee_posting_frequency_id', 'Säumniszuschlag Buchungsperiode Id');--Late Fee Posting Frequency Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'lc_credit', 'EU Haben');--LC Credit
SELECT localization.add_localized_resource('ScrudResource', 'de', 'lc_debit', 'EU Soll');--LC Debit
SELECT localization.add_localized_resource('ScrudResource', 'de', 'lead_source_code', 'Werber Code');--Lead Source Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'lead_source_id', 'Werber ID');--Lead Source Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'lead_source_name', 'Werber Name');--Lead Source Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'lead_status_code', 'Werber Status');--Lead Status Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'lead_status_id', 'Werber Status ID');--Lead Status Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'lead_status_name', 'Werber Status Name');--Lead Status Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'lead_time_in_days', 'Vorlaufzeit in Tagen');--Lead Time In Days
SELECT localization.add_localized_resource('ScrudResource', 'de', 'length_in_centimeters', 'Länge in cm');--Length In Centimeters
SELECT localization.add_localized_resource('ScrudResource', 'de', 'login_date_time', 'Login Datum Zeit');--Login Date Time
SELECT localization.add_localized_resource('ScrudResource', 'de', 'login_id', 'Login Id');--Login Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'machinable', 'Weiter Verarbeitbar');--Machinable
SELECT localization.add_localized_resource('ScrudResource', 'de', 'maintain_stock', 'Vorratslager Verwaltung');--Maintain Stock
SELECT localization.add_localized_resource('ScrudResource', 'de', 'maintained_by_user_id', 'Verwaltet von User Id');--Maintained By User Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'maximum_credit_amount', 'Maximaler Kreditbetrag');--Maximum Credit Amount
SELECT localization.add_localized_resource('ScrudResource', 'de', 'maximum_credit_period', 'Maximale Kreditlaufzeit');--Maximum Credit Period
SELECT localization.add_localized_resource('ScrudResource', 'de', 'merchant_account_id', 'Händler Konto ID');--Merchant Account Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'merchant_fee_setup_id', 'Händler Gebühr Setup-Id');--Merchant Fee Setup Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'middle_name', 'Zweiter Vorname');--Middle Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'nick_name', 'Nickname');--Nick Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'non_gl_stock_details_unit_chk', 'Ungültige Einheit eingegeben');--Invalid unit provided.
SELECT localization.add_localized_resource('ScrudResource', 'de', 'normally_debit', 'Normaleweise Lastschrift');--Normally Debit
SELECT localization.add_localized_resource('ScrudResource', 'de', 'office', 'Office');--Office
SELECT localization.add_localized_resource('ScrudResource', 'de', 'office_code', 'Office Code');--Office Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'office_id', 'Office Id');--Office Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'office_name', 'Officename');--Office Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'opportunity_stage_code', 'Chancen Stufen Code');--Opportunity Stage Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'opportunity_stage_id', 'Chancen Stufen Id');--Opportunity Stage Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'opportunity_stage_name', 'Chancen Stufe Name');--Opportunity Stage Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'pan_number', 'PAN (Permanent Account ) Nummer');--Pan Number
SELECT localization.add_localized_resource('ScrudResource', 'de', 'parent', 'Mutter');--Parent
SELECT localization.add_localized_resource('ScrudResource', 'de', 'parent_account_id', 'Mutter Account Id');--Parent Account Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'parent_account_master_id', 'Mutter Konto Master Id');--Parent Account Master Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'parent_account_name', 'Mutter Konto Name');--Parent Account Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'parent_account_number', 'Mutter Konto Number');--Parent Account Number
SELECT localization.add_localized_resource('ScrudResource', 'de', 'parent_cash_flow_heading_id', 'Mutter Cash Flow Überschrift Id');--Parent Cash Flow Heading Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'parent_cash_repository', 'Mutter Barwerte-Depot');--Parent Cash Repository
SELECT localization.add_localized_resource('ScrudResource', 'de', 'parent_cash_repository_id', 'Mutter Barwerte-Depot Id');--Parent Cash Repository Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'parent_cr_code', 'Mutter BarDepot Code');--Parent CR Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'parent_cr_name', 'Mutter BarDepot Name');--Parent CR Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'parent_industry_id', 'Mutter Industrie Id');--Parent Industry Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'parent_industry_name', 'Mutter Industrie Name');--Parent Industry Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'parent_item_group_id', 'Mutter Artikel Gruppe Id');--Parent Item Group Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'parent_office', 'Übergeordnetes Office');--Parent Office
SELECT localization.add_localized_resource('ScrudResource', 'de', 'parent_office_id', 'Übergeordnetes Office Id');--Parent Office Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'party', 'Partei');--Party
SELECT localization.add_localized_resource('ScrudResource', 'de', 'party_code', 'Partei-Code');--Party Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'party_id', 'Partei Id');--Party Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'party_name', 'Name der Partei');--Party Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'party_type', 'Partei-Typ');--Party Type
SELECT localization.add_localized_resource('ScrudResource', 'de', 'party_type_code', 'Partei Typ Code');--Party Tpye Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'party_type_id', 'Partei-Typ Id');--Party Type Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'party_type_name', 'Partei Typ Name');--Party Type Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'password', 'Passwort');--Password
SELECT localization.add_localized_resource('ScrudResource', 'de', 'payment_card_code', 'Kreditkarten Code');--Payment Card Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'payment_card_id', ' Kreditkarten Id');--Payment Card Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'payment_card_name', 'Kreditkarten Name');--Payment Card Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'payment_term', 'Zahlungsbedingungen');--Payment Term
SELECT localization.add_localized_resource('ScrudResource', 'de', 'payment_term_code', 'Zahlungsbedingungen Code');--Payment Term Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'payment_term_id', 'Zahlungsbedingungen Id');--Payment Term Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'payment_term_name', 'Zahlungsbedingungen Bezeichnung');--Payment Term Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'phone', 'Telefon');--Phone
SELECT localization.add_localized_resource('ScrudResource', 'de', 'po_box', 'Postfach');--Po Box
SELECT localization.add_localized_resource('ScrudResource', 'de', 'poco_type_name', 'Poco Typ Name');--Poco Type Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'policy_id', 'Richtlinie Id');--Policy id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'preferred_shipping_mail_type', 'Bevorzugte Zustellungsart.');--Preferred Shipping Mail Type
SELECT localization.add_localized_resource('ScrudResource', 'de', 'preferred_shipping_mail_type_id', 'Bevorzugte Zustelungsart Id');--Preferred Shipping Mail Type Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'preferred_shipping_package_shape', 'Gewünschte Verepackungsart');--Preferred Shipping Package Shape
SELECT localization.add_localized_resource('ScrudResource', 'de', 'preferred_supplier', 'Bevorzugter Lieferant');--Preferred Supplier
SELECT localization.add_localized_resource('ScrudResource', 'de', 'preferred_supplier_id', 'Bevorzugter Lieferant Id');--Preferred Supplier Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'price', 'Preis');--Price
SELECT localization.add_localized_resource('ScrudResource', 'de', 'price_from', 'Preis ab');--Price From
SELECT localization.add_localized_resource('ScrudResource', 'de', 'price_to', 'Preis bis');--Price To
SELECT localization.add_localized_resource('ScrudResource', 'de', 'price_type_code', 'Preis Art Code');--Price Type Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'price_type_id', 'Preis Art Identifier');--Price Type Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'price_type_name', 'Preis Art-Name');--Price Type Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'priority', 'Priorität');--Priority
SELECT localization.add_localized_resource('ScrudResource', 'de', 'purchase_account_id', 'Einkaufskonto Id');--Purchase Account Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'purchase_discount_account_id', 'Einkauf Discount Konto Id');--Purchase Discount Account Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'purchase_verification_limit', 'einkauf Prüfungslimit');--Purchase Verification Limit
SELECT localization.add_localized_resource('ScrudResource', 'de', 'quantity', 'Menge');--Quantity
SELECT localization.add_localized_resource('ScrudResource', 'de', 'rate', 'Quote');--Rate
SELECT localization.add_localized_resource('ScrudResource', 'de', 'recurrence_type_id', 'Wiederholungs Typ Id');--Recurrence Type Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'recurring_amount', 'Wiederholung- Betrag');--Recurring Amount
SELECT localization.add_localized_resource('ScrudResource', 'de', 'recurring_duration', 'Wiederholungsdauer');--Recurring Duration
SELECT localization.add_localized_resource('ScrudResource', 'de', 'recurring_frequency', 'wiederholungsperiode');--Recurring Frequency
SELECT localization.add_localized_resource('ScrudResource', 'de', 'recurring_frequency_id', 'wiederholungsperiode Id');--Recurring Frequency Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'recurring_invoice', 'Wiederholungsrechnung');--Recurring Invoice
SELECT localization.add_localized_resource('ScrudResource', 'de', 'recurring_invoice_code', 'Wiederholungsrechnung Id');--Recurring Invoice Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'recurring_invoice_id', 'Wiederholungsrechnung Id');--Recurring Invoice Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'recurring_invoice_name', 'Wiederkehrende Rechnung Name');--Recurring Invoice Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'recurring_invoice_setup_id', 'Wiederkehrende Rechnung Setup-Identifier');--Recurring Invoice Setup Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'recurring_invoices_item_id_auto_trigger_on_sales_uix', 'Sie können nicht mehr als eine Auto-Trigger auf den Umsatz für diesen Artikel haben');--You cannot have more than one auto trigger on sales for this item.
SELECT localization.add_localized_resource('ScrudResource', 'de', 'recurs_on_same_calendar_date', 'Wiederholung am selben Kalendertag');--Recurs on Same Calendar Date
SELECT localization.add_localized_resource('ScrudResource', 'de', 'registration_date', 'Datum der Registrierung');--Registration Date
SELECT localization.add_localized_resource('ScrudResource', 'de', 'registration_number', 'Registrierungsnummer');--Registration Number
SELECT localization.add_localized_resource('ScrudResource', 'de', 'relationship_officer_name', 'Bezugsperson Name');--Relationship Officer Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'relname', 'Beziehungsverhätnis');--Relation Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'remote_user', 'Remote User');--Remote User
SELECT localization.add_localized_resource('ScrudResource', 'de', 'reorder_level', 'Mindestbestand');--Reorder Level
SELECT localization.add_localized_resource('ScrudResource', 'de', 'reorder_quantity', 'Wiederbestell Menge');--Reorder Quantity
SELECT localization.add_localized_resource('ScrudResource', 'de', 'reorder_unit', 'Wiederbestell Einheit');--Reorder Unit
SELECT localization.add_localized_resource('ScrudResource', 'de', 'reorder_unit_id', 'Wiederbestell Einheit Id');--Reorder Unit Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'reporting_tax_authority', 'Steuermeldungs Finanzamt');--Reporting Tax Authority
SELECT localization.add_localized_resource('ScrudResource', 'de', 'reporting_tax_authority_id', 'Steuermeldungs Finanzamt Id');--Reporting Tax Authority Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'repository', 'Depot');--Repository
SELECT localization.add_localized_resource('ScrudResource', 'de', 'resource', 'Resource');--Resource
SELECT localization.add_localized_resource('ScrudResource', 'de', 'resource_id', 'Resource Id');--Resource Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'resource_key', 'Resource-Key');--Resource Key
SELECT localization.add_localized_resource('ScrudResource', 'de', 'role', 'Rolle');--Role
SELECT localization.add_localized_resource('ScrudResource', 'de', 'role_code', 'Rollen-Code');--Role Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'role_id', 'Rollen Id');--Role Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'role_name', 'Rollenname');--Role Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'rounding_decimal_places', 'Gerundete Dezimalstellen');--Rounding Decimal Places
SELECT localization.add_localized_resource('ScrudResource', 'de', 'rounding_method', 'Rundungs Methode');--Rounding Method
SELECT localization.add_localized_resource('ScrudResource', 'de', 'rounding_method_code', 'Rundungs Methoden-Code');--Rounding Method Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'rounding_method_name', 'Rundungs-Methodenname');--Rounding Method Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'sales_account_id', 'Verkaufskonto Id');--Sales Account Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'sales_discount_account_id', 'Verkaufsdiskont Konto id');--Sales Discount Account Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'sales_return_account_id', 'Retourwaren Konto Id');--Sales Return Account Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'sales_tax', 'Umsatzsteuer');--Sales Tax
SELECT localization.add_localized_resource('ScrudResource', 'de', 'sales_tax_code', 'Umsatzsteuer-Code');--Sales Tax Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'sales_tax_detail_code', 'Umsatzsteuer-Detail-Code');--Sales Tax Detail Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'sales_tax_detail_id', 'Umsatzsteuer-Detail Identifier');--Sales Tax Detail Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'sales_tax_detail_name', 'Umsatzsteuer Detail Name');--Sales Tax Detail Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'sales_tax_details_rate_chk', 'Der Prozentsatz kann nicht leer bleiben, außer sie haben Landes oder Bezirksabgaben  ausgewählt. Umgekehrt können sie keinen Prozentsatz angeben, wenn es sich um Landes oder Bezirksabgaben handelt.');--Rate should not be empty unless you have selected state or county tax. Similarly, you cannot provide both rate and choose to have state or county tax.
SELECT localization.add_localized_resource('ScrudResource', 'de', 'sales_tax_exempt', 'Umsatzsteuer ausnahme');--Sales Tax Exempt
SELECT localization.add_localized_resource('ScrudResource', 'de', 'sales_tax_exempt_code', 'Umsatzsteuer Ausnahmen Code');--Sales Tax Exempt Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'sales_tax_exempt_detail_id', 'Umsatzsteuer Ausnahmen Details Id');--Sales Tax Exempt Detail Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'sales_tax_exempt_id', 'Umsatzsteuer Ausnahme Id');--Sales Tax Exempt Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'sales_tax_exempt_name', 'Umsatzsteuer  Ausnahme Name');--Sales Tax Exempt Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'sales_tax_exempts_price_to_chk', 'Das Feld  "Preis ab" muss kleiner sein als "Preis bis."');--The field "PriceFrom" must be less than "PriceTo".
SELECT localization.add_localized_resource('ScrudResource', 'de', 'sales_tax_id', 'Umsatzsteuer-Id');--Sales Tax Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'sales_tax_name', 'Umsatzsteuer-Name');--Sales Tax Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'sales_tax_type', 'Umsatzsteuer-Typ');--Sales Tax Type
SELECT localization.add_localized_resource('ScrudResource', 'de', 'sales_tax_type_code', 'Umsatzsteuer-Typ Code');--Sales Tax Type Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'sales_tax_type_id', 'Umsatzsteuer-Typen Id');--Sales Tax Type Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'sales_tax_type_name', 'Umsatzsteuer-Typ Name');--Sales Tax Type Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'sales_team_code', 'Verkaufsteam Code');--Sales Team Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'sales_team_id', 'Verkaufsteam Id');--Sales Team Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'sales_team_name', 'Verkaufsteam Name');--Sales Team Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'sales_verification_limit', 'Verkäufe Verifizierungs Limit');--Sales Verification Limit
SELECT localization.add_localized_resource('ScrudResource', 'de', 'salesperson_bonus_setup_id', 'Verkäufer Bonus Setup Id');--Salesperson Bonus Setup Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'salesperson_code', 'Verkäufer Code');--Salesperson Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'salesperson_id', 'Verkäufer Id');--Salesperson Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'salesperson_name', 'Verkäufer-Name');--Salesperson Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'self_verification_limit', 'Selbstprüfung Limit');--Self Verification Limit
SELECT localization.add_localized_resource('ScrudResource', 'de', 'selling_price', 'Verkaufspreis');--Selling Price
SELECT localization.add_localized_resource('ScrudResource', 'de', 'selling_price_includes_tax', 'Verkaufspreis inklusive Umsatzsteuer');--Selling Price Includes Tax
SELECT localization.add_localized_resource('ScrudResource', 'de', 'shipper_code', 'Spediteur Code');--Shipper Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'shipper_id', 'Spediteur ID');--Shipper Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'shipper_name', 'Spediteur Name');--Shipper Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'shipping_address_code', 'Versandadresse-Code');--Shipping Address Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'shipping_address_id', 'Versandadresse Id');--Shipping Address Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'shipping_mail_type_code', 'Postversandart Code');--Shipping Mail Type Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'shipping_mail_type_id', 'Postversandart Id');--Shipping Mail Type Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'shipping_mail_type_name', 'Postversandart Name');--Shipping Mail Type Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'shipping_package_shape_code', 'Versandpaketart Code');--Shipping Package Shape Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'shipping_package_shape_id', 'Versandpaketart Code');--Shipping Package Shape Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'shipping_package_shape_name', 'Versandpaketart  Name');--Shipping Package Shape Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'slab_name', 'Tafel Name');--Slab Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'sst_number', 'SST Nummer');--SST Number
SELECT localization.add_localized_resource('ScrudResource', 'de', 'starts_from', 'Beginnt mit');--Starts From
SELECT localization.add_localized_resource('ScrudResource', 'de', 'state', 'Bundesland');--State
SELECT localization.add_localized_resource('ScrudResource', 'de', 'state_code', 'Bundesland-Code');--State Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'state_id', 'Bundesland Id');--State Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'state_name', 'Bundesland Name');--State Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'state_sales_tax', 'LandesUmsatzsteuer');--State Sales Tax
SELECT localization.add_localized_resource('ScrudResource', 'de', 'state_sales_tax_code', 'Landes Umsatzsteuer Code');--State Sales Tax Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'state_sales_tax_id', 'Landes Umsatzsteuer ID');--State Sales Tax Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'state_sales_tax_name', 'Landes  Umsatzsteuer Bezeichnung');--State Sales Tax Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'statement_reference', 'Erläuternder Vermerk');--Statement Reference
SELECT localization.add_localized_resource('ScrudResource', 'de', 'stock_details_unit_chk', 'Ungültige Einheit engegeben.');--Invalid unit provided.
SELECT localization.add_localized_resource('ScrudResource', 'de', 'store', 'Geschäft');--Store
SELECT localization.add_localized_resource('ScrudResource', 'de', 'store_code', 'Geschäft Code');--Store Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'store_id', 'Geschäft Id');--Store Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'store_name', 'Geschäft Name');--Store Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'store_type', 'Geschätstyp');--Store Type
SELECT localization.add_localized_resource('ScrudResource', 'de', 'store_type_code', 'Geschäftstyp Code');--Store Type Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'store_type_id', 'Geschäftstyp Id');--Store Type Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'store_type_name', 'Geschäftstyp Name');--Store Type Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'stores_default_cash_account_id_chk', 'Bitte wählen Sie die korrekte Id eines Zahlungs- oder BankKontos.');--Please select a valid Cash or Bank AccountId.
SELECT localization.add_localized_resource('ScrudResource', 'de', 'stores_sales_tax_id_chk', 'Die gewählte Mehrwertsteuer Id ist für dieses Office ungütig.');--The chosen SalesTaxId is invalid for this office.
SELECT localization.add_localized_resource('ScrudResource', 'de', 'street', 'Straße');--Street
SELECT localization.add_localized_resource('ScrudResource', 'de', 'sub_total', 'Zwischensumme');--Sub Total
SELECT localization.add_localized_resource('ScrudResource', 'de', 'sys_type', 'Systemtyp');--Sys Type
SELECT localization.add_localized_resource('ScrudResource', 'de', 'tax', 'Steuer');--Tax
SELECT localization.add_localized_resource('ScrudResource', 'de', 'tax_authority_code', 'Finanzamt-Code');--Tax Authority Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'tax_authority_id', 'Finanzamt ID');--Tax Authority Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'tax_authority_name', 'Finanzamt Name');--Tax Authority Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'tax_base_amount', 'Steuerbemessungsgrundlage');--Tax Base Amount
SELECT localization.add_localized_resource('ScrudResource', 'de', 'tax_base_amount_type_code', 'Bemessungsgrundlagen Typ-Code');--Tax Base Amount Type Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'tax_base_amount_type_name', 'Bemessungsgrundlagen Typ Name');--Tax Base Amount Type Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'tax_code', 'Steuer Kurz-Code');--Tax Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'tax_exempt_type', 'Steuer Ausnahme Typ');--Tax Exempt Type
SELECT localization.add_localized_resource('ScrudResource', 'de', 'tax_exempt_type_code', 'Steuer Ausnahmen Typ  Code');--Tax Exempt Type Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'tax_exempt_type_id', 'Steuer Ausnahme Typ Id');--Tax Exempt Type Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'tax_exempt_type_name', 'Steuer Ausnahme Name');--Tax Exempt Type Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'tax_id', 'Steuernummer');--Tax Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'tax_master', 'Steuerbasis');--Tax Master
SELECT localization.add_localized_resource('ScrudResource', 'de', 'tax_master_code', 'Steuerbasis Code');--Tax Master Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'tax_master_id', 'Steuerbasis Id');--Tax Master Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'tax_master_name', 'Steuerbasis Name');--Tax Master Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'tax_name', 'Steuerbezeichnung');--Tax Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'tax_rate_type', 'Steuersatz Typ');--Tax Rate Type
SELECT localization.add_localized_resource('ScrudResource', 'de', 'tax_rate_type_code', 'Steuersatz Typ Code');--Tax Rate Type Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'tax_rate_type_name', 'Steuersatz Typ Name');--Tax Rate Type Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'tax_type_code', 'Steuerart-Code');--Tax Type Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'tax_type_id', 'Steuerart Id');--Tax Type Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'tax_type_name', 'Steuerart Name');--Tax Type Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'to_days', 'Um Tage');--To Days
SELECT localization.add_localized_resource('ScrudResource', 'de', 'total', 'Gesamt');--Total
SELECT localization.add_localized_resource('ScrudResource', 'de', 'total_duration', 'Gesamtdauer');--Total Duration
SELECT localization.add_localized_resource('ScrudResource', 'de', 'total_sales', 'Gesamtumsatz');--Total Sales
SELECT localization.add_localized_resource('ScrudResource', 'de', 'tran_code', 'Transaktion-Code');--Tran Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'tran_type', 'Transaktionsart');--Tran Type
SELECT localization.add_localized_resource('ScrudResource', 'de', 'unit', 'Einheit');--Unit
SELECT localization.add_localized_resource('ScrudResource', 'de', 'unit_code', 'Einheit Code');--Unit Code
SELECT localization.add_localized_resource('ScrudResource', 'de', 'unit_id', 'Einheit Id');--Unit Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'unit_name', 'Einheit Name');--Unit Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'url', 'Url');--Url
SELECT localization.add_localized_resource('ScrudResource', 'de', 'use_tax_collecting_account', 'Nutzer- Steuer Sammelkonto');--Use Tax Collecting Account
SELECT localization.add_localized_resource('ScrudResource', 'de', 'use_tax_collecting_account_id', 'Nutzer-  Steuer Sammelkonto Id');--Use Tax Collecting Account Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'user_id', 'User Id');--User Id
SELECT localization.add_localized_resource('ScrudResource', 'de', 'user_name', 'Benutzername');--User Name
SELECT localization.add_localized_resource('ScrudResource', 'de', 'vacuum_count', 'Vakuum-Count');--Vacuum Count
SELECT localization.add_localized_resource('ScrudResource', 'de', 'valid_from', 'Gültig ab');--Valid From
SELECT localization.add_localized_resource('ScrudResource', 'de', 'valid_till', 'Gültig bis');--Valid Till
SELECT localization.add_localized_resource('ScrudResource', 'de', 'value', 'Wert');--Value
SELECT localization.add_localized_resource('ScrudResource', 'de', 'value_date', 'Abrechnungsdatum');--Value Date
SELECT localization.add_localized_resource('ScrudResource', 'de', 'verify_gl_transactions', 'Hauptbuchtransaktion Verifizieren');--Verify Gl Transactions
SELECT localization.add_localized_resource('ScrudResource', 'de', 'verify_purchase_transactions', 'Einkaufstansaktion Verifizieren');--Verify Purchase Transactions
SELECT localization.add_localized_resource('ScrudResource', 'de', 'verify_sales_transactions', ' Verkaufstransaktionen Verifizieren');--Verify Sales Transactions
SELECT localization.add_localized_resource('ScrudResource', 'de', 'weight_in_grams', 'Gewicht in Gramm');--Weight In Grams
SELECT localization.add_localized_resource('ScrudResource', 'de', 'width_in_centimeters', 'Breite in cm');--Width In Centimeters
SELECT localization.add_localized_resource('ScrudResource', 'de', 'zip_code', 'Postleitzahl');--Zip Code
SELECT localization.add_localized_resource('Titles', 'de', 'AboutInitializingDayEnd', 'Über: Tagesabrechnung  starten');--About Initializing Day End
SELECT localization.add_localized_resource('Titles', 'de', 'AboutYourOffice', 'Über Ihr Office');--About Your Office
SELECT localization.add_localized_resource('Titles', 'de', 'Access', 'Zugriff');--Access
SELECT localization.add_localized_resource('Titles', 'de', 'AccessIsDenied', 'Zugriff  verweigert.');--Access is denied.
SELECT localization.add_localized_resource('Titles', 'de', 'Account', 'Konto');--Account
SELECT localization.add_localized_resource('Titles', 'de', 'AccountId', 'Konto-ID');--Account Id
SELECT localization.add_localized_resource('Titles', 'de', 'AccountMaster', 'Kontenstamm');--Account Master
SELECT localization.add_localized_resource('Titles', 'de', 'AccountName', 'Kontobezeichnung');--Account Name
SELECT localization.add_localized_resource('Titles', 'de', 'AccountNumber', 'Kontonummer');--Account Number
SELECT localization.add_localized_resource('Titles', 'de', 'AccountOverview', 'Kontoübersicht');--Account Overview
SELECT localization.add_localized_resource('Titles', 'de', 'AccountStatement', 'Kontoauszug');--Account Statement
SELECT localization.add_localized_resource('Titles', 'de', 'Action', 'Aktion');--Action
SELECT localization.add_localized_resource('Titles', 'de', 'Actions', 'Aktionen');--Actions
SELECT localization.add_localized_resource('Titles', 'de', 'Actual', 'Aktuell');--Actual
SELECT localization.add_localized_resource('Titles', 'de', 'Add', 'Hinzufügen');--Add
SELECT localization.add_localized_resource('Titles', 'de', 'AddNew', 'Neu hinzufügen');--Add New
SELECT localization.add_localized_resource('Titles', 'de', 'Address', 'Anschrift');--Address
SELECT localization.add_localized_resource('Titles', 'de', 'AddressAndContactInfo', 'Adresse und Kontaktinformationen');--Address & Contact Information
SELECT localization.add_localized_resource('Titles', 'de', 'AgeingSlabs', 'Alterungstafel');--Ageing Slabs
SELECT localization.add_localized_resource('Titles', 'de', 'AgentBonusSlabAssignment', 'Bonustafel  Zuordnung');--Bonus Slab Assignment
SELECT localization.add_localized_resource('Titles', 'de', 'AgentBonusSlabs', 'Bonustafel für verkäufer');--Bonus Slab for Salespersons
SELECT localization.add_localized_resource('Titles', 'de', 'Alerts', 'Benachrichtigungen');--Alerts
SELECT localization.add_localized_resource('Titles', 'de', 'Amount', 'Betrag');--Amount
SELECT localization.add_localized_resource('Titles', 'de', 'AmountInBaseCurrency', 'Betrag (in Basiswährung)');--Amount (In Base Currency)
SELECT localization.add_localized_resource('Titles', 'de', 'AmountInHomeCurrency', 'Betrag (in Landeswährung)');--Amount (In Home Currency)
SELECT localization.add_localized_resource('Titles', 'de', 'AnalyzeDatabse', 'Datenbakanalyse');--Analyze Databse
SELECT localization.add_localized_resource('Titles', 'de', 'Approve', 'Genehmigen');--Approve
SELECT localization.add_localized_resource('Titles', 'de', 'ApproveThisTransaction', 'Genehmigen dieser Transaktion');--Approve This Transaction
SELECT localization.add_localized_resource('Titles', 'de', 'ApprovedTransactions', 'Genehmigte Transaktionen');--Approved Transactions
SELECT localization.add_localized_resource('Titles', 'de', 'AreYouSure', 'Sind Sie sicher?');--Are you sure?
SELECT localization.add_localized_resource('Titles', 'de', 'AssignCashier', 'Kassierer zuweisen');--Assign Cashier
SELECT localization.add_localized_resource('Titles', 'de', 'AttachmentsPlus', 'Anhänge (+)');--Attachments (+)
SELECT localization.add_localized_resource('Titles', 'de', 'AutoVerificationPolicy', 'Automatische Überprüfung Richtlinie');--Autoverification Policy
SELECT localization.add_localized_resource('Titles', 'de', 'AutomaticallyApprovedByWorkflow', 'Automatisch vom Workflow-zugelassen');--Automatically Approved by Workflow
SELECT localization.add_localized_resource('Titles', 'de', 'Back', 'Zurück');--Back
SELECT localization.add_localized_resource('Titles', 'de', 'BackToPreviousPage', 'Vorhergehende Seite');--Back to Previous Page
SELECT localization.add_localized_resource('Titles', 'de', 'BackupConsole', 'Backup-Konsole');--Backup Console
SELECT localization.add_localized_resource('Titles', 'de', 'BackupDatabase', 'Datenbank sichern');--Backup Database
SELECT localization.add_localized_resource('Titles', 'de', 'BackupNow', 'Jetzt sichern');--Backup Now
SELECT localization.add_localized_resource('Titles', 'de', 'Balance', 'Kontostand');--Balance
SELECT localization.add_localized_resource('Titles', 'de', 'BalanceSheet', 'Bilanz');--Balance Sheet
SELECT localization.add_localized_resource('Titles', 'de', 'BankAccounts', 'Bankkonten');--Bank Accounts
SELECT localization.add_localized_resource('Titles', 'de', 'BankTransactionCode', 'Banktransaktionscode');--Bank Transaction Code
SELECT localization.add_localized_resource('Titles', 'de', 'BaseCurrency', 'Grundwährung');--Base Currency
SELECT localization.add_localized_resource('Titles', 'de', 'BaseUnitName', 'Grundeinheit Name');--Base Unit Name
SELECT localization.add_localized_resource('Titles', 'de', 'BonusSlabDetails', 'Bonustafel Verkäufer Details');--Bonus Slab Details for Salespersons
SELECT localization.add_localized_resource('Titles', 'de', 'Book', 'Buch');--Book
SELECT localization.add_localized_resource('Titles', 'de', 'BookDate', 'Buchungsdatum');--Book Date
SELECT localization.add_localized_resource('Titles', 'de', 'Brand', 'Marke');--Brand
SELECT localization.add_localized_resource('Titles', 'de', 'Brands', 'Marken');--Brands
SELECT localization.add_localized_resource('Titles', 'de', 'Browse', 'Blättern');--Browse
SELECT localization.add_localized_resource('Titles', 'de', 'CSTNumber', 'CST Nummer');--CST Number
SELECT localization.add_localized_resource('Titles', 'de', 'Cancel', 'Abbrechen');--Cancel
SELECT localization.add_localized_resource('Titles', 'de', 'CashFlowHeading', 'Cash Flow Überschrift');--Cash Flow Heading
SELECT localization.add_localized_resource('Titles', 'de', 'CashFlowHeadings', 'Cash Flow Überschriften');--Cash Flow Headings
SELECT localization.add_localized_resource('Titles', 'de', 'CashFlowSetup', 'Cash Flow-Setup');--Cash Flow Setup
SELECT localization.add_localized_resource('Titles', 'de', 'CashRepositories', 'Barwerte-Depots');--Cash Repositories
SELECT localization.add_localized_resource('Titles', 'de', 'CashRepository', 'Barwerte-Depot');--Cash Repository
SELECT localization.add_localized_resource('Titles', 'de', 'CashRepositoryBalance', 'Barwerte-Depot Kontostand');--Cash Repository Balance
SELECT localization.add_localized_resource('Titles', 'de', 'CashTransaction', 'Geldtransaktionen');--Cash Transaction
SELECT localization.add_localized_resource('Titles', 'de', 'ChangePassword', 'Passwort ändern');--Change Password
SELECT localization.add_localized_resource('Titles', 'de', 'ChangeSideWhenNegative', 'Seite wechseln falls negative (Soll-Haben)');--Change Side When Negative
SELECT localization.add_localized_resource('Titles', 'de', 'ChartOfAccounts', 'Kontenplan');--Chart of Accounts
SELECT localization.add_localized_resource('Titles', 'de', 'Check', 'Anhaken');--Check
SELECT localization.add_localized_resource('Titles', 'de', 'CheckAll', 'Alle auswählen');--Check All
SELECT localization.add_localized_resource('Titles', 'de', 'Checklists', 'Checklisten');--Checklists
SELECT localization.add_localized_resource('Titles', 'de', 'Clear', 'Löschen');--Clear
SELECT localization.add_localized_resource('Titles', 'de', 'Close', 'Schliessen');--Close
SELECT localization.add_localized_resource('Titles', 'de', 'ClosedTransactions', 'Geschlossene Transaktionen');--Closed Transactions
SELECT localization.add_localized_resource('Titles', 'de', 'ClosingBalance', 'Endbestand');--Closing Balance
SELECT localization.add_localized_resource('Titles', 'de', 'ClosingCredit', 'Abschluss Haben');--Closing Credit
SELECT localization.add_localized_resource('Titles', 'de', 'ClosingDebit', 'Abschluss Soll');--Closing Debit
SELECT localization.add_localized_resource('Titles', 'de', 'Comment', 'Kommentar');--Comment
SELECT localization.add_localized_resource('Titles', 'de', 'CompoundItemDetails', 'Kombi Artikel Details');--Compound Item Details
SELECT localization.add_localized_resource('Titles', 'de', 'CompoundItems', 'Kombi Artikel');--Compound Items
SELECT localization.add_localized_resource('Titles', 'de', 'CompoundUnitsOfMeasure', 'Erweiterte Masseinheiten');--Compound Units of Measure
SELECT localization.add_localized_resource('Titles', 'de', 'Confidential', 'Vertraulich');--Confidential
SELECT localization.add_localized_resource('Titles', 'de', 'ConfirmPassword', 'Passwort bestätigen');--Confirm Password
SELECT localization.add_localized_resource('Titles', 'de', 'ConvertedtoBaseCurrency', 'Umgerechnet auf Grundwährung');--Converted to Base Currency
SELECT localization.add_localized_resource('Titles', 'de', 'ConvertedtoHomeCurrency', 'Umgerechnet auf Landeswährung');--Converted to Home Currency
SELECT localization.add_localized_resource('Titles', 'de', 'CostCenter', 'Kostenstelle');--Cost Center
SELECT localization.add_localized_resource('Titles', 'de', 'CostCenters', 'Kostenstellen');--Cost Centers
SELECT localization.add_localized_resource('Titles', 'de', 'Counters', 'Kassen');--Counters
SELECT localization.add_localized_resource('Titles', 'de', 'Counties', 'Bezirk');--Counties
SELECT localization.add_localized_resource('Titles', 'de', 'Countries', 'Länder');--Countries
SELECT localization.add_localized_resource('Titles', 'de', 'CountySalesTaxes', 'Bezirks Verkaufssteuern');--County Sales Taxes
SELECT localization.add_localized_resource('Titles', 'de', 'CreateaUserAccountforYourself', 'Erstellen Sie ein Benutzerkonto für sich selbst');--Create a User Account for Yourself
SELECT localization.add_localized_resource('Titles', 'de', 'CreatedOn', 'Erstellt Am');--Created On
SELECT localization.add_localized_resource('Titles', 'de', 'Credit', 'Haben');--Credit
SELECT localization.add_localized_resource('Titles', 'de', 'CreditAllowed', 'Genehmigter Kredit');--Credit Allowed
SELECT localization.add_localized_resource('Titles', 'de', 'CreditTotal', 'Haben Total');--Credit Total
SELECT localization.add_localized_resource('Titles', 'de', 'CtrlAltA', 'Ctrl + Alt + A');--Ctrl + Alt + A
SELECT localization.add_localized_resource('Titles', 'de', 'CtrlAltC', 'Ctrl + Alt + C');--Ctrl + Alt + C
SELECT localization.add_localized_resource('Titles', 'de', 'CtrlAltD', 'Ctrl + Alt + D');--Ctrl + Alt + D
SELECT localization.add_localized_resource('Titles', 'de', 'CtrlAltS', 'Ctrl + Alt + S');--Ctrl + Alt + S
SELECT localization.add_localized_resource('Titles', 'de', 'CtrlAltT', 'Ctrl + Alt + T');--Ctrl + Alt + T
SELECT localization.add_localized_resource('Titles', 'de', 'CtrlReturn', 'Ctrl + Return');--Ctrl + Return
SELECT localization.add_localized_resource('Titles', 'de', 'Currencies', 'Währungen');--Currencies
SELECT localization.add_localized_resource('Titles', 'de', 'Currency', 'Währung');--Currency
SELECT localization.add_localized_resource('Titles', 'de', 'CurrencyCode', 'Währungscode');--Currency Code
SELECT localization.add_localized_resource('Titles', 'de', 'CurrencyName', 'Währungsbezeichnung');--Currency Name
SELECT localization.add_localized_resource('Titles', 'de', 'CurrencySymbol', 'Währungssymbol');--Currency Symbol
SELECT localization.add_localized_resource('Titles', 'de', 'CurrentBookDate', 'Aktuelles Buchungsdatum');--Current Book Date
SELECT localization.add_localized_resource('Titles', 'de', 'CurrentIP', 'Aktuelle IP-');--Current IP
SELECT localization.add_localized_resource('Titles', 'de', 'CurrentLoginOn', 'Aktuell angemeldet');--Current Login On
SELECT localization.add_localized_resource('Titles', 'de', 'CurrentPassword', 'Aktuelles Passwort');--Current Password
SELECT localization.add_localized_resource('Titles', 'de', 'CurrentPeriod', 'Aktueller Zeitraum');--Current Period
SELECT localization.add_localized_resource('Titles', 'de', 'CustomerCode', 'Kundencode');--Customer Code
SELECT localization.add_localized_resource('Titles', 'de', 'CustomerName', 'Kundenname');--Customer Name
SELECT localization.add_localized_resource('Titles', 'de', 'CustomerPanNumber', 'Kunden PAN #');--Customer PAN #
SELECT localization.add_localized_resource('Titles', 'de', 'CustomerPaysFees', 'Gebühr trägt Kunde');--Customer Pays Fees
SELECT localization.add_localized_resource('Titles', 'de', 'DatabaseBackups', 'Datenbanksicherungen');--Database Backups
SELECT localization.add_localized_resource('Titles', 'de', 'DatabaseStatistics', 'Datenbankstatistik');--Database Statistics
SELECT localization.add_localized_resource('Titles', 'de', 'Date', 'Datum');--Date
SELECT localization.add_localized_resource('Titles', 'de', 'Day', 'Tag');--Day
SELECT localization.add_localized_resource('Titles', 'de', 'Days', 'Tage');--Days
SELECT localization.add_localized_resource('Titles', 'de', 'Debit', 'Soll');--Debit
SELECT localization.add_localized_resource('Titles', 'de', 'DebitTotal', 'Soll Gesamt');--Debit Total
SELECT localization.add_localized_resource('Titles', 'de', 'DefaultAddress', 'Standardadresse');--Default Address
SELECT localization.add_localized_resource('Titles', 'de', 'DefaultCurrency', 'Standardwährung');--Default Currency
SELECT localization.add_localized_resource('Titles', 'de', 'DefaultReorderQuantityAbbreviated', 'Standard Bestellmenge');--Default Reorder Qty
SELECT localization.add_localized_resource('Titles', 'de', 'Definition', 'Definition');--Definition
SELECT localization.add_localized_resource('Titles', 'de', 'Delete', 'löschen');--Delete
SELECT localization.add_localized_resource('Titles', 'de', 'DeleteSelected', 'Auswahl löschen');--Delete Selected
SELECT localization.add_localized_resource('Titles', 'de', 'DeliverTo', 'Liefern an');--Deliver To
SELECT localization.add_localized_resource('Titles', 'de', 'Department', 'Abteilung');--Department
SELECT localization.add_localized_resource('Titles', 'de', 'Departments', 'Abteilungen');--Departments
SELECT localization.add_localized_resource('Titles', 'de', 'Difference', 'Unterschied');--Difference
SELECT localization.add_localized_resource('Titles', 'de', 'DirectPurchase', 'Bareinkauf');--Direct Purchase
SELECT localization.add_localized_resource('Titles', 'de', 'DirectSales', 'Direktvertrieb');--Direct Sales
SELECT localization.add_localized_resource('Titles', 'de', 'Discount', 'Rabatt');--Discount
SELECT localization.add_localized_resource('Titles', 'de', 'Documentation', 'Dokumentation');--Documentation
SELECT localization.add_localized_resource('Titles', 'de', 'Download', 'Download');--Download
SELECT localization.add_localized_resource('Titles', 'de', 'DownloadSourceCode', 'Quellcode-Code Download');--Download Source Code
SELECT localization.add_localized_resource('Titles', 'de', 'DueDate', 'Fälligkeitsdatum');--Due Date
SELECT localization.add_localized_resource('Titles', 'de', 'EODBegun', 'Tagesabschluß hat begonnen');--End of Day Processing Has Begun
SELECT localization.add_localized_resource('Titles', 'de', 'EODConsole', 'Tagesabschluß  Console');--EOD Console
SELECT localization.add_localized_resource('Titles', 'de', 'ER', 'Wechselkurs');--ER
SELECT localization.add_localized_resource('Titles', 'de', 'ERToBaseCurrency', 'Wechselkurs (zu Basiswährung)');--Exchange Rate (To Base Currency)
SELECT localization.add_localized_resource('Titles', 'de', 'ERToHomeCurrency', 'Wechselkurs (To Home Währung)');--Exchange Rate (To Home Currency)
SELECT localization.add_localized_resource('Titles', 'de', 'EditSelected', 'Auswahl bearbeiten');--Edit Selected
SELECT localization.add_localized_resource('Titles', 'de', 'Email', 'E-Mail');--Email
SELECT localization.add_localized_resource('Titles', 'de', 'EmailAddress', 'E-Mail-Addresse');--Email Address
SELECT localization.add_localized_resource('Titles', 'de', 'EmailThisDelivery', 'Email für diese Lieferung');--Email This Delivery
SELECT localization.add_localized_resource('Titles', 'de', 'EmailThisInvoice', 'Email für diese Rechnung');--Email This Invoice
SELECT localization.add_localized_resource('Titles', 'de', 'EmailThisNote', 'Email für diesen Hinweis');--Email This Note
SELECT localization.add_localized_resource('Titles', 'de', 'EmailThisOrder', 'Email für diese  Bestellung');--Email This Order
SELECT localization.add_localized_resource('Titles', 'de', 'EmailThisQuotation', 'E-Mail für dieses Offert');--Email This Quotation
SELECT localization.add_localized_resource('Titles', 'de', 'EmailThisReceipt', 'Email der Quittung');--Email This Receipt
SELECT localization.add_localized_resource('Titles', 'de', 'EmailThisReturn', 'Email der Rücksendung');--Email This Return
SELECT localization.add_localized_resource('Titles', 'de', 'EndOfDayOperation', 'Tagesabschluß Aktion');--End of Day Operation
SELECT localization.add_localized_resource('Titles', 'de', 'EnterBackupName', 'Sicherungsdateinamen eingeben');--Enter Backup Name
SELECT localization.add_localized_resource('Titles', 'de', 'EnterNewPassword', 'Neues Passwort eingeben');--Enter a New Password
SELECT localization.add_localized_resource('Titles', 'de', 'EnteredBy', 'eingetragen von');--Entered By
SELECT localization.add_localized_resource('Titles', 'de', 'Entities', 'Körperschaften');--Entities
SELECT localization.add_localized_resource('Titles', 'de', 'ExchangeRate', 'Wechselkurs');--Exchange Rate
SELECT localization.add_localized_resource('Titles', 'de', 'Execute', 'ausführen');--Execute
SELECT localization.add_localized_resource('Titles', 'de', 'ExternalCode', 'Externer-Code');--External Code
SELECT localization.add_localized_resource('Titles', 'de', 'Factor', 'Faktor');--Factor
SELECT localization.add_localized_resource('Titles', 'de', 'Fax', 'Fax');--Fax
SELECT localization.add_localized_resource('Titles', 'de', 'FilePath', 'Dateipfad');--File Path
SELECT localization.add_localized_resource('Titles', 'de', 'FinalDueAmountinBaseCurrency', 'Restschuld ( in Grundwährung )');--Final Due Amount in Base Currency
SELECT localization.add_localized_resource('Titles', 'de', 'FirstPage', 'Erste Seite');--First Page
SELECT localization.add_localized_resource('Titles', 'de', 'FiscalYear', 'Geschäftsjahr');--Fiscal Year
SELECT localization.add_localized_resource('Titles', 'de', 'Flag', 'Markierung');--Flag
SELECT localization.add_localized_resource('Titles', 'de', 'FlagBackgroundColor', 'Markierung Hintergrundfarbe');--Flag Background Color
SELECT localization.add_localized_resource('Titles', 'de', 'FlagDescription', 'Sie können diese Transaktion markieren, aber Sie werden nicht in der Lage sein, die Markierungen von anderen Benutzern zu sehen.');--You can mark this transaction with a flag, however you will not be able to see the flags created by other users.
SELECT localization.add_localized_resource('Titles', 'de', 'FlagForegroundColor', 'Markierung Vordergrundfarbe');--Flag Foreground Color
SELECT localization.add_localized_resource('Titles', 'de', 'FlagThisTransaction', 'Markiere diese Transaktions');--Flag This Transaction
SELECT localization.add_localized_resource('Titles', 'de', 'FlaggedTransactions', 'Markierte Transaktionen');--Flagged Transactions
SELECT localization.add_localized_resource('Titles', 'de', 'Flags', 'Markierungen');--Flags
SELECT localization.add_localized_resource('Titles', 'de', 'Frequencies', 'Perioden');--Frequencies
SELECT localization.add_localized_resource('Titles', 'de', 'From', 'Von');--From
SELECT localization.add_localized_resource('Titles', 'de', 'GLAdvice', 'Hinweise zum Hauptbuch');--GL Advice
SELECT localization.add_localized_resource('Titles', 'de', 'GLDetails', 'Hauptbuch Details');--GL Details
SELECT localization.add_localized_resource('Titles', 'de', 'GLHead', 'Hauptbuch');--GL Head
SELECT localization.add_localized_resource('Titles', 'de', 'Go', 'Gehen');--Go
SELECT localization.add_localized_resource('Titles', 'de', 'GoToTop', 'Nach Oben');--GoToTop
SELECT localization.add_localized_resource('Titles', 'de', 'GoodsReceiptNote', 'Wareneingang Notiz');--Goods Receipt Note
SELECT localization.add_localized_resource('Titles', 'de', 'GrandTotal', 'Gesamtsumme');--Grand Total
SELECT localization.add_localized_resource('Titles', 'de', 'Home', 'Startseite');--Home
SELECT localization.add_localized_resource('Titles', 'de', 'HomeCurrency', 'Landeswährung');--Home Currency
SELECT localization.add_localized_resource('Titles', 'de', 'HundredthName', 'Untereinheit');--Hundredth Name
SELECT localization.add_localized_resource('Titles', 'de', 'Id', 'Id');--Id
SELECT localization.add_localized_resource('Titles', 'de', 'InVerificationStack', 'Im Verifizierungs Stapel');--In Verification Stack
SELECT localization.add_localized_resource('Titles', 'de', 'IncludeZeroBalanceAccounts', 'Nullsalden Konten einschließen');--Include Zero Balance Accounts
SELECT localization.add_localized_resource('Titles', 'de', 'Industries', 'Branchen');--Industries
SELECT localization.add_localized_resource('Titles', 'de', 'InitializeDayEnd', 'Tagesabschluß starten');--Initialize Day End
SELECT localization.add_localized_resource('Titles', 'de', 'InstallMixERP', 'MixERP  Installieren');--Install MixERP
SELECT localization.add_localized_resource('Titles', 'de', 'InstrumentCode', 'Instrument-Code');--Instrument Code
SELECT localization.add_localized_resource('Titles', 'de', 'InterestApplicable', 'Verzinsung anwendbar');--Interest Applicable
SELECT localization.add_localized_resource('Titles', 'de', 'InvalidDate', 'Dies ist kein gültiges Datum aus.');--This is not a valid date.
SELECT localization.add_localized_resource('Titles', 'de', 'InvalidImage', 'Dies ist kein gültiges Bild.');--This is not a valid image.
SELECT localization.add_localized_resource('Titles', 'de', 'InventoryAdvice', 'Inventar Hinweise');--Inventory Advice
SELECT localization.add_localized_resource('Titles', 'de', 'InvoiceAmount', 'Rechnungsbetrag');--Invoice Amount
SELECT localization.add_localized_resource('Titles', 'de', 'InvoiceDetails', 'Rechnungsdetails');--Invoice Details
SELECT localization.add_localized_resource('Titles', 'de', 'IsCash', 'Ist Barzahlung');--Is Cash
SELECT localization.add_localized_resource('Titles', 'de', 'IsEmployee', 'Ist Mitarbeiter');--Is Employee
SELECT localization.add_localized_resource('Titles', 'de', 'IsParty', 'Ist Partei');--Is Party
SELECT localization.add_localized_resource('Titles', 'de', 'IsSystemAccount', 'Ist Systemkonto');--Is System Account
SELECT localization.add_localized_resource('Titles', 'de', 'ItemCode', 'Artikel Code');--Item Code
SELECT localization.add_localized_resource('Titles', 'de', 'ItemCostPrices', 'Artikelkosten Preise');--Item Cost Prices
SELECT localization.add_localized_resource('Titles', 'de', 'ItemGroup', 'Artikelgruppe');--Item Group
SELECT localization.add_localized_resource('Titles', 'de', 'ItemGroups', 'Artikelgruppen');--Item Groups
SELECT localization.add_localized_resource('Titles', 'de', 'ItemId', 'Artikel-Id');--Item Id
SELECT localization.add_localized_resource('Titles', 'de', 'ItemName', 'Artikelname');--Item Name
SELECT localization.add_localized_resource('Titles', 'de', 'ItemOverview', 'Artikelübersicht');--Item Overview
SELECT localization.add_localized_resource('Titles', 'de', 'ItemSellingPrices', 'Artikel Verkaufs Preise');--Item Selling Prices
SELECT localization.add_localized_resource('Titles', 'de', 'ItemType', 'Artikel Typ');--Item Type
SELECT localization.add_localized_resource('Titles', 'de', 'ItemTypes', 'Artikel Typen');--Item Types
SELECT localization.add_localized_resource('Titles', 'de', 'Items', 'Artikel');--Items
SELECT localization.add_localized_resource('Titles', 'de', 'ItemsBelowReorderLevel', 'Artikel unter Mindestbestand');--Items Below Reorder Level
SELECT localization.add_localized_resource('Titles', 'de', 'JournalVoucher', 'Journal Voucher');--Journal Voucher
SELECT localization.add_localized_resource('Titles', 'de', 'JournalVoucherEntry', 'Journal Voucher Eintrrag');--Journal Voucher Entry
SELECT localization.add_localized_resource('Titles', 'de', 'KeyColumnEmptyExceptionMessage', 'Die "Schlüssel Spalte" kann nicht leer bleiben.');--The property 'KeyColumn' cannot be left empty.
SELECT localization.add_localized_resource('Titles', 'de', 'LCCredit', 'EU Haben');--LC Credit
SELECT localization.add_localized_resource('Titles', 'de', 'LCDebit', 'EU Soll');--LC Debit
SELECT localization.add_localized_resource('Titles', 'de', 'LastAccessedOn', 'Datum des letzten Zugriffs');--Last Accessed On
SELECT localization.add_localized_resource('Titles', 'de', 'LastLoginIP', 'Letzte Anmeldung IP');--Last Login IP
SELECT localization.add_localized_resource('Titles', 'de', 'LastLoginOn', 'Letzte Anmeldung am');--Last Login On
SELECT localization.add_localized_resource('Titles', 'de', 'LastPage', 'Letzte Seite');--Last Page
SELECT localization.add_localized_resource('Titles', 'de', 'LastPaymentDate', 'Letztes Auszahlungsdatum');--Last Payment Date
SELECT localization.add_localized_resource('Titles', 'de', 'LastWrittenOn', 'Zuletzt geschrieben auf');--Last Written On
SELECT localization.add_localized_resource('Titles', 'de', 'LateFees', 'Säumniszuschlag');--Late Fees
SELECT localization.add_localized_resource('Titles', 'de', 'LeadSources', 'Werbe-Quellen');--Lead Sources
SELECT localization.add_localized_resource('Titles', 'de', 'LeadStatuses', 'Werber Status');--Lead Statuses
SELECT localization.add_localized_resource('Titles', 'de', 'LeadTime', 'Vorlaufzeit');--Lead Time
SELECT localization.add_localized_resource('Titles', 'de', 'ListItems', 'Listenelemente');--List Items
SELECT localization.add_localized_resource('Titles', 'de', 'Load', 'Last');--Load
SELECT localization.add_localized_resource('Titles', 'de', 'LoggedInTo', 'Angemeldet Um');--Logged in to
SELECT localization.add_localized_resource('Titles', 'de', 'LoginView', 'Login Ansucht');--Login View
SELECT localization.add_localized_resource('Titles', 'de', 'ManageProfile', 'Profil verwalten');--Manage Profile
SELECT localization.add_localized_resource('Titles', 'de', 'MaximumCreditAmount', 'Maximaler Kreditbetrag');--Maximum Credit Amount
SELECT localization.add_localized_resource('Titles', 'de', 'MaximumCreditPeriod', 'Maximale Kreditlaufzeit');--Maximum Credit Period
SELECT localization.add_localized_resource('Titles', 'de', 'MenuAccessPolicy', 'Menü-Zugriffs Richtlinien');--Menu Access Policy
SELECT localization.add_localized_resource('Titles', 'de', 'MenuCode', 'Menü-Code');--Menu Code
SELECT localization.add_localized_resource('Titles', 'de', 'MenuId', 'Meuü Id');--Menu Id
SELECT localization.add_localized_resource('Titles', 'de', 'MenuText', 'Menütext');--Menu Text
SELECT localization.add_localized_resource('Titles', 'de', 'MerchantFeeInPercent', 'Händlergebühr (in Prozent)');--Merchant Fee (In percent)
SELECT localization.add_localized_resource('Titles', 'de', 'MerchantFeeSetup', 'Händler Gebühr-Setup');--Merchant Fee Setup
SELECT localization.add_localized_resource('Titles', 'de', 'MergeBatchToGRN', 'Stapel  mit  Lieferschein zusammenführen');--Merge Batch to GRN
SELECT localization.add_localized_resource('Titles', 'de', 'MergeBatchToSalesDelivery', 'Stapel mit Ausslieferung zusammenführen');--Merge Batch to Sales Delivery
SELECT localization.add_localized_resource('Titles', 'de', 'MergeBatchToSalesOrder', 'Stapel mit Kundenbestellunng  zusammenführen');--Merge Batch to Sales Order
SELECT localization.add_localized_resource('Titles', 'de', 'MixERPDocumentation', 'MixERP Dokumentation');--MixERP Documentation
SELECT localization.add_localized_resource('Titles', 'de', 'MixERPLinks', 'MixERP-Links');--MixERP Links
SELECT localization.add_localized_resource('Titles', 'de', 'MixERPOnFacebook', 'MixERP auf Facebook');--MixERP on Facebook
SELECT localization.add_localized_resource('Titles', 'de', 'Month', 'Monat');--Month
SELECT localization.add_localized_resource('Titles', 'de', 'Name', 'Name');--Name
SELECT localization.add_localized_resource('Titles', 'de', 'NewBookDate', 'Neues Buchungsdatum');--New Book Date
SELECT localization.add_localized_resource('Titles', 'de', 'NewJournalEntry', 'Neuer Journaleintrag');--New Journal Entry
SELECT localization.add_localized_resource('Titles', 'de', 'NewPassword', 'Neues Passwort');--New Password
SELECT localization.add_localized_resource('Titles', 'de', 'NextPage', 'Nächste Seite');--Next Page
SELECT localization.add_localized_resource('Titles', 'de', 'No', 'Nein');--No
SELECT localization.add_localized_resource('Titles', 'de', 'NonTaxableSales', 'Steuerfreiie Verkäufe');--Nontaxable Sales
SELECT localization.add_localized_resource('Titles', 'de', 'None', 'Keine');--None
SELECT localization.add_localized_resource('Titles', 'de', 'NormallyDebit', 'Normalerweise Soll');--Normally Debit
SELECT localization.add_localized_resource('Titles', 'de', 'NothingSelected', 'Nichts ausgewählt !');--Nothing selected!
SELECT localization.add_localized_resource('Titles', 'de', 'Notifications', 'Benachrichtigungen');--Notifications
SELECT localization.add_localized_resource('Titles', 'de', 'OK', 'OK');--OK
SELECT localization.add_localized_resource('Titles', 'de', 'Office', 'Office');--Office
SELECT localization.add_localized_resource('Titles', 'de', 'OfficeCode', 'Office-Code');--Office Code
SELECT localization.add_localized_resource('Titles', 'de', 'OfficeInformation', 'Office Information');--Office Information
SELECT localization.add_localized_resource('Titles', 'de', 'OfficeName', 'Officename');--Office Name
SELECT localization.add_localized_resource('Titles', 'de', 'OfficeNickName', 'Office - Nickname');--Office Nick Name
SELECT localization.add_localized_resource('Titles', 'de', 'OfficeSetup', 'Office Einrichtung');--Office Setup
SELECT localization.add_localized_resource('Titles', 'de', 'OnlyNumbersAllowed', 'Bitte geben Sie eine gültige Zahl ein.');--Please type a valid number.
SELECT localization.add_localized_resource('Titles', 'de', 'OpeningInventory', 'Eröffnungsinvetar');--Opening Inventory
SELECT localization.add_localized_resource('Titles', 'de', 'OpportunityStages', 'Chancen Stufen');--Opportunity Stages
SELECT localization.add_localized_resource('Titles', 'de', 'OtherDetails', 'Weitere Einzelheiten');--Other Details
SELECT localization.add_localized_resource('Titles', 'de', 'PANNumber', 'PAN Nummer');--PAN Number
SELECT localization.add_localized_resource('Titles', 'de', 'PageN', 'Seite {0}');--Page {0}
SELECT localization.add_localized_resource('Titles', 'de', 'ParentAccount', 'Muttergesellschaft Konto');--Parent Account
SELECT localization.add_localized_resource('Titles', 'de', 'Parties', 'Parteien');--Parties
SELECT localization.add_localized_resource('Titles', 'de', 'Party', 'Partei');--Party
SELECT localization.add_localized_resource('Titles', 'de', 'PartyCode', 'Partei-Code');--Party Code
SELECT localization.add_localized_resource('Titles', 'de', 'PartyName', 'Name der Partei');--Party Name
SELECT localization.add_localized_resource('Titles', 'de', 'PartySummary', 'Partei Zusammenfassung');--Party Summary
SELECT localization.add_localized_resource('Titles', 'de', 'PartyType', 'Partei Typ');--Party Type
SELECT localization.add_localized_resource('Titles', 'de', 'PartyTypes', 'Partei-Typen');--Party Types
SELECT localization.add_localized_resource('Titles', 'de', 'Password', 'Passwort');--Password
SELECT localization.add_localized_resource('Titles', 'de', 'PasswordUpdated', 'Kennwort wurde aktualisiert.');--Password was updated.
SELECT localization.add_localized_resource('Titles', 'de', 'PaymentCards', 'Kreditkarten');--Payment Cards
SELECT localization.add_localized_resource('Titles', 'de', 'PaymentTerms', 'Zahlungsbedingungen');--Payment Terms
SELECT localization.add_localized_resource('Titles', 'de', 'PerformEODOperation', 'Tagensabschluß durchführen.');--Perform EOD Operation
SELECT localization.add_localized_resource('Titles', 'de', 'PerformingEODOperation', 'Tagesabschluß wurd durchgeführt');--Performing EOD Operation
SELECT localization.add_localized_resource('Titles', 'de', 'Phone', 'Telefon');--Phone
SELECT localization.add_localized_resource('Titles', 'de', 'PlaceReorderRequests', 'Nachbestellungen durchführen.');--Place Reorder Request(s)
SELECT localization.add_localized_resource('Titles', 'de', 'PostTransaction', 'Transaktion durchführen');--Post Transaction
SELECT localization.add_localized_resource('Titles', 'de', 'PostedBy', 'Erstellt von');--Posted By
SELECT localization.add_localized_resource('Titles', 'de', 'PostedDate', 'Durchgeführt am');--Posted Date
SELECT localization.add_localized_resource('Titles', 'de', 'PreferredSupplier', 'Bevorzugter Lieferant');--Preferred Supplier
SELECT localization.add_localized_resource('Titles', 'de', 'PreferredSupplierIdAbbreviated', 'Bevorzugter Lieferant Kennung');--Pref SupId
SELECT localization.add_localized_resource('Titles', 'de', 'Prepare', 'Vorbereiten');--Prepare
SELECT localization.add_localized_resource('Titles', 'de', 'PreparedOn', 'Vorbereitet am');--Prepared On
SELECT localization.add_localized_resource('Titles', 'de', 'Preview', 'Vorschau');--Preview
SELECT localization.add_localized_resource('Titles', 'de', 'PreviousBalance', 'Vorherige Salden');--Previous Balance
SELECT localization.add_localized_resource('Titles', 'de', 'PreviousCredit', 'Vorheriges Haben');--Previous Credit
SELECT localization.add_localized_resource('Titles', 'de', 'PreviousDebit', 'Vorheriges Soll');--Previous Debit
SELECT localization.add_localized_resource('Titles', 'de', 'PreviousPage', 'Vorherige Seite');--Previous Page
SELECT localization.add_localized_resource('Titles', 'de', 'PreviousPeriod', 'Vorperiode');--Previous Period  
SELECT localization.add_localized_resource('Titles', 'de', 'Price', 'Preis');--Price
SELECT localization.add_localized_resource('Titles', 'de', 'PriceType', 'Preis Art');--Price Type
SELECT localization.add_localized_resource('Titles', 'de', 'Print', 'Drucken');--Print
SELECT localization.add_localized_resource('Titles', 'de', 'PrintGlEntry', 'Drucken Sachkonto Eintrag');--Print GL Entry
SELECT localization.add_localized_resource('Titles', 'de', 'PrintReceipt', 'Quittung drucken');--Print Receipt
SELECT localization.add_localized_resource('Titles', 'de', 'ProfitAndLossStatement', 'Gewinn- und Verlustrechnung');--Profit & Loss Statement
SELECT localization.add_localized_resource('Titles', 'de', 'Progress', 'Fortschritt');--Progress
SELECT localization.add_localized_resource('Titles', 'de', 'PurchaseInvoice', 'Einkaufsrechnung');--Purchase Invoice
SELECT localization.add_localized_resource('Titles', 'de', 'PurchaseOrder', 'Bestellung');--Purchase Order
SELECT localization.add_localized_resource('Titles', 'de', 'PurchaseReturn', 'Warenrücksendung');--Purchase Return
SELECT localization.add_localized_resource('Titles', 'de', 'PurchaseType', 'Einkauf Typ');--Purchase Type
SELECT localization.add_localized_resource('Titles', 'de', 'Quantity', 'Menge');--Quantity
SELECT localization.add_localized_resource('Titles', 'de', 'QuantityAbbreviated', 'Menge');--Qty
SELECT localization.add_localized_resource('Titles', 'de', 'QuantityOnHandAbbreviated', 'Menge (auf der Hand)');--Qty (On Hand)
SELECT localization.add_localized_resource('Titles', 'de', 'Rate', 'Quote');--Rate
SELECT localization.add_localized_resource('Titles', 'de', 'Reason', 'Grund');--Reason
SELECT localization.add_localized_resource('Titles', 'de', 'Receipt', 'Quittung');--Receipt
SELECT localization.add_localized_resource('Titles', 'de', 'ReceiptAmount', 'Quittingsbetrag');--Receipt Amount
SELECT localization.add_localized_resource('Titles', 'de', 'ReceiptCurrency', 'Quittungswährung');--Receipt Currency
SELECT localization.add_localized_resource('Titles', 'de', 'ReceiptType', 'Quittungs Typ');--Receipt Type
SELECT localization.add_localized_resource('Titles', 'de', 'ReceivedAmountInaboveCurrency', 'Empfangener Betrag (In obiger Währungs)');--Received Amount (In above Currency)
SELECT localization.add_localized_resource('Titles', 'de', 'ReceivedCurrency', 'Empfangene Währung');--Received Currency
SELECT localization.add_localized_resource('Titles', 'de', 'Reconcile', 'Kontenabstimmung');--Reconcile
SELECT localization.add_localized_resource('Titles', 'de', 'RecurringInvoiceSetup', 'Wiederkehrende Rechnung einrichten');--Recurring Invoice Setup
SELECT localization.add_localized_resource('Titles', 'de', 'RecurringInvoices', 'Wiederkehrende Rechnungen');--Recurring Invoices
SELECT localization.add_localized_resource('Titles', 'de', 'ReferenceNumber', 'Referenz-Nr #');--Reference Number
SELECT localization.add_localized_resource('Titles', 'de', 'ReferenceNumberAbbreviated', 'Referenz-Nr #');--Ref#
SELECT localization.add_localized_resource('Titles', 'de', 'RefererenceNumberAbbreviated', 'Referenz-Nr #');--Ref #
SELECT localization.add_localized_resource('Titles', 'de', 'RegistrationDate', 'Registrierungsdatum');--Registration Date
SELECT localization.add_localized_resource('Titles', 'de', 'Reject', 'Zurückweisen');--Reject
SELECT localization.add_localized_resource('Titles', 'de', 'RejectThisTransaction', 'Diese Transaktion zurückweisen');--Reject This Transaction
SELECT localization.add_localized_resource('Titles', 'de', 'RejectedTransactions', 'Zurückgewiesene Transaktionen');--Rejected Transactions
SELECT localization.add_localized_resource('Titles', 'de', 'RememberMe', 'Bitte Erinnern');--Remember Me
SELECT localization.add_localized_resource('Titles', 'de', 'ReorderLevel', 'Mindestbestand');--Reorder Level
SELECT localization.add_localized_resource('Titles', 'de', 'ReorderQuantityAbbreviated', 'Nachbestellung Menge');--Reorder Qty
SELECT localization.add_localized_resource('Titles', 'de', 'ReorderUnitName', 'Nachbestellungs Einheit Name');--Reorder Unit Name
SELECT localization.add_localized_resource('Titles', 'de', 'RequiredField', 'Dies ist ein Pflichtfeld.');--This is a required field.
SELECT localization.add_localized_resource('Titles', 'de', 'RequiredFieldDetails', 'Die mit Stern (*) gekennzeichneten Felder sind Pflichtfelder.');--The fields marked with asterisk (*) are required.
SELECT localization.add_localized_resource('Titles', 'de', 'RequiredFieldIndicator', '*');-- *
SELECT localization.add_localized_resource('Titles', 'de', 'Reset', 'Rücksetzen');--Reset
SELECT localization.add_localized_resource('Titles', 'de', 'RestrictedTransactionMode', 'Eingeschränkter Transaktions Modus');--Restricted Transaction Mode
SELECT localization.add_localized_resource('Titles', 'de', 'RetainedEarnings', 'Gewinnrücklagen');--Retained Earnings
SELECT localization.add_localized_resource('Titles', 'de', 'Return', 'Zurück');--Return
SELECT localization.add_localized_resource('Titles', 'de', 'ReturnToView', 'Zurück zur Ansicht');--Return to View
SELECT localization.add_localized_resource('Titles', 'de', 'Role', 'Rolle');--Role
SELECT localization.add_localized_resource('Titles', 'de', 'Roles', 'Rollen');--Roles
SELECT localization.add_localized_resource('Titles', 'de', 'RowNumber', 'Zeilennummer');--Row Number
SELECT localization.add_localized_resource('Titles', 'de', 'RunningTotal', 'Laufende Summe');--Running Total
SELECT localization.add_localized_resource('Titles', 'de', 'SSTNumber', 'SST Nummer');--SST Number
SELECT localization.add_localized_resource('Titles', 'de', 'SalesByMonthInThousands', 'Umsätze nach Monat (in Tausend)');--Sales By Month (In Thousands)
SELECT localization.add_localized_resource('Titles', 'de', 'SalesByOfficeInThousands', 'Umsatz nach Geschäftsstelle (in Tausend)');--Sales By Office (In Thousands)
SELECT localization.add_localized_resource('Titles', 'de', 'SalesDelivery', 'Vertrieb Lieferung');--Sales Delivery
SELECT localization.add_localized_resource('Titles', 'de', 'SalesDeliveryNote', 'Lieferschein');--Delivery Note
SELECT localization.add_localized_resource('Titles', 'de', 'SalesInvoice', 'Verkaufs Rechnung');--Sales Invoice
SELECT localization.add_localized_resource('Titles', 'de', 'SalesOrder', 'Kundenauftrag');--Sales Order
SELECT localization.add_localized_resource('Titles', 'de', 'SalesPersons', 'Verkäufer');--Salespersons
SELECT localization.add_localized_resource('Titles', 'de', 'SalesQuotation', 'Verkaufsangebot');--Sales Quotation
SELECT localization.add_localized_resource('Titles', 'de', 'SalesReceipt', 'Kassenbeleg');--Sales Receipt
SELECT localization.add_localized_resource('Titles', 'de', 'SalesReturn', 'Retouren');--Sales Return
SELECT localization.add_localized_resource('Titles', 'de', 'SalesTaxDetails', 'Umsatzsteuer-Details');--Sales Tax Details
SELECT localization.add_localized_resource('Titles', 'de', 'SalesTaxExemptDetails', 'Umsatzsteuer Ausnahmen Detail');--Sales Tax Exempt Details
SELECT localization.add_localized_resource('Titles', 'de', 'SalesTaxExempts', 'Umsatzsteuer Ausnahmen');--Sales Tax Exempts
SELECT localization.add_localized_resource('Titles', 'de', 'SalesTaxTypes', 'Umsatzsteuerarten');--Sales Tax Types
SELECT localization.add_localized_resource('Titles', 'de', 'SalesTaxes', 'Umsatzsteuern');--Sales Taxes
SELECT localization.add_localized_resource('Titles', 'de', 'SalesTeams', 'Vertriebs -Teams');--Sales Teams
SELECT localization.add_localized_resource('Titles', 'de', 'SalesType', 'Verkaufs-Art');--Sales Type
SELECT localization.add_localized_resource('Titles', 'de', 'Salesperson', 'Verkäufer');--Salesperson
SELECT localization.add_localized_resource('Titles', 'de', 'Save', 'Speichern');--Save
SELECT localization.add_localized_resource('Titles', 'de', 'Saving', 'Speichern');--Saving
SELECT localization.add_localized_resource('Titles', 'de', 'Select', 'Wähle');--Select
SELECT localization.add_localized_resource('Titles', 'de', 'SelectCompany', 'Wähle Firma');--Select Company
SELECT localization.add_localized_resource('Titles', 'de', 'SelectCustomer', 'Wähle Kunden');--Select Customer
SELECT localization.add_localized_resource('Titles', 'de', 'SelectFlag', 'Wähle Markierung');--Select a Flag
SELECT localization.add_localized_resource('Titles', 'de', 'SelectLanguage', 'Sprache wählen');--Select Language
SELECT localization.add_localized_resource('Titles', 'de', 'SelectOffice', 'Wäle Office');--Select Office
SELECT localization.add_localized_resource('Titles', 'de', 'SelectParty', 'Wähle Partei');--Select Party
SELECT localization.add_localized_resource('Titles', 'de', 'SelectPaymentCard', 'Wähle kreditkarte');--Select Payment Card
SELECT localization.add_localized_resource('Titles', 'de', 'SelectStore', 'Wähle Geschäft');--Select Store
SELECT localization.add_localized_resource('Titles', 'de', 'SelectSupplier', 'Wähle Lieferant');--Select Supplier
SELECT localization.add_localized_resource('Titles', 'de', 'SelectUnit', 'Wähle Einheit');--Select Unit
SELECT localization.add_localized_resource('Titles', 'de', 'SelectUser', 'Wähle Benutzer');--Select User
SELECT localization.add_localized_resource('Titles', 'de', 'SelectYourBranch', 'Wählen Sie Ihre Branche');--Select Your Branch
SELECT localization.add_localized_resource('Titles', 'de', 'Shipper', 'Spediteur');--Shipper
SELECT localization.add_localized_resource('Titles', 'de', 'Shippers', 'Speditionen');--Shippers
SELECT localization.add_localized_resource('Titles', 'de', 'ShippingAddress', 'Versandadresse');--Shipping Address
SELECT localization.add_localized_resource('Titles', 'de', 'ShippingAddressMaintenance', 'Versandadressen Wartung');--Shipping Address Maintenance
SELECT localization.add_localized_resource('Titles', 'de', 'ShippingAddresses', 'Versandadressen Wartung');--Shipping Address(es)
SELECT localization.add_localized_resource('Titles', 'de', 'ShippingCharge', 'Versandgebühr');--Shipping Charge
SELECT localization.add_localized_resource('Titles', 'de', 'ShippingCompany', 'Speditionsfirma');--Shipping Company
SELECT localization.add_localized_resource('Titles', 'de', 'Show', 'Anzeigen');--Show
SELECT localization.add_localized_resource('Titles', 'de', 'ShowAll', 'Alle anzeigen');--Show All
SELECT localization.add_localized_resource('Titles', 'de', 'ShowCompact', 'Kompact anzeigen');--Show Compact
SELECT localization.add_localized_resource('Titles', 'de', 'SignIn', 'Anmelden');--Sign In
SELECT localization.add_localized_resource('Titles', 'de', 'SignOut', 'Abmelden');--Sign Out
SELECT localization.add_localized_resource('Titles', 'de', 'SigningIn', 'Anmeldung');--Signing In
SELECT localization.add_localized_resource('Titles', 'de', 'Start', 'Start');--Start
SELECT localization.add_localized_resource('Titles', 'de', 'StateSalesTaxes', 'Landes Mehrwertsteuer');--State Sales Taxes
SELECT localization.add_localized_resource('Titles', 'de', 'StatementOfCashFlows', 'Cash Flow Statement');--Statement of Cash Flows
SELECT localization.add_localized_resource('Titles', 'de', 'StatementReference', 'Erläuternder Vermerk');--Statement Reference
SELECT localization.add_localized_resource('Titles', 'de', 'States', 'Bundesländer');--States
SELECT localization.add_localized_resource('Titles', 'de', 'Status', 'Status');--Status
SELECT localization.add_localized_resource('Titles', 'de', 'StockAdjustment', 'Lager Ausgleich');--Stock Adjustment
SELECT localization.add_localized_resource('Titles', 'de', 'StockTransaction', 'Lager Transaktion');--Stock Transaction
SELECT localization.add_localized_resource('Titles', 'de', 'StockTransferJournal', 'Lager Transfer Journal');--Stock Transfer Journal
SELECT localization.add_localized_resource('Titles', 'de', 'Store', 'Geschäftsjahr');--Store
SELECT localization.add_localized_resource('Titles', 'de', 'StoreName', 'Geschäftsname');--Store Name
SELECT localization.add_localized_resource('Titles', 'de', 'StoreTypes', 'Geschäfts Typ');--Store Types
SELECT localization.add_localized_resource('Titles', 'de', 'Stores', 'Geschäfte');--Stores
SELECT localization.add_localized_resource('Titles', 'de', 'SubTotal', 'Zwischensumme');--Sub Total
SELECT localization.add_localized_resource('Titles', 'de', 'SubmitBugs', 'Bugs einreichen');--Submit Bugs
SELECT localization.add_localized_resource('Titles', 'de', 'SupplierName', 'Lieferant Name');--Supplier Name
SELECT localization.add_localized_resource('Titles', 'de', 'Support', 'Support');--Support
SELECT localization.add_localized_resource('Titles', 'de', 'TableEmptyExceptionMessage', 'Die Eigenschaft "Tabelle" kann nicht leer bleiben.');--The property 'Table' cannot be left empty.
SELECT localization.add_localized_resource('Titles', 'de', 'TableSchemaEmptyExceptionMessage', 'Die Eigenschaft "Table" kann nicht leer bleiben.');--The property 'TableSchema' cannot be left empty.
SELECT localization.add_localized_resource('Titles', 'de', 'TaskCompletedSuccessfully', 'Die Aufgabe wurde erfolgreich abgeschlossen.');--The task was completed successfully.
SELECT localization.add_localized_resource('Titles', 'de', 'Tax', 'Steuer');--Tax
SELECT localization.add_localized_resource('Titles', 'de', 'TaxAuthorities', 'Steuerbehörden');--Tax Authorities
SELECT localization.add_localized_resource('Titles', 'de', 'TaxExemptTypes', 'Steuer Ausnahme Arten');--Tax Exempt Types
SELECT localization.add_localized_resource('Titles', 'de', 'TaxForm', 'Steuererklärung');--Tax Form
SELECT localization.add_localized_resource('Titles', 'de', 'TaxMaster', 'Steuer Basis');--Tax Master
SELECT localization.add_localized_resource('Titles', 'de', 'TaxRate', 'Steuersatz');--Tax Rate
SELECT localization.add_localized_resource('Titles', 'de', 'TaxSetup', 'Tax-Setup');--Tax Setup
SELECT localization.add_localized_resource('Titles', 'de', 'TaxTotal', 'Steuer Total');--Tax Total
SELECT localization.add_localized_resource('Titles', 'de', 'TaxTypes', 'Steuerarten');--Tax Types
SELECT localization.add_localized_resource('Titles', 'de', 'TaxableSales', 'Steuerpflichtiger Umsatz');--Taxable Sales
SELECT localization.add_localized_resource('Titles', 'de', 'Tel', 'Telefon');--Tel
SELECT localization.add_localized_resource('Titles', 'de', 'To', 'bis Zu');--To
SELECT localization.add_localized_resource('Titles', 'de', 'TopSellingProductsOfAllTime', 'Meistverkaufte Produkte aller Zeiten');--Top Selling Products of All Time
SELECT localization.add_localized_resource('Titles', 'de', 'Total', 'Gesamt');--Total
SELECT localization.add_localized_resource('Titles', 'de', 'TotalDueAmount', 'Gesamt Fälliger Betrag');--Total Due Amount
SELECT localization.add_localized_resource('Titles', 'de', 'TotalDueAmountCurrentOffice', 'Gesamt Fälliger Betrag (Aktuelles Office)');--Total Due Amount (Current Office)
SELECT localization.add_localized_resource('Titles', 'de', 'TotalDueAmountInBaseCurrency', 'Gesamt Fälliger Betrag (in Grundwährung)');--Total Due Amount (In Base Currency)
SELECT localization.add_localized_resource('Titles', 'de', 'TotalSales', 'Gesamtverkäufe:');--Total Sales :
SELECT localization.add_localized_resource('Titles', 'de', 'TranCode', 'Transaktionscode');--Tran Code
SELECT localization.add_localized_resource('Titles', 'de', 'TranId', 'Transaction-Id');--Tran Id
SELECT localization.add_localized_resource('Titles', 'de', 'TranIdParameter', 'Transaction-Identifier: #{0}');--TranId: #{0}
SELECT localization.add_localized_resource('Titles', 'de', 'TransactionDate', 'Transaktionsdatum');--Transaction Date
SELECT localization.add_localized_resource('Titles', 'de', 'TransactionDetails', 'Details der Transaktion');--Transaction Details
SELECT localization.add_localized_resource('Titles', 'de', 'TransactionStatement', 'Transaktions Statement');--TransactionStatement
SELECT localization.add_localized_resource('Titles', 'de', 'TransactionStatus', 'Transaktionsstatus');--Transaction Status
SELECT localization.add_localized_resource('Titles', 'de', 'TransactionSummary', 'Transaktionsübersicht');--Transaction Summary
SELECT localization.add_localized_resource('Titles', 'de', 'TransactionTimestamp', 'Transaktionszeitstempel');--Transaction Timestamp
SELECT localization.add_localized_resource('Titles', 'de', 'TransactionType', 'Transaktionsart');--Transaction Type
SELECT localization.add_localized_resource('Titles', 'de', 'TransactionValue', 'Transaktionswert');--Transaction Value
SELECT localization.add_localized_resource('Titles', 'de', 'TransferDetails', 'Überweisung Details');--Transfer Details
SELECT localization.add_localized_resource('Titles', 'de', 'TrialBalance', 'Rohbilanz');--Trial Balance
SELECT localization.add_localized_resource('Titles', 'de', 'Type', 'Art');--Type
SELECT localization.add_localized_resource('Titles', 'de', 'UncheckAll', 'Alle deaktivieren');--Uncheck All
SELECT localization.add_localized_resource('Titles', 'de', 'Undo', 'Rückgängig machen');--Undo
SELECT localization.add_localized_resource('Titles', 'de', 'Unit', 'Einheit');--Unit
SELECT localization.add_localized_resource('Titles', 'de', 'UnitId', 'Einheit Id');--Unit Id
SELECT localization.add_localized_resource('Titles', 'de', 'UnitName', 'Einheit Name');--Unit Name
SELECT localization.add_localized_resource('Titles', 'de', 'UnitsOfMeasure', 'Maßeinheiten');--Units of Measure
SELECT localization.add_localized_resource('Titles', 'de', 'UnknownError', 'Unbekannter Fehler. Operation fehlgeschlagen.');--Operation failed due to an unknown error.
SELECT localization.add_localized_resource('Titles', 'de', 'Update', 'Aktualisierung');--Update
SELECT localization.add_localized_resource('Titles', 'de', 'Upload', 'Hochladen');--Upload
SELECT localization.add_localized_resource('Titles', 'de', 'UploadAttachments', 'Anhänge hochladen');--Upload Attachments
SELECT localization.add_localized_resource('Titles', 'de', 'UploadAttachmentsForThisTransaction', 'Anhänge zu dieser Transaktion Hochladen');--Upload Attachments for This Transaction
SELECT localization.add_localized_resource('Titles', 'de', 'Url', 'Url');--Url
SELECT localization.add_localized_resource('Titles', 'de', 'Use', 'Verwendung');--Use
SELECT localization.add_localized_resource('Titles', 'de', 'User', 'Benutzer');--User
SELECT localization.add_localized_resource('Titles', 'de', 'UserId', 'Benutzerkennung');--User Id
SELECT localization.add_localized_resource('Titles', 'de', 'Username', 'Benutzername');--Username
SELECT localization.add_localized_resource('Titles', 'de', 'Users', 'Benutzer');--Users
SELECT localization.add_localized_resource('Titles', 'de', 'VacuumDatabase', 'Vakuum-Datenbank');--Vacuum Database
SELECT localization.add_localized_resource('Titles', 'de', 'VacuumFullDatabase', 'Vakuum-Datenbank (voll)');--Vacuum Database (Full)
SELECT localization.add_localized_resource('Titles', 'de', 'ValueDate', 'Abrechnungstag');--Value Date
SELECT localization.add_localized_resource('Titles', 'de', 'VerificationReason', 'Begründung der Verifizierung');--Verification Reason
SELECT localization.add_localized_resource('Titles', 'de', 'VerifiedBy', 'Verifiziert von');--Verified By
SELECT localization.add_localized_resource('Titles', 'de', 'VerifiedOn', 'Verifiziert am');--VerifiedOn
SELECT localization.add_localized_resource('Titles', 'de', 'Verify', 'Verifizieren');--Verify
SELECT localization.add_localized_resource('Titles', 'de', 'View', 'Ansicht');--View
SELECT localization.add_localized_resource('Titles', 'de', 'ViewAttachments', 'Anhänge anzeigen');--View Attachments
SELECT localization.add_localized_resource('Titles', 'de', 'ViewBackups', 'Backups anzeigen');--View Backups
SELECT localization.add_localized_resource('Titles', 'de', 'ViewCustomerCopy', 'Kundenkopie anzeigen');--View Customer Copy
SELECT localization.add_localized_resource('Titles', 'de', 'ViewEmptyExceptionMessage', 'Die Eigenschaft "Anzeige" kann nicht leer gelassen werden.');--The property 'View' cannot be left empty.
SELECT localization.add_localized_resource('Titles', 'de', 'ViewSalesInovice', 'Verkausrechnungsansicht');--View Sales Invoice
SELECT localization.add_localized_resource('Titles', 'de', 'ViewSchemaEmptyExceptionMessage', 'Die Eigenschaft"Anzeigenschema" Kann nicht leer gelassen werden.');--The property 'ViewSchema' cannot be left empty.
SELECT localization.add_localized_resource('Titles', 'de', 'ViewThisAdjustment', 'Einstellungen anzeigen');--View This Adjustment
SELECT localization.add_localized_resource('Titles', 'de', 'ViewThisDelivery', 'Lieferung anzeigen');--View This Delivery
SELECT localization.add_localized_resource('Titles', 'de', 'ViewThisInvoice', 'Verkaufsrechnung anzeigen');--View This Invoice
SELECT localization.add_localized_resource('Titles', 'de', 'ViewThisNote', 'Vermerk anzeigen');--View This Note
SELECT localization.add_localized_resource('Titles', 'de', 'ViewThisOrder', 'Bestellung anzeigen');--View This Order
SELECT localization.add_localized_resource('Titles', 'de', 'ViewThisQuotation', 'Offert anzeigen');--View This Quotation
SELECT localization.add_localized_resource('Titles', 'de', 'ViewThisReturn', 'Rücksendung anzeigen');--View This Return
SELECT localization.add_localized_resource('Titles', 'de', 'ViewThisTransfer', 'Transfer anzeigen');--View This Transfer
SELECT localization.add_localized_resource('Titles', 'de', 'VoucherVerification', 'Bescheinigungsverifizierung');--Voucher Verification
SELECT localization.add_localized_resource('Titles', 'de', 'VoucherVerificationPolicy', 'Bescheinigungs Verifizierungs Richtlinie');--Voucher Verification Policy
SELECT localization.add_localized_resource('Titles', 'de', 'Warning', 'Warnungen');--Warning
SELECT localization.add_localized_resource('Titles', 'de', 'WhichBank', 'Welche Bank');--Which Bank?
SELECT localization.add_localized_resource('Titles', 'de', 'WithdrawTransaction', 'Transaktion zurückziehen');--Withdraw Transaction
SELECT localization.add_localized_resource('Titles', 'de', 'WithdrawnTransactions', 'Zurückgezogene Transaktionen');--Withdrawn Transactions
SELECT localization.add_localized_resource('Titles', 'de', 'Workflow', 'Workflow-');--Workflow
SELECT localization.add_localized_resource('Titles', 'de', 'WorldSalesStatistics', 'Weltweite  Verkaufs Statistiken');--World Sales Statistics
SELECT localization.add_localized_resource('Titles', 'de', 'Year', 'Jahr');--Year
SELECT localization.add_localized_resource('Titles', 'de', 'Yes', 'ja');--Yes
SELECT localization.add_localized_resource('Titles', 'de', 'YourName', 'Ihr Name');--Your Name
SELECT localization.add_localized_resource('Titles', 'de', 'YourOffice', 'Ihr Office');--Your Office
SELECT localization.add_localized_resource('Warnings', 'de', 'AccessIsDenied', 'Zugriff wird verweigert.');--Access is denied.
SELECT localization.add_localized_resource('Warnings', 'de', 'CannotCreateABackup', 'Sorry, Datenbank Backup kann nicht erstellt werden.');--Sorry, cannot create a database backup at this time.
SELECT localization.add_localized_resource('Warnings', 'de', 'CannotCreateFlagTransactionTableNull', 'Kann die Markierung weder setzen noch updaten.Die Transaktionstabelle  stand nicht zur Verfügung.');--Cannot create or update flag. Transaction table was not provided.
SELECT localization.add_localized_resource('Warnings', 'de', 'CannotCreateFlagTransactionTablePrimaryKeyNull', 'Kann die Markierung weder setzen noch uupdaten. Der Promary Key der Transaktionstabelle war nicht verfügbar,');--Cannot create or update flag. Transaction table primary key was not provided.
SELECT localization.add_localized_resource('Warnings', 'de', 'CannotMergeAlreadyMerged', 'Die gewählten Transaktionen enthalten Artikel, die bereits  zusammengeführt wurden. Bitte versuchen Sie es noch einmal.');--The selected transactions contain items which have already been merged. Please try again.
SELECT localization.add_localized_resource('Warnings', 'de', 'CannotMergeDifferentPartyTransaction', 'Kann keine Transaktionen verschiedener Parteien in einem Stapel zusammenmischen. Bitte versuchen Sie es noch einmal.');--Cannot merge transactions of different parties into a single batch. Please try again.
SELECT localization.add_localized_resource('Warnings', 'de', 'CannotMergeIncompatibleTax', 'Kann Keine Transaktionen die incompatible Steuerarten enthalten zusammenmischen. Bitte versuchen Sie es noch einmal.');--Cannot merge transactions having incompatible tax types. Please try again.
SELECT localization.add_localized_resource('Warnings', 'de', 'CannotMergeUrlNull', 'Kann Transaktionen nicht zusammenführen. Die Misch Url war nicht angegeben.');--Cannot merge transactions. The merge url was not provided.
SELECT localization.add_localized_resource('Warnings', 'de', 'CashTransactionCannotContainBankInfo', 'Eine Barzahlungs-Transaktion darf keine Banktransaktionsdetails enthalten.');--A cash transaction cannot contain bank transaction details.
SELECT localization.add_localized_resource('Warnings', 'de', 'CompareAmountErrorMessage', 'Der Betrag,"An"muß größer sein als d Betrag "Von" sein.');--The amount to should be greater than the amount from.
SELECT localization.add_localized_resource('Warnings', 'de', 'CompareDaysErrorMessage', '"Von" Tag muß kleiner sein als "Bis zu" Tag.');--From days should be less than to days.
SELECT localization.add_localized_resource('Warnings', 'de', 'ComparePriceErrorMessage', 'Preis "Ab" sollte weniger als der "Bis" Preis sein.');--Price from should be less than price to.
SELECT localization.add_localized_resource('Warnings', 'de', 'ConfigurationError', 'Kann die Aufgabe nicht fortsetzen. Bitte korrigieren Sie Konfigurationsprobleme.');--Cannot continue the task. Please correct configuration issues.
SELECT localization.add_localized_resource('Warnings', 'de', 'ConfirmationPasswordDoesNotMatch', 'Das Bestätigungs Kennswort stimmt nicht mit dem neuen Kennwort überein.');--The confirmation password does not match with the new password.
SELECT localization.add_localized_resource('Warnings', 'de', 'CouldNotDetermineEmailImageParserType', 'Der Bild Parser Typ für E-Mail konnte nicht ermittelt werden.');--Could not determine image parser type for email.
SELECT localization.add_localized_resource('Warnings', 'de', 'CouldNotRegisterJavascript', 'Konnte Java Script auf dieser Seite nicht registrieren, weil die Seite ungültig oder leer war.');--Could not register JavaScript on this page because the page instance was invalid or empty.
SELECT localization.add_localized_resource('Warnings', 'de', 'DateErrorMessage', 'Ausgewähltes Datum ist ungültig');--Selected date is invalid.
SELECT localization.add_localized_resource('Warnings', 'de', 'DueFrequencyErrorMessage', 'Laufzeit muß 0 sein wenn  eine Ablauf Zeitraum Id angegeben wurde.');--Due days should be 0 if due frequency id is selected.
SELECT localization.add_localized_resource('Warnings', 'de', 'DuplicateEntry', 'Doppelter Eintrag');--Duplicate entry.
SELECT localization.add_localized_resource('Warnings', 'de', 'DuplicateFiles', 'Doppelte Datei');--Duplicate files.
SELECT localization.add_localized_resource('Warnings', 'de', 'GridViewEmpty', 'Anzeigetabelle war leer.');--Gridview is empty.
SELECT localization.add_localized_resource('Warnings', 'de', 'InsufficientBalanceInCashRepository', 'Es gibt keine ausreichenden n Kontostand im Barwerte-Depot, um diese Transaktion zu verarbeiten.');--There is no sufficient balance in the cash repository to process this transaction.
SELECT localization.add_localized_resource('Warnings', 'de', 'InsufficientStockWarning', 'Nur {0} {1} von {2} auf Lager.');--Only {0} {1} of {2} left in stock.
SELECT localization.add_localized_resource('Warnings', 'de', 'InvalidAccount', 'Ungültigse Konto.');--Invalid account.
SELECT localization.add_localized_resource('Warnings', 'de', 'InvalidCashRepository', 'Ungültiges Barwerte-Depot.');--Invalid cash repository.
SELECT localization.add_localized_resource('Warnings', 'de', 'InvalidCostCenter', 'Ungültige Kostenstelle');--Invalid cost center.
SELECT localization.add_localized_resource('Warnings', 'de', 'InvalidData', 'Ungültige Daten.');--Invalid data.
SELECT localization.add_localized_resource('Warnings', 'de', 'InvalidDate', 'Ungültige Daten.');--Invalid date.
SELECT localization.add_localized_resource('Warnings', 'de', 'InvalidParameterName', 'Ungültige Npgsql Parameternamen {0}. . Stellen Sie sicher, dass der Name des Parameters mit dem Befehlstext übereinstimmt');--Invalid Npgsql parameter name {0}. . Make sure that the parameter name matches with your command text.
SELECT localization.add_localized_resource('Warnings', 'de', 'InvalidParty', 'Ungültige Partei.');--Invalid party.
SELECT localization.add_localized_resource('Warnings', 'de', 'InvalidPaymentTerm', 'Ungültige Zahlungsbedingungen');--Invalid payment term.
SELECT localization.add_localized_resource('Warnings', 'de', 'InvalidPriceType', 'Ungültiger Preistyp');--Invalid price type.
SELECT localization.add_localized_resource('Warnings', 'de', 'InvalidReceiptMode', 'Ungültiger Quittungs Mode');--Invalid receipt mode.
SELECT localization.add_localized_resource('Warnings', 'de', 'InvalidSalesPerson', 'Ungültiger Verkäfer.');--Invalid salesperson.
SELECT localization.add_localized_resource('Warnings', 'de', 'InvalidShippingCompany', 'Ungültige Speditionsfirma');--Invalid shipping company.
SELECT localization.add_localized_resource('Warnings', 'de', 'InvalidStockTransaction', 'Ungültige Lager Transaktion.');--Invalid stock transaction.
SELECT localization.add_localized_resource('Warnings', 'de', 'InvalidStore', 'UngültigesGeschäft');--Invalid store.
SELECT localization.add_localized_resource('Warnings', 'de', 'InvalidSubTranBookInventoryDelivery', 'Tochtergesellschaftsbuchung ungültig : "Inventar Lieferung"');--Invalid SubTranBook "Inventory Delivery"
SELECT localization.add_localized_resource('Warnings', 'de', 'InvalidSubTranBookInventoryDirect', 'Tochtergesellschaftsbuchung ungültig: " Inventar Direct"');--Invalid SubTranBook "Inventory Direct"
SELECT localization.add_localized_resource('Warnings', 'de', 'InvalidSubTranBookInventoryInvoice', 'Tochtergesellschaftsbuchung ungültig:  "Inventar Rechnung"');--Invalid SubTranBook "Inventory Invoice"
SELECT localization.add_localized_resource('Warnings', 'de', 'InvalidSubTranBookInventoryOrder', 'Tochtergesellschaftsbuchung ungültig:  "Inventar Order"');--Invalid SubTranBook "Inventory Order"
SELECT localization.add_localized_resource('Warnings', 'de', 'InvalidSubTranBookInventoryPayment', 'Tochtergesellschaftsbuchung ungültig:  "Inventar Vergütung"');--Invalid SubTranBook "Inventory Payment"
SELECT localization.add_localized_resource('Warnings', 'de', 'InvalidSubTranBookInventoryQuotation', 'Tochtergesellschaftsbuchung ungültig:  Inventar Anbot');--Invalid SubTranBook "Inventory Quotation"
SELECT localization.add_localized_resource('Warnings', 'de', 'InvalidSubTranBookInventoryReceipt', 'Tochtergesellschaftsbuchung ungültig:  "Inventar Quittung"');--Invalid SubTranBook "Inventory Receipt"
SELECT localization.add_localized_resource('Warnings', 'de', 'InvalidSubTranBookInventoryReturn', 'Tochtergesellschaftsbuchung ungültig:  "Inventar Retouren"');--Invalid SubTranBook "Inventory Return"
SELECT localization.add_localized_resource('Warnings', 'de', 'InvalidSubTranBookPurchaseDelivery', 'Tochtergesellschaftsbuchung ungültig:  "Einkaufs- Lieferung"');--Invalid SubTranBook "Purchase Delivery"
SELECT localization.add_localized_resource('Warnings', 'de', 'InvalidSubTranBookPurchaseQuotation', 'Tochtergesellschaftsbuchung ungültig:  "Einkauf Anbot"');--Invalid SubTranBook "Purchase Quotation"
SELECT localization.add_localized_resource('Warnings', 'de', 'InvalidSubTranBookPurchaseSuspense', 'Tochtergesellschaftsbuchung ungültig:  "Einkauf Zwischenkonto"');--Invalid SubTranBook "Purchase Suspense"
SELECT localization.add_localized_resource('Warnings', 'de', 'InvalidSubTranBookPurchaseTransfer', 'Tochtergesellschaftsbuchung ungültig:  "Einkauf  Transfer"');--Invalid SubTranBook "Purchase Transfer"
SELECT localization.add_localized_resource('Warnings', 'de', 'InvalidSubTranBookSalesPayment', 'Tochtergesellschaftsbuchung ungültig:  "Verkaufs Vergütung"');--Invalid SubTranBook "Sales Payment"
SELECT localization.add_localized_resource('Warnings', 'de', 'InvalidSubTranBookSalesSuspense', 'Tochtergesellschaftsbuchung ungültig: "Verkaufs Zwischenkonto"');--Invalid SubTranBook "Sales Suspense"
SELECT localization.add_localized_resource('Warnings', 'de', 'InvalidSubTranBookSalesTransfer', 'Tochtergesellschaftsbuchung ungültig:  "Verkaufs Transfer"');--Invalid SubTranBook "Sales Transfer"
SELECT localization.add_localized_resource('Warnings', 'de', 'InvalidUser', 'Ungültige Benutzer.');--Invalid user.
SELECT localization.add_localized_resource('Warnings', 'de', 'ItemErrorMessage', 'Sie müssen entweder Artikel-ID oder eine Kombi Artikel ID auswählen.');--You have to select either item id or  compound item id.
SELECT localization.add_localized_resource('Warnings', 'de', 'LateFeeErrorMessage', 'Säumniszuschlag Id und Säumniszuschlag Buchungszeitraum Id können nur gemeinsam ausgewählt werden oder nicht ausgewählt werden.');--Late fee id and late fee posting frequency id both should be either selected or not.
SELECT localization.add_localized_resource('Warnings', 'de', 'NegativeValueSupplied', 'Negativer Wert angegeben');--Negative value supplied.
SELECT localization.add_localized_resource('Warnings', 'de', 'NewPasswordCannotBeOldPassword', 'Neues Passwort kann nicht das alte Kennwort sein.');--New password can not be old password.
SELECT localization.add_localized_resource('Warnings', 'de', 'NoFileSpecified', 'Keine Datei angegeben.');--No file specified.
SELECT localization.add_localized_resource('Warnings', 'de', 'NoTransactionToPost', 'Keine Transaktion durchzuführen.');--No transaction to post.
SELECT localization.add_localized_resource('Warnings', 'de', 'NotAuthorized', 'Sie sind nicht berechtigt, auf diese Ressourcen zu diesem Zeitpunkt zugreifen.');--You are not authorized to access this resource at this time.
SELECT localization.add_localized_resource('Warnings', 'de', 'NothingSelected', 'Nichts ausgewählt.');--Nothing selected.
SELECT localization.add_localized_resource('Warnings', 'de', 'PasswordCannotBeEmpty', 'Das Passwort darf nicht leer sein.');--Password cannot be empty.
SELECT localization.add_localized_resource('Warnings', 'de', 'PleaseEnterCurrentPassword', 'Bitte geben Sie Ihr aktuelles Kennwort ein.');--Please enter your current password.
SELECT localization.add_localized_resource('Warnings', 'de', 'PleaseEnterNewPassword', 'Bitte geben Sie ein neues Kennwort ein.');--Please enter a new password.
SELECT localization.add_localized_resource('Warnings', 'de', 'RecurringAmountErrorMessage', 'Die Zahl derr Wiederholungen darf nicht kleiner oder gleich 0 sein.');--Recurring amount should not be less than or equal to 0.
SELECT localization.add_localized_resource('Warnings', 'de', 'ReferencingSidesNotEqual', 'Die referenzierenden Seiten sind nicht gleich.');--The referencing sides are not equal.
SELECT localization.add_localized_resource('Warnings', 'de', 'RestrictedTransactionMode', 'Dieser Niederlassung sind keine Transaktions Buchungen erlaubt.');--This establishment does not allow transaction posting.
SELECT localization.add_localized_resource('Warnings', 'de', 'ReturnButtonUrlNull', 'Fehler:  Return Taste enhthält keinen Url Eintrag.');--Cannot return this entry. The return url was not provided.
SELECT localization.add_localized_resource('Warnings', 'de', 'UserIdOrPasswordIncorrect', 'Benutzerkennung oder Passwort falsch.');--User id or password incorrect.