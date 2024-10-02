
using GameZone.Attribute;

namespace GameZone.ViewModels
{
    public class CreateGameFormVM : GameFormViewModel
    {

        [AllowedExtensions(FileSettings.AllowedExtensions)
        , MaxFileSize(FileSettings.MaxFileSizeInByte)]
        public IFormFile Cover { get; set; } = default!;
    }
}
