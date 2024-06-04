using Integration.Backend;
using Integration.Common;

namespace Integration.Service
{
    public sealed class ItemIntegrationServiceSemaphoreSlimSolution : ISolution
    {
        private static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1);

        private ItemOperationBackend ItemIntegrationBackend { get; set; } = new();

        public Result SaveItem(string itemContent)
        {
            try
            {
                semaphoreSlim.Wait();

                if (ItemIntegrationBackend.FindItemsWithContent(itemContent).Count != 0)
                {
                    return new Result(false, $"(SemaphoreSlim Solution) Duplicate item received with content {itemContent}.");
                }

                var item = ItemIntegrationBackend.SaveItem(itemContent);

                return new Result(true, $"(SemaphoreSlim Solution) Item with content {itemContent} saved with id {item.Id}");
            }
            catch (Exception)
            {
                return new Result(false, $"(SemaphoreSlim Solution) Something went wrong about Semaphore.");
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        public List<Item> GetAllItems()
        {
            List<Item> items = new List<Item>();
            try
            {
                semaphoreSlim.Wait();
                items = ItemIntegrationBackend.GetAllItems();
            }
            finally
            {
                semaphoreSlim.Release();
            }
            return items;
        }
    }
}
