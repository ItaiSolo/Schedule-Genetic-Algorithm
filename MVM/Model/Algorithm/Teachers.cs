public class Teachers
{
    public string Id { get; set; }
    public string Name { get; set; }
    private MyList<DateRange> MeetingTimesPerTeacher;// gets data prom page2

    public Teachers(string id, string name, MyList<DateRange> meetingTimesPerTeacher)
    {
        this.Id = id;
        this.Name = name;
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