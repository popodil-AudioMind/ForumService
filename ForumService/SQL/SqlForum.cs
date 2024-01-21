using ForumService.Context;
using ForumService.Data;
using ForumService.Interfaces;
using ForumService.Models;
using System.ComponentModel.DataAnnotations;

namespace ForumService.SQL
{
    public class SqlForum : ISqlForum
    {
        //private readonly ForumDatabaseContext _forumContext;
        /*public SqlForum(ForumDatabaseContext userContext)
        {
            this._forumContext = userContext;
        }*/
        public SqlForum()
        {

        }
        public Forum AddForum(Forum forum)
        {
            /*Guid id = Guid.NewGuid();
            IForum? existingForum = _forumContext.Forums.FirstOrDefault(x => x.id == id.ToString());
            if (existingForum == null)
            {
                
                *//*while (id.ToString() == existingForum.id)
                {
                    id = Guid.NewGuid();
                }*//*
                forum.id = id;
                //_forumContext.Forums.Add(new IForum(forum));
                //_forumContext.SaveChanges();
                return forum;
            }*/
            return null;
        }

        public bool DeleteForum(string id)
        {
            /*IForum? existingUser = _forumContext.Forums.FirstOrDefault(x => x.id == id);
            if (existingUser != null)
            {
                _forumContext.Forums.Remove(existingUser);
                _forumContext.SaveChanges();
                return true;
            }*/
            return false;
        }

        public bool GDPRDeleteForum(string userId)
        {
            /*IForum[] exitingForums = _forumContext.Forums.Where(x => x.userId == userId).ToArray();
            int forumUpdated = 0;
            for (int i = 0; i < exitingForums.Length; i++)
            {
                //TODO update each forum 


                //If forum updated
                if (true) forumUpdated++;
            }
            if (forumUpdated == exitingForums.Length) return true;*/
            return false;
        }

        public Forum GetForum(string id)
        {
            /*IForum? existingUser = _forumContext.Forums.FirstOrDefault(x => x.id == id);
            if (existingUser != null)
            {
                return new Forum(existingUser);
            }*/
            return null;
        }

        public List<ForumView> GetForums()
        {
            List<ForumView> forumViews = new List<ForumView>();
            List<IForum> forums = new List<IForum>()
            {
                new IForum(){id="3fa85f64-5717-4562-b3fc-2c963f66afa4", title="JustATitle",description="TestDesc",uploadDate=DateTime.Now,updateDate=DateTime.Now,userId="3fa85f64-5717-4562-b3fc-2c963f66afa1"},
                new IForum(){id="3fa85f64-5717-4562-b3fc-2c963f66afa3", title="JustATitle",description="TestDesc",uploadDate=DateTime.Now,updateDate=DateTime.Now,userId="3fa85f64-5717-4562-b3fc-2c963f66afa1"},
                new IForum(){id="3fa85f64-5717-4562-b3fc-2c963f66afa5", title="JustATitle",description="TestDesc",uploadDate=DateTime.Now,updateDate=DateTime.Now,userId="3fa85f64-5717-4562-b3fc-2c963f66afa1"}
            };
            /*foreach (IForum iuser in _forumContext.Forums.ToList())
            {
                forumViews.Add(new ForumView(iuser));
            }*/
            foreach (IForum iuser in forums)
            {
                forumViews.Add(new ForumView(iuser));
            }
            return forumViews;
        }

        public Forum UpdateForum(Forum forum)
        {
            /*IForum? existingForum = _forumContext.Forums.FirstOrDefault(x => x.id == forum.id.ToString());
            if (existingForum != null)
            {
                existingForum.title = forum.title;
                existingForum.description = forum.description;
                existingForum.updateDate = DateTime.Now;

                _forumContext.Forums.Update(existingForum);
                _forumContext.SaveChanges();
                return new Forum(existingForum);
            }*/
            return null;
        }
    }
}
