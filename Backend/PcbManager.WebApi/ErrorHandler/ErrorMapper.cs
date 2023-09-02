using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PcbManager.Domain.Errors;
using PcbManager.Domain.Errors.Abstractions;

namespace PcbManager.WebApi.ErrorHandler;

public static class ErrorMapper
{
    public static ActionResult Map(BaseError error)
    {
        return error switch
        {
            ValidationError validationError => new BadRequestObjectResult(validationError),
            EntityNotFoundError entityNotFoundError => new NotFoundObjectResult(entityNotFoundError),
            ConflictError conflictError => new ConflictObjectResult(conflictError),
            _ => new StatusCodeResult(StatusCodes.Status500InternalServerError)
        };
    }
}