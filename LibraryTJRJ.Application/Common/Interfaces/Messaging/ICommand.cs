﻿using ErrorOr;
using MediatR;

namespace LibraryTJRJ.Application.Common.Interfaces.Messaging;

public interface ICommand : IRequest<ErrorOr<Success>>, IBaseCommand
{
}

public interface ICommand<TReponse> : IRequest<ErrorOr<TReponse>>, IBaseCommand
{
}

public interface IBaseCommand
{
}
