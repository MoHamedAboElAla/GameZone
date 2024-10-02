
namespace GameZone.Services
{
    public class CategoriesServices : ICategoriesServices
    {

        private readonly AppDbContext _context;

        public CategoriesServices(AppDbContext context)
        {
            _context = context;
        }

        

        public IEnumerable<SelectListItem> GetSelectList()
        {
        
            var categories= _context.Categories
             .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
             .OrderBy(c => c.Text).AsNoTracking()
              .ToList();
         
            return categories;
        }
    }
}
