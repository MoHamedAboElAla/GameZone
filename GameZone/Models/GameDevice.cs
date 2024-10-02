namespace GameZone.Models
{
    public class GameDevice
    {

       public int GameId { get; set; }

        public Game Game { get; set; } = default!;

        public int DeviceId { get; set; }
        public Device Devcie { get; set; } = default!;
    }
}
