using System.Collections.Generic;
using System.Linq;

namespace IRunes_HomeEdition.Models
{
    public class Album : BaseModel<string>
    {
        public Album()
        {
            this.Tracks = new HashSet<Track>();
        }

        public string Name { get; set; }

        public string Cover { get; set; }

        public decimal Price => this.Tracks.Sum(x => x.Price) - this.Tracks.Sum(x => x.Price) * 0.13m;

        public ICollection<Track> Tracks { get; set; }
    }
}
