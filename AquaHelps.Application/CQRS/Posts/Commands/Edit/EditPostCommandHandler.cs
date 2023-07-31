using AquaHelps.Application.CQRS.Posts.Models;
using AquaHelps.Application.Validation;
using AquaHelps.Infrastructure;
using AquaHelps.Infrastructure.Repository;
using AutoMapper;

namespace AquaHelps.Application.CQRS.Posts.Commands.Edit;
public class EditPostCommandHandler : IRequestHandler<EditPostCommand, OneOf<PostDto, ErrorCollection>>
{
    private readonly IRepository<Post> _repository;
    private readonly IValidator<EditPostCommand> _validator;
    private readonly IMapper _mapper;

    public EditPostCommandHandler(IRepository<Post> repository, IValidator<EditPostCommand> validator, IMapper mapper)
    {
        _repository = repository;
        _validator = validator;
        _mapper = mapper;
    }
    public async Task<OneOf<PostDto, ErrorCollection>> Handle(EditPostCommand request, CancellationToken cancellationToken)
    {
        if (!_validator.ValidateTo(request, out var errors, cancellationToken))
            return errors;

        var post = await _repository.GetByIdRequired(request.Id, cancellationToken);

        post.Text = request.Text;

        await _repository.Update(post, cancellationToken);

        return _mapper.Map<PostDto>(post);
    }
}
