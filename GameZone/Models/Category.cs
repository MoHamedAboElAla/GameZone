namespace GameZone.Models
{
    public class Category : BaseClass
    {
        public ICollection<Game>Games { get; set; }=new List<Game>();


    }
}
