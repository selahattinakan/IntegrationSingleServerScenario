using Integration.Backend;
using Integration.Common;

namespace Integration.Service
{
    public sealed class ItemIntegrationServiceLockSolution : ISolution
    {
        
        private ItemOperationBackend ItemIntegrationBackend { get; set; } = new();

        public Result SaveItem(string itemContent)
        {
            lock (ItemIntegrationBackend)
            {
                if (ItemIntegrationBackend.FindItemsWithContent(itemContent).Count != 0)
                {
                    return new Result(false, $"(Lock Solution) Duplicate item received with content {itemContent}.");
                }

                var item = ItemIntegrationBackend.SaveItem(itemContent);

                return new Result(true, $"(Lock Solution) Item with content {itemContent} saved with id {item.Id}"); 
            }
        }

        public List<Item> GetAllItems()
        {
            lock (ItemIntegrationBackend)
            {
                return ItemIntegrationBackend.GetAllItems(); 
            }
        }
    }
}
