namespace Roamer.ViewModels
{
    public class PostViewModel
    {
        public string PostTitle { get; set; }
        public string PostContent { get; set; }
        public string PostContent2 { get; set; }
        public string PostHeading { get; set; }
        public string PostContent3 { get; set; }
        public string PostHeading2 { get; set; }
        public DateTime CreatedAt { get; set; }
        public IFormFile PostImagePath { get; set; }

    }
}
