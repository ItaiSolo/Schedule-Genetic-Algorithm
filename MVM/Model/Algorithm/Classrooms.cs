/*
 * This class holds the classroom information like room id and seating capacity
 */

public class Classrooms
{
    public string RoomId { get; set; }
    public int SeatingCapacity { get; set; }

    public Classrooms(string number, int seatingCapacity)
    {
        RoomId = number;
        SeatingCapacity = seatingCapacity;
    }

    public override string ToString()
    {
        return $"[{RoomId}, {SeatingCapacity}]";
    }
}