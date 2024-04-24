using System;

    public class ScheduleResult
        //need to show corrent data
    {
        public  int ClassNumber { get; set; }
        public  string Courses { get; set; }
        public  string Rooms { get; set; }
        public  string Instructors { get; set; }
        public  string MeetingTimes { get; set; }
        public double Fitness { get; set; }
        public int Generations { get; set; } 
        public MyList<string> Errors { get; set; }
        public int Conflicts { get; set; }

        public ScheduleResult(int classNumber, string courses, string rooms, 
            string instructors, string meetingTimes, double fitness, int generations, int conflicts)
        {
            ClassNumber = classNumber;
            Courses = courses;
            Rooms = rooms;
            Instructors = instructors;
            MeetingTimes = meetingTimes;
            Fitness = fitness;
            Generations = generations;
            Conflicts = conflicts;
        }
        public void SetErrors(MyList<string> errors)
        {
            Errors = errors;
        }
    }
