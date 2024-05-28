using System;

public class MainProgram
{
    //initialize constraints/ constants
    public const int POPULATION_SIZE = 200;
    public const double MUTATION_RATE = 0.2;
    public const double CROSSOVER_RATE = 0.9;
    public const int TOURNAMENT_SELECTION_SIZE = 3;
    public const int NUM_OF_ELITE_SCHEDULES = 2;
    public const int MaxIterations = 5000;
    public const double BestFitness = 1;
    public const int BestConflicts = 0;
    public Random mainRand; //do not make it static it makes the algorithm worse
    public int classNum = 1;
    public static Data data;
    public static bool hasDataChanged = false;//if the data from the last run have not changed I can that the best population and start from there!!
    private Population savedPopulation = null;

    //Run the genetic algorithm according to the data,constraints and number of iterations
    //returns a list of schedule results
    public MyList<ScheduleResult> MainRun()
    {
        hasDataChanged = false;
        mainRand = new Random();

        MyList<ScheduleResult> scheduleResultList = new MyList<ScheduleResult>();

        int generationNumber = 0;
        var geneticAlgorithm = new GeneticAlgorithm(mainRand);
        Population population = null;
        //if (!hasDataChanged && savedPopulation != null)
        //    population = savedPopulation;
        //else
        population = new Population(POPULATION_SIZE, mainRand);// Initialize population with random schedules

        classNum = 1;
        if (population.Schedules.GetAt(0) == null) return null;

        // Initialize fitness and conflict values
        double currentBestFitness = population.Schedules.GetAt(0).Fitness;
        int currentBestConflicts = population.Schedules.GetAt(0).NumbOfConflicts;

        while (currentBestFitness != BestFitness && generationNumber < MaxIterations - 1 && currentBestConflicts > BestConflicts)
        {
            population = geneticAlgorithm.EvolvePopulation(population);
            population.SortSchedulesByFitness(); // Sort only after evolution

            currentBestFitness = population.Schedules.GetAt(0).Fitness;
            currentBestConflicts = population.Schedules.GetAt(0).NumbOfConflicts;
            generationNumber++;
        }

        if (generationNumber == MaxIterations - 1)
        {
            population = geneticAlgorithm.EvolvePopulation(population);
            population.SortSchedulesByFitnessLast(); // Sort only after evolution

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
                currentBestFitness,
                (generationNumber),
                currentBestConflicts
            ));
        }
        savedPopulation = population.Copy();
        scheduleResultList.GetAt(0).SetErrors(population.Schedules.GetAt(0).ConflictsList.Copy());

        return scheduleResultList;
    }

    public static void SetDataChanged()
    {
        hasDataChanged = true;
    }
}
