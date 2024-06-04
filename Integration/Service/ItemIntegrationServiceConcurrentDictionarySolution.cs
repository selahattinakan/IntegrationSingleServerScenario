using Integration.Backend;
using Integration.Common;
using System.Collections.Concurrent;

namespace Integration.Service
{
    public sealed class ItemIntegrationServiceConcurrentDictionarySolution : ISolution
    {
        private static ConcurrentDictionary<string, bool> existingItems = new ConcurrentDictionary<string, bool>();

        private ItemOperationBackend ItemIntegrationBackend { get; set; } = new();

        public Result SaveItem(string itemContent)
        {
            if (existingItems.TryAdd(itemContent, true))
            {
                var item = ItemIntegrationBackend.SaveItem(itemContent);

                return new Result(true, $"(ConcurrentDictionary Solution) Item with content {itemContent} saved with id {item.Id}");
            }
            else
            {
                return new Result(false, $"(ConcurrentDictionary Solution) Duplicate item received with content {itemContent}.");
            }
        }

        public List<Item> GetAllItems()
        {
            return ItemIntegrationBackend.GetAllItems();
        }
    }
}
