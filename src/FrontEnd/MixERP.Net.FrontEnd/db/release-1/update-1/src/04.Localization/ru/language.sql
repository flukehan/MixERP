--Translated using a tool
SELECT localization.add_localized_resource('CommonResource', 'ru', 'DateMustBeGreaterThan', 'Неправильная дата. Должно быть больше, чем "{0}".');--Invalid date. Must be greater than "{0}".
SELECT localization.add_localized_resource('CommonResource', 'ru', 'DateMustBeLessThan', 'Неправильная дата. Должна быть меньше, чем "{0}".');--Invalid date. Must be less than "{0}".
SELECT localization.add_localized_resource('CommonResource', 'ru', 'InvalidDate', 'Это не действительной датой.');--Invalid date.
SELECT localization.add_localized_resource('CommonResource', 'ru', 'NoRecordFound', 'К сожалению, никаких записей не найдено.');--Sorry, no record found.
SELECT localization.add_localized_resource('CommonResource', 'ru', 'RequiredField', 'Это обязательное поле.');--This is a required field.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P1301', 'Не можете начислять проценты.Количество дней в году не было.');--Cannot calculate interest. The number of days in a year was not provided.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P1302', 'Не можете прикреплять продаж. Неверный отображение денежный счет на складе.');--Cannot post sales. Invalid cash account mapping on store.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P3000', 'Неверные данные.');--Invalid data.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P3001', 'Неверное имя пользователя.');--Invalid user name.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P3005', 'Пароль не может быть пустым.');--Password cannot be empty.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P3006', 'Пожалуйста, укажите новый пароль.');--Please provide a new password.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P3007', 'Неверный дата валютирования.');--Invalid value date.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P3008', 'Неправильная дата.');--Invalid date.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P3009', 'Неверный указанный период.');--Invalid period specified.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P3010', 'Неверный офис ID.');--Invalid office id.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P3011', 'Неверный офис.');--Invalid office.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P3012', 'Неверный магазин.');--Invalid store.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P3013', 'Неверный хранилище наличными.');--Invalid cash repository.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P3050', 'Неверный партия.');--Invalid party.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P3051', 'Неверный пункта.');--Invalid item.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P3052', 'Неверный блок.');--Invalid unit.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P3053', 'Неверный или несовместимый блок.');--Invalid or incompatible unit.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P3054', 'Блок повторного заказа несовместимо с базовым блоком.');--The reorder unit is incompatible with the base unit.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P3055', 'Неверный курс.');--Invalid exchange rate.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P3101', 'Неверный LoginID.');--Invalid LoginId.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P3105', 'Ваш текущий пароль неверен.');--Your current password is incorrect.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P3201', 'Пункт / блок несоответствие.');--Item/unit mismatch.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P3202', 'Форма налоговой несоответствие.');--Tax form mismatch.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P3301', 'Неверный количество.');--Invalid quantity.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P3302', 'Неверный идентификатор транзакции.');--Invalid transaction id.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P3501', 'Колонка account_id не может быть пустым.');--The column account_id cannot be null.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P4010', 'Скорость обмена между валютами не был найден.');--Exchange rate between the currencies was not found.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P4020', 'Этот пункт не связан с этой сделкой.');--This item is not associated with this transaction.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P4030', 'Нет проверка политики нашли для этого пользователя.');--No verification policy found for this user.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P4031', 'Пожалуйста, попросите кого-нибудь еще, чтобы проверить вашу сделку.');--Please ask someone else to verify your transaction.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P5000', 'Ссылки на стороны не равны.');--Referencing sides are not equal.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P5001', 'Отрицательный акции, не допускается.');--Negative stock is not allowed.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P5002', 'Размещение этой сделки будет производить отрицательный денежный баланс.');--Posting this transaction would produce a negative cash balance.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P5010', 'Прошедшие датированные операции не допускаются.');--Past dated transactions are not allowed.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P5100', 'Это учреждение не позволяет объявление транзакций.');--This establishment does not allow transaction posting.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P5101', 'Не можете прикреплять сделки в режиме ограниченного сделки.');--Cannot post transaction during restricted transaction mode.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P5102', 'Конец дня работы уже была выполнена.');--End of day operation was already performed.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P5103', 'Прошедшие датированные операции в очереди проверки.');--Past dated transactions in verification queue.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P5104', 'Пожалуйста, проверьте сделок перед выполнением конце дня работы.');--Please verify transactions before performing end of day operation.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P5110', 'Вы не можете предоставить налога с продаж информации для не облагаемых продаж.');--You cannot provide sales tax information for non taxable sales.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P5111', 'Информация Неверный банковская транзакция предусмотрено.');--Invalid bank transaction information provided.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P5112', 'Неправильная информация платежной карты.');--Invalid payment card information.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P5113', 'Не удалось найти аккаунт для расходов купец плату.');--Could not find an account to post merchant fee expenses.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P5201', 'Запись регулировка акции не могут содержать дебетовой товар (ов).');--A stock adjustment entry can not contain debit item(s).
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P5202', 'Элемент может появляться только один раз в магазине.');--An item can appear only once in a store.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P5203', 'Возвращенное количество не может быть больше, чем фактическое количество.');--The returned quantity cannot be greater than actual quantity.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P5204', 'Возвращенная сумма не может быть больше, чем фактическое количество.');--The returned amount cannot be greater than actual amount.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P5301', 'Неверный или отклонено транзакций.');--Invalid or rejected transaction.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P5500', 'Недостаточное количество элемента.');--Insufficient item quantity.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P5800', 'Удаление сделка не допускается. Отметить сделку отклоненным вместо этого.');--Deleting a transaction is not allowed. Mark the transaction as rejected instead.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P5901', 'Пожалуйста, попросите кого-нибудь еще, чтобы проверить сделку вас в курсе.');--Please ask someone else to verify the transaction you posted.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P5910', 'Само ограничение подтверждение превышен.Сделка не была подтверждена.');--Self verification limit exceeded. The transaction was not verified.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P5911', 'Ограничение подтверждение по продажам превышен.Сделка не была подтверждена.');--Sales verification limit exceeded. The transaction was not verified.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P5912', 'Покупка ограничение подтверждение превышен.Сделка не была подтверждена.');--Purchase verification limit exceeded. The transaction was not verified.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P5913', 'GL проверка предел превышен.Сделка не была подтверждена.');--GL verification limit exceeded. The transaction was not verified.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P6010', 'Недопустимая конфигурация: Метод себестоимости.');--Invalid configuration: COGS method.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P8001', 'Невозможно сформировать P / L заявления о кабинете (акционер), имеющие разные базовые валюты.');--Cannot produce P/L statement of office(s) having different base currencies.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P8002', 'Не можете произвести пробный баланс полномочий (акционер), имеющие разные базовые валюты.');--Cannot produce trial balance of office(s) having different base currencies.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P8003', 'Вы не можете иметь различную валюту на ленточном внимание GL.');--You cannot have a different currency on the mapped GL account.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P8101', 'Операция саперов уже был инициализирован.');--EOD operation was already initialized.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P8501', 'Только один столбец не требуется.');--Only one column is required.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P8502', 'Не можете колонку обновить.');--Cannot update column.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P8990', 'Вы не можете изменить системные учетные записи.');--You are not allowed to change system accounts.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P8991', 'Вы не можете добавить системные учетные записи.');--You are not allowed to add system accounts.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P8992', 'Пользователь SYS не может иметь пароль.');--A sys user cannot have a password.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P9001', 'Отказано в доступе.');--Access is denied.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P9010', 'Отказано в доступе. Вы не авторизованы, чтобы разместить этот сделку.');--Access is denied. You are not authorized to post this transaction.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P9011', 'Отказано в доступе. Недопустимые значения в комплект поставки.');--Access is denied. Invalid values supplied.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P9012', 'Отказано в доступе!Сделка регулировка акции не могут ссылки несколько филиалов.');--Access is denied! A stock adjustment transaction cannot references multiple branches.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P9013', 'Отказано в доступе!Сделка акции журнала не может ссылки несколько филиалов.');--Access is denied! A stock journal transaction cannot references multiple branches.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P9014', 'Отказано в доступе. Вы не можете проверить сделку другую должность.');--Access is denied. You cannot verify a transaction of another office.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P9015', 'Отказано в доступе. Вы не можете проверить в прошлом или futuer датированный сделки.');--Access is denied. You cannot verify past or futuer dated transaction.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P9016', 'Отказано в доступе. Вы don''''t имеют право проверять сделки.');--Access is denied. You don''t have the right to verify the transaction.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P9017', 'Отказано в доступе. Вы don''''t имеют право отозвать сделку.');--Access is denied. You don''t have the right to withdraw the transaction.
SELECT localization.add_localized_resource('DbErrors', 'ru', 'P9201', 'Acess отказано. Вы не можете обновлять "transaction_details" стол.');--Acess is denied. You cannot update the "transaction_details" table.
SELECT localization.add_localized_resource('DbResource', 'ru', 'actions', 'акции');--Actions
SELECT localization.add_localized_resource('DbResource', 'ru', 'amount', 'количество');--Amount
SELECT localization.add_localized_resource('DbResource', 'ru', 'currency', 'валюта');--Currency
SELECT localization.add_localized_resource('DbResource', 'ru', 'flag_background_color', 'Цвет флаг фон');--Flag Background Color
SELECT localization.add_localized_resource('DbResource', 'ru', 'flag_foreground_color', 'Флаг цвет переднего плана');--Flag Foreground Color
SELECT localization.add_localized_resource('DbResource', 'ru', 'id', 'идентифицировать');--ID
SELECT localization.add_localized_resource('DbResource', 'ru', 'office', 'офис');--Office
SELECT localization.add_localized_resource('DbResource', 'ru', 'party', 'партия');--Party
SELECT localization.add_localized_resource('DbResource', 'ru', 'reference_number', 'номер для ссылок');--Reference Number
SELECT localization.add_localized_resource('DbResource', 'ru', 'statement_reference', 'О себе Ссылка');--Statement Reference
SELECT localization.add_localized_resource('DbResource', 'ru', 'transaction_ts', 'сделка Отметка времени');--Transaction Timestamp
SELECT localization.add_localized_resource('DbResource', 'ru', 'user', 'пользователь');--User
SELECT localization.add_localized_resource('DbResource', 'ru', 'value_date', 'Дата валютирования');--Value Date
SELECT localization.add_localized_resource('Errors', 'ru', 'BothSidesCannotHaveValue', 'И дебет и кредит не может иметь значения.');--Both debit and credit cannot have values.
SELECT localization.add_localized_resource('Errors', 'ru', 'CompoundUnitOfMeasureErrorMessage', 'Базовый блок и сравнить устройство не может быть таким же.');--Base unit id and compare unit id cannot be same.
SELECT localization.add_localized_resource('Errors', 'ru', 'InsufficientStockWarning', 'Только {0} {1} {2} оставил в запасе.');--Only {0} {1} of {2} left in stock.
SELECT localization.add_localized_resource('Errors', 'ru', 'InvalidSubTranBookPurchaseDelivery', 'Неверный Вспомогательные Сделки Книга "Покупка Доставка"');--Invalid SubTranBook 'Purchase Delivery'.
SELECT localization.add_localized_resource('Errors', 'ru', 'InvalidSubTranBookPurchaseQuotation', 'Недействительные сделки Вспомогательные Книга "Покупка цитаты"');--Invalid SubTranBook 'Purchase Quotation'.
SELECT localization.add_localized_resource('Errors', 'ru', 'InvalidSubTranBookPurchaseReceipt', 'Неверный Вспомогательные Сделки Книга "Покупка Получение"');--Invalid SubTranBook 'Purchase Receipt'.
SELECT localization.add_localized_resource('Errors', 'ru', 'InvalidSubTranBookSalesPayment', 'Неверный Вспомогательные Сделки Книга "Оплата по продажам"');--Invalid SubTranBook 'Sales Payment'.
SELECT localization.add_localized_resource('Errors', 'ru', 'InvalidUserId', 'Неверный идентификатор пользователя.');--Invalid user id.
SELECT localization.add_localized_resource('Errors', 'ru', 'KeyValueMismatch', 'Существует несоответствия кол-во ключ / значение пунктов в этом списке контроля.');--There is a mismatching count of key/value items in this ListControl.
SELECT localization.add_localized_resource('Errors', 'ru', 'NoTransactionToPost', 'Сделка не чтобы оставлять сообщения.');--No transaction to post.
SELECT localization.add_localized_resource('Errors', 'ru', 'ReferencingSidesNotEqual', 'Ссылающейся стороны не равны.');--The referencing sides are not equal.
SELECT localization.add_localized_resource('Labels', 'ru', 'AllFieldsRequired', 'Все поля обязательны для заполнения.');--All fields are required.
SELECT localization.add_localized_resource('Labels', 'ru', 'CannotWithdrawNotValidGLTransaction', 'Не можете отозвать сделку. Это не действует общее сделки Леджер.');--Cannot withdraw transaction. This is a not a valid GL transaction.
SELECT localization.add_localized_resource('Labels', 'ru', 'CannotWithdrawTransaction', 'Не можете отозвать сделку.');--Cannot withdraw transaction.
SELECT localization.add_localized_resource('Labels', 'ru', 'ClickHereToDownload', 'Нажмите здесь, чтобы загрузить.');--Click here to download.
SELECT localization.add_localized_resource('Labels', 'ru', 'ConfirmedPasswordDoesNotMatch', 'Подтверждение пароля не совпадают.');--The confirmed password does not match.
SELECT localization.add_localized_resource('Labels', 'ru', 'DatabaseBackupSuccessful', 'Резервное копирование базы данных было успешным.');--The database backup was successful.
SELECT localization.add_localized_resource('Labels', 'ru', 'DaysLowerCase', 'дней');--days
SELECT localization.add_localized_resource('Labels', 'ru', 'EODBegunSaveYourWork', 'Пожалуйста, закройте это окно и сохранить существующую работу, прежде чем будет подписан автоматически.');--Please close this window and save your existing work before you will be signed off automatically.
SELECT localization.add_localized_resource('Labels', 'ru', 'EmailBody', '<h2> Привет, </ h2> <p> Вы можете найти в прикрепленном документе. </ p> <p> Спасибо. <br /> MixERP </ p>');--<h2>Hi,</h2><p>Please find the attached document.</p><p>Thank you.<br />MixERP</p>
SELECT localization.add_localized_resource('Labels', 'ru', 'EmailSentConfirmation', 'Письмо было отправлено на адрес {0}.');--An email was sent to {0}.
SELECT localization.add_localized_resource('Labels', 'ru', 'FlagLabel', 'Вы можете отметить эту сделку с флагом, однако вы не сможете увидеть флажки созданные другими пользователями.');--You can mark this transaction with a flag, however you will not be able to see the flags created by other users.
SELECT localization.add_localized_resource('Labels', 'ru', 'GoToChecklistWindow', 'Перейти к контрольный список окно.');--Go to checklist window.
SELECT localization.add_localized_resource('Labels', 'ru', 'GoToTop', 'Перейти к началу');--Go to top.
SELECT localization.add_localized_resource('Labels', 'ru', 'JustAMomentPlease', 'Минуту, пожалуйста!');--Just a moment, please!
SELECT localization.add_localized_resource('Labels', 'ru', 'NumRowsAffected', '{0} строк, затронутых.');--{0} rows affected.
SELECT localization.add_localized_resource('Labels', 'ru', 'OpeningInventoryAlreadyEntered', 'Открытие запасов уже были введены на эту должность.');--Opening inventory has already been entered for this office.
SELECT localization.add_localized_resource('Labels', 'ru', 'PartyDescription', 'Стороны совместно обратиться к поставщиков, клиентов, агентов и дилеров.');--Parties collectively refer to suppliers, customers, agents, and dealers.
SELECT localization.add_localized_resource('Labels', 'ru', 'SelectAFlag', 'Выберите флаг.');--Select a flag.
SELECT localization.add_localized_resource('Labels', 'ru', 'TaskCompletedSuccessfully', 'Задача была успешно завершена.');--Task completed successfully.
SELECT localization.add_localized_resource('Labels', 'ru', 'ThankYouForYourBusiness', 'Спасибо для вашего бизнеса.');--Thank you for your business.
SELECT localization.add_localized_resource('Labels', 'ru', 'ThisFieldIsRequired', 'Это поле обязательно к заполнению.');--This field is required.
SELECT localization.add_localized_resource('Labels', 'ru', 'TransactionApprovedDetails', 'Эта сделка была одобрена {0} на {1}.');--This transaction was approved by {0} on {1}.
SELECT localization.add_localized_resource('Labels', 'ru', 'TransactionAutoApprovedDetails', 'Данная сделка была автоматически одобрена {0} на {1}.');--This transaction was automatically approved by {0} on {1}.
SELECT localization.add_localized_resource('Labels', 'ru', 'TransactionAwaitingVerification', 'Эта сделка ожидает проверки от имени администратора.');--This transaction is awaiting verification from an administrator.
SELECT localization.add_localized_resource('Labels', 'ru', 'TransactionClosedDetails', 'Эта сделка была закрыта через {0} на {1}. Причина: "{2}".');--This transaction was closed by {0} on {1}. Reason: "{2}".
SELECT localization.add_localized_resource('Labels', 'ru', 'TransactionPostedSuccessfully', 'Сделка была успешно размещена.');--The transaction was posted successfully.
SELECT localization.add_localized_resource('Labels', 'ru', 'TransactionRejectedDetails', 'Эта сделка была отклонена {0} {1}. Причина: "{2}".');--This transaction was rejected by {0} on {1}. Reason: "{2}".
SELECT localization.add_localized_resource('Labels', 'ru', 'TransactionWithdrawalInformation', 'Когда вы снимаете транзакции, он не будет направлен к модулю рабочего процесса. Это означает, что ваши изъятые сделки, отклоняются и не требуют дополнительной проверки. Тем не менее, вы не сможете unwithdraw эту сделку позже.');--When you withdraw a transaction, it won't be forwarded to the workflow module. This means that your withdrawn transactions are rejected and require no further verification. However, you won't be able to unwithdraw this transaction later.
SELECT localization.add_localized_resource('Labels', 'ru', 'TransactionWithdrawnDetails', 'Данная сделка была отозвана {0} на {1}. Причина: "{2}".');--This transaction was withdrawn by {0} on {1}. Reason: "{2}".
SELECT localization.add_localized_resource('Labels', 'ru', 'TransactionWithdrawnMessage', 'Сделка была успешно снята. Кроме того, это действие будет влиять на все отчеты, полученные после "{0}".');--The transaction was withdrawn successfully. Moreover, this action will affect the all the reports produced on and after "{0}".
SELECT localization.add_localized_resource('Labels', 'ru', 'UserGreeting', 'Привет {0}!');--Hi {0}!
SELECT localization.add_localized_resource('Labels', 'ru', 'YourPasswordWasChanged', 'Был ли ваш пароль изменен.');--Your password was changed.
SELECT localization.add_localized_resource('Messages', 'ru', 'AreYouSure', 'Вы уверены, что вы знаете, что вы делаете?');--Are you sure?
SELECT localization.add_localized_resource('Messages', 'ru', 'CouldNotDetermineVirtualPathError', 'Не удалось определить виртуальный путь, чтобы создать изображение.');--Could not determine virtual path to create an image.
SELECT localization.add_localized_resource('Messages', 'ru', 'DuplicateFile', 'Duplicate File!');--Duplicate File!
SELECT localization.add_localized_resource('Messages', 'ru', 'EODDoNotCloseWindow', 'Пожалуйста, не закрывайте это окно или уйдете с этой страницы во время инициализации.');--Please do not close this window or navigate away from this page during initialization.
SELECT localization.add_localized_resource('Messages', 'ru', 'EODElevatedPriviledgeCanLogIn', 'В течение дня на конец периода, только пользователи, имеющие повышенными привилегиями разрешено войти в.');--During the day-end period, only users having elevated privilege are allowed to log-in.
SELECT localization.add_localized_resource('Messages', 'ru', 'EODLogsOffUsers', 'При инициализации день окончания работы, уже вошедшего в приложения пользователи том числе Вы выходите на 120 секунд.');--When you initialize day-end operation, the already logged-in application users including you are logged off on 120 seconds.
SELECT localization.add_localized_resource('Messages', 'ru', 'EODProcessIsIrreversible', 'Этот процесс является необратимым.');--This process is irreversible.
SELECT localization.add_localized_resource('Messages', 'ru', 'EODRoutineTasks', 'В конец операционного дня, рутинных задач, таких как расчет процентов, населенных пунктов и создания отчетов выполняются.');--During EOD operation, routine tasks such as interest calculation, settlements, and report generation are performed.
SELECT localization.add_localized_resource('Messages', 'ru', 'EODTransactionPosting', 'При выполнении конец операционного дня для определенной даты, никакая транзакция в этот день или до не могут быть изменены, изменены или удалены.');--When you perform EOD operation for a particular date, no transaction on that date or before can be altered, changed, or deleted.
SELECT localization.add_localized_resource('Messages', 'ru', 'InvalidFile', 'Неверный формат файла!');--Invalid file!
SELECT localization.add_localized_resource('Messages', 'ru', 'TempDirectoryNullError', 'Не удается создать образ, когда временный каталог является недействительным.');--Cannot create an image when the temp directory is null.
SELECT localization.add_localized_resource('Messages', 'ru', 'UploadFilesDeleted', 'Загруженные файлы были успешно удалены.');--The uploaded files were successfully deleted.
SELECT localization.add_localized_resource('Questions', 'ru', 'AreYouSure', 'Вы уверены, что вы знаете, что вы делаете?');--Are you sure?
SELECT localization.add_localized_resource('Questions', 'ru', 'CannotAccessAccount', 'Не можете войти в аккаунт?');--Cannot access your account?
SELECT localization.add_localized_resource('Questions', 'ru', 'ConfirmAnalyze', 'Это заблокирует доступ к базе данных клиента во время выполнения. Вы уверены, что хотите выполнить это действие прямо сейчас?');--This will lock client database access during execution. Are you sure you want to execute this action right now?
SELECT localization.add_localized_resource('Questions', 'ru', 'ConfirmVacuum', 'Это заблокирует доступ к базе данных клиента во время выполнения. Вы уверены, что хотите выполнить это действие прямо сейчас?');--This will lock client database access during execution. Are you sure you want to execute this action right now?
SELECT localization.add_localized_resource('Questions', 'ru', 'ConfirmVacuumFull', 'Это заблокирует доступ к базе данных клиента во время выполнения. Вы уверены, что хотите выполнить это действие прямо сейчас?');--This will lock client database access during execution. Are you sure you want to execute this action right now?
SELECT localization.add_localized_resource('Questions', 'ru', 'WhatIsYourHomeCurrency', 'Что Является ли ваш дом валюте?');--What Is Your Home Currency?
SELECT localization.add_localized_resource('Questions', 'ru', 'WithdrawalReason', 'Почему вы хотите, чтобы снять эту сделку?');--Why do you want to withdraw this transaction?
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'Select', 'выбрать');--Select
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'account', 'счет');--Account
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'account_id', 'Определить аккаунт');--Account Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'account_master', 'основной счет');--Account Master
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'account_master_code', 'Account Master Code');--Account Master Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'account_master_id', 'Основной учетной записи Идентификатор');--Account Master Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'account_master_name', 'Мастер Имя счета');--Account Master Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'account_name', 'Имя Учетной Записи');--Account Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'account_number', 'Номер Счета');--Account Number
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'address', 'адрес');--Address
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'address_line_1', 'Адресная Строка 1');--Address Line 1
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'address_line_2', 'Адресная Строка 2');--Address Line 2
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'ageing_slab_id', 'Старение Определить перекрытия');--Ageing Slab Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'ageing_slab_name', 'Старение Плиты Имя');--Ageing Slab Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'allow_credit', 'Разрешить кредит');--Allow Credit
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'allow_sales', 'Разрешить продаж');--Allow Sales
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'allow_transaction_posting', 'Разрешить сделка проводки');--Allow Transaction Posting
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'amount', 'количество');--Amount
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'amount_from', 'Количество От');--Amount From
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'amount_to', 'Сумма к');--Amount To
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'analyze_count', 'Анализ графа');--Analyze Count
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'api_access_policy_id', 'API доступа Политика Id');--API Access Policy Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'api_access_policy_uix', 'Дубликат записи по политике доступа API');--Duplicate Entry for API Access Policy
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'applied_on_shipping_charge', 'Применяют на доставке обязанности');--Applied on Shipping Charge
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'audit_ts', 'Аудит Отметка времени');--Audit Timestamp
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'audit_user_id', 'Аудит Идентификатор пользователя');--Audit User Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'auto_trigger_on_sales', 'Автоматически Запуск по продажам');--Automatically Trigger on Sales
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'autoanalyze_count', 'Autoanalyze Граф');--Autoanalyze Count
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'autovacuum_count', 'автовакуумной Граф');--Autovacuum Count
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'background_color', 'Цвет фона');--Background Color
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'balance', 'баланс');--Balance
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'bank_account_number', 'Номер банковского счета');--Bank Account Number
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'bank_account_type', 'Банк Тип счета');--Bank Account Type
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'bank_accounts_account_id_chk', 'Выбранный элемент не действует банковский счет.');--The selected item is not a valid bank account.
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'bank_accounts_pkey', 'Скопируйте банковский счет.');--Duplicate bank account.
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'bank_address', 'Адрес банка');--Bank Address
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'bank_branch', 'филиал банка');--Bank Branch
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'bank_contact_number', 'Банк Контактный номер');--Bank Contact Number
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'bank_name', 'Имя банк');--Bank Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'base_unit_id', 'Определить базовый блок');--Base Unit Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'base_unit_name', 'Имя базового блока');--Base Unit Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'based_on_shipping_address', 'Основан на Адрес доставки');--Based On Shipping Address
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'bonus_rate', 'Оценить Бонус');--Bonus Rate
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'bonus_slab_code', 'Плиты Бонусный код');--Bonus Slab Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'bonus_slab_detail_id', 'Бонус Плиты Деталь Определить');--Bonus Slab Detail Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'bonus_slab_details_amounts_chk', 'Поле «Сумма по" должно быть больше, чем "сумму Из".');--The field "AmountTo" must be greater than "AmountFrom".
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'bonus_slab_id', 'Определить Бонус перекрытия');--Bonus Slab Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'bonus_slab_name', 'Плиты Имя опыту');--Bonus Slab Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'book', 'книга');--Book
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'book_date', 'Книга Дата');--Book Date
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'brand', 'марка');--Brand
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'brand_code', 'Брэнд-код');--Brand Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'brand_id', 'Определить Марка');--Brand Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'brand_name', 'бренд');--Brand Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'browser', 'браузер');--Browser
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'can_change_password', 'Может Изменить пароль');--Can Change Password
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'can_self_verify', 'Может Убедитесь, Я.');--Can Self Verify
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'can_verify_gl_transactions', 'Может Убедитесь, главной книги операций');--Can Verify Gl Transactions
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'can_verify_purchase_transactions', 'Может Убедитесь сделок купли-');--Can Verify Purchase Transactions
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'can_verify_sales_transactions', 'Может Убедитесь, сделки купли-продажи');--Can Verify Sales Transactions
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'card_type', 'Тип Карты');--Card Type
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'card_type_code', 'Тип карты Код');--Card Type Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'card_type_id', 'Тип карты Identifier');--Card Type Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'card_type_name', 'Тип карты Имя');--Card Type Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'cash_flow_heading', 'О движении денежных средств товарной позиции');--Cash Flow Heading
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'cash_flow_heading_cash_flow_heading_type_chk', 'Invalid Cash flow Heading Type. Allowed values: O, I, F.');--Invalid Cashflow Heading Type. Allowed values: O, I, F.
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'cash_flow_heading_code', 'О движении денежных средств товарной позиции Код');--Cash Flow Heading Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'cash_flow_heading_id', 'Денежный поток Идентификатор Заголовок');--Cash Flow Heading Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'cash_flow_heading_name', 'О движении денежных средств товарной позиции Имя');--Cash Flow Heading Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'cash_flow_heading_type', 'Денежный поток Заголовок Тип');--Cashflow Heading Type
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'cash_flow_master_code', 'Денежный поток Мастер-код');--Cash Flow Master Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'cash_flow_master_id', 'Денежный поток Мастер Определить');--Cash Flow Master Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'cash_flow_master_name', 'Денежный поток Магистр Название');--Cash Flow Master Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'cash_flow_setup_id', 'Денежный поток Идентификатор Setup');--Cashflow Setup Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'cash_repositories_cash_repository_code_uix', 'Скопируйте Cash Code Repository');--Duplicate Cash Repository Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'cash_repositories_cash_repository_name_uix', 'Повторяющееся имя Денежные средства Repository');--Duplicate Cash Repository Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'cash_repository', 'Денежные средства Repository');--Cash Repository
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'cash_repository_code', 'Денежные Код Repository');--Cash Repository Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'cash_repository_id', 'Денежные средства Repository Идентификатор');--Cash Repository Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'cash_repository_name', 'Денежные Имя хранилища');--Cash Repository Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'cell', 'клетка');--Cell
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'charge_interest', 'расходы на уплату процентов');--Charge Interest
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'check_nexus', 'Проверьте Nexus');--Check Nexus
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'checking_frequency', 'Проверка частоты');--Checking Frequency
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'checking_frequency_id', 'Проверка частоты Идентификатор');--Checking Frequency Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'city', 'город');--City
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'collecting_account', 'Сбор аккаунт');--Collecting Account
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'collecting_account_id', 'Сбор аккаунт Идентификатор');--Collecting Account Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'collecting_tax_authority', 'Сбор налогового органа');--Collecting Tax Authority
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'collecting_tax_authority_id', 'Сбор налогового органа идентификатор');--Collecting Tax Authority Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'commision_rate', 'Оценить Комиссия');--Commission Rate
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'commission_rate', 'Оценить Комиссия');--Commission Rate
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'company_name', 'Название Компании');--Company Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'compare_unit_id', 'Сравните Unit Identifier');--Compare Unit Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'compare_unit_name', 'Сравните Имя устройства');--Compare Unit Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'compound_item', 'Соединение Item');--Compound Item
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'compound_item_code', 'Соединение Код товара');--Compound Item Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'compound_item_detail_id', 'Определить соединение Подробности предмета');--Compound Item Detail Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'compound_item_details_unit_chk', 'При условии, Invalid устройства.');--Invalid unit provided.
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'compound_item_id', 'Соединение Item Identifier');--Compound Item Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'compound_item_name', 'Соединение Название товара');--Compound Item Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'compound_unit_id', 'Соединение блока идентификатор');--Compound Unit Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'compound_units_chk', 'Базовый блок не может определить, как Сами по сравнению блок определены.');--The base unit id cannot same as compare unit id.
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'compounding_frequency', 'Усугубляет Частота');--Compounding Frequency
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'confidential', 'конфиденциальная');--Confidential
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'contact_address_line_1', 'Контактный адрес Line 1');--Contact Address Line 1
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'contact_address_line_2', 'Контактный адрес Line 2');--Contact Address Line 2
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'contact_cell', 'Связаться с сотового');--Contact Cell
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'contact_city', 'Контакты Город');--Contact City
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'contact_country', 'Contact Country');--Contact Country
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'contact_email', 'Контактный адрес электронной почты');--Contact Email
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'contact_number', 'Контактный телефон');--Contact Number
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'contact_person', 'Контактное Лицо');--Contact Person
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'contact_phone', 'Контактный телефон');--Contact Phone
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'contact_po_box', 'Контактный телефон');--Contact Po Box
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'contact_state', 'Связаться с Государственной');--Contact State
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'contact_street', 'Связаться с улицы');--Contact Street
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'cost_center_code', 'МВЗ Код');--Cost Center Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'cost_center_id', 'Определить Центр Стоимость');--Cost Center Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'cost_center_name', 'Стоимость Имя центр');--Cost Center Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'cost_of_goods_sold_account_id', 'Себестоимость проданных товаров аккаунт Идентификатор');--COGS Account Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'cost_price', 'Себестоимость');--Cost Price
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'cost_price_includes_tax', 'Стоимость Цена включает налог');--Cost Price Includes Tax
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'counter_code', 'Код счетчика');--Counter Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'counter_id', 'Определить счетчик');--Counter Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'counter_name', 'Имя счетчика');--Counter Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'country', 'страна');--Country
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'country_code', 'Код страны');--Country Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'country_id', 'Определить Страна');--Country Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'country_name', 'Название страны');--Country Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'county', 'графство');--County
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'county_code', 'Графство Код');--County Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'county_id', 'Определить округ');--County Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'county_name', 'Каунти название');--County Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'county_sales_tax', 'Графство налог с продаж');--County Sales Tax
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'county_sales_tax_code', 'Графство Продажи Налоговый кодекс');--County Sales Tax Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'county_sales_tax_id', 'Графство налог с продаж Identifier');--County Sales Tax Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'county_sales_tax_name', 'Графство налог с продаж Имя');--County Sales Tax Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'credit', 'кредит');--Credit
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'cst_number', 'Центральный налог с продаж Количество');--CST Number
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'culture', 'культура');--Culture
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'currency', 'валюта');--Currency
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'currency_code', 'Код валюты');--Currency Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'currency_name', 'Название валюты');--Currency Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'currency_symbol', 'Символ валюты');--Currency Symbol
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'customer_pays_fee', 'Клиент платит взнос');--Customer Pays Fee
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'date_of_birth', 'Дата Рождения');--Date Of Birth
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'debit', 'дебет');--Debit
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'default_cash_account_id', 'По умолчанию денежный счет идентификатор');--Default Cash Account Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'default_cash_repository_id', 'По умолчанию Cash хранилища идентификатор');--Default Cash Repository Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'department_code', 'Код отдела');--Department Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'department_id', 'Определение отдел');--Department Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'department_name', 'Название отдела');--Department Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'description', 'описание');--Description
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'discount', 'скидка');--Discount
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'due_days', 'Из-за дня');--Due Days
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'due_frequency', 'Из-за частоты');--Due Frequency
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'due_frequency_id', 'Из-за частоты Идентификатор');--Due Frequency Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'due_on_date', 'Due Date является');--Due on Date
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'effective_from', 'вступает в силу с');--Effective From
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'elevated', 'высокий');--Elevated
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'email', 'Электронная почта');--Email
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'ends_on', 'Она заканчивается');--Ends On
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'entity_id', 'Entity Identifier');--Entity Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'entity_name', 'Имя Entity');--Entity Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'entry_ts', 'Вступление Отметка времени');--Entry Ts
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'er', 'Эффективная ставка');--ER
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'exclude_from_purchase', 'Исключить от покупки');--Exclude From Purchase
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'exclude_from_sales', 'Исключить от продаж');--Exclude From Sales
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'external_code', 'Внешний код');--External Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'factory_address', 'Адрес завода');--Factory Address
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'fax', 'факс');--Fax
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'first_name', 'Имя');--First Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'fiscal_year_code', 'Финансовый год Код');--Fiscal Year Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'fiscal_year_name', 'Имя финансовый год');--Fiscal Year Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'flag_id', 'Флаг Идентификатор');--Flag Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'flag_type_id', 'Флаг Идентификатор');--Flag Type Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'flag_type_name', 'Флаг Тип Название');--Flag Type Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'flagged_on', 'Помечено На');--Flagged On
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'foreground_color', 'Цвет переднего плана');--Foreground Color
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'frequency_code', 'Частота код');--Frequency Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'frequency_id', 'Частота Идентификатор');--Frequency Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'frequency_name', 'Частота Имя');--Frequency Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'frequency_setup_code', 'Частота код установки');--Frequency Setup Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'frequency_setup_id', 'Частота настройки идентификатора');--Frequency Setup Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'from_days', 'От дней');--From Days
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'full_name', 'Полное Имя');--Full Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'gl_head', 'Главная книга Руководитель');--GL Head
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'gl_verification_limit', 'Главная книга Проверка Limit');--Gl Verification Limit
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'grace_period', 'льготный срок');--Grace Period
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'has_child', 'Имеет ребенка');--Has Child
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'height_in_centimeters', 'Высота в сантиметрах');--Height In Centimeters
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'hot_item', 'Горячий деталь');--Hot item
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'http_action_code', 'HTTP Code Экшн');--HTTP Action Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'hundredth_name', 'Сотый Имя');--Hundredth Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'id', 'идентифицировать');--Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'includes_tax', 'Включает в себя налог');--Includes Tax
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'income_tax_rate', 'Ставка налога на прибыль');--Income Tax Rate
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'industry_id', 'Identify Industry');--Industry Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'industry_name', 'Industry Name');--Industry Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'interest_compounding_frequency_id', 'Interest Compounding Frequency Identifier');--Interest Compounding Frequency Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'interest_rate', 'interest Rate');--Interest Rate
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'inventory_account_id', 'nventory Account Identifier');--Inventory Account Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'ip_address', 'IP-адрес');--IP Address
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'is_active', 'является активным');--Is Active
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'is_added', 'Добавлена');--Is Added
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'is_admin', 'Есть Админ');--Is Admin
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'is_cash', 'это денежные средства');--Is Cash
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'is_debit', 'Есть Дебет');--Is Debit
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'is_employee', 'Является сотрудником');--Is Employee
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'is_exempt', 'освобождается');--Is Exempt
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'is_exemption', 'Это освобождение');--Is Exemption
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'is_flat_amount', 'Это фиксированная сумма');--Is Flat Amount
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'is_merchant_account', 'Является Merchant Account');--Is Merchant Account
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'is_party', 'является участником');--Is Party
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'is_purchase', 'является покупка');--Is Purchase
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'is_rectangular', 'прямоугольная');--Is Rectangular
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'is_sales', 'Есть в продаже');--Is Sales
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'is_summary', 'Является Резюме');--Is Summary
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'is_supplier', 'Есть Поставщик');--Is Supplier
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'is_system', 'Это система');--Is System
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'is_transaction_node', 'Это узел сделка');--Is Transaction Node
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'is_vat', 'ИС НДС');--Is Vat
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'item', 'пункт');--Item
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'item_code', 'Код товара');--Item Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'item_cost_price_id', 'Определяет предметы себестоимости');--Item Cost Price Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'item_cost_prices_unit_chk', 'При условии, Invalid устройства.');--Invalid unit provided.
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'item_group', 'Пункт Группа');--Item Group
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'item_group_code', 'Группа Код товара');--Item Group Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'item_group_id', 'Определить группу товаров');--Item Group Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'item_group_name', 'Название товара Группа');--Item Group Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'item_id', 'Пункт Идентификатор');--Item Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'item_name', 'Название товара');--Item Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'item_opening_inventory_unit_chk', 'Неверный блок предоставляются.');--Invalid unit provided.
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'item_selling_price_id', 'Пункт Продажа Цена Идентификатор');--Item Selling Price Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'item_selling_prices_unit_chk', 'Неверный блок предоставляются.');--Invalid unit provided.
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'item_type_code', 'Тип элемента Код');--Item Type Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'item_type_id', 'Пункт Идентификатор');--Item Type Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'item_type_name', 'Тип элемента Имя');--Item Type Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'items_item_code_uix', 'Дубликат кода.');--Duplicate item code.
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'items_item_name_uix', 'Повторяющееся имя элемента.');--Duplicate item name.
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'items_reorder_quantity_chk', 'Количество переупор должно быть большим, чем или равен уровню переупор.');--The reorder quantity must be great than or equal to the reorder level.
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'last_analyze', 'Последнее Анализ на');--Last Analyze On
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'last_autoanalyze', 'Последняя авто анализировать На');--Last Autoanalyze On
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'last_autovacuum', 'Последняя авто вакууме на');--Last Autovacuum On
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'last_name', 'фамилия');--Last Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'last_vacuum', 'Последнее вакууме на');--Last Vacuum On
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'late_fee', 'штраф за опоздание');--Late Fee
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'late_fee_code', 'Штраф за опоздание Код');--Late Fee Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'late_fee_id', 'Штраф за опоздание Идентификатор');--Late Fee Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'late_fee_name', 'Штраф за опоздание Имя');--Late Fee Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'late_fee_posting_frequency', 'Штраф за опоздание Проводка Частота');--Late Fee Posting Frequency
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'late_fee_posting_frequency_id', 'Штраф за опоздание Проводка Частота Идентификатор');--Late Fee Posting Frequency Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'lc_credit', 'Аккредитив кредитов');--LC Credit
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'lc_debit', 'Аккредитив дебету');--LC Debit
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'lead_source_code', 'Ведущий Исходный код');--Lead Source Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'lead_source_id', 'Ведущий Источник идентификатор');--Lead Source Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'lead_source_name', 'Ведущий Название источника');--Lead Source Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'lead_status_code', 'Ведущий код Статус');--Lead Status Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'lead_status_id', 'Ведущий Идентификатор Статус');--Lead Status Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'lead_status_name', 'Ведущий Статус Имя');--Lead Status Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'lead_time_in_days', 'Время выполнения в днях');--Lead Time In Days
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'length_in_centimeters', 'Длина в см');--Length In Centimeters
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'login_date_time', 'Войти Дата Время');--Login Date Time
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'login_id', 'Войти Id');--Login Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'machinable', 'механической обработке');--Machinable
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'maintain_stock', 'Поддержание Stock');--Maintain Stock
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'maintained_by_user_id', 'Управляющий Идентификатор пользователя');--Maintained By User Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'maximum_credit_amount', 'Максимальная сумма кредита');--Maximum Credit Amount
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'maximum_credit_period', 'Максимальный размер кредита Период');--Maximum Credit Period
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'merchant_account_id', 'Merchant Account Идентификатор');--Merchant Account Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'merchant_fee_setup_id', 'Торговец Стоимость установки идентификатора');--Merchant Fee Setup Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'middle_name', 'Второе Имя');--Middle Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'nick_name', 'Имя Ник');--Nick Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'non_gl_stock_details_unit_chk', 'Неверный блок предоставляются.');--Invalid unit provided.
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'normally_debit', 'Обычно Дебет');--Normally Debit
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'office', 'офис');--Office
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'office_code', 'Код офиса');--Office Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'office_id', 'Управление Идентификатор');--Office Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'office_name', 'Имя офиса');--Office Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'opportunity_stage_code', 'Возможность Стадия Код');--Opportunity Stage Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'opportunity_stage_id', 'Возможность Стадия Код');--Opportunity Stage Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'opportunity_stage_name', 'Имя возможность Стадия');--Opportunity Stage Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'pan_number', 'Частный счет Количество Количество');--Pan Number
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'parent', 'родитель');--Parent
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'parent_account_id', 'Родитель идентификатор учетной записи');--Parent Account Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'parent_account_master_id', 'Родитель счета Учитель Идентификатор');--Parent Account Master Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'parent_account_name', 'Родитель Имя счета');--Parent Account Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'parent_account_number', 'Родитель Номер счета');--Parent Account Number
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'parent_cash_flow_heading_id', 'Родитель движении денежных средств товарной позиции Идентификатор');--Parent Cash Flow Heading Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'parent_cash_repository', 'Родитель наличными Repository');--Parent Cash Repository
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'parent_cash_repository_id', 'Идентификатор родительской наличными Repository');--Parent Cash Repository Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'parent_cr_code', 'Родитель наличными репозиторий кода');--Parent CR Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'parent_cr_name', 'Родитель наличными Repository Имя');--Parent CR Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'parent_industry_id', 'Родитель Промышленность Идентификатор');--Parent Industry Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'parent_industry_name', 'Родитель название отрасли');--Parent Industry Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'parent_item_group_id', 'Родительский элемент Идентификатор Группы');--Parent Item Group Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'parent_office', 'родитель Управление');--Parent Office
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'parent_office_id', 'Родитель Управление Идентификатор');--Parent Office Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'party', 'партия');--Party
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'party_code', 'партия Код');--Party Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'party_id', 'Идентификация стороны');--Party Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'party_name', 'Имя партия');--Party Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'party_type', 'Тип партия');--Party Type
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'party_type_code', 'Партия введите код');--Party Tpye Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'party_type_id', 'Партия Идентификатор');--Party Type Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'party_type_name', 'Тип партия Имя');--Party Type Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'password', 'пароль');--Password
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'payment_card_code', 'Код платежных карт');--Payment Card Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'payment_card_id', 'Идентификатор платежных карт');--Payment Card Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'payment_card_name', 'Оплата Название карты');--Payment Card Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'payment_term', 'срок оплаты');--Payment Term
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'payment_term_code', 'Оплата Срок Код');--Payment Term Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'payment_term_id', 'Срок оплаты Идентификатор');--Payment Term Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'payment_term_name', 'Срок оплаты Имя');--Payment Term Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'phone', 'телефон');--Phone
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'po_box', 'Почтовый ящик');--Po Box
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'poco_type_name', 'Poco Тип Название');--Poco Type Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'policy_id', 'Почтовый ящик...');--Policy id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'preferred_shipping_mail_type', 'Предпочтительный тип Доставка почты');--Preferred Shipping Mail Type
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'preferred_shipping_mail_type_id', 'Привилегированные Доставка почты Идентификатор');--Preferred Shipping Mail Type Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'preferred_shipping_package_shape', 'Привилегированные Перевозка груза Пакет Форма');--Preferred Shipping Package Shape
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'preferred_supplier', 'предпочтительным поставщиком');--Preferred Supplier
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'preferred_supplier_id', 'Предпочтительным поставщиком Идентификатор');--Preferred Supplier Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'price', 'цена');--Price
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'price_from', 'Цена от');--Price From
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'price_to', 'Цена до');--Price To
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'price_type_code', 'Цена Тип Код');--Price Type Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'price_type_id', 'Цена Идентификатор');--Price Type Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'price_type_name', 'Цена Тип Название');--Price Type Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'priority', 'приоритет');--Priority
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'purchase_account_id', 'Закупка расходных идентификатор');--Purchase Account Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'purchase_discount_account_id', 'Купить со скидкой аккаунта идентификатор');--Purchase Discount Account Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'purchase_verification_limit', 'Покупка Проверка Limit');--Purchase Verification Limit
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'quantity', 'количество');--Quantity
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'rate', 'ставка');--Rate
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'recurrence_type_id', 'Повторение Идентификатор');--Recurrence Type Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'recurring_amount', 'Периодическая Сумма');--Recurring Amount
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'recurring_duration', 'Период подписки');--Recurring Duration
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'recurring_frequency', 'Периодическая Частота');--Recurring Frequency
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'recurring_frequency_id', 'Периодическая Частота Идентификатор');--Recurring Frequency Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'recurring_invoice', 'Повторяющиеся счета');--Recurring Invoice
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'recurring_invoice_code', 'Повторяющиеся счета Код');--Recurring Invoice Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'recurring_invoice_id', 'Повторяющиеся счета Идентификатор');--Recurring Invoice Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'recurring_invoice_name', 'Повторяющиеся счета Имя');--Recurring Invoice Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'recurring_invoice_setup_id', 'Повторяющиеся счета Setup Идентификатор');--Recurring Invoice Setup Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'recurring_invoices_item_id_auto_trigger_on_sales_uix', 'Вы не можете иметь более одного автоматической синхронизации по продажам для этого элемента.');--You cannot have more than one auto trigger on sales for this item.
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'recurs_on_same_calendar_date', 'Повторяется на том же календарном Дата');--Recurs on Same Calendar Date
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'registration_date', 'Дата регистрации');--Registration Date
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'registration_number', 'Регистрационный номер');--Registration Number
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'relationship_officer_name', 'Имя Отношения директор');--Relationship Officer Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'relname', 'связь Имя');--Relation Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'remote_user', 'Удаленная пользователя');--Remote User
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'reorder_level', 'Изменить порядок Уровень');--Reorder Level
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'reorder_quantity', 'Изменение порядка Количество');--Reorder Quantity
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'reorder_unit', 'Изменение порядка Unit');--Reorder Unit
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'reorder_unit_id', 'Изменить порядок Раздел Идентификатор');--Reorder Unit Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'reporting_tax_authority', 'Отчетность налогового органа');--Reporting Tax Authority
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'reporting_tax_authority_id', 'Отчетность налогового органа идентификатор');--Reporting Tax Authority Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'repository', 'хранилище');--Repository
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'resource', 'ресурс');--Resource
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'resource_id', 'идентификатор ресурса');--Resource Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'resource_key', 'Ресурс Key');--Resource Key
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'role', 'роль');--Role
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'role_code', 'Роль Код');--Role Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'role_id', 'Роль идентификатора');--Role Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'role_name', 'Имя роли');--Role Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'rounding_decimal_places', 'Округление десятичных разрядов');--Rounding Decimal Places
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'rounding_method', 'Округление Метод');--Rounding Method
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'rounding_method_code', 'Округление код метода');--Rounding Method Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'rounding_method_name', 'Округление Имя метода');--Rounding Method Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'sales_account_id', 'Счет по продажам Идентификатор');--Sales Account Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'sales_discount_account_id', 'Продажи Скидка аккаунт Идентификатор');--Sales Discount Account Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'sales_return_account_id', 'Продажи Вернуться аккаунт Идентификатор');--Sales Return Account Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'sales_tax', 'налог с продаж');--Sales Tax
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'sales_tax_code', 'Налоговый код');--Sales Tax Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'sales_tax_detail_code', 'Налог с продаж Деталь Код');--Sales Tax Detail Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'sales_tax_detail_id', 'Налог с продаж Деталь идентификатор');--Sales Tax Detail Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'sales_tax_detail_name', 'Налог с продаж Наименование детали');--Sales Tax Detail Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'sales_tax_details_rate_chk', 'Ставка не должна быть пустой, если вы не выбрали штата или округа налога. Кроме того, вы не можете предоставить как скорость и выбрать для штата или округа налога.');--Rate should not be empty unless you have selected state or county tax. Similarly, you cannot provide both rate and choose to have state or county tax.
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'sales_tax_exempt', 'Освобождаются от налогообложения по продажам');--Sales Tax Exempt
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'sales_tax_exempt_code', 'Налог с продаж Освобождение Код');--Sales Tax Exempt Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'sales_tax_exempt_detail_id', 'Налог с продаж Освобождение Деталь идентификатор');--Sales Tax Exempt Detail Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'sales_tax_exempt_id', 'Налог с продаж Освобождение Идентификатор');--Sales Tax Exempt Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'sales_tax_exempt_name', 'Налог с продаж Освобождение Имя');--Sales Tax Exempt Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'sales_tax_exempts_price_to_chk', 'Поле "Цена от ''должно быть меньше, чем« цена ».');--The field "PriceFrom" must be less than "PriceTo".
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'sales_tax_id', 'Налог с продаж  Идентификатор');--Sales Tax Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'sales_tax_name', 'Налог с продаж Имя');--Sales Tax Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'sales_tax_type', 'Налог с продаж Вид');--Sales Tax Type
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'sales_tax_type_code', 'Налог с продаж введите код');--Sales Tax Type Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'sales_tax_type_id', 'Налог с продаж Идентификатор');--Sales Tax Type Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'sales_tax_type_name', 'Налог с продаж Тип Название');--Sales Tax Type Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'sales_team_code', 'Отдел продаж Код');--Sales Team Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'sales_team_id', 'Отдел продаж Идентификатор');--Sales Team Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'sales_team_name', 'Отдел продаж Имя');--Sales Team Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'sales_verification_limit', 'Продажи Проверка предел');--Sales Verification Limit
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'salesperson_bonus_setup_id', 'Salesperson Бонус Идентификатор Setup');--Salesperson Bonus Setup Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'salesperson_code', 'Salesperson Код');--Salesperson Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'salesperson_id', 'Salesperson Идентификатор');--Salesperson Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'salesperson_name', 'Имя Salesperson');--Salesperson Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'self_verification_limit', 'Самостоятельная проверка Limit');--Self Verification Limit
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'selling_price', 'отпускная цена');--Selling Price
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'selling_price_includes_tax', 'Продажа Цена включает налог');--Selling Price Includes Tax
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'shipper_code', 'Грузовладелец Код');--Shipper Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'shipper_id', 'Грузовладелец Идентификатор');--Shipper Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'shipper_name', 'Грузовладелец Имя');--Shipper Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'shipping_address_code', 'Адрес доставки Код');--Shipping Address Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'shipping_address_id', 'Адрес доставки Идентификатор');--Shipping Address Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'shipping_mail_type_code', 'Доставка Тип Почтовый код');--Shipping Mail Type Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'shipping_mail_type_id', 'Доставка тип почтового идентификатора');--Shipping Mail Type Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'shipping_mail_type_name', 'Доставка тип почтового Имя');--Shipping Mail Type Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'shipping_package_shape_code', 'Перевозка груза Пакет Форма Код');--Shipping Package Shape Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'shipping_package_shape_id', 'Перевозка груза Пакет Форма Идентификатор');--Shipping Package Shape Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'shipping_package_shape_name', 'Перевозка груза Пакет Форма Имя');--Shipping Package Shape Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'slab_name', 'Плиты Имя');--Slab Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'sst_number', 'Государственный налог с продаж Количество');--SST Number
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'starts_from', 'начинается с');--Starts From
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'state', 'государственный');--State
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'state_code', 'код состояния');--State Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'state_id', 'Государственный Идентификатор');--State Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'state_name', 'Государственный Имя');--State Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'state_sales_tax', 'Государственной налоговой продаж');--State Sales Tax
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'state_sales_tax_code', 'Продажи государственного Налоговый кодекс');--State Sales Tax Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'state_sales_tax_id', 'Государственная налоговая Продажи Идентификатор');--State Sales Tax Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'state_sales_tax_name', 'Государственная налоговая продажам Имя');--State Sales Tax Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'statement_reference', 'О себе Ссылка');--Statement Reference
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'stock_details_unit_chk', 'Неверный блок предоставляются.');--Invalid unit provided.
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'store', 'магазин');--Store
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'store_code', 'Код хранения');--Store Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'store_id', 'магазин Идентификатор');--Store Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'store_name', 'Сохранение имени');--Store Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'store_type', 'Тип магазин');--Store Type
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'store_type_code', 'Тип хранения кода');--Store Type Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'store_type_id', 'Тип Магазин  Идентификатор');--Store Type Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'store_type_name', 'Тип название магазина');--Store Type Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'stores_default_cash_account_id_chk', 'Пожалуйста, выберите правильный наличными или банковского счета Идентификатор.');--Please select a valid Cash or Bank AccountId.
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'stores_sales_tax_id_chk', 'Выбрали SalesTax Идентификатор является недопустимым для этой должности.');--The chosen SalesTaxId is invalid for this office.
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'street', 'улица');--Street
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'sub_total', 'Промежуточный итог');--Sub Total
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'sys_type', 'Тип системы');--Sys Type
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'tax', 'налог');--Tax
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'tax_authority_code', 'Налоговый орган код');--Tax Authority Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'tax_authority_id', 'Налоговый орган Идентификатор');--Tax Authority Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'tax_authority_name', 'Имя Налоговая инспекция');--Tax Authority Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'tax_base_amount', 'Налоговая база Сумма');--Tax Base Amount
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'tax_base_amount_type_code', 'Налоговая база Сумма введите код');--Tax Base Amount Type Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'tax_base_amount_type_name', 'Налоговая база Сумма Тип Название');--Tax Base Amount Type Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'tax_code', 'налоговый кодекс');--Tax Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'tax_exempt_type', 'Освобождаются от налогообложения Тип');--Tax Exempt Type
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'tax_exempt_type_code', 'Освобождаются от налогообложения введите код');--Tax Exempt Type Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'tax_exempt_type_id', 'Освобождаются от налогообложения Идентификатор');--Tax Exempt Type Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'tax_exempt_type_name', 'Освобождаются от налогообложения Тип Название');--Tax Exempt Type Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'tax_id', 'Налоговый идентификатор');--Tax Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'tax_master', 'Налоговый Мастер');--Tax Master
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'tax_master_code', 'Налоговый Мастер-код');--Tax Master Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'tax_master_id', 'Налоговый Мастер Идентификатор');--Tax Master Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'tax_master_name', 'Налоговый Магистр Название');--Tax Master Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'tax_name', 'Налоговый Имя');--Tax Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'tax_rate_type', 'Налоговая ставка Тип');--Tax Rate Type
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'tax_rate_type_code', 'Налоги Тип Цена Код');--Tax Rate Type Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'tax_rate_type_name', 'Налоговая ставка Тип Название');--Tax Rate Type Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'tax_type_code', 'Налоги Тип Код');--Tax Type Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'tax_type_id', 'Налоговый Идентификатор');--Tax Type Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'tax_type_name', 'Налоги Тип Имя');--Tax Type Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'to_days', 'Для дней');--To Days
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'total', 'общий');--Total
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'total_duration', 'Общая продолжительность');--Total Duration
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'total_sales', 'Всего продаж');--Total Sales
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'tran_code', 'код транзакции');--Tran Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'tran_type', 'тип операции');--Tran Type
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'unit', 'блок');--Unit
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'unit_code', 'значным кодом');--Unit Code
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'unit_id', 'блок Идентификатор');--Unit Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'unit_name', 'Имя единицы');--Unit Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'url', 'Унифицированный указатель информационного ресурса');--Url
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'use_tax_collecting_account', 'Используйте сбора налогов аккаунт');--Use Tax Collecting Account
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'use_tax_collecting_account_id', 'Используйте сбора налогов аккаунта идентификатор');--Use Tax Collecting Account Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'user_id', 'Идентификатор пользователя');--User Id
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'user_name', 'Имя пользователя');--User Name
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'vacuum_count', 'Вакуумный Граф');--Vacuum Count
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'valid_from', 'Действует с');--Valid From
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'valid_till', 'Действительно до');--Valid Till
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'value', 'значение');--Value
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'value_date', 'Дата валютирования');--Value Date
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'verify_gl_transactions', 'Убедитесь, главной книги операций');--Verify Gl Transactions
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'verify_purchase_transactions', 'Убедитесь, сделок купли-');--Verify Purchase Transactions
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'verify_sales_transactions', 'Убедитесь, сделки купли-продажи');--Verify Sales Transactions
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'weight_in_grams', 'Вес в граммах');--Weight In Grams
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'width_in_centimeters', 'Ширина в см');--Width In Centimeters
SELECT localization.add_localized_resource('ScrudResource', 'ru', 'zip_code', 'Почтовый Индекс');--Zip Code
SELECT localization.add_localized_resource('Titles', 'ru', 'AboutInitializingDayEnd', 'Об инициализации в конце дня');--About Initializing Day End
SELECT localization.add_localized_resource('Titles', 'ru', 'AboutYourOffice', 'О вашем офисе');--About Your Office
SELECT localization.add_localized_resource('Titles', 'ru', 'Access', 'доступ');--Access
SELECT localization.add_localized_resource('Titles', 'ru', 'AccessIsDenied', 'Отказано в доступе.');--Access is denied.
SELECT localization.add_localized_resource('Titles', 'ru', 'Account', 'счет');--Account
SELECT localization.add_localized_resource('Titles', 'ru', 'AccountId', 'Идентификатор аккаунта');--Account Id
SELECT localization.add_localized_resource('Titles', 'ru', 'AccountMaster', 'основной счет');--Account Master
SELECT localization.add_localized_resource('Titles', 'ru', 'AccountName', 'Имя Учетной Записи');--Account Name
SELECT localization.add_localized_resource('Titles', 'ru', 'AccountNumber', 'Номер Счета');--Account Number
SELECT localization.add_localized_resource('Titles', 'ru', 'AccountOverview', 'Обзор аккаунт');--Account Overview
SELECT localization.add_localized_resource('Titles', 'ru', 'AccountStatement', 'Выписка по счету');--Account Statement
SELECT localization.add_localized_resource('Titles', 'ru', 'Action', 'действие');--Action
SELECT localization.add_localized_resource('Titles', 'ru', 'Actions', 'действия');--Actions
SELECT localization.add_localized_resource('Titles', 'ru', 'Actual', 'фактический');--Actual
SELECT localization.add_localized_resource('Titles', 'ru', 'Add', 'добавлять');--Add
SELECT localization.add_localized_resource('Titles', 'ru', 'AddNew', 'Добавить новый');--Add New
SELECT localization.add_localized_resource('Titles', 'ru', 'Address', 'адрес');--Address
SELECT localization.add_localized_resource('Titles', 'ru', 'AddressAndContactInfo', 'Адрес и Контактная информация');--Address & Contact Information
SELECT localization.add_localized_resource('Titles', 'ru', 'AgeingSlabs', 'Старение плиты');--Ageing Slabs
SELECT localization.add_localized_resource('Titles', 'ru', 'AgentBonusSlabAssignment', 'Бонус Плиты Назначение');--Bonus Slab Assignment
SELECT localization.add_localized_resource('Titles', 'ru', 'AgentBonusSlabs', 'Бонус Плиты для продавцов');--Bonus Slab for Salespersons
SELECT localization.add_localized_resource('Titles', 'ru', 'Alerts', 'Предупреждения');--Alerts
SELECT localization.add_localized_resource('Titles', 'ru', 'Amount', 'количество');--Amount
SELECT localization.add_localized_resource('Titles', 'ru', 'AmountInBaseCurrency', 'Сумма (в базовой валюте)');--Amount (In Base Currency)
SELECT localization.add_localized_resource('Titles', 'ru', 'AmountInHomeCurrency', 'Сумма (в местной валюте)');--Amount (In Home Currency)
SELECT localization.add_localized_resource('Titles', 'ru', 'AnalyzeDatabse', 'Анализ базы данных');--Analyze Databse
SELECT localization.add_localized_resource('Titles', 'ru', 'Approve', 'утвердить');--Approve
SELECT localization.add_localized_resource('Titles', 'ru', 'ApproveThisTransaction', 'Утвердить эту сделку');--Approve This Transaction
SELECT localization.add_localized_resource('Titles', 'ru', 'ApprovedTransactions', 'утвержденные операции');--Approved Transactions
SELECT localization.add_localized_resource('Titles', 'ru', 'AreYouSure', 'Вы уверены, что вы знаете, что вы делаете?');--Are you sure?
SELECT localization.add_localized_resource('Titles', 'ru', 'AssignCashier', 'Связать Касса');--Assign Cashier
SELECT localization.add_localized_resource('Titles', 'ru', 'AttachmentsPlus', 'Оснастка (+)');--Attachments (+)
SELECT localization.add_localized_resource('Titles', 'ru', 'AutoVerificationPolicy', 'Авто проверка Политика');--Autoverification Policy
SELECT localization.add_localized_resource('Titles', 'ru', 'AutomaticallyApprovedByWorkflow', 'Автоматически Утверждено Workflow');--Automatically Approved by Workflow
SELECT localization.add_localized_resource('Titles', 'ru', 'Back', 'назад');--Back
SELECT localization.add_localized_resource('Titles', 'ru', 'BackToPreviousPage', 'Вернуться на предыдущую страницу');--Back to Previous Page
SELECT localization.add_localized_resource('Titles', 'ru', 'BackupConsole', 'Резервное копирование консоли');--Backup Console
SELECT localization.add_localized_resource('Titles', 'ru', 'BackupDatabase', 'Резервное копирование базы данных');--Backup Database
SELECT localization.add_localized_resource('Titles', 'ru', 'BackupNow', 'Резервное копирование сейчас');--Backup Now
SELECT localization.add_localized_resource('Titles', 'ru', 'Balance', 'баланс');--Balance
SELECT localization.add_localized_resource('Titles', 'ru', 'BalanceSheet', 'баланс');--Balance Sheet
SELECT localization.add_localized_resource('Titles', 'ru', 'BankAccounts', 'Банковские счета');--Bank Accounts
SELECT localization.add_localized_resource('Titles', 'ru', 'BankTransactionCode', 'Банк Код транзакции');--Bank Transaction Code
SELECT localization.add_localized_resource('Titles', 'ru', 'BaseCurrency', 'базисная валюта');--Base Currency
SELECT localization.add_localized_resource('Titles', 'ru', 'BaseUnitName', 'Имя базового блока');--Base Unit Name
SELECT localization.add_localized_resource('Titles', 'ru', 'BonusSlabDetails', 'Бонусные Плиты Детали для продавцов');--Bonus Slab Details for Salespersons
SELECT localization.add_localized_resource('Titles', 'ru', 'Book', 'книга');--Book
SELECT localization.add_localized_resource('Titles', 'ru', 'BookDate', 'Книга Дата');--Book Date
SELECT localization.add_localized_resource('Titles', 'ru', 'Brand', 'марка');--Brand
SELECT localization.add_localized_resource('Titles', 'ru', 'Brands', 'Бренды');--Brands
SELECT localization.add_localized_resource('Titles', 'ru', 'Browse', 'просматривать');--Browse
SELECT localization.add_localized_resource('Titles', 'ru', 'CSTNumber', 'Количество CST');--CST Number
SELECT localization.add_localized_resource('Titles', 'ru', 'Cancel', 'отменить');--Cancel
SELECT localization.add_localized_resource('Titles', 'ru', 'CashFlowHeading', 'О движении денежных средств товарной позиции');--Cash Flow Heading
SELECT localization.add_localized_resource('Titles', 'ru', 'CashFlowHeadings', 'Денежные средства Заголовки потока');--Cash Flow Headings
SELECT localization.add_localized_resource('Titles', 'ru', 'CashFlowSetup', 'Денежный поток установки');--Cash Flow Setup
SELECT localization.add_localized_resource('Titles', 'ru', 'CashRepositories', 'Денежные Хранилища');--Cash Repositories
SELECT localization.add_localized_resource('Titles', 'ru', 'CashRepository', 'Денежные средства Repository');--Cash Repository
SELECT localization.add_localized_resource('Titles', 'ru', 'CashRepositoryBalance', 'Остатки денежных средств репозиторий');--Cash Repository Balance
SELECT localization.add_localized_resource('Titles', 'ru', 'CashTransaction', 'сделка за наличный расчет');--Cash Transaction
SELECT localization.add_localized_resource('Titles', 'ru', 'ChangePassword', 'Изменить Пароль');--Change Password
SELECT localization.add_localized_resource('Titles', 'ru', 'ChangeSideWhenNegative', 'Изменить стороне, когда Отрицательный');--Change Side When Negative
SELECT localization.add_localized_resource('Titles', 'ru', 'ChartOfAccounts', 'План счетов');--Chart of Accounts
SELECT localization.add_localized_resource('Titles', 'ru', 'Check', 'Проверить');--Check
SELECT localization.add_localized_resource('Titles', 'ru', 'CheckAll', 'Проверьте все');--Check All
SELECT localization.add_localized_resource('Titles', 'ru', 'Checklists', 'Контрольные');--Checklists
SELECT localization.add_localized_resource('Titles', 'ru', 'Clear', 'ясно');--Clear
SELECT localization.add_localized_resource('Titles', 'ru', 'Close', 'близко');--Close
SELECT localization.add_localized_resource('Titles', 'ru', 'ClosedTransactions', 'Закрытые Сделки');--Closed Transactions
SELECT localization.add_localized_resource('Titles', 'ru', 'ClosingBalance', 'конечное сальдо');--Closing Balance
SELECT localization.add_localized_resource('Titles', 'ru', 'ClosingCredit', 'Закрытие Кредит');--Closing Credit
SELECT localization.add_localized_resource('Titles', 'ru', 'ClosingDebit', 'Закрытие Дебет');--Closing Debit
SELECT localization.add_localized_resource('Titles', 'ru', 'Comment', 'комментарий');--Comment
SELECT localization.add_localized_resource('Titles', 'ru', 'CompoundItemDetails', 'Соединение Пункт подробности');--Compound Item Details
SELECT localization.add_localized_resource('Titles', 'ru', 'CompoundItems', 'Составные товары');--Compound Items
SELECT localization.add_localized_resource('Titles', 'ru', 'CompoundUnitsOfMeasure', 'Составные единицы измерения');--Compound Units of Measure
SELECT localization.add_localized_resource('Titles', 'ru', 'Confidential', 'конфиденциальная');--Confidential
SELECT localization.add_localized_resource('Titles', 'ru', 'ConfirmPassword', 'Подтвердите Пароль');--Confirm Password
SELECT localization.add_localized_resource('Titles', 'ru', 'ConvertedtoBaseCurrency', 'В пересчете на базовой валюте');--Converted to Base Currency
SELECT localization.add_localized_resource('Titles', 'ru', 'ConvertedtoHomeCurrency', 'В пересчете на местной валюте');--Converted to Home Currency
SELECT localization.add_localized_resource('Titles', 'ru', 'CostCenter', 'Центр Стоимость');--Cost Center
SELECT localization.add_localized_resource('Titles', 'ru', 'CostCenters', 'Центр Стоимость');--Cost Centers
SELECT localization.add_localized_resource('Titles', 'ru', 'Counters', 'Счетчики');--Counters
SELECT localization.add_localized_resource('Titles', 'ru', 'Counties', 'Округа');--Counties
SELECT localization.add_localized_resource('Titles', 'ru', 'Countries', 'Страны');--Countries
SELECT localization.add_localized_resource('Titles', 'ru', 'CountySalesTaxes', 'Налоги графства продаж');--County Sales Taxes
SELECT localization.add_localized_resource('Titles', 'ru', 'CreateaUserAccountforYourself', 'Создание учетной записи пользователя для себя');--Create a User Account for Yourself
SELECT localization.add_localized_resource('Titles', 'ru', 'CreatedOn', 'Дата создания');--Created On
SELECT localization.add_localized_resource('Titles', 'ru', 'Credit', 'кредит');--Credit
SELECT localization.add_localized_resource('Titles', 'ru', 'CreditAllowed', 'Кредитная животных');--Credit Allowed
SELECT localization.add_localized_resource('Titles', 'ru', 'CreditTotal', 'Кредитная Всего');--Credit Total
SELECT localization.add_localized_resource('Titles', 'ru', 'CtrlAltA', 'Ctrl + Alt + A');--Ctrl + Alt + A
SELECT localization.add_localized_resource('Titles', 'ru', 'CtrlAltC', 'Ctrl + Alt + C');--Ctrl + Alt + C
SELECT localization.add_localized_resource('Titles', 'ru', 'CtrlAltD', 'Ctrl + Alt + D');--Ctrl + Alt + D
SELECT localization.add_localized_resource('Titles', 'ru', 'CtrlAltS', 'Ctrl + Alt + S');--Ctrl + Alt + S
SELECT localization.add_localized_resource('Titles', 'ru', 'CtrlAltT', 'Ctrl + Alt + T');--Ctrl + Alt + T
SELECT localization.add_localized_resource('Titles', 'ru', 'CtrlReturn', 'Ctrl + Return');--Ctrl + Return
SELECT localization.add_localized_resource('Titles', 'ru', 'Currencies', 'валюты');--Currencies
SELECT localization.add_localized_resource('Titles', 'ru', 'Currency', 'валюта');--Currency
SELECT localization.add_localized_resource('Titles', 'ru', 'CurrencyCode', 'Код валюты');--Currency Code
SELECT localization.add_localized_resource('Titles', 'ru', 'CurrencyName', 'Название валюты');--Currency Name
SELECT localization.add_localized_resource('Titles', 'ru', 'CurrencySymbol', 'Символ валюты');--Currency Symbol
SELECT localization.add_localized_resource('Titles', 'ru', 'CurrentBookDate', 'Текущий Книга Дата');--Current Book Date
SELECT localization.add_localized_resource('Titles', 'ru', 'CurrentIP', 'Текущий IP');--Current IP
SELECT localization.add_localized_resource('Titles', 'ru', 'CurrentLoginOn', 'Текущий Войти на');--Current Login On
SELECT localization.add_localized_resource('Titles', 'ru', 'CurrentPassword', 'Текущий Пароль');--Current Password
SELECT localization.add_localized_resource('Titles', 'ru', 'CurrentPeriod', 'текущий период');--Current Period
SELECT localization.add_localized_resource('Titles', 'ru', 'CustomerCode', 'Код клиента');--Customer Code
SELECT localization.add_localized_resource('Titles', 'ru', 'CustomerName', 'Имя клиента');--Customer Name
SELECT localization.add_localized_resource('Titles', 'ru', 'CustomerPanNumber', 'Номер клиента постоянного счета #');--Customer PAN #
SELECT localization.add_localized_resource('Titles', 'ru', 'CustomerPaysFees', 'Заказчик оплачивает сборы');--Customer Pays Fees
SELECT localization.add_localized_resource('Titles', 'ru', 'DatabaseBackups', 'Резервное копирование баз данных');--Database Backups
SELECT localization.add_localized_resource('Titles', 'ru', 'DatabaseStatistics', 'Статистика базы данных');--Database Statistics
SELECT localization.add_localized_resource('Titles', 'ru', 'Date', 'дата');--Date
SELECT localization.add_localized_resource('Titles', 'ru', 'Day', 'день');--Day
SELECT localization.add_localized_resource('Titles', 'ru', 'Days', 'дней');--Days
SELECT localization.add_localized_resource('Titles', 'ru', 'Debit', 'дебет');--Debit
SELECT localization.add_localized_resource('Titles', 'ru', 'DebitTotal', 'Дебет Всего');--Debit Total
SELECT localization.add_localized_resource('Titles', 'ru', 'DefaultAddress', 'Адрес по умолчанию');--Default Address
SELECT localization.add_localized_resource('Titles', 'ru', 'DefaultCurrency', 'Базовая валюта');--Default Currency
SELECT localization.add_localized_resource('Titles', 'ru', 'DefaultReorderQuantityAbbreviated', 'По умолчанию Reorder Количество');--Default Reorder Qty
SELECT localization.add_localized_resource('Titles', 'ru', 'Definition', 'определение');--Definition
SELECT localization.add_localized_resource('Titles', 'ru', 'Delete', 'удалять');--Delete
SELECT localization.add_localized_resource('Titles', 'ru', 'DeleteSelected', 'Удалить выбранные');--Delete Selected
SELECT localization.add_localized_resource('Titles', 'ru', 'DeliverTo', 'доставить');--Deliver To
SELECT localization.add_localized_resource('Titles', 'ru', 'Department', 'отдел');--Department
SELECT localization.add_localized_resource('Titles', 'ru', 'Departments', 'ведомства');--Departments
SELECT localization.add_localized_resource('Titles', 'ru', 'Difference', 'разница');--Difference
SELECT localization.add_localized_resource('Titles', 'ru', 'DirectPurchase', 'Прямая Покупка');--Direct Purchase
SELECT localization.add_localized_resource('Titles', 'ru', 'DirectSales', 'Прямые продажи');--Direct Sales
SELECT localization.add_localized_resource('Titles', 'ru', 'Discount', 'скидка');--Discount
SELECT localization.add_localized_resource('Titles', 'ru', 'Documentation', 'документация');--Documentation
SELECT localization.add_localized_resource('Titles', 'ru', 'Download', 'скачать');--Download
SELECT localization.add_localized_resource('Titles', 'ru', 'DownloadSourceCode', 'Скачать исходный код');--Download Source Code
SELECT localization.add_localized_resource('Titles', 'ru', 'DueDate', 'Срок Оплаты');--Due Date
SELECT localization.add_localized_resource('Titles', 'ru', 'EODBegun', 'Конец дня обработка Бегун');--End of Day Processing Has Begun
SELECT localization.add_localized_resource('Titles', 'ru', 'EODConsole', 'В конце дня консоли');--EOD Console
SELECT localization.add_localized_resource('Titles', 'ru', 'ER', 'Эффективная ставка');--ER
SELECT localization.add_localized_resource('Titles', 'ru', 'ERToBaseCurrency', 'Обменный курс (Для базовой валюты)');--Exchange Rate (To Base Currency)
SELECT localization.add_localized_resource('Titles', 'ru', 'ERToHomeCurrency', 'Обменный курс (To Home Валюта)');--Exchange Rate (To Home Currency)
SELECT localization.add_localized_resource('Titles', 'ru', 'EditSelected', 'Изменить выбранные');--Edit Selected
SELECT localization.add_localized_resource('Titles', 'ru', 'Email', 'Email');--Email
SELECT localization.add_localized_resource('Titles', 'ru', 'EmailAddress', 'Адрес Электронной Почты');--Email Address
SELECT localization.add_localized_resource('Titles', 'ru', 'EmailThisDelivery', 'Электронная почта этой поставки');--Email This Delivery
SELECT localization.add_localized_resource('Titles', 'ru', 'EmailThisInvoice', 'Электронная почта этот счет');--Email This Invoice
SELECT localization.add_localized_resource('Titles', 'ru', 'EmailThisNote', 'Пошлите это к сведению');--Email This Note
SELECT localization.add_localized_resource('Titles', 'ru', 'EmailThisOrder', 'Электронная почта Этот заказ');--Email This Order
SELECT localization.add_localized_resource('Titles', 'ru', 'EmailThisQuotation', 'Электронная почта эта цитата');--Email This Quotation
SELECT localization.add_localized_resource('Titles', 'ru', 'EmailThisReceipt', 'Электронная почта эту квитанцию');--Email This Receipt
SELECT localization.add_localized_resource('Titles', 'ru', 'EmailThisReturn', 'Электронная почта Этот RecReturneip');--Email This Return
SELECT localization.add_localized_resource('Titles', 'ru', 'EndOfDayOperation', 'Конец операционного дня');--End of Day Operation
SELECT localization.add_localized_resource('Titles', 'ru', 'EnterBackupName', 'Электронная почта Этот возврат...');--Enter Backup Name
SELECT localization.add_localized_resource('Titles', 'ru', 'EnterNewPassword', 'Введите новый пароль');--Enter a New Password
SELECT localization.add_localized_resource('Titles', 'ru', 'EnteredBy', 'Вступил По');--Entered By
SELECT localization.add_localized_resource('Titles', 'ru', 'Entities', 'юридические лица');--Entities
SELECT localization.add_localized_resource('Titles', 'ru', 'ExchangeRate', 'Обменный Курс');--Exchange Rate
SELECT localization.add_localized_resource('Titles', 'ru', 'Execute', 'выполнять');--Execute
SELECT localization.add_localized_resource('Titles', 'ru', 'ExternalCode', 'Внешний код');--External Code
SELECT localization.add_localized_resource('Titles', 'ru', 'Factor', 'фактор');--Factor
SELECT localization.add_localized_resource('Titles', 'ru', 'Fax', 'факс');--Fax
SELECT localization.add_localized_resource('Titles', 'ru', 'FilePath', 'Путь к файлу');--File Path
SELECT localization.add_localized_resource('Titles', 'ru', 'FinalDueAmountinBaseCurrency', 'Заключительный надлежащей суммы в базовой валюте');--Final Due Amount in Base Currency
SELECT localization.add_localized_resource('Titles', 'ru', 'FirstPage', 'Первая страница');--First Page
SELECT localization.add_localized_resource('Titles', 'ru', 'FiscalYear', 'Финансовый Год');--Fiscal Year
SELECT localization.add_localized_resource('Titles', 'ru', 'Flag', 'флаг');--Flag
SELECT localization.add_localized_resource('Titles', 'ru', 'FlagBackgroundColor', 'Цвет флаг фон');--Flag Background Color
SELECT localization.add_localized_resource('Titles', 'ru', 'FlagDescription', 'Вы можете отметить эту сделку с флагом, однако вы не сможете увидеть флажки созданные другими пользователями.');--You can mark this transaction with a flag, however you will not be able to see the flags created by other users.
SELECT localization.add_localized_resource('Titles', 'ru', 'FlagForegroundColor', 'Флаг цвет переднего плана');--Flag Foreground Color
SELECT localization.add_localized_resource('Titles', 'ru', 'FlagThisTransaction', 'Флаг Это сделка');--Flag This Transaction
SELECT localization.add_localized_resource('Titles', 'ru', 'FlaggedTransactions', 'Отмеченные Сделки');--Flagged Transactions
SELECT localization.add_localized_resource('Titles', 'ru', 'Flags', 'Флаги');--Flags
SELECT localization.add_localized_resource('Titles', 'ru', 'Frequencies', 'частоты');--Frequencies
SELECT localization.add_localized_resource('Titles', 'ru', 'From', 'от');--From
SELECT localization.add_localized_resource('Titles', 'ru', 'GLAdvice', 'Главная книга Советы');--GL Advice
SELECT localization.add_localized_resource('Titles', 'ru', 'GLDetails', 'General Ledger Подробнее');--GL Details
SELECT localization.add_localized_resource('Titles', 'ru', 'GLHead', 'GL Руководитель');--GL Head
SELECT localization.add_localized_resource('Titles', 'ru', 'Go', 'идти');--Go
SELECT localization.add_localized_resource('Titles', 'ru', 'GoToTop', 'Перейти к началу');--GoToTop
SELECT localization.add_localized_resource('Titles', 'ru', 'GoodsReceiptNote', 'Поступление Примечание');--Goods Receipt Note
SELECT localization.add_localized_resource('Titles', 'ru', 'GrandTotal', 'Общий Итог');--Grand Total
SELECT localization.add_localized_resource('Titles', 'ru', 'Home', 'домой');--Home
SELECT localization.add_localized_resource('Titles', 'ru', 'HomeCurrency', 'Главная валют');--Home Currency
SELECT localization.add_localized_resource('Titles', 'ru', 'HundredthName', 'Сотый Имя');--Hundredth Name
SELECT localization.add_localized_resource('Titles', 'ru', 'Id', 'идентификатор');--Id
SELECT localization.add_localized_resource('Titles', 'ru', 'InVerificationStack', 'В проверке стека');--In Verification Stack
SELECT localization.add_localized_resource('Titles', 'ru', 'IncludeZeroBalanceAccounts', 'Включить нулевой баланс счетов');--Include Zero Balance Accounts
SELECT localization.add_localized_resource('Titles', 'ru', 'Industries', 'промышленности');--Industries
SELECT localization.add_localized_resource('Titles', 'ru', 'InitializeDayEnd', 'Инициализация в конце дня');--Initialize Day End
SELECT localization.add_localized_resource('Titles', 'ru', 'InstallMixERP', 'Установите MixERP');--Install MixERP
SELECT localization.add_localized_resource('Titles', 'ru', 'InstrumentCode', 'Инструмент Код');--Instrument Code
SELECT localization.add_localized_resource('Titles', 'ru', 'InterestApplicable', 'ставка, применяемая');--Interest Applicable
SELECT localization.add_localized_resource('Titles', 'ru', 'InvalidDate', 'Это не действительной датой.');--This is not a valid date.
SELECT localization.add_localized_resource('Titles', 'ru', 'InvalidImage', 'Это не действует изображения.');--This is not a valid image.
SELECT localization.add_localized_resource('Titles', 'ru', 'InventoryAdvice', 'Инвентаризация Советы');--Inventory Advice
SELECT localization.add_localized_resource('Titles', 'ru', 'InvoiceAmount', 'Сумма счета');--Invoice Amount
SELECT localization.add_localized_resource('Titles', 'ru', 'InvoiceDetails', 'Счет-фактура Подробнее');--Invoice Details
SELECT localization.add_localized_resource('Titles', 'ru', 'IsCash', 'это денежные средства');--Is Cash
SELECT localization.add_localized_resource('Titles', 'ru', 'IsEmployee', 'Является сотрудником');--Is Employee
SELECT localization.add_localized_resource('Titles', 'ru', 'IsParty', 'является участником');--Is Party
SELECT localization.add_localized_resource('Titles', 'ru', 'IsSystemAccount', 'Это Системная учетная запись');--Is System Account
SELECT localization.add_localized_resource('Titles', 'ru', 'ItemCode', 'Код товара');--Item Code
SELECT localization.add_localized_resource('Titles', 'ru', 'ItemCostPrices', 'Статья расходов Цены');--Item Cost Prices
SELECT localization.add_localized_resource('Titles', 'ru', 'ItemGroup', 'Пункт Группа');--Item Group
SELECT localization.add_localized_resource('Titles', 'ru', 'ItemGroups', 'Группы товаров');--Item Groups
SELECT localization.add_localized_resource('Titles', 'ru', 'ItemId', 'идентификатор элемента');--Item Id
SELECT localization.add_localized_resource('Titles', 'ru', 'ItemName', 'Название товара');--Item Name
SELECT localization.add_localized_resource('Titles', 'ru', 'ItemOverview', 'Пункт Обзор');--Item Overview
SELECT localization.add_localized_resource('Titles', 'ru', 'ItemSellingPrices', 'Пункт Продажа Цены');--Item Selling Prices
SELECT localization.add_localized_resource('Titles', 'ru', 'ItemType', 'Тип элемента');--Item Type
SELECT localization.add_localized_resource('Titles', 'ru', 'ItemTypes', 'Типы элементов');--Item Types
SELECT localization.add_localized_resource('Titles', 'ru', 'Items', 'Предметы');--Items
SELECT localization.add_localized_resource('Titles', 'ru', 'ItemsBelowReorderLevel', 'Ниже пункты изменить порядок уровне');--Items Below Reorder Level
SELECT localization.add_localized_resource('Titles', 'ru', 'JournalVoucher', 'Журнал Ваучер');--Journal Voucher
SELECT localization.add_localized_resource('Titles', 'ru', 'JournalVoucherEntry', 'Журнал Ваучер запись');--Journal Voucher Entry
SELECT localization.add_localized_resource('Titles', 'ru', 'KeyColumnEmptyExceptionMessage', 'Ключ Столбец имущество не может быть пустым.');--The property 'KeyColumn' cannot be left empty.
SELECT localization.add_localized_resource('Titles', 'ru', 'LCCredit', 'Аккредитив кредитов');--LC Credit
SELECT localization.add_localized_resource('Titles', 'ru', 'LCDebit', 'Аккредитив дебету');--LC Debit
SELECT localization.add_localized_resource('Titles', 'ru', 'LastAccessedOn', 'Последняя доступна на');--Last Accessed On
SELECT localization.add_localized_resource('Titles', 'ru', 'LastLoginIP', 'Последняя Войти IP');--Last Login IP
SELECT localization.add_localized_resource('Titles', 'ru', 'LastLoginOn', 'Последняя Войти на');--Last Login On
SELECT localization.add_localized_resource('Titles', 'ru', 'LastPage', 'Предыдущая страница');--Last Page
SELECT localization.add_localized_resource('Titles', 'ru', 'LastPaymentDate', 'Последний платеж Дата');--Last Payment Date
SELECT localization.add_localized_resource('Titles', 'ru', 'LastWrittenOn', 'Последнее, написанных на');--Last Written On
SELECT localization.add_localized_resource('Titles', 'ru', 'LateFees', 'Штраф за просрочку платежей');--Late Fees
SELECT localization.add_localized_resource('Titles', 'ru', 'LeadSources', 'Ведущие Источники');--Lead Sources
SELECT localization.add_localized_resource('Titles', 'ru', 'LeadStatuses', 'Ведущие Статусы');--Lead Statuses
SELECT localization.add_localized_resource('Titles', 'ru', 'LeadTime', 'Время Выполнения');--Lead Time
SELECT localization.add_localized_resource('Titles', 'ru', 'ListItems', 'Список Предметов');--List Items
SELECT localization.add_localized_resource('Titles', 'ru', 'Load', 'нагрузка');--Load
SELECT localization.add_localized_resource('Titles', 'ru', 'LoggedInTo', 'Вошедшего в');--Logged in to
SELECT localization.add_localized_resource('Titles', 'ru', 'LoginView', 'Войти Посмотреть');--Login View
SELECT localization.add_localized_resource('Titles', 'ru', 'ManageProfile', 'Управление профиля');--Manage Profile
SELECT localization.add_localized_resource('Titles', 'ru', 'MaximumCreditAmount', 'Максимальная сумма кредита');--Maximum Credit Amount
SELECT localization.add_localized_resource('Titles', 'ru', 'MaximumCreditPeriod', 'Максимальный размер кредита Период');--Maximum Credit Period
SELECT localization.add_localized_resource('Titles', 'ru', 'MenuAccessPolicy', 'Меню политика доступа');--Menu Access Policy
SELECT localization.add_localized_resource('Titles', 'ru', 'MenuCode', 'Меню Код');--Menu Code
SELECT localization.add_localized_resource('Titles', 'ru', 'MenuId', 'Меню Идентификатор');--Menu Id
SELECT localization.add_localized_resource('Titles', 'ru', 'MenuText', 'Текст меню');--Menu Text
SELECT localization.add_localized_resource('Titles', 'ru', 'MerchantFeeInPercent', 'Торговец Стоимость (в процентах)');--Merchant Fee (In percent)
SELECT localization.add_localized_resource('Titles', 'ru', 'MerchantFeeSetup', 'Торговец Стоимость установки');--Merchant Fee Setup
SELECT localization.add_localized_resource('Titles', 'ru', 'MergeBatchToGRN', 'Слияние партии к поступления материала ноте');--Merge Batch to GRN
SELECT localization.add_localized_resource('Titles', 'ru', 'MergeBatchToSalesDelivery', 'Слияние партии к продажам Доставка');--Merge Batch to Sales Delivery
SELECT localization.add_localized_resource('Titles', 'ru', 'MergeBatchToSalesOrder', 'Слияние пакетного на заказ клиента');--Merge Batch to Sales Order
SELECT localization.add_localized_resource('Titles', 'ru', 'MixERPDocumentation', 'MixERP Документация');--MixERP Documentation
SELECT localization.add_localized_resource('Titles', 'ru', 'MixERPLinks', 'MixERP Ссылки');--MixERP Links
SELECT localization.add_localized_resource('Titles', 'ru', 'MixERPOnFacebook', 'MixERP на Facebook');--MixERP on Facebook
SELECT localization.add_localized_resource('Titles', 'ru', 'Month', 'месяц');--Month
SELECT localization.add_localized_resource('Titles', 'ru', 'Name', 'имя');--Name
SELECT localization.add_localized_resource('Titles', 'ru', 'NewBookDate', 'Новая книга Дата');--New Book Date
SELECT localization.add_localized_resource('Titles', 'ru', 'NewJournalEntry', 'Новый журнал запись');--New Journal Entry
SELECT localization.add_localized_resource('Titles', 'ru', 'NewPassword', 'Новый Пароль');--New Password
SELECT localization.add_localized_resource('Titles', 'ru', 'NextPage', 'Следующая страница');--Next Page
SELECT localization.add_localized_resource('Titles', 'ru', 'No', 'Нет');--No
SELECT localization.add_localized_resource('Titles', 'ru', 'NonTaxableSales', 'необлагаемый продаж');--Nontaxable Sales
SELECT localization.add_localized_resource('Titles', 'ru', 'None', 'Ни один');--None
SELECT localization.add_localized_resource('Titles', 'ru', 'NormallyDebit', 'Обычно Дебет');--Normally Debit
SELECT localization.add_localized_resource('Titles', 'ru', 'NothingSelected', 'Ничего не выбрано.');--Nothing selected!
SELECT localization.add_localized_resource('Titles', 'ru', 'Notifications', 'Уведомления');--Notifications
SELECT localization.add_localized_resource('Titles', 'ru', 'OK', 'хОРОШО');--OK
SELECT localization.add_localized_resource('Titles', 'ru', 'Office', 'офис');--Office
SELECT localization.add_localized_resource('Titles', 'ru', 'OfficeCode', 'код бюро');--Office Code
SELECT localization.add_localized_resource('Titles', 'ru', 'OfficeInformation', 'Информационный');--Office Information
SELECT localization.add_localized_resource('Titles', 'ru', 'OfficeName', 'Имя офиса');--Office Name
SELECT localization.add_localized_resource('Titles', 'ru', 'OfficeNickName', 'Имя Офисная Ник');--Office Nick Name
SELECT localization.add_localized_resource('Titles', 'ru', 'OfficeSetup', 'Программа установки офис');--Office Setup
SELECT localization.add_localized_resource('Titles', 'ru', 'OnlyNumbersAllowed', 'Пожалуйста, введите правильный номер.');--Please type a valid number.
SELECT localization.add_localized_resource('Titles', 'ru', 'OpeningInventory', 'Открытие Инвентарь');--Opening Inventory
SELECT localization.add_localized_resource('Titles', 'ru', 'OpportunityStages', 'Возможность Этапы');--Opportunity Stages
SELECT localization.add_localized_resource('Titles', 'ru', 'OtherDetails', 'Другие подробности');--Other Details
SELECT localization.add_localized_resource('Titles', 'ru', 'PANNumber', 'PAN Количество');--PAN Number
SELECT localization.add_localized_resource('Titles', 'ru', 'PageN', 'Страница {0}');--Page {0}
SELECT localization.add_localized_resource('Titles', 'ru', 'ParentAccount', 'родитель счета');--Parent Account
SELECT localization.add_localized_resource('Titles', 'ru', 'Parties', 'стороны');--Parties
SELECT localization.add_localized_resource('Titles', 'ru', 'Party', 'партия');--Party
SELECT localization.add_localized_resource('Titles', 'ru', 'PartyCode', 'партия Код');--Party Code
SELECT localization.add_localized_resource('Titles', 'ru', 'PartyName', 'Имя партия');--Party Name
SELECT localization.add_localized_resource('Titles', 'ru', 'PartySummary', 'партия Резюме');--Party Summary
SELECT localization.add_localized_resource('Titles', 'ru', 'PartyType', 'Тип партия');--Party Type
SELECT localization.add_localized_resource('Titles', 'ru', 'PartyTypes', 'Вечеринка Типы');--Party Types
SELECT localization.add_localized_resource('Titles', 'ru', 'Password', 'пароль');--Password
SELECT localization.add_localized_resource('Titles', 'ru', 'PasswordUpdated', 'Пароль был обновлен.');--Password was updated.
SELECT localization.add_localized_resource('Titles', 'ru', 'PaymentCards', 'Платежные карты');--Payment Cards
SELECT localization.add_localized_resource('Titles', 'ru', 'PaymentTerms', 'условия платежа');--Payment Terms
SELECT localization.add_localized_resource('Titles', 'ru', 'PerformEODOperation', 'Выполните конце дня работы');--Perform EOD Operation
SELECT localization.add_localized_resource('Titles', 'ru', 'PerformingEODOperation', 'Выполнение конце дня работы');--Performing EOD Operation
SELECT localization.add_localized_resource('Titles', 'ru', 'Phone', 'телефон');--Phone
SELECT localization.add_localized_resource('Titles', 'ru', 'PlaceReorderRequests', 'Поместите повторно запросы заказа');--Place Reorder Request(s)
SELECT localization.add_localized_resource('Titles', 'ru', 'PostTransaction', 'сообщение сделка');--Post Transaction
SELECT localization.add_localized_resource('Titles', 'ru', 'PostedBy', 'Автор');--Posted By
SELECT localization.add_localized_resource('Titles', 'ru', 'PostedDate', 'Сообщение Дата');--Posted Date
SELECT localization.add_localized_resource('Titles', 'ru', 'PreferredSupplier', 'предпочтительным поставщиком');--Preferred Supplier
SELECT localization.add_localized_resource('Titles', 'ru', 'PreferredSupplierIdAbbreviated', 'Предпочтительные идентификатор Поставщик');--Pref SupId
SELECT localization.add_localized_resource('Titles', 'ru', 'Prepare', 'подготовить');--Prepare
SELECT localization.add_localized_resource('Titles', 'ru', 'PreparedOn', 'Подготовлено на');--Prepared On
SELECT localization.add_localized_resource('Titles', 'ru', 'Preview', 'предварительный просмотр');--Preview
SELECT localization.add_localized_resource('Titles', 'ru', 'PreviousBalance', 'Предыдущая Баланс');--Previous Balance
SELECT localization.add_localized_resource('Titles', 'ru', 'PreviousCredit', 'Предыдущая Кредит');--Previous Credit
SELECT localization.add_localized_resource('Titles', 'ru', 'PreviousDebit', 'Предыдущая Дебет');--Previous Debit
SELECT localization.add_localized_resource('Titles', 'ru', 'PreviousPage', 'Предыдущая страница');--Previous Page
SELECT localization.add_localized_resource('Titles', 'ru', 'PreviousPeriod', 'Предыдущий период');--Previous Period  
SELECT localization.add_localized_resource('Titles', 'ru', 'Price', 'цена');--Price
SELECT localization.add_localized_resource('Titles', 'ru', 'PriceType', 'Цена Тип');--Price Type
SELECT localization.add_localized_resource('Titles', 'ru', 'Print', 'печать');--Print
SELECT localization.add_localized_resource('Titles', 'ru', 'PrintGlEntry', 'Распечатать общую запись ГК');--Print GL Entry
SELECT localization.add_localized_resource('Titles', 'ru', 'PrintReceipt', 'Распечатать Получение');--Print Receipt
SELECT localization.add_localized_resource('Titles', 'ru', 'ProfitAndLossStatement', 'Прибыль и убытках');--Profit & Loss Statement
SELECT localization.add_localized_resource('Titles', 'ru', 'Progress', 'прогресс');--Progress
SELECT localization.add_localized_resource('Titles', 'ru', 'PurchaseInvoice', 'Покупка Счет');--Purchase Invoice
SELECT localization.add_localized_resource('Titles', 'ru', 'PurchaseOrder', 'Заказ На Покупку');--Purchase Order
SELECT localization.add_localized_resource('Titles', 'ru', 'PurchaseReturn', 'Покупка Возврат');--Purchase Return
SELECT localization.add_localized_resource('Titles', 'ru', 'PurchaseType', 'Тип Покупка');--Purchase Type
SELECT localization.add_localized_resource('Titles', 'ru', 'Quantity', 'количество');--Quantity
SELECT localization.add_localized_resource('Titles', 'ru', 'QuantityAbbreviated', 'количество');--Qty
SELECT localization.add_localized_resource('Titles', 'ru', 'QuantityOnHandAbbreviated', 'Количество (на руки)');--Qty (On Hand)
SELECT localization.add_localized_resource('Titles', 'ru', 'Rate', 'ставка');--Rate
SELECT localization.add_localized_resource('Titles', 'ru', 'Reason', 'причина');--Reason
SELECT localization.add_localized_resource('Titles', 'ru', 'Receipt', 'квитанция');--Receipt
SELECT localization.add_localized_resource('Titles', 'ru', 'ReceiptAmount', 'получение сумма');--Receipt Amount
SELECT localization.add_localized_resource('Titles', 'ru', 'ReceiptCurrency', 'Получение валют');--Receipt Currency
SELECT localization.add_localized_resource('Titles', 'ru', 'ReceiptType', 'Получение Тип');--Receipt Type
SELECT localization.add_localized_resource('Titles', 'ru', 'ReceivedAmountInaboveCurrency', 'Полученную сумму (в выше Валюты)');--Received Amount (In above Currency)
SELECT localization.add_localized_resource('Titles', 'ru', 'ReceivedCurrency', 'получаемую валюту');--Received Currency
SELECT localization.add_localized_resource('Titles', 'ru', 'Reconcile', 'согласовать');--Reconcile
SELECT localization.add_localized_resource('Titles', 'ru', 'RecurringInvoiceSetup', 'Повторяющиеся установки Счет');--Recurring Invoice Setup
SELECT localization.add_localized_resource('Titles', 'ru', 'RecurringInvoices', 'Повторяющиеся Счета');--Recurring Invoices
SELECT localization.add_localized_resource('Titles', 'ru', 'ReferenceNumber', 'Ссылка #');--Reference Number
SELECT localization.add_localized_resource('Titles', 'ru', 'ReferenceNumberAbbreviated', 'Ссылка #');--Ref#
SELECT localization.add_localized_resource('Titles', 'ru', 'RefererenceNumberAbbreviated', 'Ссылка #');--Ref #
SELECT localization.add_localized_resource('Titles', 'ru', 'RegistrationDate', 'Дата Регистрации');--Registration Date
SELECT localization.add_localized_resource('Titles', 'ru', 'Reject', 'отклонить');--Reject
SELECT localization.add_localized_resource('Titles', 'ru', 'RejectThisTransaction', 'Отклонить Эта сделка');--Reject This Transaction
SELECT localization.add_localized_resource('Titles', 'ru', 'RejectedTransactions', 'Забракованные Сделки');--Rejected Transactions
SELECT localization.add_localized_resource('Titles', 'ru', 'RememberMe', 'Запомнить Меня');--Remember Me
SELECT localization.add_localized_resource('Titles', 'ru', 'ReorderLevel', 'Изменить порядок Уровень');--Reorder Level
SELECT localization.add_localized_resource('Titles', 'ru', 'ReorderQuantityAbbreviated', 'количество повторного заказа');--Reorder Qty
SELECT localization.add_localized_resource('Titles', 'ru', 'ReorderUnitName', 'Имя Изменить порядок Раздел');--Reorder Unit Name
SELECT localization.add_localized_resource('Titles', 'ru', 'RequiredField', 'Это обязательное поле.');--This is a required field.
SELECT localization.add_localized_resource('Titles', 'ru', 'RequiredFieldDetails', 'Поля, отмеченные звездочкой (*) обязательны для заполнения.');--The fields marked with asterisk (*) are required.
SELECT localization.add_localized_resource('Titles', 'ru', 'RequiredFieldIndicator', '*');-- *
SELECT localization.add_localized_resource('Titles', 'ru', 'Reset', 'сброс');--Reset
SELECT localization.add_localized_resource('Titles', 'ru', 'RestrictedTransactionMode', 'Это учреждение не позволяет объявление транзакций.');--Restricted Transaction Mode
SELECT localization.add_localized_resource('Titles', 'ru', 'RetainedEarnings', 'Нераспределенная Прибыль');--Retained Earnings
SELECT localization.add_localized_resource('Titles', 'ru', 'Return', 'возвращение');--Return
SELECT localization.add_localized_resource('Titles', 'ru', 'ReturnToView', 'Вернуться Просмотр');--Return to View
SELECT localization.add_localized_resource('Titles', 'ru', 'Role', 'роль');--Role
SELECT localization.add_localized_resource('Titles', 'ru', 'Roles', 'Роли');--Roles
SELECT localization.add_localized_resource('Titles', 'ru', 'RowNumber', 'Рядное Количество');--Row Number
SELECT localization.add_localized_resource('Titles', 'ru', 'RunningTotal', 'Запуск Всего');--Running Total
SELECT localization.add_localized_resource('Titles', 'ru', 'SSTNumber', 'SST Количество');--SST Number
SELECT localization.add_localized_resource('Titles', 'ru', 'SalesByMonthInThousands', 'Продажи по месяц (в тысячах)');--Sales By Month (In Thousands)
SELECT localization.add_localized_resource('Titles', 'ru', 'SalesByOfficeInThousands', 'Продажи в офис (в тысячах)');--Sales By Office (In Thousands)
SELECT localization.add_localized_resource('Titles', 'ru', 'SalesDelivery', 'продажи Доставка');--Sales Delivery
SELECT localization.add_localized_resource('Titles', 'ru', 'SalesDeliveryNote', 'накладная');--Delivery Note
SELECT localization.add_localized_resource('Titles', 'ru', 'SalesInvoice', 'Счет по продажам');--Sales Invoice
SELECT localization.add_localized_resource('Titles', 'ru', 'SalesOrder', 'продажи Заказать');--Sales Order
SELECT localization.add_localized_resource('Titles', 'ru', 'SalesPersons', 'продавцы');--Salespersons
SELECT localization.add_localized_resource('Titles', 'ru', 'SalesQuotation', 'Цитата продаж');--Sales Quotation
SELECT localization.add_localized_resource('Titles', 'ru', 'SalesReceipt', 'Получение продажи');--Sales Receipt
SELECT localization.add_localized_resource('Titles', 'ru', 'SalesReturn', 'продажи Вернуться');--Sales Return
SELECT localization.add_localized_resource('Titles', 'ru', 'SalesTaxDetails', 'Налог на продажу Подробнее');--Sales Tax Details
SELECT localization.add_localized_resource('Titles', 'ru', 'SalesTaxExemptDetails', 'Налог на продажу Освобожденные Подробнее');--Sales Tax Exempt Details
SELECT localization.add_localized_resource('Titles', 'ru', 'SalesTaxExempts', 'Налог на продажу льготников');--Sales Tax Exempts
SELECT localization.add_localized_resource('Titles', 'ru', 'SalesTaxTypes', 'Типы Налог на продажу');--Sales Tax Types
SELECT localization.add_localized_resource('Titles', 'ru', 'SalesTaxes', 'налогов с продаж');--Sales Taxes
SELECT localization.add_localized_resource('Titles', 'ru', 'SalesTeams', 'Продажи команды');--Sales Teams
SELECT localization.add_localized_resource('Titles', 'ru', 'SalesType', 'Тип продажи');--Sales Type
SELECT localization.add_localized_resource('Titles', 'ru', 'Salesperson', 'продавец');--Salesperson
SELECT localization.add_localized_resource('Titles', 'ru', 'Save', 'Сохранить');--Save
SELECT localization.add_localized_resource('Titles', 'ru', 'Saving', 'экономия');--Saving
SELECT localization.add_localized_resource('Titles', 'ru', 'Select', 'выбрать');--Select
SELECT localization.add_localized_resource('Titles', 'ru', 'SelectCompany', 'Выберите компании');--Select Company
SELECT localization.add_localized_resource('Titles', 'ru', 'SelectCustomer', 'Выберите клиента');--Select Customer
SELECT localization.add_localized_resource('Titles', 'ru', 'SelectFlag', 'Выберите Отметить');--Select a Flag
SELECT localization.add_localized_resource('Titles', 'ru', 'SelectLanguage', 'Выберите язык');--Select Language
SELECT localization.add_localized_resource('Titles', 'ru', 'SelectOffice', 'Выберите офис');--Select Office
SELECT localization.add_localized_resource('Titles', 'ru', 'SelectParty', 'Выберите партию');--Select Party
SELECT localization.add_localized_resource('Titles', 'ru', 'SelectPaymentCard', 'Выберите платежных карт');--Select Payment Card
SELECT localization.add_localized_resource('Titles', 'ru', 'SelectStore', 'Выберите магазин');--Select Store
SELECT localization.add_localized_resource('Titles', 'ru', 'SelectSupplier', 'Выберите Поставщик');--Select Supplier
SELECT localization.add_localized_resource('Titles', 'ru', 'SelectUnit', 'Выберите блок');--Select Unit
SELECT localization.add_localized_resource('Titles', 'ru', 'SelectUser', 'Выберите пользователя');--Select User
SELECT localization.add_localized_resource('Titles', 'ru', 'SelectYourBranch', 'Выберите вашу ветку');--Select Your Branch
SELECT localization.add_localized_resource('Titles', 'ru', 'Shipper', 'грузоотправитель');--Shipper
SELECT localization.add_localized_resource('Titles', 'ru', 'Shippers', 'Грузоотправители');--Shippers
SELECT localization.add_localized_resource('Titles', 'ru', 'ShippingAddress', 'Адрес Доставки');--Shipping Address
SELECT localization.add_localized_resource('Titles', 'ru', 'ShippingAddressMaintenance', 'Адрес доставки обслуживание');--Shipping Address Maintenance
SELECT localization.add_localized_resource('Titles', 'ru', 'ShippingAddresses', 'Адрес Доставки');--Shipping Address(es)
SELECT localization.add_localized_resource('Titles', 'ru', 'ShippingCharge', 'Плата за доставку');--Shipping Charge
SELECT localization.add_localized_resource('Titles', 'ru', 'ShippingCompany', 'Транспортная Компания');--Shipping Company
SELECT localization.add_localized_resource('Titles', 'ru', 'Show', 'шоу');--Show
SELECT localization.add_localized_resource('Titles', 'ru', 'ShowAll', 'Показать все');--Show All
SELECT localization.add_localized_resource('Titles', 'ru', 'ShowCompact', 'Показать компактный');--Show Compact
SELECT localization.add_localized_resource('Titles', 'ru', 'SignIn', 'Показать компактный');--Sign In
SELECT localization.add_localized_resource('Titles', 'ru', 'SignOut', 'Выход');--Sign Out
SELECT localization.add_localized_resource('Titles', 'ru', 'SigningIn', 'Вход в систему');--Signing In
SELECT localization.add_localized_resource('Titles', 'ru', 'Start', 'начало');--Start
SELECT localization.add_localized_resource('Titles', 'ru', 'StateSalesTaxes', 'Государственные налогов с продаж');--State Sales Taxes
SELECT localization.add_localized_resource('Titles', 'ru', 'StatementOfCashFlows', 'Отчет о движении денежных средств');--Statement of Cash Flows
SELECT localization.add_localized_resource('Titles', 'ru', 'StatementReference', 'О себе Ссылка');--Statement Reference
SELECT localization.add_localized_resource('Titles', 'ru', 'States', 'состояния');--States
SELECT localization.add_localized_resource('Titles', 'ru', 'Status', 'статус');--Status
SELECT localization.add_localized_resource('Titles', 'ru', 'StockAdjustment', 'Фото Регулировка');--Stock Adjustment
SELECT localization.add_localized_resource('Titles', 'ru', 'StockTransaction', 'Фото сделка');--Stock Transaction
SELECT localization.add_localized_resource('Titles', 'ru', 'StockTransferJournal', 'Перемещение запаса журнал');--Stock Transfer Journal
SELECT localization.add_localized_resource('Titles', 'ru', 'Store', 'магазин');--Store
SELECT localization.add_localized_resource('Titles', 'ru', 'StoreName', 'Сохранение имени');--Store Name
SELECT localization.add_localized_resource('Titles', 'ru', 'StoreTypes', 'Типы магазин');--Store Types
SELECT localization.add_localized_resource('Titles', 'ru', 'Stores', 'магазины');--Stores
SELECT localization.add_localized_resource('Titles', 'ru', 'SubTotal', 'Промежуточный итог');--Sub Total
SELECT localization.add_localized_resource('Titles', 'ru', 'SubmitBugs', 'отправляйте сообщения об ошибках');--Submit Bugs
SELECT localization.add_localized_resource('Titles', 'ru', 'SupplierName', 'Наименование поставщика');--Supplier Name
SELECT localization.add_localized_resource('Titles', 'ru', 'Support', 'поддержка');--Support
SELECT localization.add_localized_resource('Titles', 'ru', 'TableEmptyExceptionMessage', 'Свойство ''Таблица'' не может быть пустым.');--The property 'Table' cannot be left empty.
SELECT localization.add_localized_resource('Titles', 'ru', 'TableSchemaEmptyExceptionMessage', 'Свойство ''Схема таблиц "не может быть пустым.');--The property 'TableSchema' cannot be left empty.
SELECT localization.add_localized_resource('Titles', 'ru', 'TaskCompletedSuccessfully', 'Задача была успешно завершена.');--The task was completed successfully.
SELECT localization.add_localized_resource('Titles', 'ru', 'Tax', 'налог');--Tax
SELECT localization.add_localized_resource('Titles', 'ru', 'TaxAuthorities', 'Налоговые органы');--Tax Authorities
SELECT localization.add_localized_resource('Titles', 'ru', 'TaxExemptTypes', 'Освобождаются от налогообложения Типы');--Tax Exempt Types
SELECT localization.add_localized_resource('Titles', 'ru', 'TaxForm', 'Налоговый Форма');--Tax Form
SELECT localization.add_localized_resource('Titles', 'ru', 'TaxMaster', 'Налоговый Мастер');--Tax Master
SELECT localization.add_localized_resource('Titles', 'ru', 'TaxRate', 'ставка налога');--Tax Rate
SELECT localization.add_localized_resource('Titles', 'ru', 'TaxSetup', 'Налоговый Setup');--Tax Setup
SELECT localization.add_localized_resource('Titles', 'ru', 'TaxTotal', 'Налоговый Всего');--Tax Total
SELECT localization.add_localized_resource('Titles', 'ru', 'TaxTypes', 'Виды налогов');--Tax Types
SELECT localization.add_localized_resource('Titles', 'ru', 'TaxableSales', 'Налогооблагаемая продаж');--Taxable Sales
SELECT localization.add_localized_resource('Titles', 'ru', 'Tel', 'телефон');--Tel
SELECT localization.add_localized_resource('Titles', 'ru', 'To', 'для');--To
SELECT localization.add_localized_resource('Titles', 'ru', 'TopSellingProductsOfAllTime', 'Самые продаваемые продукты всех времен');--Top Selling Products of All Time
SELECT localization.add_localized_resource('Titles', 'ru', 'Total', 'общий');--Total
SELECT localization.add_localized_resource('Titles', 'ru', 'TotalDueAmount', 'Всего надлежащей суммы');--Total Due Amount
SELECT localization.add_localized_resource('Titles', 'ru', 'TotalDueAmountCurrentOffice', 'Всего надлежащей суммы (Текущий Office)');--Total Due Amount (Current Office)
SELECT localization.add_localized_resource('Titles', 'ru', 'TotalDueAmountInBaseCurrency', 'Всего Благодаря Сумма (в базовой валюте)');--Total Due Amount (In Base Currency)
SELECT localization.add_localized_resource('Titles', 'ru', 'TotalSales', 'Всего продаж:');--Total Sales :
SELECT localization.add_localized_resource('Titles', 'ru', 'TranCode', 'код транзакции');--Tran Code
SELECT localization.add_localized_resource('Titles', 'ru', 'TranId', 'идентификатор транзакции');--Tran Id
SELECT localization.add_localized_resource('Titles', 'ru', 'TranIdParameter', 'идентификатор транзакции: #{0}');--TranId: #{0}
SELECT localization.add_localized_resource('Titles', 'ru', 'TransactionDate', 'сделка Дата');--Transaction Date
SELECT localization.add_localized_resource('Titles', 'ru', 'TransactionDetails', 'информация об операции');--Transaction Details
SELECT localization.add_localized_resource('Titles', 'ru', 'TransactionStatement', 'О себе сделка');--TransactionStatement
SELECT localization.add_localized_resource('Titles', 'ru', 'TransactionStatus', 'Статус сделки');--Transaction Status
SELECT localization.add_localized_resource('Titles', 'ru', 'TransactionSummary', 'Сводка транзакций');--Transaction Summary
SELECT localization.add_localized_resource('Titles', 'ru', 'TransactionTimestamp', 'сделка Отметка времени');--Transaction Timestamp
SELECT localization.add_localized_resource('Titles', 'ru', 'TransactionType', 'тип операции');--Transaction Type
SELECT localization.add_localized_resource('Titles', 'ru', 'TransactionValue', 'стоимость сделки');--Transaction Value
SELECT localization.add_localized_resource('Titles', 'ru', 'TransferDetails', 'подробности трансфера');--Transfer Details
SELECT localization.add_localized_resource('Titles', 'ru', 'TrialBalance', 'пробный баланс');--Trial Balance
SELECT localization.add_localized_resource('Titles', 'ru', 'Type', 'тип');--Type
SELECT localization.add_localized_resource('Titles', 'ru', 'UncheckAll', 'Снять отметку со всех');--Uncheck All
SELECT localization.add_localized_resource('Titles', 'ru', 'Undo', 'аннулировать');--Undo
SELECT localization.add_localized_resource('Titles', 'ru', 'Unit', 'блок');--Unit
SELECT localization.add_localized_resource('Titles', 'ru', 'UnitId', 'идентификатор устройства');--Unit Id
SELECT localization.add_localized_resource('Titles', 'ru', 'UnitName', 'Имя единицы');--Unit Name
SELECT localization.add_localized_resource('Titles', 'ru', 'UnitsOfMeasure', 'Единицы измерения');--Units of Measure
SELECT localization.add_localized_resource('Titles', 'ru', 'UnknownError', 'Операция не из-за неизвестной ошибки.');--Operation failed due to an unknown error.
SELECT localization.add_localized_resource('Titles', 'ru', 'Update', 'обновление');--Update
SELECT localization.add_localized_resource('Titles', 'ru', 'Upload', 'Загрузить');--Upload
SELECT localization.add_localized_resource('Titles', 'ru', 'UploadAttachments', 'загружать вложения');--Upload Attachments
SELECT localization.add_localized_resource('Titles', 'ru', 'UploadAttachmentsForThisTransaction', 'Загружать вложения по данной сделке');--Upload Attachments for This Transaction
SELECT localization.add_localized_resource('Titles', 'ru', 'Url', 'Унифицированный указатель информационного ресурса');--Url
SELECT localization.add_localized_resource('Titles', 'ru', 'Use', 'использование');--Use
SELECT localization.add_localized_resource('Titles', 'ru', 'User', 'пользователь');--User
SELECT localization.add_localized_resource('Titles', 'ru', 'UserId', 'Идентификатор пользователя');--User Id
SELECT localization.add_localized_resource('Titles', 'ru', 'Username', 'Имя пользователя');--Username
SELECT localization.add_localized_resource('Titles', 'ru', 'Users', 'пользователи');--Users
SELECT localization.add_localized_resource('Titles', 'ru', 'VacuumDatabase', 'Вакуумный База данных');--Vacuum Database
SELECT localization.add_localized_resource('Titles', 'ru', 'VacuumFullDatabase', 'Вакуумный базы данных (полное)');--Vacuum Database (Full)
SELECT localization.add_localized_resource('Titles', 'ru', 'ValueDate', 'Дата валютирования');--Value Date
SELECT localization.add_localized_resource('Titles', 'ru', 'VerificationReason', 'Проверка Причина');--Verification Reason
SELECT localization.add_localized_resource('Titles', 'ru', 'VerifiedBy', 'Проверено');--Verified By
SELECT localization.add_localized_resource('Titles', 'ru', 'VerifiedOn', 'проверен на');--VerifiedOn
SELECT localization.add_localized_resource('Titles', 'ru', 'Verify', 'проверить');--Verify
SELECT localization.add_localized_resource('Titles', 'ru', 'View', 'вид');--View
SELECT localization.add_localized_resource('Titles', 'ru', 'ViewAttachments', 'Просмотр вложений');--View Attachments
SELECT localization.add_localized_resource('Titles', 'ru', 'ViewBackups', 'Посмотреть Резервные копии');--View Backups
SELECT localization.add_localized_resource('Titles', 'ru', 'ViewCustomerCopy', 'Посмотреть Заказчик Копировать');--View Customer Copy
SELECT localization.add_localized_resource('Titles', 'ru', 'ViewEmptyExceptionMessage', ''' Просмотреть'' собственность не может быть пустым.');--The property 'View' cannot be left empty.
SELECT localization.add_localized_resource('Titles', 'ru', 'ViewSalesInovice', 'Посмотреть накладная');--View Sales Invoice
SELECT localization.add_localized_resource('Titles', 'ru', 'ViewSchemaEmptyExceptionMessage', 'Свойство ''Просмотр схемы "не может быть пустым.');--The property 'ViewSchema' cannot be left empty.
SELECT localization.add_localized_resource('Titles', 'ru', 'ViewThisAdjustment', 'Просмотреть эта корректировка');--View This Adjustment
SELECT localization.add_localized_resource('Titles', 'ru', 'ViewThisDelivery', 'Открыть этот Доставка');--View This Delivery
SELECT localization.add_localized_resource('Titles', 'ru', 'ViewThisInvoice', 'Открыть этот счет-фактуру');--View This Invoice
SELECT localization.add_localized_resource('Titles', 'ru', 'ViewThisNote', 'Открыть этот рейтинг');--View This Note
SELECT localization.add_localized_resource('Titles', 'ru', 'ViewThisOrder', 'Просмотреть этот порядок');--View This Order
SELECT localization.add_localized_resource('Titles', 'ru', 'ViewThisQuotation', 'Просмотреть эту цитату');--View This Quotation
SELECT localization.add_localized_resource('Titles', 'ru', 'ViewThisReturn', 'Смотреть это возвращение');--View This Return
SELECT localization.add_localized_resource('Titles', 'ru', 'ViewThisTransfer', 'Смотреть эту передачу');--View This Transfer
SELECT localization.add_localized_resource('Titles', 'ru', 'VoucherVerification', 'Ваучер Проверка');--Voucher Verification
SELECT localization.add_localized_resource('Titles', 'ru', 'VoucherVerificationPolicy', 'Политика Ваучер Проверка');--Voucher Verification Policy
SELECT localization.add_localized_resource('Titles', 'ru', 'Warning', 'предупреждение');--Warning
SELECT localization.add_localized_resource('Titles', 'ru', 'WhichBank', 'Какой банк?');--Which Bank?
SELECT localization.add_localized_resource('Titles', 'ru', 'WithdrawTransaction', 'Вывод сделка');--Withdraw Transaction
SELECT localization.add_localized_resource('Titles', 'ru', 'WithdrawnTransactions', 'Изъятые Сделки');--Withdrawn Transactions
SELECT localization.add_localized_resource('Titles', 'ru', 'Workflow', 'Workflow');--Workflow
SELECT localization.add_localized_resource('Titles', 'ru', 'WorldSalesStatistics', 'Мировые продажи Статистика');--World Sales Statistics
SELECT localization.add_localized_resource('Titles', 'ru', 'Year', 'год');--Year
SELECT localization.add_localized_resource('Titles', 'ru', 'Yes', 'Да');--Yes
SELECT localization.add_localized_resource('Titles', 'ru', 'YourName', 'Ваше Имя');--Your Name
SELECT localization.add_localized_resource('Titles', 'ru', 'YourOffice', 'Ваш офис');--Your Office
SELECT localization.add_localized_resource('Warnings', 'ru', 'AccessIsDenied', 'Отказано в доступе.');--Access is denied.
SELECT localization.add_localized_resource('Warnings', 'ru', 'CannotCreateABackup', 'Извините, не могу создать резервную копию базы данных на данный момент.');--Sorry, cannot create a database backup at this time.
SELECT localization.add_localized_resource('Warnings', 'ru', 'CannotCreateFlagTransactionTableNull', 'Невозможно создать или обновить флаг. Таблицу транзакций не было.');--Cannot create or update flag. Transaction table was not provided.
SELECT localization.add_localized_resource('Warnings', 'ru', 'CannotCreateFlagTransactionTablePrimaryKeyNull', 'Невозможно создать или обновить флаг. Таблицу транзакций первичный ключ не был обеспечен.');--Cannot create or update flag. Transaction table primary key was not provided.
SELECT localization.add_localized_resource('Warnings', 'ru', 'CannotMergeAlreadyMerged', 'Выбранные операции содержат элементы, которые уже были объединены. Пожалуйста, попробуйте еще раз.');--The selected transactions contain items which have already been merged. Please try again.
SELECT localization.add_localized_resource('Warnings', 'ru', 'CannotMergeDifferentPartyTransaction', 'Невозможно объединить операции различных партий в единую партию. Пожалуйста, попробуйте еще раз.');--Cannot merge transactions of different parties into a single batch. Please try again.
SELECT localization.add_localized_resource('Warnings', 'ru', 'CannotMergeIncompatibleTax', 'Невозможно объединить операции, имеющие несовместимые типы налогов. Пожалуйста, попробуйте еще раз.');--Cannot merge transactions having incompatible tax types. Please try again.
SELECT localization.add_localized_resource('Warnings', 'ru', 'CannotMergeUrlNull', 'Невозможно объединить операции. Слияние гиперссылка, не было.');--Cannot merge transactions. The merge url was not provided.
SELECT localization.add_localized_resource('Warnings', 'ru', 'CashTransactionCannotContainBankInfo', 'Кассовая сделка не может содержать банковские реквизиты сделки.');--A cash transaction cannot contain bank transaction details.
SELECT localization.add_localized_resource('Warnings', 'ru', 'CompareAmountErrorMessage', 'Сумма должна быть больше, чем количество из.');--The amount to should be greater than the amount from.
SELECT localization.add_localized_resource('Warnings', 'ru', 'CompareDaysErrorMessage', 'Из дней не должно быть больше, чем до нескольких дней.');--From days should be less than to days.
SELECT localization.add_localized_resource('Warnings', 'ru', 'ComparePriceErrorMessage', 'Цена от должно быть меньше, чем цена на.');--Price from should be less than price to.
SELECT localization.add_localized_resource('Warnings', 'ru', 'ConfigurationError', 'Не можете продолжить задачу. Пожалуйста, исправьте проблемы конфигурации.');--Cannot continue the task. Please correct configuration issues.
SELECT localization.add_localized_resource('Warnings', 'ru', 'ConfirmationPasswordDoesNotMatch', 'Подтверждение пароля не совпадает с новым паролем.');--The confirmation password does not match with the new password.
SELECT localization.add_localized_resource('Warnings', 'ru', 'CouldNotDetermineEmailImageParserType', 'Не удалось определить тип изображения парсер электронной почте.');--Could not determine image parser type for email.
SELECT localization.add_localized_resource('Warnings', 'ru', 'CouldNotRegisterJavascript', 'Не удалось зарегистрировать наличие на этой странице, потому что экземпляр страницы недействительным или пустым.');--Could not register JavaScript on this page because the page instance was invalid or empty.
SELECT localization.add_localized_resource('Warnings', 'ru', 'DateErrorMessage', 'Выбранная дата находится вне диапазона.');--Selected date is invalid.
SELECT localization.add_localized_resource('Warnings', 'ru', 'DueFrequencyErrorMessage', 'Из-за дня может быть только 0, если выбран из-за частоты ID.');--Due days should be 0 if due frequency id is selected.
SELECT localization.add_localized_resource('Warnings', 'ru', 'DuplicateEntry', 'Дубликат записи.');--Duplicate entry.
SELECT localization.add_localized_resource('Warnings', 'ru', 'DuplicateFiles', 'Дубликаты файлов!');--Duplicate files.
SELECT localization.add_localized_resource('Warnings', 'ru', 'GridViewEmpty', 'Значки пуст.');--Gridview is empty.
SELECT localization.add_localized_resource('Warnings', 'ru', 'InsufficientBalanceInCashRepository', 'Там нет достаточного остатка в кассовой хранилища для обработки транзакции.');--There is no sufficient balance in the cash repository to process this transaction.
SELECT localization.add_localized_resource('Warnings', 'ru', 'InsufficientStockWarning', 'Только {0} {1} {2} оставил в запасе.');--Only {0} {1} of {2} left in stock.
SELECT localization.add_localized_resource('Warnings', 'ru', 'InvalidAccount', 'Неверный счет.');--Invalid account.
SELECT localization.add_localized_resource('Warnings', 'ru', 'InvalidCashRepository', 'Неверный хранилище наличными.');--Invalid cash repository.
SELECT localization.add_localized_resource('Warnings', 'ru', 'InvalidCostCenter', 'Неверный МВЗ.');--Invalid cost center.
SELECT localization.add_localized_resource('Warnings', 'ru', 'InvalidData', 'Неверные данные.');--Invalid data.
SELECT localization.add_localized_resource('Warnings', 'ru', 'InvalidDate', 'Это не действительной датой.');--Invalid date.
SELECT localization.add_localized_resource('Warnings', 'ru', 'InvalidParameterName', 'Неверное имя параметра Npgsql {0}. , Убедитесь, что имя параметра матчи с командной текста.');--Invalid Npgsql parameter name {0}. . Make sure that the parameter name matches with your command text.
SELECT localization.add_localized_resource('Warnings', 'ru', 'InvalidParty', 'Неверный партия. Эта партия не связана с этой сделки.');--Invalid party.
SELECT localization.add_localized_resource('Warnings', 'ru', 'InvalidPaymentTerm', 'Неверный срок оплаты.');--Invalid payment term.
SELECT localization.add_localized_resource('Warnings', 'ru', 'InvalidPriceType', 'Неверный тип цена.');--Invalid price type.
SELECT localization.add_localized_resource('Warnings', 'ru', 'InvalidReceiptMode', 'Неверный режим получения.');--Invalid receipt mode.
SELECT localization.add_localized_resource('Warnings', 'ru', 'InvalidSalesPerson', 'Неверный продавцом.');--Invalid salesperson.
SELECT localization.add_localized_resource('Warnings', 'ru', 'InvalidShippingCompany', 'Неверный судоходная компания.');--Invalid shipping company.
SELECT localization.add_localized_resource('Warnings', 'ru', 'InvalidStockTransaction', 'Неверный фондовые операции.');--Invalid stock transaction.
SELECT localization.add_localized_resource('Warnings', 'ru', 'InvalidStore', 'Неверный магазин.');--Invalid store.
SELECT localization.add_localized_resource('Warnings', 'ru', 'InvalidSubTranBookInventoryDelivery', 'Неверный Вспомогательные Сделки Книга «Инвентаризация Доставка"');--Invalid SubTranBook "Inventory Delivery"
SELECT localization.add_localized_resource('Warnings', 'ru', 'InvalidSubTranBookInventoryDirect', 'Недействительные сделки Вспомогательные Книга "Склад Прямая"');--Invalid SubTranBook "Inventory Direct"
SELECT localization.add_localized_resource('Warnings', 'ru', 'InvalidSubTranBookInventoryInvoice', 'Неверный Вспомогательные Сделки Книга «Инвентаризация Счет"');--Invalid SubTranBook "Inventory Invoice"
SELECT localization.add_localized_resource('Warnings', 'ru', 'InvalidSubTranBookInventoryOrder', 'Неверный Вспомогательные Сделки Книга "инвентаризации заказ"');--Invalid SubTranBook "Inventory Order"
SELECT localization.add_localized_resource('Warnings', 'ru', 'InvalidSubTranBookInventoryPayment', 'Неверный Вспомогательные Сделки Книга «Инвентаризация Оплата"');--Invalid SubTranBook "Inventory Payment"
SELECT localization.add_localized_resource('Warnings', 'ru', 'InvalidSubTranBookInventoryQuotation', 'Неверный Вспомогательные Сделки Книга «Инвентаризация цитаты"');--Invalid SubTranBook "Inventory Quotation"
SELECT localization.add_localized_resource('Warnings', 'ru', 'InvalidSubTranBookInventoryReceipt', 'Неверный Вспомогательные Сделки Книга «Инвентаризация Получение"');--Invalid SubTranBook "Inventory Receipt"
SELECT localization.add_localized_resource('Warnings', 'ru', 'InvalidSubTranBookInventoryReturn', 'Неверный Вспомогательные Сделки Книга «Инвентаризация Возвращение"');--Invalid SubTranBook "Inventory Return"
SELECT localization.add_localized_resource('Warnings', 'ru', 'InvalidSubTranBookPurchaseDelivery', 'Неверный Вспомогательные Сделки Книга "Покупка Доставка"');--Invalid SubTranBook "Purchase Delivery"
SELECT localization.add_localized_resource('Warnings', 'ru', 'InvalidSubTranBookPurchaseQuotation', 'Недействительные сделки Вспомогательные Книга "Покупка цитаты"');--Invalid SubTranBook "Purchase Quotation"
SELECT localization.add_localized_resource('Warnings', 'ru', 'InvalidSubTranBookPurchaseSuspense', 'Недействительные сделки Вспомогательные Книга "Покупка Триллер"');--Invalid SubTranBook "Purchase Suspense"
SELECT localization.add_localized_resource('Warnings', 'ru', 'InvalidSubTranBookPurchaseTransfer', 'Неверный Вспомогательные Сделки Книга "Покупка Transfer"');--Invalid SubTranBook "Purchase Transfer"
SELECT localization.add_localized_resource('Warnings', 'ru', 'InvalidSubTranBookSalesPayment', 'Неверный Вспомогательные Сделки Книга "Оплата по продажам"');--Invalid SubTranBook "Sales Payment"
SELECT localization.add_localized_resource('Warnings', 'ru', 'InvalidSubTranBookSalesSuspense', 'Неверный Вспомогательные Сделки Книга "Триллер Продажи"');--Invalid SubTranBook "Sales Suspense"
SELECT localization.add_localized_resource('Warnings', 'ru', 'InvalidSubTranBookSalesTransfer', 'Неверный Вспомогательные Сделки Книга "Передача по продажам"');--Invalid SubTranBook "Sales Transfer"
SELECT localization.add_localized_resource('Warnings', 'ru', 'InvalidUser', 'Неправильный пользователь.');--Invalid user.
SELECT localization.add_localized_resource('Warnings', 'ru', 'ItemErrorMessage', 'Вы должны выбрать либо идентификатор элемента или соединения идентификатор элемента.');--You have to select either item id or  compound item id.
SELECT localization.add_localized_resource('Warnings', 'ru', 'LateFeeErrorMessage', 'Просрочку платежа ID и штраф за опоздание размещение частота ID оба должны быть либо выбран или нет.');--Late fee id and late fee posting frequency id both should be either selected or not.
SELECT localization.add_localized_resource('Warnings', 'ru', 'NegativeValueSupplied', 'Отрицательное значение в комплект поставки.');--Negative value supplied.
SELECT localization.add_localized_resource('Warnings', 'ru', 'NewPasswordCannotBeOldPassword', 'Новый пароль не может быть старый пароль.');--New password can not be old password.
SELECT localization.add_localized_resource('Warnings', 'ru', 'NoFileSpecified', 'Не указано ни одного файла.');--No file specified.
SELECT localization.add_localized_resource('Warnings', 'ru', 'NoTransactionToPost', 'Сделка не чтобы оставлять сообщения.');--No transaction to post.
SELECT localization.add_localized_resource('Warnings', 'ru', 'NotAuthorized', 'Вы не авторизованы для доступа к этой ресурсы в настоящее время.');--You are not authorized to access this resource at this time.
SELECT localization.add_localized_resource('Warnings', 'ru', 'NothingSelected', 'Ничего не выбрано.');--Nothing selected.
SELECT localization.add_localized_resource('Warnings', 'ru', 'PasswordCannotBeEmpty', 'Пароль не может быть пустым.');--Password cannot be empty.
SELECT localization.add_localized_resource('Warnings', 'ru', 'PleaseEnterCurrentPassword', 'Пожалуйста, введите ваш текущий пароль.');--Please enter your current password.
SELECT localization.add_localized_resource('Warnings', 'ru', 'PleaseEnterNewPassword', 'Пожалуйста, введите новый пароль.');--Please enter a new password.
SELECT localization.add_localized_resource('Warnings', 'ru', 'RecurringAmountErrorMessage', 'Периодическое количество не должно быть меньше или равно 0.');--Recurring amount should not be less than or equal to 0.
SELECT localization.add_localized_resource('Warnings', 'ru', 'ReferencingSidesNotEqual', 'Ссылающейся стороны не равны.');--The referencing sides are not equal.
SELECT localization.add_localized_resource('Warnings', 'ru', 'RestrictedTransactionMode', 'Это учреждение не позволяет объявление транзакций.');--This establishment does not allow transaction posting.
SELECT localization.add_localized_resource('Warnings', 'ru', 'ReturnButtonUrlNull', 'Не может вернуться на эту запись.URL возврата не было.');--Cannot return this entry. The return url was not provided.
SELECT localization.add_localized_resource('Warnings', 'ru', 'UserIdOrPasswordIncorrect', 'Идентификатор пользователя или пароль неверен.');--User id or password incorrect.
