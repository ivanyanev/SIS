namespace IRunes_HomeEdition.Models
{
    public class Track : BaseModel<string>
    {
        public string Name { get; set; }

        public string Link { get; set; }

        public decimal Price { get; set; }
    }
}