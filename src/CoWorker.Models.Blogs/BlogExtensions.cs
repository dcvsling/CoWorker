using System;

using System.Collections.Generic;
using System.Linq;

namespace CoWorker.Models.Blog
{
    public static class BlogExtensions
    {
        public static PostRelated CreateRelated(this PostState state, PostRelated related = null)
        {
            if (related == null) related = new PostRelated();
            related.Owner = state.Owner;
            related.Level = state.Level;
            related.EndDate = state.EndDate;
            related.StartDate = state.StartDate;
            return related;
        }
        public static PostState SetPost(this PostState state, Post post)
        {
            state.Content = post.Content;
            state.Description = post.Description;
            state.Source = post.Source;
            state.Title = post.Title;
            return state;
        }
        public static Post GetPost(this PostState state)
        {
            return new Post()
            {
                Content = state.Content,
                Description = state.Description,
                Source = state.Source,
                Title = state.Title
            };
        }

        public static IEnumerable<object> ToFormat(this IEnumerable<PostRelated> relateds)
        {
            var tops = relateds.First(y => y.Current.Id == Guid.Empty)
                .Posts
                .Where(x => x.Current.Id != Guid.Empty)
                .Select(x => x.Current);

            return relateds.Where(x => x.Current.Id != Guid.Empty)
                .Select(
                    x => new
                    {
                        Post = new {
                            x.Current.Id,
                            x.Current.Title,
                            x.Current.Source,
                            x.Current.Description,
                            x.Current.Content,
                        },
                        x.Posts
                    })
                .ToArray()
                .Where(x => tops.Any(y => y.Id == x.Post.Id));
        }
    }
}
