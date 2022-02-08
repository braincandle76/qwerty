using QwertyAPI.Models;

namespace QwertyAPI.ViewModels
{
    public class QwertyFavColorResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public QwertyFavColorResponse(QwertyFavColor c)
        {
            Id = c.Id;
            Name = c.Color;
        }
    }
}