﻿using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FuDever.Application.Shared.Attributes;
using MediatR;

namespace FuDever.Application.Features.Major.RemoveMajorTemporarilyByMajorId.Middlewares;

/// <summary>
///     Remove major temporarily by major
///     id request validation middleware.
/// </summary>
[FeatureMiddlewareOrder(value: 2)]
internal sealed class RemoveMajorTemporarilyByMajorIdValidationMiddleware
    : IPipelineBehavior<
        RemoveMajorTemporarilyByMajorIdRequest,
        RemoveMajorTemporarilyByMajorIdResponse
    >,
        IRemoveMajorTemporarilyByMajorIdMiddleware
{
    private readonly IValidator<RemoveMajorTemporarilyByMajorIdRequest> _validator;

    public RemoveMajorTemporarilyByMajorIdValidationMiddleware(
        IValidator<RemoveMajorTemporarilyByMajorIdRequest> validator
    )
    {
        _validator = validator;
    }

    /// <summary>
    ///     Entry to middleware handler.
    /// </summary>
    /// <param name="request">
    ///     Current request object.
    /// </param>
    /// <param name="next">
    ///     Navigate to next middleware and get back response.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     Response of use case.
    /// </returns>
    public async Task<RemoveMajorTemporarilyByMajorIdResponse> Handle(
        RemoveMajorTemporarilyByMajorIdRequest request,
        RequestHandlerDelegate<RemoveMajorTemporarilyByMajorIdResponse> next,
        CancellationToken cancellationToken
    )
    {
        // Validate input.
        var inputValidationResult = await _validator.ValidateAsync(
            instance: request,
            cancellation: cancellationToken
        );

        // Input is not valid.
        if (!inputValidationResult.IsValid)
        {
            return new()
            {
                StatusCode = RemoveMajorTemporarilyByMajorIdResponseStatusCode.INPUT_VALIDATION_FAIL
            };
        }

        return await next();
    }
}