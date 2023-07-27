using AquaHelps.Application.Validation;
using AquaHelps.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace AquaHelps.Application.CQRS.Posts.Commands.Create;
public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, OneOf<Post, ErrorCollection>>
{
    private readonly IRepository<Post> _repository;
    private readonly IValidator<CreatePostCommand> _validator;
    private readonly UserManager<ApplicationUser> _userManager;

    public CreatePostCommandHandler(IRepository<Post> repository, IValidator<CreatePostCommand> validator, UserManager<ApplicationUser> userManager)
    {
        _repository = repository;
        _validator = validator;
        _userManager = userManager;
    }
    public async Task<OneOf<Post, ErrorCollection>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        if (!_validator.ValidateTo(request, out var errors, cancellationToken))
            return errors;

        var post = new Post
        {
            CreatorId = (await _userManager.GetUserAsync(request.User))!.Id,
            CreatedOn = DateTime.UtcNow,
            Text = request.Text,
        };
        await _repository.Create(post, cancellationToken);

        return post;
    }
}
