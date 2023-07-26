using Markdig;
using Microsoft.AspNetCore.Components;

namespace AquaHelps.Client.Services;

public class MarkdownService
{
    public MarkupString ParseToHtml(string markdown)
    {
        return (MarkupString)Markdown
            .ToHtml(markdown);
    }
    public static readonly MarkdownPipeline Pipeline =
        new MarkdownPipelineBuilder()
        .UseTaskLists()
        .UseDiagrams()
        .Build();
}
