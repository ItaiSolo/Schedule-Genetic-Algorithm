//This class holds the course thought by each teacher. it contains information like course id and course name
public class Courses
{
    public string number = null;
    public string name = null;
    public int maxNumberOfStudents { get; set; }
    private MyList<Teachers> instructors;
    private static int counter = 0;


    public Courses(string number, string name, MyList<Teachers> instructors, int maxNumbOfStudents)
    {
        this.number = number;
        this.name = name;
        this.instructors = instructors;
        this.maxNumberOfStudents = maxNumbOfStudents;
        counter++;
    }

    public static int GetCounter() { return counter; }
    public string GetNumber() { return number; }
    public string GetName() { return name; }
    public MyList<Teachers> GetInstructors() { return instructors; }
    public int GetMaxNumbOfStudents() { return maxNumberOfStudents; }
    public override string ToString() { return name; }
}
