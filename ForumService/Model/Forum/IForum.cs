using System.ComponentModel.DataAnnotations;
using ForumService.Models;

namespace ForumService.Interfaces
{
    public class IForum
    {
        public IForum(Forum forum) 
        {
            id = forum.id.ToString();
            title = forum.title;
            description = forum.description;
            uploadDate = forum.uploadDate;
            updateDate = forum.updateDate;
        }
        public IForum() 
        { 
        
        }
        public string id { get; set; }

        [Required]
        public string title { get; set; }

        [Required]
        public string description { get; set; }

        [Required]
        public DateTime uploadDate { get; set; }

        [Required]
        public DateTime updateDate { get; set; }

        [Required]
        public string userId { get; set; }
    }
}
