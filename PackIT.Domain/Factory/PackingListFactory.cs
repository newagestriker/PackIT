using PackIT.Domain.Consts;
using PackIT.Domain.Entities;
using PackIT.Domain.Policies;
using PackIT.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackIT.Domain.Factory
{
    public sealed class PackingListFactory : IPackingListFactory
    {
        private readonly IEnumerable<IPackingItemsPolicy> _polices;

        public PackingListFactory(IEnumerable<IPackingItemsPolicy> polices)
        {
            _polices = polices;
        }

        public PackingList Create(PackingListId id, PackingListName name, Localization localization)
            => new(id,name,localization);

        public PackingList CreateWithDefaultItems(PackingListId id, PackingListName name, TravelDays days, Gender gender, ValueObjects.Temperature temperature, Localization localization)
        {
            var data = new PolicyData(days, gender, temperature, localization);
            var applicablePolicies = _polices.Where(p=>p.IsApplicable(data));

            var items = applicablePolicies.SelectMany(a => a.GenerateItems(data));
            var packingList = Create(id, name, localization);

            packingList.AddItems(items); 
            return packingList;
             
        }
    }
}
