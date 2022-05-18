namespace TaxiManager9000.DataAccess.Interface
{
    public interface IDatabase<T>
    {
        Task InsertAsync(T data);

        T AutoIncrementId(T data);

        Task UpdateAsync();
    }
}
