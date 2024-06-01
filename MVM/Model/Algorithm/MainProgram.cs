using System;

public class MainProgram
{
    //initialize constraints/ constants
    public const int POPULATION_SIZE = 250;
    public const double MUTATION_RATE = 0.2;
    public const double CROSSOVER_RATE = 0.9;
    public const int TOURNAMENT_SELECTION_SIZE = 4;
    public const int NUM_OF_ELITE_SCHEDULES = 5;
    public const int MaxIterations = 8000;
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
        mainRand = new Random();
        MyList<ScheduleResult> scheduleResultList = new MyList<ScheduleResult>();

        int generationNumber = 0;
        var geneticAlgorithm = new GeneticAlgorithm(mainRand);
        Population population = null;
        
        population = new Population(POPULATION_SIZE, mainRand);// Initialize population with random schedules
        population.SortSchedulesByFitness();
        if (!hasDataChanged && savedPopulation != null)
        { // takes the 2 best schedules from the last run to replace the worst schedules in this run
            for (int i = 0; i < 2; i++)
            {
                population.Schedules.SetAt(savedPopulation.Schedules.GetAt(i), population.Schedules.Size - i - 1);
            }
        }
        population.SortSchedulesByFitness();
        classNum = 1;
        if (population.Schedules.GetAt(0) == null) return null;

        // Initialize fitness and conflict values
        double currentBestFitness = population.Schedules.GetAt(0).Fitness;
        int currentBestConflicts = population.Schedules.GetAt(0).NumbOfConflicts;

        while (currentBestFitness != BestFitness && generationNumber < MaxIterations - 1 && currentBestConflicts > BestConflicts)
        {
            population = geneticAlgorithm.EvolvePopulation(population); // Evolve sorted population
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
        savedPopulation = new Population(population.Schedules);
        scheduleResultList.GetAt(0).SetErrors(population.Schedules.GetAt(0).ConflictsList.Copy());
        hasDataChanged = false;
        return scheduleResultList;
    }

    public static void SetDataChanged()
    {
        hasDataChanged = true;
    }
}
