﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bike_EShop.Application.Common.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bike_EShop.Application.Common.Behaviours
{
    [ExcludeFromCodeCoverage]
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;
        public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            //Request
            _logger.LogInformation($"Handling {typeof(TRequest).Name}");

            try
            {
                var response = await next();

                //Response
                _logger.LogInformation($"Handled {typeof(TResponse).Name}");
                return response;
            }
            catch (NotFoundException e)
            {
                _logger.LogWarning(e, "Requestparamater is not found");
                throw;
            }
        }
    }
}
