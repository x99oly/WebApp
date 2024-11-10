using WebApp.Domain.DTOs;
using WebApp.Domain.Enums;

namespace WebApp.Domain.Entities
{
    internal class ScheduleAvaibility
    {
        public string cod;
        public DiasDaSemana day;
        public TimeSpan begin_time;
        public TimeSpan end_time;

        internal ScheduleAvaibility(string cod, DiasDaSemana dia, TimeSpan bTime, TimeSpan eTime)
        {
            this.cod = cod;
            day = dia;
            begin_time = bTime;
            end_time = eTime;
        }

        internal ScheduleAvaibility(Pc pc)
        {
            cod = pc.cod;
        }

        public void ConvertFrom(AvaibilityDto input)
        {
            day = input.DAY;
            begin_time = input.BEGIN_TIME;
            end_time = input.END_TIME;
        }

        public AvaibilityDto ConvertTo()
        {
            return new AvaibilityDto(this);
        }
    }
}
