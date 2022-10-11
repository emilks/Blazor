namespace MaskinPark.Shared
{
    public class Machine
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("n");

        public string Name { get; set; }

        public string Status { get; set; }

        public string Data { get; set; }

        public DateTime DataTime { get; set; }
    }
}
