/*
 * This class represents a teacher in the system that has a name,ID and a list of available meeting times.
 */
public class Teachers
{
    public string Id { get; set; }
    public string Name { get; set; }
    private MyList<DateRange> MeetingTimesPerTeacher;// gets data prom page2
    public MyList<string> MeetingTimesPerTeacherString;

    public Teachers(string id, string name, MyList<DateRange> meetingTimesPerTeacher)
    {
        Id = id;
        Name = name;
        MeetingTimesPerTeacher = meetingTimesPerTeacher;
    }

    public MyList<DateRange> GetMeetingTimesPerTeacher()
    {
        return MeetingTimesPerTeacher;
    }

    public override string ToString()
    {
        return Name;
    }

}