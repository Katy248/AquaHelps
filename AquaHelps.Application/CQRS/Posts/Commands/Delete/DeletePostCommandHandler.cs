using AquaHelps.Application.Validation;
using AquaHelps.Infrastructure;

namespace AquaHelps.Application.CQRS.Posts.Commands.Delete;
public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, OneOf<bool, ErrorCollection>>
{
    private readonly IRepository<Post> _repository;
    private readonly IValidator<DeletePostCommand> _validator;

    public DeletePostCommandHandler(IRepository<Post> repository, IValidator<DeletePostCommand> validator)
    {
        _repository = repository;
        _validator = validator;
    }
    public async Task<OneOf<bool, ErrorCollection>> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        if (!_validator.ValidateTo(request, out var errors, cancellationToken))
            return errors;

        await _repository.DeleteById(request.Id, cancellationToken);

        return true;
    }
}
