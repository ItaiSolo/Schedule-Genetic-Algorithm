using System;

/*
 * This class represents a date range in the system that has a start date, end date and an ID.
 */
public class DateRange
{
    public DateTime Start { get; set; } // Start date in DateTime format
    public DateTime End { get; set; } // End date in DateTime format
    public int Id = 0;

    public DateRange(DateTime start, DateTime end, int id)
    {
        Start = start;
        End = end;
        Id = id;
    }

    // Returns a string representation of the date range in the format "DayOfWeek StartTime - EndTime"
    public override string ToString()
    {
        string dayOfWeek = Start.DayOfWeek.ToString();
        string startTime = Start.ToString("HH:mm"); // Start time in HH:mm format -> example: 09:00 hours in 24 hour format and then minutes
        string endTime = End.ToString("HH:mm"); // End time in HH:mm format

        return $"{dayOfWeek} {startTime} - {endTime}";
    }
}
