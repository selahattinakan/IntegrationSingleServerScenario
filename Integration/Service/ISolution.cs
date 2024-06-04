using Integration.Common;

namespace Integration.Service
{
    public interface ISolution
    {
        Result SaveItem(string itemContent);
        
        List<Item> GetAllItems();
    }
}
