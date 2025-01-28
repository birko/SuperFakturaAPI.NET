# SuperFakturaAPI.NET
.NET client library for SuperFaktura API.
Codes are wrapped as .net Shared Library. So you can download them and include into your programs or .dll libraries.
Library uses System.Net.Http.HttpClient as communication layer and Newtonsoft.Json for serialization and deserialization

Implementation used by [FinStat.sk](http://www.finstat.sk)

## Client structure
The main api client has this hierarchy. Most of used classes is from **Birko.SuperFaktura** namespace
 
### 1. SuperFaktura
Also: **SuperFakturaCZ** and **SuperFakturaAT**
Main class. Ensures the propper API calling, serialization of request parameters and deserialization of response from SuperFaktura servers.
It ensures that the delay between api calls is more than 1 second.
> Includes **SuperFakturaSandbox** and **SuperFakturaSandboxCZ** for testing purposes

#### 1.1. Public Properties
* **EnsureSuccessStatusCode** - boolean, default true. Switches the behaviour if System.Net.Http.HttpClient should raise exception for not succesfull response
* **BankAccounts** - instance of [BankAccounts](#2-bankaccounts) class, description bellow
* **CashRegisters** - instance of [CashRegisters][#3-cashregisters] class, description bellow
* **Clients** - instance of [Clients](#4-clients) class, description bellow
* **ContactPersons** - instance of [ContactPersons](#5-contactpersons) class, description bellow
* **Expenses** - instance of [Expenses](#6-expenses) class, description bellow
* **Exports** - instance of [Exports](#7-exports) class, description bellow
* **Invoices** - instance of [Invoices](#8-invoices) class, description bellow
* **Other** - instance of [Other](#9-other) class, description bellow
* **Stock** - instance of [Stock](#10-stock) class, description bellow
* **Tags** - instance of [Tags](#11-tags) class, description bellow
* **ValueLists** - instance of [ValueLists](#11-valuelists) class, description bellow

#### 1.2. Public Methods
* **SuperFaktura(string email, string apiKey, string apptitle = null, string module = "API", int? companyId = null)** - constructor. email and apiKey are mandatory parameters. Given from SuperFaktura.

* **Register(string email, bool sendEmail)** - registers new user to your SuperFaktura account.

### 2. BankAccounts
Class that wrappes bank accounts API
#### 2.1. Public Methods
* **List** - returns lists of bank accounts
* **Add(Request.BankAccounts.BankAccount account)** - adds new bank account 
* **Edit(int id, Request.BankAccounts.BankAccoun account)** - edits bank account with given `id`
* **Delete(int id)** - deletes bank account with given `id`

### 3. CashRegisters
Class that wrappes cash registers API calls
#### 3.1. Public Methods
* **List** - returns lists of cash registers
* **View(int id)** - get cash register detail according given `id`
* **ListItems(Request.CashRegister.Filter filter)** - gets all items in cash register according `filter`
* **AddItem(Request.CashRegister.CashRegisterItem item)** - adds cash register item
* **DeleteItem(int id)** - deletes cash register item with given `id`
* **DeleteItem(int[] ids)** - deletes cash register item with given list of `id`
* **GetReceipt(int id)** - get PDF receipt according cash register item with given `id` as byte array

### 4. Clients
Class that wrappes clients API calls
#### 4.1. Public Methods
* **List(Request.Client.Filter filter, bool listInfo = true)** - returns lists of clients according `filter`
* **Add(Request.Client.Client client, int[] tags = null)** - add new client, optional 'tags'
* **Edit(int id, Request.Client.Client client, int[] tags = null)** - add client according given `id`. Optional `tags` can be specified
* **View(int id)** - get client detail according given `id`
* **Delete(int id)** - deletes client with given `id`

### 5. ContactPersons
Class that wrappes contact people API calls
#### 5.1. Public Methods
* **List(int id)** - returns list of contacts according given client `id`
* **Add(Request.ContactPersons.ContactPerson)** - add new contact person
* **Delete(int id)** - deletes contact person with given `id`

### 6. Expenses
Class that wrappes all API calls for expenses handling in SuperFaktura.

#### 6.1. Public Methods
* **List(Request.Expense.Filter filter, bool listInfo)** - gets list of expenses according `filter`
* **Add(Request.Expense.Expense expense, Request.Expense.ExpenseItem[] items = null, Request.Client.Client client = null, Request.Expense.Extra extra = null, int[] tags = null)** - adds new expense entry. Optional  `items`, `client`, `extra` and `tags` can be specified
* **Edit(Request.Expense.Expense expense, Request.Expense.ExpenseItem[] items = null, Request.Client.Client client = null, Request.Expense.Extra extra = null, int[] tags = null)** - edits expense entry. Optional  `items`, `client`, `extra` and `tags` can be specified
* **View(int id)** - get expense detail with given `id`
* **Delete(int id)** - deletes expense with given `id`
* **AddPayment(Request.Expense.Payment payment)** - adds payment to expense
* **DeletePayment(int id)** - deletes expense payment with given payment `id`
* **AddRelatedItem(Request.RelatedItem relatedItem)** - adds related item to expense
* **DeleteRelatedItem(int id)** - deletes related item with given relation `id`

### 7. Exports
Class that wrappes exports API calls

#### 7.1. Public Methods
* **Export(Request.Export.ExportData export)** - creates an export request with given `export` data
* **Status(int id)** - show export status according given `id`
* **Download(int id)** - downloads export according given `id` as byte array

### 8. Invoices
Class that wrappes invoce API calls

#### 8.1. Public Methods
* **List(Request.Invoice.Filter filter, bool listInfo = true)** - gets list of invoices according `filter`
* **Add(Request.Invoice.Invoice invoice, Client client, Request.Invoice.Item[] items, int[] tags = null, Request.Invoice.InvoiceSettings setting = null, Request.Invoice.Extra extra = null, Request.Invoice.MyData myData = null)** - creates new invoice. Optional  `tags`, `setting`, `extra` and `myData` can be specified
* **Edit(Request.Invoice.Invoice invoice, Client client, Request.Invoice.Item[] items, int[] tags = null, Request.Invoice.InvoiceSettings setting = null, Request.Invoice.Extra extra = null, Request.Invoice.MyData myData = null)** - updates invoice. Optional  `tags`, `setting`, `extra` and `myData` can be specified
* **View(int id)** - gets invoice detail according `id`
* **ListDetails(int[] ids)** - gets invoice details according given list of invoice `id`
* **SetInvoiceLanguage(int id, string language)** - sets the default language for given invoice `id`
* **GetPdf(int id, string token, string language)** - gets the PDF for given invoice `id` as byte array
* **Delete(int id)** - deletes invoice with given `id`
* **WillNotBePid(int id)** - sets invoice with given `id` asn wil not be paid
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
* **GetReceipt(int id)** - downloads receipt according given invoice `id` as  byte array

### 9. Other
Class that wrappes all other API calls

#### 9.1. Public Methods
* **ListAccounts()** - gets list of user accounts
* **ListUserCompanies(bool all = false)** - gets list of user copmanies. Oprional `all` switch parameter
* **SendSMS(Request.Other.SMS sms)** - sends and SMS
* **ListBankAccountMovements(Request.Other.BankMovementFilter filter)** - returns all bank account movements according given `filter`
* **ListActivityLogs(string documentType, int documentId, int limit = 10)** - returns all log for given `documentType` and `documentId`. Optional list `limit` parameter can be specified

### 10. Stock
Class that wrappes stock API calls

#### 10.1. Public Methods
* **List(Filter filter, bool listInfo = true)** - gets list of stock items according filter
* **Add(Request.Stock.Item item)** - adds new stock item
* **Edit(int id, Request.Stock.Item item)** - edits stock item according `id`
* **View(int id)** - gets stock item detail according `id`
* **Delete(int id)** - deletes stock item according `id`
* **AddStockMovement(IEnumerable<Request.Stock.Log> items)** - adds stock movement logs to stock item 
* **AddStockMovement(Request.Stock.Log item)** - adds stock movement log  to stock item
* **ListStockMovements(int id, Request.PagedParameters filter, bool listInfo = true)** - gets stock movements for items item detail according `id` and given `filter`

### 11. Tags
Class that wrappes tags API calls

#### 11.1. Public Methods
* **List()** - gets list of stored tags
* **Add(Request.Tags.Tag tag)** - adds new tag
* **Edit(int id, equest.Tags.Tag tag)** - edits tag with `id`
* **Delete(int id)** - deletes tag with `id`

### 11. ValueLists
Class that wrappes lits API calls

#### 11.1. Public Methods
* **ListCountries()** - gets list country names and and iso ids as dictionary
* **ListCountriesFull()** - gets full Country list
* **ListExpenseCategories** - gets list of ExpenseCategories
* **ListSequences()** - gets list of uploaded logos
* **Delete(int id)** - gets available sequences as dictionary

#### 11.2. Constant Lists
List are stored in `Birko.SuperFaktura.Request.ValueLists` namespace
* **AccountingDetailType** - list of accounting types for invocies
* **DeliveryType** - list of deliveries types
* **DocumenType** - list of document types for related items and logs
* **ExpenseStatus** - list of expense statuses
* **ExpenseVersion** - list of expense type versions
* **ExpenseType** - list of expense types
* **ExportStatus** - list of export statuses
* **InvoiceType** - list of invoice types
* **LanguageType** - list of supported languages
* **PaymentType** - list of payment types
* **PeriodTypes** - list of period types
* **RoundingType** - list of invoce or expense rounding types
* **TimeFilterConstants** - list of time numeric constants used in some filters
* **TimeFilter** - list of time string constants used in some filters
* **StatusType** - list invoce payment status types for used in some  filter