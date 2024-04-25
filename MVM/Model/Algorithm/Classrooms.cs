/*
 * 
 */

public class Classrooms
{
    public string RoomId { get; private set; }
    public int SeatingCapacity { get; private set; }

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