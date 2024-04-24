using System;

public class MainProgram
{
    //initialize data
    public const int POPULATION_SIZE = 120;
    public const double MUTATION_RATE = 0.2;
    public const double CROSSOVER_RATE = 0.9;//
    public const int TOURNAMENT_SELECTION_SIZE = 5;
    public const int NUM_OF_ELITE_SCHEDULES = 2;
    public const int MaxIteretions = 1100;
    public const double BestFitness = 1;
    public const int BestConflicts = 0;
    public Random mainRand;
    public static int scheduleNumb = 0;
    public int classNum = 1;
    public Data data;

    public MyList<ScheduleResult> MainRun(MainProgram Program)
    {
        if (Program == null) return null;
        mainRand = new Random();

        MyList<ScheduleResult> scheduleResultList = new MyList<ScheduleResult>();

        int generationNumber = 0;
        var geneticAlgorithm = new GeneticAlgorithm(Program.data, mainRand);
        var population = new Population(POPULATION_SIZE, Program.data, mainRand);

        Program.classNum = 1;
        if (population.Schedules.GetAt(0) == null) return null;

        // Initialize fitness and conflict values
        double currentBestFitness = population.Schedules.GetAt(0).Fitness;
        int currentBestConflicts = population.Schedules.GetAt(0).NumbOfConflicts;

        while (currentBestFitness != BestFitness && generationNumber < MaxIteretions && currentBestConflicts > BestConflicts)
        {
            population = geneticAlgorithm.EvolvePopulation(population);
            population.SortSchedulesByFitness(); // Sort only after evolution

            currentBestFitness = population.Schedules.GetAt(0).Fitness;
            currentBestConflicts = population.Schedules.GetAt(0).NumbOfConflicts;
            generationNumber++;

        }

        // Extract and record class details
        foreach (var x in population.Schedules.GetAt(0).Classes)
        {
            scheduleResultList.Add(new ScheduleResult(
                classNum++,
                x.Course.GetName(),
                x.Room.RoomId,
                x.Instructor.Name,
                x.MeetingTime.ToString(),
                population.Schedules.GetAt(0).Fitness,
                (generationNumber + 1),
                population.Schedules.GetAt(0).NumbOfConflicts
            ));
        }
        scheduleResultList.GetAt(0).SetErrors(population.Schedules.GetAt(0).ConflictsList.Copy());

        return scheduleResultList;
    }
}
