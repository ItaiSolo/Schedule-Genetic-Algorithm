using System;

    public class DateRange
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Id = 0;

        public DateRange(DateTime start, DateTime end,int id)
        {
            Start = start;
            End = end;
            Id = id;
        }

        public override string ToString()
        {

            string dayOfWeek = Start.DayOfWeek.ToString();
            string startTime = Start.ToString("HH:mm"); // Start time in HH:mm format
            string endTime = End.ToString("HH:mm"); // End time in HH:mm format

            return $"{dayOfWeek} {startTime} - {endTime}";
        }
    
}
