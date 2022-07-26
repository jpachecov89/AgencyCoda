using AgencyCoda.BusinessLayer.Dtos;
using AgencyCoda.BusinessLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AgencyCoda.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OauthController : ControllerBase
    {
        private readonly ILogger<OauthController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IOauthManager _iOauthManager;

        public OauthController(ILogger<OauthController> logger, IConfiguration configuration, IOauthManager iOauthManager)
        {
            _logger = logger;
            _configuration = configuration;
            _iOauthManager = iOauthManager;
        }

        [HttpGet("RedirectUrl")]
        public ActionResult<string> RedirectUrl()
        {
            string scope = _configuration.GetValue<string>("AppSettings:Credentials:Scope");
            string redirect_url = _configuration.GetValue<string>("AppSettings:Credentials:RedirectUrl");
            string client_id = _configuration.GetValue<string>("AppSettings:Credentials:ClientId");

            
            return _iOauthManager.RedirectUrl(scope, redirect_url, client_id);
        }

        [HttpGet("Callback")]
        public ActionResult<string> Callback(string code)
        {
            string scope = _configuration.GetValue<string>("AppSettings:Credentials:Scope");
            string redirect_url = _configuration.GetValue<string>("AppSettings:Credentials:RedirectUrl");
            string client_id = _configuration.GetValue<string>("AppSettings:Credentials:ClientId");
            string client_secret = _configuration.GetValue<string>("AppSettings:Credentials:ClientSecret");

            if (!string.IsNullOrWhiteSpace(code))
            {
                return _iOauthManager.Callback(client_id, scope, redirect_url, code, client_secret);
            }

            return BadRequest();
        }

        [HttpGet("AllEvents")]
        public IEnumerable<CalendarEventDto> AllEvents()
        {
            StringValues tokens;
            Request.Headers.TryGetValue("access_token", out tokens);
            string token = tokens.ToString();

            return _iOauthManager.GetAllEvents(token);
        }

        [HttpGet("GetEvent")]
        public CalendarEventDto GetEvent(string eventId)
        {
            StringValues tokens;
            Request.Headers.TryGetValue("access_token", out tokens);
            string token = tokens.ToString();

            return _iOauthManager.GetEvent(eventId, token);
        }

        [HttpPost("CreateEvent")]
        public ActionResult CreateEvent(CalendarEventDto calendarEvent)
        {
            StringValues tokens;
            Request.Headers.TryGetValue("access_token", out tokens);
            string token = tokens.ToString();

            CalendarEventDto newCalendarEvent = new CalendarEventDto();
            newCalendarEvent.Subject = calendarEvent.Subject;
            newCalendarEvent.Body.Content = calendarEvent.Body.Content;
            newCalendarEvent.Start.DateTime = calendarEvent.Start.DateTime;
            newCalendarEvent.End.DateTime = calendarEvent.End.DateTime;

            _iOauthManager.CreateEvent(newCalendarEvent, token);
            return Ok();
        }

        [HttpPost("UpdateEvent")]
        public ActionResult UpdateEvent(CalendarEventDto calendarEvent)
        {
            StringValues tokens;
            Request.Headers.TryGetValue("access_token", out tokens);
            string token = tokens.ToString();

            CalendarEventDto newCalendarEvent = new CalendarEventDto();
            newCalendarEvent.Id = calendarEvent.Id;
            newCalendarEvent.Subject = calendarEvent.Subject;
            newCalendarEvent.Body.Content = calendarEvent.Body.Content;
            newCalendarEvent.Start.DateTime = calendarEvent.Start.DateTime;
            newCalendarEvent.End.DateTime = calendarEvent.End.DateTime;

            _iOauthManager.UpdateEvent(newCalendarEvent, token);
            return Ok();
        }

        [HttpPost("DeleteEvent/{eventId}")]
        public ActionResult DeleteEvent(string eventId)
        {
            StringValues tokens;
            Request.Headers.TryGetValue("access_token", out tokens);
            string token = tokens.ToString();

            _iOauthManager.DeleteEvent(eventId, token);
            return Ok();
        }
    }
}
