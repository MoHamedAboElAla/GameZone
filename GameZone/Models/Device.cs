
namespace GameZone.Models
{
    public class Device :BaseClass
    {
        [MaxLength(50)]
        public string Icon { get; set; }=string.Empty;

    }
}
