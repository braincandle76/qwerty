using System.ComponentModel.DataAnnotations;

namespace QwertyAPI.ViewModels
{
    public class QwertyProfileRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Range(1, int.MaxValue, ErrorMessage = "The QwertyFavColorId field is required.")]
        public int QwertyFavColorId { get; set; }
    }
}