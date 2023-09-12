using System.Text.Json;
using Domain.Exceptions;
using Domain.Exceptions.Implementation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Services.Dto.Responses;

namespace Web.Api.Filters
{
    public class ExceptionsFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionsFilter> _logger;

        public ExceptionsFilter( ILogger<ExceptionsFilter> logger )
        {
            _logger = logger;
        }

        public void OnException( ExceptionContext context )
        {
            AbstractRuntimeException abstractRuntimeException;
            if ( context.Exception is AbstractRuntimeException )
            {
                abstractRuntimeException = (AbstractRuntimeException) context.Exception.GetBaseException();
                _logger.LogWarning( "{ExceptionMessage}", abstractRuntimeException.Message );
            }
            else
            {
                abstractRuntimeException = new InternalException( "Error on handling request", context.Exception );
                _logger.LogWarning( context.Exception, "Error on handling request" );
            }

            ErrorDto exceptionDto = new ErrorDto
            {
                Message = abstractRuntimeException.Message,
                StatusCode = 500
            };
            
            context.Result = new ContentResult
            {
                Content = JsonSerializer.Serialize( exceptionDto ),
                StatusCode = exceptionDto.StatusCode
            };
        }
    }
}