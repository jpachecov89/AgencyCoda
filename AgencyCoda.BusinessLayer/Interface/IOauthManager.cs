using AgencyCoda.BusinessLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyCoda.BusinessLayer.Interface
{
    public interface IOauthManager
    {
        string RedirectUrl(string scope, string redirect_url, string client_id);
        string Callback(string client_id, string scope, string redirect_url, string code, string client_secret);
        IEnumerable<CalendarEventDto> GetAllEvents(string access_token);
        CalendarEventDto GetEvent(string eventId, string access_token);
        void CreateEvent(CalendarEventDto calendarEvent, string access_token);
        void UpdateEvent(CalendarEventDto calendarEvent, string access_token);
        void DeleteEvent(string eventId, string access_token);
    }
}
