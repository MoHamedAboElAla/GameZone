﻿
namespace GameZone.Services
{
    public class DevicesServices : IDevicesServices
    {
        private readonly AppDbContext _context;
        public DevicesServices(AppDbContext context)
        {
            _context=context;
        }
        public IEnumerable<SelectListItem> GetSelectList()
        {
            var devices= _context.Devices
               .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
               .OrderBy(d => d.Text).AsNoTracking()
               .ToList();

                return devices;
        }
    }
}
