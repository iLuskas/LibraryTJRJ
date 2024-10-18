using ErrorOr;
using MediatR;

namespace LibraryTJRJ.Application.Common.Interfaces.Messaging;

public interface IQuery<TResponse> : IRequest<ErrorOr<TResponse>>
{
}
