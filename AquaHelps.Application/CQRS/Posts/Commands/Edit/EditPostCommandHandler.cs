using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AquaHelps.Application.Validation;
using AquaHelps.Domain.Models;
using AquaHelps.Infrastructure;
using AquaHelps.Infrastructure.Repository;

namespace AquaHelps.Application.CQRS.Posts.Commands.Edit;
public class EditPostCommandHandler : IRequestHandler<EditPostCommand, OneOf<Post, ErrorCollection>>
{
    private readonly IRepository<Post> _repository;
    private readonly IValidator<EditPostCommand> _validator;

    public EditPostCommandHandler(IRepository<Post> repository, IValidator<EditPostCommand> validator)
    {
        _repository = repository;
        _validator = validator;
    }
    public async Task<OneOf<Post, ErrorCollection>> Handle(EditPostCommand request, CancellationToken cancellationToken)
    {
        if (!_validator.ValidateTo(request, out var errors, cancellationToken))
            return errors;

        var post = await _repository.GetByIdRequired(request.Id, cancellationToken);

        post.Text = request.Text;
        
        await _repository.Update(post, cancellationToken);

        return post;
    }
}
