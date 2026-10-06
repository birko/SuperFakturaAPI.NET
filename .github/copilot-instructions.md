# SuperFakturaAPI.NET - instructions for Copilot

.NET client for the SuperFaktura API. Sources of truth: the API documentation
(https://github.com/superfaktura/docs) and the official PHP client
(https://github.com/superfaktura/apiclient). Where they disagree, the behaviour of the sandbox wins.

## Layout
- `SuperFakturaAPI.NET/` - the library as a shared project; every new `.cs` file must be added to `SuperFakturaAPI.NET.projitems`.
  - `Request/` - models sent to the API, `Response/` - models read from it, `Request/ValueLists/Constants.cs` - value lists.
  - `Invoices.cs`, `Expenses.cs`, ... - endpoint methods; `SuperFaktura.cs` - HTTP client, errors, rate limit.
- `SuperFaktura/` - NuGet package project (netstandard2.0). `SuperFaktura.Tests/` - xUnit tests.

## Conventions
- Request properties are nullable and default to `null`; `null` is not sent (`NullValueHandling.Ignore`), so Edit changes only what was set. Never add defaults like `""`, `0`, `true`, `DateTime.Now` or the machine region currency.
- JSON names exactly as in the API (`[JsonProperty("snake_case")]`). Booleans the API documents as int 0/1 use `Converters.StringBooleanConverter`; `date` attributes use `Converters.DateConverter` (YYYY-MM-DD).
- Numbers and dates in URL filters use `CultureInfo.InvariantCulture`; a `*_since`/`*_to` range adds the time filter `3` automatically.
- Do not break public signatures without need: add optional parameters at the end, prefer adding properties to changing types.
- Response models inherit request models; a response-only field goes to the response model.
- Known API behaviour: `invoices/edit` updates items with `id` and adds items without one, but never deletes left-out items.

## Tests
- Every change gets an offline test in `SuperFaktura.Tests/SerializationTest.cs` first (serialize the request / deserialize a documented or observed response). These tests need no credentials:
  `dotnet test SuperFaktura.slnx --filter "FullyQualifiedName~SerializationTest"`
- Integration tests call the sandbox and need `SuperFaktura.Tests/Properties/launchSettings.json` (not in the repository); they run only in Debug.
- Build with zero warnings: `dotnet build SuperFaktura.slnx -c Release`.
