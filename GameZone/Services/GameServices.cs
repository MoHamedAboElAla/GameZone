



namespace GameZone.Services
{
    public class GameServices : IGameServices
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagesPath;


        public GameServices(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagesPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagesPath}";
        }
        public async Task Create(CreateGameFormVM model)
        {

            var coverName=await SaveCover(model.Cover);
           

            Game game = new()
            {
                Name = model.Name,
                Description = model.Description,
                cover = coverName,
                CategoryId = model.CategoryId,
                Devices = model.SelectedDevices.Select(d=>new GameDevice { DeviceId=d}).ToList()
            };
            _context.Games.Add(game);
            _context.SaveChanges();

        }

        public async Task<Game?> Update(EditGameFormViewModel model)
        {

            var IsNewCover = model.Cover is not null;
            
            var game = _context.Games
            .Include(g=>g.Devices)
            .SingleOrDefault(g=>g.Id==model.Id);
            var oldcover = game.cover;
            if (game is null)
                return null;

            game.Name=model.Name;
            game.Description=model.Description;
            game.CategoryId=model.CategoryId;
            game.Devices = model.SelectedDevices.Select(d=> new GameDevice { DeviceId = d }).ToList();

            if (IsNewCover)
            {
                game.cover = await SaveCover(model.Cover!);
            }
            var effectedRows = _context.SaveChanges();
            if (effectedRows > 0)
            {
                if (IsNewCover)
                {
                    var cover = Path.Combine(_imagesPath, oldcover);
                    File.Delete(cover);
                }
                return game;
            }
            else
            {
                var cover = Path.Combine(_imagesPath, game.cover);
                File.Delete(cover);
                return null;
            }
        }

        public IEnumerable<Game> GetAll()
        {
            var games = _context.Games
                .Include(g=>g.Category)
                .Include(g=>g.Devices)
                .ThenInclude(d=>d.Devcie)
                .AsNoTracking().ToList();
            return games;
        }

        public Game? GetById(int id)
        {
            var game = _context.Games
             .Include(g => g.Category)
             .Include(g => g.Devices)
             .ThenInclude(d => d.Devcie)
             .AsNoTracking().SingleOrDefault(g=>g.Id==id);

            return game;

        }
        public bool Delete(int id)
        {
            var IsDeleted = false;
            var game = _context.Games.Find(id);
            if (game is null)
                return IsDeleted;

            _context.Remove(game);
            var effectedrows = _context.SaveChanges();

            if (effectedrows > 0)
            {
                IsDeleted = true;
                var cover = Path.Combine(_imagesPath, game.cover);
                File.Delete(cover);

            }
            return IsDeleted;
        }
        private async Task<string> SaveCover(IFormFile cover)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";
            var path = Path.Combine(_imagesPath, coverName);
            using var stream = File.Create(path);
            await cover.CopyToAsync(stream);

            return coverName;
        }

       
    }
}
