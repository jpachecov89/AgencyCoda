using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgencyCoda.BusinessLayer.Dtos
{
    public class CalendarEventDto
    {
        public CalendarEventDto()
        {
            this.Body = new Body()
            {
                ContentType = "html"
            };
            this.Start = new EventDateTime()
            {
                TimeZone = "America/Bogota"
            };
            this.End = new EventDateTime()
            {
                TimeZone = "America/Bogota"
            };
        }

        public string Id { get; set; }
        public string Subject { get; set; }
        public Body Body { get; set; }
        public EventDateTime Start { get; set; }
        public EventDateTime End { get; set; }
    }

    public class Body
    {
        public string ContentType { get; set; }
        public string Content { get; set; }
    }

    public class EventDateTime
    {
        public DateTime DateTime { get; set; }
        public string TimeZone { get; set; }
    }
}
