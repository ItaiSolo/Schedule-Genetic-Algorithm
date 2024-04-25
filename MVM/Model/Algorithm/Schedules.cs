using System;
using System.Text;

public class Schedules
{
    private MyList<TimeCube> classes;// a list of all the time cubes in the schedule
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

    //get fitness and if it was changed calls CalculateFitness
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

    //calculate fitness of schedule accurding to number of conflicts
    private double CalculateFitness()
    {
        numbOfConflicts = 0;

        for (int i = 0; i < classes.Size; i++)
        {
            timeCubeX = classes.GetAt(i);
            if (timeCubeX.Room.SeatingCapacity < timeCubeX.Course.GetMaxNumbOfStudents())
            {
                numbOfConflicts++;
            }

            if (!timeCubeX.Instructor.GetMeetingTimesPerTeacher().Contains(timeCubeX.MeetingTime))
            {
                numbOfConflicts++;
            }
            for (int j = i; j < classes.Size; j++)
            {
                CheckMeetingTimesConflicts(timeCubeX, classes.GetAt(j));
            }
        }
        return (1 / (double)(numbOfConflicts + 1));
    }

    //Only called on last generation in order to include conflicts strings
    public double AddLastGenerationConflicts()
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
                TimeCube y = classes.GetAt(j);
                if (timeCubeX.MeetingTime == y.MeetingTime && timeCubeX.Id != y.Id)
                {
                    if (timeCubeX.Room == y.Room)
                    {
                        numbOfConflicts++;
                        ConflictsList.Add("2 Courses[" + timeCubeX.Course.GetName() + "," + y.Course.GetName() + "] At The Same Room " + timeCubeX.Room.RoomId + " And Time " +
                        timeCubeX.MeetingTime.ToString());
                    }
                    if (timeCubeX.Instructor == y.Instructor)
                    {
                        numbOfConflicts++;
                        ConflictsList.Add("2 Courses[" + timeCubeX.Course.GetName() + "," + y.Course.GetName() + "] With The Same Instructor " +
                        timeCubeX.Instructor.ToString() + " And Time " + timeCubeX.MeetingTime.ToString());
                    }
                }
            }
        }
        return (1 / (double)(numbOfConflicts + 1));
    }

    //helper function
    private void CheckMeetingTimesConflicts(TimeCube x, TimeCube y)
    {
        if (x.MeetingTime == y.MeetingTime && x.Id != y.Id)
        {
            if (x.Room == y.Room)
            {
                numbOfConflicts++;
            }
            if (x.Instructor == y.Instructor)
            {
                numbOfConflicts++;
            }
        }
    }

    //init the schedule with random classes for each course
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