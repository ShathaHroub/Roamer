namespace Roamer.ViewModels
{
    public class GuideViewModel
    {
        public string? GuideName { get; set; }
        public string? GuideDescription { get; set; }

        public IFormFile GuideUrl { get; set; }
    }
}
