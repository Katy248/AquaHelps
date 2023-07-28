using AquaHelps.Domain.Models;

namespace AquaHelps.Infrastructure.Repository;
public class PostRepository : SearchableRepository<Post>
{
    public PostRepository(ApplicationDbContext context) : base(context)
    {
        SearchExpression = (query, searchQuery) => query.Where(post => post.Text.Contains(searchQuery));
    }
}
