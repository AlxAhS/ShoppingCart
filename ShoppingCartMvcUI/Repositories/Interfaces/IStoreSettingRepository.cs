namespace ShoppingCartMvcUI.Repositories.Interfaces
{
    public interface IStoreSettingRepository
    {
        Task<IEnumerable<Book>> BooksList();
    }
}
