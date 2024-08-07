﻿using Application.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAPI.Modules.Filters;
using WebAPI.ViewModels.Errors;

namespace WebAPI.Controllers
{
    [LogAction]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionViewModel))]
    public abstract class ApiController : ControllerBase
    {
        protected readonly Notification _notification;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected IActionResult _viewModel;

        protected ApiController(Notification notification, IHttpContextAccessor httpContextAccessor)
        {
            _notification = notification;
            _httpContextAccessor = httpContextAccessor;
        }

        protected UnprocessableEntityObjectResult UnprocessableEntity(Notification notification)
        {
            return UnprocessableEntity(new ErrorsResponse(notification));
        }

        protected UnprocessableEntityObjectResult UnprocessableEntity(string notification)
        {
            return UnprocessableEntity(new ErrorsResponse(notification));
        }

        protected UnauthorizedObjectResult Unauthorized(Notification notification)
        {
            var problemDetails = new ValidationProblemDetails(notification.ModelState);
            return Unauthorized(problemDetails);
        }

        protected OkObjectResult Ok<T>(IMapper mapper, object value)
        {
            var result = mapper.Map<T>(value);
            return Ok(result);
        }
    }
}