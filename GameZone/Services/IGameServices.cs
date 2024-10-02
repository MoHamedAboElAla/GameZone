namespace GameZone.Services
{
    public interface IGameServices
    {
        IEnumerable<Game> GetAll();
        Task Create(CreateGameFormVM model);

        Game? GetById(int id);
        bool Delete(int id);
        Task<Game?> Update(EditGameFormViewModel model);
     
    }
}
