

using WebApp.Domain.Entities;
using WebApp.Domain.Enums;

namespace WebApp.Domain.DTOs
{
    public class AvaibilityDto
    {
        public DiasDaSemana DAY { get;}
        public TimeSpan BEGIN_TIME { get; }
        public TimeSpan END_TIME { get; }

        internal AvaibilityDto() { }

        internal AvaibilityDto(DiasDaSemana dia, TimeSpan bTime, TimeSpan eTime)
        {
            DAY = dia;
            BEGIN_TIME = bTime;
            END_TIME = eTime;
        }

        internal AvaibilityDto(ScheduleAvaibility ava)
        {
            DAY = ava.day;
            BEGIN_TIME = ava.begin_time;
            END_TIME = ava.end_time;
        }
    }
}