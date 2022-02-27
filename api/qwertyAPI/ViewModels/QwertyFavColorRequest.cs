using System.ComponentModel.DataAnnotations;

namespace QwertyAPI.ViewModels
{
    public class QwertyFavColorRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string Color { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Range(1, int.MaxValue, ErrorMessage = "The QwertyFavColorId field is required.")]
        public int QwertyFavColorId { get; set; }
    }
}