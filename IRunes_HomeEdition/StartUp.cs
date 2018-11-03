using IRunes_HomeEdition.Models;
using System.Linq;

namespace IRunes_HomeEdition
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var album = new Album();
            album.Tracks.Add(new Track() { Price = 100});
            
            var result = album.Price;
        }
    }
}