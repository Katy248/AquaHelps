using AquaHelps.Domain.Models;

namespace AquaHelps.Infrastructure.Repository;
public class PostRepository : SearchableRepository<Post>
{
    public PostRepository(ApplicationDbContext context) : base(context)
    {
        //AddIncludeStatement(post => post.Creator);
        AddSearchSelector(post => post.Text);
        AddSearchSelector(post => post.Creator?.UserName ?? "");
    }
}
