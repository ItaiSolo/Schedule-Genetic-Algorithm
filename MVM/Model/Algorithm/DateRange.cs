using System;

/*
 * This class represents a date range in the system that has a start date, end date and an ID.
 */
public class DateRange
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
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
        string startTime = Start.ToString("HH:mm"); // Start time in HH:mm format
        string endTime = End.ToString("HH:mm"); // End time in HH:mm format

        return $"{dayOfWeek} {startTime} - {endTime}";
    }
}
