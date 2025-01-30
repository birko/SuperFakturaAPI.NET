# SuperFakturaAPI.NET
.NET client library for SuperFaktura API.
> Codes are wrapped as .net Shared Library. So you can [download](https://github.com/birko/SuperFakturaAPI.NET/releases) them and include into your programs or .dll libraries.

> You can also use [![NuGet version (SuperFaktura)](https://img.shields.io/nuget/v/SuperFaktura.svg?style=flat-square)](https://www.nuget.org/packages/SuperFaktura/)

Library uses System.Net.Http.HttpClient as communication layer and Newtonsoft.Json for serialization and deserialization

Implementation used by [FinStat.sk](http://www.finstat.sk)

## Client structure
The main api client class [`SuperFaktura`](#superfaktura) has this hierarchy. Most of used classes is from `Birko.SuperFaktura` namespace
 
### SuperFaktura
Main class. Ensures the propper API calling, serialization of request parameters and deserialization of response from SuperFaktura servers.
It ensures that the delay between api calls is more than 1 second.
> Localized classes `SuperFakturaCZ` and `SuperFakturaAT` for country specific endpoints

> Sandbox classes `SuperFakturaSandbox` and `SuperFakturaSandboxCZ` for testing endpoints

#### Public Properties
* **EnsureSuccessStatusCode** - boolean, default true. Switches the behaviour if System.Net.Http.HttpClient should raise exception for not succesfull response
* **BankAccounts** - instance of [`BankAccounts`](#bankaccounts) class, description bellow
* **CashRegisters** - instance of [`CashRegisters`](#cashregisters) class, description bellow
* **Clients** - instance of [`Clients`](#clients) class, description bellow
* **ContactPersons** - instance of [`ContactPersons`](#contactpersons) class, description bellow
* **Expenses** - instance of [`Expenses`](#expenses) class, description bellow
* **Exports** - instance of [`Exports`](#exports) class, description bellow
* **Invoices** - instance of [`Invoices`](#invoices) class, description bellow
* **Other** - instance of [`Other`](#other) class, description bellow
* **Stock** - instance of [`Stock`](#stock) class, description bellow
* **Tags** - instance of [`Tags`](#tags) class, description bellow
* **ValueLists** - instance of [`ValueLists`](#valuelists) class, description bellow

#### Public Methods
* **SuperFaktura(string email, string apiKey, string apptitle = null, string module = "API", int? companyId = null)** - constructor. email and apiKey are mandatory parameters. Given from SuperFaktura.

### BankAccounts
Class that wrappes bank accounts API
#### Public Methods
* **List** - returns lists of bank accounts
* **Add(Request.BankAccounts.BankAccount account)** - adds new bank account 
* **Edit(int id, Request.BankAccounts.BankAccoun account)** - edits bank account with given `id`
* **Delete(int id)** - deletes bank account with given `id`

### CashRegisters
Class that wrappes cash registers API calls
#### Public Methods
* **List** - returns lists of cash registers
* **View(int id)** - get cash register detail according given `id`
* **ListItems(Request.CashRegister.Filter filter)** - gets all items in cash register according `filter`
* **AddItem(Request.CashRegister.CashRegisterItem item)** - adds cash register item
* **DeleteItem(int id)** - deletes cash register item with given `id`
* **DeleteItem(int[] ids)** - deletes cash register item with given list of `id`
* **Download(int id)** - get PDF receipt according cash register item with given `id` as byte array

### Clients
Class that wrappes clients API calls
#### Public Methods
* **List(Request.Client.Filter filter, bool listInfo = true)** - returns lists of clients according `filter`
* **Add(Request.Client.Client client, int[] tags = null)** - add new client, optional 'tags'
* **Edit(int id, Request.Client.Client client, int[] tags = null)** - add client according given `id`. Optional `tags` can be specified
* **View(int id)** - get client detail according given `id`
* **Delete(int id)** - deletes client with given `id`

### ContactPersons
Class that wrappes contact people API calls
#### Public Methods
* **List(int id)** - returns list of contacts according given client `id`
* **Add(Request.ContactPersons.ContactPerson)** - add new contact person
* **Delete(int id)** - deletes contact person with given `id`

### Expenses
Class that wrappes all API calls for expenses handling in SuperFaktura.

#### Public Methods
* **List(Request.Expense.Filter filter, bool listInfo)** - gets list of expenses according `filter`
* **Add(Request.Expense.Expense expense, Request.Expense.ExpenseItem[] items = null, Request.Client.Client client = null, Request.Expense.Extra extra = null, int[] tags = null)** - adds new expense entry. Optional  `items`, `client`, `extra` and `tags` can be specified
* **Edit(Request.Expense.Expense expense, Request.Expense.ExpenseItem[] items = null, Request.Client.Client client = null, Request.Expense.Extra extra = null, int[] tags = null)** - edits expense entry. Optional  `items`, `client`, `extra` and `tags` can be specified
* **View(int id)** - get expense detail with given `id`
* **Delete(int id)** - deletes expense with given `id`
* **AddPayment(Request.Expense.Payment payment)** - adds payment to expense
* **DeletePayment(int id)** - deletes expense payment with given payment `id`
* **AddRelatedItem(Request.RelatedItem relatedItem)** - adds related item to expense
* **DeleteRelatedItem(int id)** - deletes related item with given relation `id`

### Exports
Class that wrappes exports API calls

#### Public Methods
* **Export(Request.Export.ExportData export)** - creates an export request with given `export` data
* **Status(int id)** - show export status according given `id`
* **Download(int id)** - downloads export according given `id` as byte array

### Invoices
Class that wrappes invoce API calls

#### Public Methods
* **List(Request.Invoice.Filter filter, bool listInfo = true)** - gets list of invoices according `filter`
* **Add(Request.Invoice.Invoice invoice, Client client, Request.Invoice.Item[] items, int[] tags = null, Request.Invoice.InvoiceSettings setting = null, Request.Invoice.Extra extra = null, Request.Invoice.MyData myData = null)** - creates new invoice. Optional  `tags`, `setting`, `extra` and `myData` can be specified
* **Edit(Request.Invoice.Invoice invoice, Client client, Request.Invoice.Item[] items, int[] tags = null, Request.Invoice.InvoiceSettings setting = null, Request.Invoice.Extra extra = null, Request.Invoice.MyData myData = null)** - updates invoice. Optional  `tags`, `setting`, `extra` and `myData` can be specified
* **View(int id)** - gets invoice detail according `id`
* **ListDetails(int[] ids)** - gets invoice details according given list of invoice `id`
* **SetInvoiceLanguage(int id, string language)** - sets the default language for given invoice `id`
* **Download(int id, string token, string language)** - gets the PDF for given invoice `id` as byte array
* **Delete(int id)** - deletes invoice with given `id`
* **WillNotBePaid(int id)** - sets invoice with given `id` asn wil not be paid
* **SendEmail(Request.Invoice.Email email)** - sends Email with invoice
* **MarkAsSentViaMail(Request.Invoice.MarkEmail email)** - marks sended email with invoice
* **SendPost(Post post)** - send invoice throw regular post
* **MarkAsSent(int id)** - marks invoce as send
* **DeleteItem(int id, int itemid)** - deletes item with `itemid` from invoice with given `id`
* **DeleteItem(int id, int[] itemid)** - deletes items according given list of `itemid` from invoice with given `id`
* **AddPayment(Request.Invoice.Payment payment)** - adds payment to invoice
* **DeletePayment(int id)** - deletes invoice payment  with given payment `id`
* **AddRelatedItem(Request.RelatedItem relatedItem)** - adds related item to expense
* **DeleteRelatedItem(int id)** - deletes related item with given relation `id`
* **DownloadReceipt(int id)** - downloads receipt according given invoice `id` as  byte array

### Other
Class that wrappes all other API calls

#### Public Methods
* **ListAccounts()** - gets list of user accounts
* **ListUserCompanies(bool all = false)** - gets list of user copmanies. Oprional `all` switch parameter
* **SendSMS(Request.Other.SMS sms)** - sends and SMS
* **ListBankAccountMovements(Request.Other.BankMovementFilter filter)** - returns all bank account movements according given `filter`
* **ListActivityLogs(string documentType, int documentId, int limit = 10)** - returns all log for given `documentType` and `documentId`. Optional list `limit` parameter can be specified

### Stock
Class that wrappes stock API calls

#### Public Methods
* **List(Filter filter, bool listInfo = true)** - gets list of stock items according filter
* **Add(Request.Stock.Item item)** - adds new stock item
* **Edit(int id, Request.Stock.Item item)** - edits stock item according `id`
* **View(int id)** - gets stock item detail according `id`
* **Delete(int id)** - deletes stock item according `id`
* **AddStockMovement(IEnumerable<Request.Stock.Log> items)** - adds stock movement logs to stock item 
* **AddStockMovement(Request.Stock.Log item)** - adds stock movement log  to stock item
* **ListStockMovements(int id, Request.PagedParameters filter, bool listInfo = true)** - gets stock movements for items item detail according `id` and given `filter`

### Tags
Class that wrappes tags API calls

#### Public Methods
* **List()** - gets list of stored tags
* **Add(Request.Tags.Tag tag)** - adds new tag
* **Edit(int id, equest.Tags.Tag tag)** - edits tag with `id`
* **Delete(int id)** - deletes tag with `id`

### ValueLists
Class that wrappes lists API calls

#### Public Methods
* **ListCountries()** - gets country names and and ids as dictionary
* **ListCountriesFull()** - gets full Country list
* **ListExpenseCategories** - gets list of ExpenseCategories
* **ListLogos()** - gets list of uploaded logos
* **ListSequences()** - gets available sequences as dictionary

#### Constant Lists
List are stored in `Birko.SuperFaktura.Request.ValueLists` namespace
* **AccountingDetailType** - list of accounting types for invocies
* **DeliveryType** - list of deliveries types
* **DocumentType** - list of document types for related items and logs
* **ExpenseStatus** - list of expense statuses
* **ExpenseVersion** - list of expense type versions
* **ExpenseType** - list of expense types
* **ExportStatus** - list of export statuses
* **InvoiceType** - list of invoice types
* **InvoiceStatus** - list of invoice statuses
* **LanguageType** - list of supported languages
* **PaymentType** - list of payment types
* **PeriodTypes** - list of period types
* **RoundingType** - list of invoce or expense rounding types
* **TimeFilterConstants** - list of time numeric constants used in some filters
* **TimeFilter** - list of time string constants used in some filters