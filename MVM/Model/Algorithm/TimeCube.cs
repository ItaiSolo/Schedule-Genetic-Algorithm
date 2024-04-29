/*
 * 
 * This class is used to represent one time cube slot in a schedule.
 * it contains Id, Course, Instructor, MeetingTime, Room.
 */
public class TimeCube
{
    public int Id { get; private set; }
    public Courses Course { get; private set; }
    public Teachers Instructor { get; private set; }
    public DateRange MeetingTime { get; private set; }


    public Classrooms Room { get; private set; }

    public TimeCube(int id, Courses course)
    {
        Id = id;
        Course = course;
    }

    public void SetInstructor(Teachers instructor) => Instructor = instructor;
    public void SetMeetingTime(DateRange meetingTime) => MeetingTime = meetingTime;
    public void SetRoom(Classrooms room) => Room = room;


    public override string ToString()
    {
        return $"[{Course.GetNumber()}, {Room.RoomId}, {Instructor.Id}, {MeetingTime.Id}]";
    }
}
