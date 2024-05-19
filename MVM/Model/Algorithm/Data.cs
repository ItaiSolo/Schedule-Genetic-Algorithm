using System;
using WpfApp;

/*
 * This class represents the data in the system, that is used by the algorithms and shown in the UI.
 */

public class Data
{
    // All have to be Public for the Deserialization to was on loading a schedule!
    public MyList<Classrooms> Rooms { get; set; }
    public MyList<Teachers> Instructors { get; set; } // Instructor == Teacher
    public MyList<Teachers> InstructorsPerCourse { get; set; }
    public MyList<DateRange> MeetingTimesPerTeacher { get; set; }
    public MyList<Courses> Courses1 { get;  set; }
    public MyList<DateRange> MeetingTimes { get; set; }


    public Data()
    {
        Initialize();
    }

    public void Initialize()
    {
        // Initialize rooms

        Rooms = new MyList<Classrooms>();
        MeetingTimes = new MyList<DateRange>();
        Instructors = new MyList<Teachers>();
        InstructorsPerCourse = new MyList<Teachers>();
        MeetingTimesPerTeacher = new MyList<DateRange>();
        Courses1 = new MyList<Courses>();
    }

    public void AddDefaultData()
    {
        var a = new Classrooms("R101", 25);
        Rooms.Add(a);
        Rooms.Add(new Classrooms("R102", 45));
        Rooms.Add(new Classrooms("R103", 35));
        Rooms.Add(new Classrooms("R104", 60));

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

        var range5StartDay = 14;
        var range5StartHour = 8;
        var range5EndDay = 14;
        var range5EndHour = 16;

        var today = DateTime.Today;

        var range1Start = new DateTime(today.Year, today.Month, range1StartDay, range1StartHour, 0, 0);
        var range1End = new DateTime(today.Year, today.Month, range1EndDay, range1EndHour, 0, 0);

        var range2Start = new DateTime(today.Year, today.Month, range2StartDay, range2StartHour, 0, 0);
        var range2End = new DateTime(today.Year, today.Month, range2EndDay, range2EndHour, 0, 0);

        var range3Start = new DateTime(today.Year, today.Month, range3StartDay, range3StartHour, 0, 0);
        var range3End = new DateTime(today.Year, today.Month, range3EndDay, range3EndHour, 30, 0);

        var range4Start = new DateTime(today.Year, today.Month, range4StartDay, range4StartHour, 30, 0);
        var range4End = new DateTime(today.Year, today.Month, range4EndDay, range4EndHour, 0, 0);

        var range5Start = new DateTime(today.Year, today.Month, range5StartDay, range5StartHour, 30, 0);
        var range5End = new DateTime(today.Year, today.Month, range5EndDay, range5EndHour, 0, 0);

        // Initialize meeting times
        // שעות הלימוד האפשריות
        MeetingTimes.Add(new DateRange(range1Start, range1End, 1));      //  09:00 - 10:00
        MeetingTimes.Add(new DateRange(range2Start, range2End, 2));     //  10:00 - 11:00
        MeetingTimes.Add(new DateRange(range3Start, range3End, 3));    //  09:00 - 10:30
        MeetingTimes.Add(new DateRange(range4Start, range4End, 4));   //  10:30 - 12:00
        MeetingTimes.Add(new DateRange(range5Start, range5End, 5));  //  8:30 - 16:00

        // Initialize instructors
        Instructors.Add(new Teachers("I1", "Dr. James Web", new MyList <DateRange> { MeetingTimes.GetAt(4) }));
        Instructors.Add(new Teachers("I2", "Mr. Mike Brown", MeetingTimes));
        Instructors.Add(new Teachers("I3", "Mr. Steve Day", MeetingTimes));
        Instructors.Add(new Teachers("I4", "Mrs. Jane Doe", MeetingTimes));
        Instructors.Add(new Teachers("I5", "Dr. Reuven Babai", MeetingTimes));
        Instructors.Add(new Teachers("I6", "Dr. Yoram Bar", MeetingTimes));
        Instructors.Add(new Teachers("I7", "Prof. Eran Bacharach", MeetingTimes));
        Instructors.Add(new Teachers("I8", "Dr. Gil Bachar", MeetingTimes));

        // Initialize courses
        Courses1.Add(new Courses("1","Agriculture and Veterinary Medicine", new MyList<Teachers> { Instructors.GetAt(0), Instructors.GetAt(1), Instructors.GetAt(2) }, 80));
        Courses1.Add(new Courses("2","Philosophy and Theology", new MyList<Teachers> { Instructors.GetAt(0), Instructors.GetAt(2), Instructors.GetAt(7) }, 10));
        Courses1.Add(new Courses("3","Philosophy and Theology", new MyList<Teachers> { Instructors.GetAt(3), Instructors.GetAt(2) }, 30));
        Courses1.Add(new Courses("4","Modern Languages", new MyList<Teachers> { Instructors.GetAt(6) }, 35));
        Courses1.Add(new Courses("5","Mathematics and Statistics", new MyList<Teachers> { Instructors.GetAt(7), Instructors.GetAt(5) }, 45));
        Courses1.Add(new Courses("6","History and Economics", new MyList<Teachers> { Instructors.GetAt(1), Instructors.GetAt(3),Instructors.GetAt(6) }, 50));

        Courses1.Add(new Courses("7","History and English", new MyList<Teachers> { Instructors.GetAt(3), Instructors.GetAt(2) }, 30));
        Courses1.Add(new Courses("8","Computer Science", new MyList<Teachers> { Instructors.GetAt(4) }, 35));
        Courses1.Add(new Courses("9","Fine Art", new MyList<Teachers> { Instructors.GetAt(0), Instructors.GetAt(2) }, 20));
        Courses1.Add(new Courses("10","Medicine", new MyList<Teachers> { Instructors.GetAt(1), Instructors.GetAt(3) }, 45));

        Courses1.Add(new Courses("11","Materials Science", new MyList<Teachers> { Instructors.GetAt(3), Instructors.GetAt(2) }, 30));
        Courses1.Add(new Courses("12","Chemistry", new MyList<Teachers> { Instructors.GetAt(3) }, 35));
        Courses1.Add(new Courses("13","Biology", new MyList<Teachers> { Instructors.GetAt(0), Instructors.GetAt(2) }, 45));
        Courses1.Add(new Courses("14","Biomedical Sciences", new MyList<Teachers> { Instructors.GetAt(1), Instructors.GetAt(3) }, 45));
        Courses1.Add(new Courses("15","Asian and Middle Eastern Studies", Instructors, 60));
        Courses1.Add(new Courses("16", "Engineering", new MyList<Teachers> { Instructors.GetAt(0), Instructors.GetAt(1), Instructors.GetAt(7) }, 25));

        MainWindow.showData.UpdateData(this);
    }
}