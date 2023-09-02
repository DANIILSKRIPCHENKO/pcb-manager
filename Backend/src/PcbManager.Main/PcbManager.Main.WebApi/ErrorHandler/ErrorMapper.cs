using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PcbManager.Main.Domain.Errors;
using PcbManager.Main.Domain.Errors.Abstractions;

namespace PcbManager.Main.WebApi.ErrorHandler;

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