using PackIT.Domain.Events;
using PackIT.Domain.Exceptions;
using PackIT.Domain.ValueObjects;
using PackIT.Shared.Abstractions.Domain;

namespace PackIT.Domain.Entities
{
    public class PackingList: AggregateRoot<PackingListId>
    {
        private PackingListName _name;
        private Localization _localization;

        private readonly LinkedList<PackingItem> _items = new();

        public PackingListId Id { get; private set; }

        private PackingList(PackingListId id, PackingListName name, Localization localization, LinkedList<PackingItem> items):this(id,name,localization)
        {
           
            _items = items;
        }

        internal PackingList(PackingListId id, PackingListName name, Localization localization) : base(id)
        {
            Id = id;
            _name = name;
            _localization = localization;
           
        }

        public void AddItem(PackingItem item)
        {
            var alreadyExists = _items.Any(i => i.Name == item.Name);
            if (alreadyExists)
            {
                throw new PackingItemAlreadyExistsException(_name, item.Name);
            }
            _items.AddLast(item);
            AddEvent(new PackingItemAdded(this, item));
        }

        public void AddItems(IEnumerable<PackingItem> items)
        {
            foreach (var item in items)
            {
                AddItem(item);
            }
        }

        public void PackItem(string itemName)
        {
            var item = GetItem(itemName);
            var packedItem = item with { IsPacked = true };
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            _items.Find(item).Value = packedItem;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            AddEvent(new PackingItemPacked(this, packedItem));

        }

        public void RemoveItem(string itemName)
        {
            var item = GetItem(itemName);
            _items.Remove(item);
            AddEvent(new PackingItemRemoved(this,item));
        }

        private PackingItem GetItem(string itemName)
        {
            var item = _items.SingleOrDefault(i => i.Name == itemName);

            if(item == null)
            {
                throw new PackingItemNotFoundException(itemName);
            }

            return item;
        }

    }
}
