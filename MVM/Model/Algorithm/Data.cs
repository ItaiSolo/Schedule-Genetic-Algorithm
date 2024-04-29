using System;

/*
 * This class represents the data in the system, that is used by the algorithms and shown in the UI.
 */

public class Data
{
    public MyList<Classrooms> Rooms { get;  set; }
    public MyList<Teachers> Instructors { get;  set; } // Instructor == Teacher
    public MyList<Teachers> InstructorsPerCourse { get;  set; } //temp
    public MyList<DateRange> MeetingTimesPerTeacher { get; set; }//temp
    public MyList<Courses> Courses1 { get; set; }
    public MyList<DateRange> MeetingTimes { get; set; }


    public Data()
    {
        Initialize();
    }

    public Data Initialize()
    {
        // Initialize rooms
        Rooms = new MyList<Classrooms>();
        MeetingTimes = new MyList<DateRange>();
        Instructors = new MyList<Teachers>();
        InstructorsPerCourse = new MyList<Teachers>();
        MeetingTimesPerTeacher = new MyList<DateRange>();
        Courses1 = new MyList<Courses>();
        return this;
    }

    public void AddTempData()
    {
        //delete all later -----------------------------------
        //all from here is not needed
        var a = new Classrooms("R1", 25);
        Rooms.Add(a);
        //Rooms.Add(new Classrooms("R2", 45));
        Rooms.Add(new Classrooms("R3", 35));

        var range1StartDay = 15;
        var range1StartHour = 9;
        var range1EndDay = 15;
        var range1EndHour = 10;

        var range2StartDay = 15;
        var range2StartHour = 10;
        var range2EndDay = 15;
        var range2EndHour = 11;

        var range3StartDay = 16;
        var range3StartHour = 9;
        var range3EndDay = 16;
        var range3EndHour = 10;


        var range4StartDay = 16;
        var range4StartHour = 10;
        var range4EndDay = 16;
        var range4EndHour = 12;

        var today = DateTime.Today;

        var range1Start = new DateTime(today.Year, today.Month, range1StartDay, range1StartHour, 0, 0);
        var range1End = new DateTime(today.Year, today.Month, range1EndDay, range1EndHour, 0, 0);

        var range2Start = new DateTime(today.Year, today.Month, range2StartDay, range2StartHour, 0, 0);
        var range2End = new DateTime(today.Year, today.Month, range2EndDay, range2EndHour, 0, 0);

        var range3Start = new DateTime(today.Year, today.Month, range3StartDay, range3StartHour, 0, 0);
        var range3End = new DateTime(today.Year, today.Month, range3EndDay, range3EndHour, 30, 0);

        var range4Start = new DateTime(today.Year, today.Month, range4StartDay, range4StartHour, 30, 0);
        var range4End = new DateTime(today.Year, today.Month, range4EndDay, range4EndHour, 0, 0);


        // Initialize meeting times
        //שעות הלימוד האפשריות
        MeetingTimes.Add(new DateRange(range1Start, range1End, 1));      // "MWF 09:00 - 10:00"));
        MeetingTimes.Add(new DateRange(range2Start, range2End, 2));     // "MWF 10:00 - 11:00"));
        MeetingTimes.Add(new DateRange(range3Start, range3End, 3));    // "TTH 09:00 - 10:30"));
        MeetingTimes.Add(new DateRange(range4Start, range4End, 4));   // "TTH 10:30 - 12:00"));

        // Initialize instructors
        Instructors.Add(new Teachers("I1", "Dr. James Web", MeetingTimes));
        Instructors.Add(new Teachers("I2", "Mr. Mike Brown", MeetingTimes));
        Instructors.Add(new Teachers("I3", "Mr. Steve Day", MeetingTimes));
        Instructors.Add(new Teachers("I4", "Mrs. Jane Doe", MeetingTimes));
        Instructors.Add(new Teachers("I1", "Dr. James Web", MeetingTimes));
        Instructors.Add(new Teachers("I2", "Mr. Mike Brown", MeetingTimes));
        Instructors.Add(new Teachers("I3", "Mr. Steve Day", MeetingTimes));
        Instructors.Add(new Teachers("I4", "Mrs. Jane Doe", MeetingTimes));

        // Initialize courses

        Courses1.Add(new Courses("C1", "325K", new MyList<Teachers> { Instructors.GetAt(0), Instructors.GetAt(1) }, 25));

        Courses1.Add(new Courses("C2", "319K", new MyList<Teachers> { Instructors.GetAt(0), Instructors.GetAt(1), Instructors.GetAt(2) }, 35));
        Courses1.Add(new Courses("C3", "462K", new MyList<Teachers> { Instructors.GetAt(0), Instructors.GetAt(2), Instructors.GetAt(3) }, 30));
        Courses1.Add(new Courses("C4", "464K", new MyList<Teachers> { Instructors.GetAt(3), Instructors.GetAt(2) }, 30));
        Courses1.Add(new Courses("C5", "360C", new MyList<Teachers> { Instructors.GetAt(3) }, 35));
        Courses1.Add(new Courses("C6", "303K", new MyList<Teachers> { Instructors.GetAt(0), Instructors.GetAt(2) }, 45));
        Courses1.Add(new Courses("C7", "303L", new MyList<Teachers> { Instructors.GetAt(1), Instructors.GetAt(3) }, 45));

        Courses1.Add(new Courses("C4", "464K", new MyList<Teachers> { Instructors.GetAt(3), Instructors.GetAt(2) }, 30));
        Courses1.Add(new Courses("C5", "360C", new MyList<Teachers> { Instructors.GetAt(3) }, 35));
        Courses1.Add(new Courses("C6", "303K", new MyList<Teachers> { Instructors.GetAt(0), Instructors.GetAt(2) }, 45));
        Courses1.Add(new Courses("C7", "303L", new MyList<Teachers> { Instructors.GetAt(1), Instructors.GetAt(3) }, 45));


        Courses1.Add(new Courses("C4", "464K", new MyList<Teachers> { Instructors.GetAt(3), Instructors.GetAt(2) }, 30));
        Courses1.Add(new Courses("C5", "360C", new MyList<Teachers> { Instructors.GetAt(3) }, 35));
        Courses1.Add(new Courses("C6", "303K", new MyList<Teachers> { Instructors.GetAt(0), Instructors.GetAt(2) }, 45));
        Courses1.Add(new Courses("C7", "303L", new MyList<Teachers> { Instructors.GetAt(1), Instructors.GetAt(3) }, 45));
    }
}
