using System.ComponentModel.DataAnnotations;

namespace FrontToBack.ViewModels
{
    public class SliderCreateVM
    {
        [Required(ErrorMessage="Should be not emty")]
        public IFormFile Photo { get; set; }
    }
}
