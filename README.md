# SuperFakturaAPI.NET
.NET client library for SuperFaktura API.
Codes are wrapped as .net Shared Library. So you can download them and include into your programs or .dll libraries

## Client structure
The main api client has this hierarchy. Most of used classes is from **Birko.SuperFaktura** namespace

### 1. SuperFaktura
Also: **SuperFakturaCZ**  and **SuperFakturaAt**
Main class. Ensures the propper API calling, serialization of request parameters and deserialization of response from SuperFaktura servers.
It ensures that the delay between api calls is more than 1 second.

#### 1.1. Public Properties
* **EnsureSucccessStatusCode** - boolean, default true. Switches the behaviour if System.Net.Http.HttpClient should raise exception for not succesfull response
* **Clients** - instance of Clients class, description bellow
* **Expenses** - instance of Expenses class, description bellow
* **Invoices** - instance of Invoices class, description bellow
* **Stock** - instance of Stock class, description bellow

#### 1.2. Public Methods
* **GetCountries()** - returns Dictionary of available countries from servers. Key: is the country ID, Value: is country name
* **GetSequences()** - returns Dictionary of available sequences in your SuperFactura account,  Key: is the sequence name ID, Value: is array of Response.Sequence objects
* **GetTags()** - returns Dictionary of your used tags from servers. Key: is the tag ID, Value: is tag name
* **GetLogos()** - returns array of Response.Logo objects. List of logos uploaded to SuperFaktura
* **Register(string email, bool sendEmail)** - registers new user to your SuperFaktura account.

### 2. Clients
Class that wrappes all API calls for clients handling in SuperFaktura
Used namespaces:
* Birko.SuperFaktura.Request.Client
* Birko.SuperFaktura.Response
* Birko.SuperFaktura.Response.Client

#### 2.1. Public Methods
* **Get(Request.Client.Filter filter, bool listInfo)** - returns lists of clients according filter
* **Save(Request.Client.Client client)** - saves client 
* **AddContactPerson(Request.Client.ContactPerson person)** - add client contact person

### 3. Expenses
Class that wrappes all API calls for expenses handling in SuperFaktura
Used namespaces:
* Birko.SuperFaktura.Request.Expense
* Birko.SuperFaktura.Response
* Birko.SuperFaktura.Response.Expense

#### 3.1. Public Methods
* **Get(int ID)** - gets Expense according ID. as System.Dynamic.ExpandoObject data
* **Get(Request.Expense.Filter filter,  bool listInfo)** - gets list of expenses according filter
* **Delete(int ID)** - deletes expense with given ID from SuperFaktura servers
* **Save(Request.Expense.Expense expense)** - saves expense
* **Edit(Request.Expense.Expense expense)** - updates expense
* **Pay(Request.Expense.Payment payment)** - adds payment to expense
* **GetExpenseCategories()** - returns list of ExpenseCategories

### 4. Invoices
Class that wrappes all API calls for expenses invoice in SuperFaktura.
Used namespaces:
* Birko.SuperFaktura.Request.Client
* Birko.SuperFaktura.Request.Invoice
* Birko.SuperFaktura.Response
* Birko.SuperFaktura.Response.Invoice

#### 4.1. Public Methods
* **Get(int ID)** - gets Invoice according ID
* **Get(Request.Invoice.Filter filter, bool listInfo)** - gets list of invoices according filter
* **Pay(Request.Invoice.Payment payment)** - adds payment to invoice
* **GetPdf(int invoiceId, string token, string language)** - gets the pdf for given invoice as byte array
* **SetInvoiceLanguage(int ID, string language)** - sets the default language for given invoice
* **MarkAsSent(Request.Invoice.MarkEmail email)** - marks given email to invoice as send
* **SendEmail(Request.Invoice.Email email)** - sends Email  with invoice
* **Save(Request.Invoice.Invoice invoice, Client client, Request.Invoice.Item[] items, int[] tags = null, Setting setting = null)** - creates new invoice
* **Update(Request.Invoice.Invoice invoice, Client client, Request.Invoice.Item[] items, int[] tags = null, Setting setting = null)** - updates invoice
* **DeleteItem(int invoiceID, int itemID)** - deletes item from invoice
* **Delete(int invoiceID)** - deletes invoice
* **SendPost(Post post)** - send invoice throw regular post


### 5. Stock
Class that wrappes all API calls for stock handling in SuperFaktura
Used namespaces:
* Birko.SuperFaktura.Request.Stock
* Birko.SuperFaktura.Response
* Birko.SuperFaktura.Response.Stock

#### 5.1. Public Methods
* **Get(int ID)** - gets stock item according ID
* **Get(Filter filter, bool listInfo = true)** - gets list of stock items according filter
* **Save(Request.Stock.Item item)** - saves stock item
* **Edit(Request.Stock.Item item)** - updates stock item
* **Delete(int ID)** - deletes stock item
* **AddStockMovement(IEnumerable<Request.Stock.Log> items)** - adds stock movement logs to stock item 
* **AddStockMovement(Request.Stock.Log item)** - adds stock movement log  to stock item
