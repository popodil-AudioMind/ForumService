using System.ComponentModel.DataAnnotations;
using ForumService.Interfaces;

namespace ForumService.Models
{
    public class Forum
    {
        public Forum (IForum forum)
        {
            id = Guid.Parse(forum.id);
            title = forum.title;
            description = forum.description;
            uploadDate = forum.uploadDate;
            updateDate = forum.updateDate;
        }

        [Key]
        [Required]
        public Guid id { get; set; }

        [Required]
        public string title { get; set; }

        [Required]
        public string description { get; set; }

        [Required]
        public DateTime uploadDate { get; set; }

        [Required]
        public DateTime updateDate { get; set; }

        [Required]
        public Guid userId { get; set; }
    }
}
