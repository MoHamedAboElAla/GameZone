using GameZone.Attribute;

namespace GameZone.ViewModels
{
    public class EditGameFormViewModel : GameFormViewModel
    {

      public int Id {  get; set; }
        public string? CurrentCover { get; set; }

        [AllowedExtensions(FileSettings.AllowedExtensions)
           , MaxFileSize(FileSettings.MaxFileSizeInByte)]
        public IFormFile? Cover { get; set; } = default!;
    }
}
