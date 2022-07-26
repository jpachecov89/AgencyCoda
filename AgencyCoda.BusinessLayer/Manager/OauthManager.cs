using AgencyCoda.BusinessLayer.Dtos;
using AgencyCoda.BusinessLayer.Interface;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AgencyCoda.BusinessLayer.Manager
{
    public class OauthManager : IOauthManager
    {
        public string RedirectUrl(string scope, string redirect_url, string client_id)
        {
            string redirectUrl = "https://login.microsoftonline.com/common/oauth2/v2.0/authorize?" +
                                 "&scope=" + scope +
                                 "&response_type=code" +
                                 "&response_mode=query" +
                                 "&state=themessydeveloper" +
                                 "&redirect_uri=" + redirect_url +
                                 "&client_id=" + client_id;
            return redirectUrl;
        }

        public string Callback(string client_id, string scope, string redirect_url, string code, string client_secret)
        {
            RestClient restClient = new RestClient("https://login.microsoftonline.com/common/oauth2/v2.0/token");
            RestRequest restRequest = new RestRequest();

            restRequest.AddParameter("client_id", client_id);
            restRequest.AddParameter("scope", scope);
            restRequest.AddParameter("redirect_uri", redirect_url);
            restRequest.AddParameter("code", code);
            restRequest.AddParameter("grant_type", "authorization_code");
            restRequest.AddParameter("client_secret", client_secret);

            var response = restClient.Post(restRequest);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.Content;
            }
            else
            {
                return "_error";
            }
        }

        public IEnumerable<CalendarEventDto> GetAllEvents(string access_token)
        {
            RestClient restClient = new RestClient("https://graph.microsoft.com/v1.0/me/calendar/events");
            RestRequest restRequest = new RestRequest();

            restRequest.AddHeader("Authorization", "Bearer " + access_token);
            restRequest.AddHeader("Prefer", "outlook.timezone=\"SA Pacific Standard Time\"");
            restRequest.AddHeader("Prefer", "outlook.body-content-type=\"text\"");

            var response = restClient.Get(restRequest);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                JObject eventsList = JObject.Parse(response.Content);
                var calendarEvents = eventsList["value"].ToObject<IEnumerable<CalendarEventDto>>();
                return calendarEvents;
            }

            return new List<CalendarEventDto>();
        }

        public CalendarEventDto GetEvent(string eventId, string access_token)
        {
            RestClient restClient = new RestClient($"https://graph.microsoft.com/v1.0/me/calendar/events/{eventId}");
            RestRequest restRequest = new RestRequest();

            restRequest.AddHeader("Authorization", "Bearer " + access_token);
            restRequest.AddHeader("Prefer", "outlook.timezone=\"SA Pacific Standard Time\"");
            restRequest.AddHeader("Prefer", "outlook.body-content-type=\"text\"");

            var response = restClient.Get(restRequest);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var calendarEvent = JObject.Parse(response.Content).ToObject<CalendarEventDto>();
                return calendarEvent;
            }

            return null;
        }

        public void CreateEvent(CalendarEventDto calendarEvent, string access_token)
        {
            RestClient restClient = new RestClient("https://graph.microsoft.com/v1.0/me/calendar/events");
            RestRequest restRequest = new RestRequest();

            restRequest.AddHeader("Authorization", "Bearer " + access_token);
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddParameter("application/json", JsonConvert.SerializeObject(calendarEvent), ParameterType.RequestBody);

            var response = restClient.Post(restRequest);

            if (response.StatusCode == HttpStatusCode.Created)
            {
                
            }
        }

        public void UpdateEvent(CalendarEventDto calendarEvent, string access_token)
        {
            RestClient restClient = new RestClient($"https://graph.microsoft.com/v1.0/me/calendar/events/{calendarEvent.Id}");
            RestRequest restRequest = new RestRequest();

            restRequest.AddHeader("Authorization", "Bearer " + access_token);
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddParameter("application/json", JsonConvert.SerializeObject(calendarEvent), ParameterType.RequestBody);

            var response = restClient.Patch(restRequest);

            if (response.StatusCode == HttpStatusCode.OK)
            {

            }
        }

        public void DeleteEvent(string eventId, string access_token)
        {
            RestClient restClient = new RestClient($"https://graph.microsoft.com/v1.0/me/calendar/events/{eventId}");
            RestRequest restRequest = new RestRequest();

            restRequest.AddHeader("Authorization", "Bearer " + access_token);

            var response = restClient.Delete(restRequest);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {

            }
        }
    }
}
