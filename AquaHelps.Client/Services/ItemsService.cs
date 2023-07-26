using AquaHelps.Client.Models;

namespace AquaHelps.Client.Services;

public class ItemsService
{
    private readonly PostsService _postsService;
    private readonly DocumentsService _documentsService;

    public ItemsService(PostsService postsService, DocumentsService documentsService)
    {
        _postsService = postsService;
        _documentsService = documentsService;
    }

    public IEnumerable<Item> GetItems()
    {
        return _documentsService.GetDocuments();
    }
    public IEnumerable<Item> Search(string searchQuery)
    {
        if (string.IsNullOrWhiteSpace(searchQuery))
            return Array.Empty<Item>();

        return GetItems()
            .Where(item => item.Name.Contains(searchQuery) || item.Description.Contains(searchQuery));
    }
}
