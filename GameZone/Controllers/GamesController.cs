
    public class GamesController : Controller
    {

        private readonly AppDbContext _context;
        private readonly ICategoriesServices _categoriesServices;
        private readonly IDevicesServices _devicesServices;
        private readonly IGameServices _gamesServices;


        public GamesController(ICategoriesServices categoriesServices, IDevicesServices devicesServices, IGameServices gamesServices)
        {

            _devicesServices = devicesServices;
            _categoriesServices = categoriesServices;
            _gamesServices = gamesServices;
        }


        public IActionResult Index()
        {
            var games = _gamesServices.GetAll();
            return View(games);
        }
        [HttpGet]
        public IActionResult Create()
        {
            CreateGameFormVM viewModel = new()
            {

                Categories = _categoriesServices.GetSelectList(),
                Devices = _devicesServices.GetSelectList()
            };

            return View(viewModel);
        }
        public IActionResult Details(int id)
        {
            var game = _gamesServices.GetById(id);

            if (game is null)
                return NotFound();

            return View(game);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameFormVM Model)
        {
            if (!ModelState.IsValid)

            {
                Model.Categories = _categoriesServices.GetSelectList();
                Model.Devices = _devicesServices.GetSelectList();

                return View(Model);
            }
            await _gamesServices.Create(Model);

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            var game = _gamesServices.GetById(id);

            if (game is null)
                return NotFound();

            EditGameFormViewModel viewModel = new()
            {
                Id = id,
                Name = game.Name,
                Description = game.Description,
                CategoryId = game.CategoryId,
                SelectedDevices = game.Devices.Select(d => d.DeviceId).ToList(),
                Categories = _categoriesServices.GetSelectList(),
                Devices = _devicesServices.GetSelectList(),
                CurrentCover = game.cover
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameFormViewModel Model)
        {
            if (!ModelState.IsValid)

            {
                Model.Categories = _categoriesServices.GetSelectList();
                Model.Devices = _devicesServices.GetSelectList();

                return View(Model);
            }
            var game = await _gamesServices.Update( Model);
        if (game is null)

            return BadRequest();

        return RedirectToAction(nameof(Index));
        
    }
    [HttpDelete]
    public IActionResult Delete(int id)
    {
        var IsDeleted=_gamesServices.Delete(id);
        return IsDeleted? Ok() : BadRequest() ;
    }
        
        }
