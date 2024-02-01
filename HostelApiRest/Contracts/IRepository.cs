namespace HostelApiRest.Contracts
{
    public interface IRepository<T>
    {
        public Task<List<T>> ListAsync();

        public Task<int> AddAsync(T model);

        public Task<T> SearchAsync(int id);

        public Task<int> ChangeAsync(T model);

        public Task<int> EraseAsync(int id);
    }
}
