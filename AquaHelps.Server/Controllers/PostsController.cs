using AquaHelps.Application.CQRS.Posts.Commands.Create;
using AquaHelps.Application.CQRS.Posts.Commands.Delete;
using AquaHelps.Application.CQRS.Posts.Commands.Edit;
using AquaHelps.Application.CQRS.Posts.Queries.Get;
using AquaHelps.Application.CQRS.Posts.Queries.GetAll;
using AquaHelps.Server.ViewModels.Account;
using AquaHelps.Server.ViewModels.Posts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> Create([FromBody] CreatePostViewModel model)
    {
        var command = new CreatePostCommand(model.Text, User);

        var result = await _mediator.Send(command);

        return result.Match<IActionResult>(Ok, BadRequest);
    }
    [HttpGet("Get")]
    public async Task<IActionResult> Get()
    {
        var command = new GetAllPostsQuery(Application.Interfaces.CQRS.Queries.OrderType.ByDateDescending);

        var result = await _mediator.Send(command);

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
    public async Task<IActionResult> Edit([FromBody] EditPostViewModel model)
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
