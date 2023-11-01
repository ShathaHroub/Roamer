using System.ComponentModel.DataAnnotations;

namespace Roamer.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string PostTitle { get; set; }
        public string PostContent { get; set; }
        public string PostContent2 { get; set; }
        public string PostHeading { get; set; }
        public string PostContent3 { get; set; }
        public string PostHeading2 { get; set; }
      


        public DateTime CreatedAt { get; set; }
      
        [DataType(DataType.Upload)]
        public string PostImagePath { get; set; }

        public List<Comment> Comments { get; set; }
        

    }

  
    
}
