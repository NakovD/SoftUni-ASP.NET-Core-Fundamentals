namespace Forum.Services.Contracts
{
    using Models;

    public interface IPostService
    {
        Task<List<PostViewModel>> GetAllAsync();

        Task<PostViewModel?> GetByIdAsync(int id);

        Task<PostViewModel> AddSingleAsync(PostFormViewModel model);

        Task<bool> DeleteSingleAsync(int id);

        Task<PostViewModel?> UpdateSingleAsync(int id, PostFormViewModel model);
    }
}
