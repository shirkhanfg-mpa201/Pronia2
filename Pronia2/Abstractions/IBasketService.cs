namespace Pronia2.Abstractions
{
    public interface IBasketService
    {
        Task<List<BasketItem>> GetBasketItemsAsync();
    }
}
