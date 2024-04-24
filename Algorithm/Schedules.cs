using System;
using System.Text;

public class Schedules
{
    private MyList<TimeCube> classes;
    private bool isFitnessChanged = true;
    private double fitness = -1;
    private int classNumb = 0;
    private int numbOfConflicts = 0;
    private Data data;
    public MyList<string> ConflictsList = new MyList<string>();
    //temp variables
    public TimeCube timeCubeX;
    public Courses courseTemp;

    public Schedules(Data data)
    {
        this.data = data;
        classes = new MyList<TimeCube>(Courses.GetCounter());
    }

    public MyList<TimeCube> Classes
    {
        get
        {
            isFitnessChanged = true;
            return classes;
        }
    }

    public double Fitness
    {
        get
        {
            if (isFitnessChanged)
            {
                fitness = CalculateFitness();
                isFitnessChanged = false;
            }
            return fitness;
        }
    }

    public int NumbOfConflicts => numbOfConflicts;

    public bool IsFitnessChanged
    {
        get
        {
            return isFitnessChanged;
        }
    }
    //need to be refactored and made more efficient
    private double CalculateFitness()
    {
        ConflictsList.Clear();
        numbOfConflicts = 0;

        for (int i = 0; i < classes.Size; i++)
        {
            timeCubeX = classes.GetAt(i);
            if (timeCubeX.Room.SeatingCapacity < timeCubeX.Course.GetMaxNumbOfStudents())
            {
                numbOfConflicts++;
                ConflictsList.Add("Room " + timeCubeX.Room.RoomId + " Seating Capacity: " + timeCubeX.Room.SeatingCapacity
                  + " But The Course " + timeCubeX.Course.GetName() + " Number Of Students: " + timeCubeX.Course.GetMaxNumbOfStudents().ToString());
            }

            if (!timeCubeX.Instructor.GetMeetingTimesPerTeacher().Contains(timeCubeX.MeetingTime))
            {
                numbOfConflicts++;
                ConflictsList.Add("Teacher " + timeCubeX.Instructor.ToString() + "Is Not Available At Meeting Time: " + timeCubeX.MeetingTime.ToString());
            }
            for (int j = i; j < classes.Size; j++)
            {
                CheckMeetingTimesConflicts(timeCubeX, classes.GetAt(j));
            }
        }
        return (1 / (double)(numbOfConflicts + 1));
    }

    private void CheckMeetingTimesConflicts(TimeCube x, TimeCube y)
    {
        if (x.MeetingTime == y.MeetingTime && x.Id != y.Id)
        {
            if (x.Room == y.Room)
            {
                numbOfConflicts++;
                ConflictsList.Add("2 Courses[" + x.Course.GetName() + "," + y.Course.GetName() + "] At The Same Room " + x.Room.RoomId + " And Time " +
                   x.MeetingTime.ToString());
            }
            if (x.Instructor == y.Instructor)
            {
                numbOfConflicts++;
                ConflictsList.Add("2 Courses[" + x.Course.GetName() + "," + y.Course.GetName() + "] With The Same Instructor " +
                    x.Instructor.ToString() + " And Time " + x.MeetingTime.ToString());
            }
        }
    }

    //במקום מחלקה רשימה אחת שתחזיק את כל הכיתות
    //מאחל את האוכלוסייה
    public Schedules Initialize(Random rand)
    {
        try
        {
            for (int i = 0; i < data.Courses1.Size; i++)
            {
                courseTemp = data.Courses1.GetAt(i);//passess every course
                var newClass = new TimeCube(classNumb++, courseTemp);
                newClass.SetMeetingTime(data.MeetingTimes.GetAt((int)(data.MeetingTimes.Size * rand.NextDouble())));
                newClass.SetRoom(data.Rooms.GetAt((int)(data.Rooms.Size * rand.NextDouble())));
                newClass.SetInstructor(courseTemp.GetInstructors().GetAt((int)(courseTemp.GetInstructors().Size * rand.NextDouble())));
                classes.Add(newClass);
            }
        }
        catch
        {
            return null;
        }
        return this;
    }


    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < classes.Size; i++)
        {
            sb.Append(classes.GetAt(i).ToString());
            if (i < classes.Size - 1)
            {
                sb.Append(", ");
            }
        }
        return sb.ToString();
    }
}