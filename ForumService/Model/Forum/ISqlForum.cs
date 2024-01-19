using ForumService.Models;
namespace ForumService.Data
{
    public interface ISqlForum
    {
        Forum GetForum(string forumId);
        List<ForumView> GetForums();
        Forum AddForum(Forum forum);
        bool DeleteForum(string forumId);
        Forum UpdateForum(Forum forum);
        bool GDPRDeleteForum(string userId);
    }
}
