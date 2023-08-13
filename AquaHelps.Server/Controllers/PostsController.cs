using AquaHelps.Application.CQRS.Posts.Commands.Create;
using AquaHelps.Application.CQRS.Posts.Commands.Delete;
using AquaHelps.Application.CQRS.Posts.Commands.Edit;
using AquaHelps.Application.CQRS.Posts.Queries.Get;
using AquaHelps.Application.CQRS.Posts.Queries.GetAll;
using AquaHelps.Application.CQRS.Posts.Queries.Search;
using AquaHelps.Shared.Requests.Posts;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Web;

namespace AquaHelps.Server.Controllers;

[ApiController, Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PostsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("Create"), Authorize]
    public async Task<IActionResult> Create([FromBody] CreatePostRequest model)
    {
        var command = new CreatePostCommand(model.Text, User);

        var result = await _mediator.Send(command);

        return result.Match<IActionResult>(Ok, BadRequest);
    }
    [HttpGet("Get"), Authorize]
    public async Task<IActionResult> Get()
    {
        var ctx = HttpContext.Request;
        var user = User;
        var claim = ClaimTypes.Email;
        var command = new GetAllPostsQuery(Application.Interfaces.CQRS.Queries.OrderType.ByDateDescending);

        var result = await _mediator.Send(command);

        return Ok(result);
    }
    [HttpGet("Search/{searchQuery}")]
    public async Task<IActionResult> Search([FromRoute] string searchQuery)
    {
        var query = new SearchPostQuery(HttpUtility.HtmlDecode(searchQuery));

        var result = await _mediator.Send(query);

        return Ok(result);
    }
    [HttpGet("Get/{id}")]
    public async Task<IActionResult> Get([FromRoute] string id)
    {
        var command = new GetPostQuery(id);

        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(Ok, BadRequest);
    }
    [HttpPost("Edit"), Authorize]
    public async Task<IActionResult> Edit([FromBody] EditPostRequest model)
    {
        var command = new EditPostCommand(model.Id, model.Text, User);

        var result = await _mediator.Send(command);

        return result.Match<IActionResult>(Ok, BadRequest);
    }
    [HttpPost("Delete/{id}"), Authorize]
    public async Task<IActionResult> Delete([FromRoute] string id)
    {
        var command = new DeletePostCommand(id, User);

        var result = await _mediator.Send(command);

        return result.Match<IActionResult>(_ => Ok(), BadRequest);
    }
}

