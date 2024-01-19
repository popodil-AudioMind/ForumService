using System.ComponentModel.DataAnnotations;
using ForumService.Interfaces;

namespace ForumService.Models
{
    public class ForumView
    {
        public ForumView(IForum forum)
        {
            id = Guid.Parse(forum.id);
            title = forum.title;
            uploadDate = forum.uploadDate;
        }

        public ForumView() 
        {
        
        }

        [Key]
        [Required]
        public Guid id { get; set; }

        [Required]
        public string title { get; set; }

        [Required]
        public DateTime uploadDate { get; set; }
    }
}
