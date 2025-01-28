using Birko.SuperFaktura.Response;
using Birko.SuperFaktura.Response.ValueLists;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Dictionary<int, string>> ListCountries()
        {
            var result = await superFaktura.Get("countries").ConfigureAwait(false);
            return superFaktura.DeserializeResult<Dictionary<int, string>>(result);
        }

        public async Task<IEnumerable<Country>> ListCountriesFull()
        {
            var result = await superFaktura.Get("/countries/index/view_full:1").ConfigureAwait(false);
            var data = superFaktura.DeserializeResult<IEnumerable<CountryData>>(result);
            return data.Select(x => x.Country);
        }

        public async Task<IEnumerable<Category>> ListExpenseCategories()
        {
            var result = await superFaktura.Get("expenses/expense_categories").ConfigureAwait(false);
            return superFaktura.DeserializeResult<IEnumerable<Category>>(result);
        }

        public async Task<IEnumerable<Logo>> ListLogos()
        {
            var result = await superFaktura.Get("users/logo").ConfigureAwait(false);
            return superFaktura.DeserializeResult<IEnumerable<Logo>>(result);
        }

        public async Task<Dictionary<string, IEnumerable<Sequence>>> ListSequences()
        {
            var result = await superFaktura.Get("sequences/index.json").ConfigureAwait(false);
            return superFaktura.DeserializeResult<Dictionary<string, IEnumerable<Sequence>>>(result);
        }
    }
}
