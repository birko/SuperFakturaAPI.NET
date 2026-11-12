using Birko.SuperFaktura.Response;
using Birko.SuperFaktura.Response.ValueLists;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Birko.SuperFaktura
{
    public class ValueLists
    {
        private readonly AbstractSuperFaktura superFaktura;

        public ValueLists(AbstractSuperFaktura superFaktura)
        {
            this.superFaktura = superFaktura;
        }

        public async Task<Dictionary<int, string>> ListCountries(CancellationToken cancellationToken = default)
        {
            var result = await superFaktura.Get("countries", cancellationToken).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Dictionary<int, string>>(result);
        }

        public async Task<IEnumerable<Country>> ListCountriesFull(CancellationToken cancellationToken = default)
        {
            var result = await superFaktura.Get("/countries/index/view_full:1", cancellationToken).ConfigureAwait(false);
            var data = superFaktura.DeserializeResult<IEnumerable<CountryData>>(result);
            return data.Select(x => x.Country);
        }

        public async Task<IEnumerable<Category>> ListExpenseCategories(CancellationToken cancellationToken = default)
        {
            var result = await superFaktura.Get("expenses/expense_categories", cancellationToken).ConfigureAwait(false);
            return superFaktura.DeserializeResult<IEnumerable<Category>>(result);
        }

        public async Task<IEnumerable<Logo>> ListLogos(CancellationToken cancellationToken = default)
        {
            var result = await superFaktura.Get("users/logo", cancellationToken).ConfigureAwait(false);
            return superFaktura.DeserializeResult<IEnumerable<Logo>>(result);
        }

        public async Task<Dictionary<string, IEnumerable<Sequence>>> ListSequences(CancellationToken cancellationToken = default)
        {
            var result = await superFaktura.Get("sequences/index.json", cancellationToken).ConfigureAwait(false);
            return superFaktura.DeserializeResult<Dictionary<string, IEnumerable<Sequence>>>(result);
        }
    }
}
