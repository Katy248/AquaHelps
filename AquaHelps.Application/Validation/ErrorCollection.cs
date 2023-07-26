using System.Collections;
using FluentValidation.Results;

namespace AquaHelps.Application.Validation;
public record class ErrorCollection(IEnumerable<ValidationFailure> Failures) : IEnumerable<ValidationFailure>
{
    public IEnumerator<ValidationFailure> GetEnumerator() => Failures.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public static readonly ErrorCollection Empty = new(Array.Empty<ValidationFailure>());
}
