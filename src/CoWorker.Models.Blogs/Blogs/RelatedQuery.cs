using System.Collections;
using System.Linq;

namespace CoWorker.Models.Blog
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public class RelatedQuery : IQueryable<PostRelated>
    {
        private readonly PostRelated _related;
        private readonly IEnumerable<PostRelated> _posts;
        public Type ElementType => typeof(PostRelated);

        public Expression Expression { get; }

        public IQueryProvider Provider { get; }

        public RelatedQuery(PostRelated related)
        {
            this._related = related;
            this._posts = Enumerable.Empty<PostRelated>();
            this.Provider = new EnumerableQuery<PostRelated>(_posts);
        }
        public IEnumerator<PostRelated> GetEnumerator()
            => _posts.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();
    }
}
