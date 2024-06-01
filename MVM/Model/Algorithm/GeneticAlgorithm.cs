using System;

/*
 * this class contains the logic for the genetic algorithm
 * includes the crossover SelectTournament and mutation of the population of schedules
 */
public class GeneticAlgorithm
{
    private readonly Random rand;
    private Schedules scheduleParent1;
    private Schedules scheduleParent2;
    private MyList<Schedules> TempSchedules;
    public GeneticAlgorithm(Random mainRand)
    {
        rand = mainRand;
    }

    //calls CrossoverPopulation and mutates the result schedules
    public Population EvolvePopulation(Population population)
    {
        return MutatePopulation(CrossoverPopulation(population));
    }

    //Crossover takes two schedules from the best population and returns one mixed. effected by crossover rate in main program
    public Population CrossoverPopulation(Population population)
    {
        Population crossoverPopulation = new Population(population.Schedules);

        for (int x = MainProgram.NUM_OF_ELITE_SCHEDULES; x < population.Schedules.Size; x++) // it does not change the best schedules - "NUM_OF_ELITE_SCHEDULES"
        {
            if (MainProgram.CROSSOVER_RATE > rand.NextDouble())
            {
                //note: I can add more than 2 parents
                scheduleParent1 = SelectTournamentPopulation(population).Schedules.GetAt(0);
                scheduleParent2 = SelectTournamentPopulation(population).Schedules.GetAt(0);
                crossoverPopulation.Schedules.SetAt(CrossoverSchedule(scheduleParent1, scheduleParent2), x);
            }
        }
        return crossoverPopulation;
    }

    //Crossover per Schedule
    private Schedules CrossoverSchedule(Schedules schedule1, Schedules schedule2)
    {
        Schedules crossoverSchedule = new Schedules();

        for (int x = 0; x < schedule1.Classes.Size; x++) //because schedule1.Classes.Size == schedule2.Classes.Size == crossoverSchedule.Classes.Size
        {
            crossoverSchedule.Classes.Add(rand.NextDouble() > 0.5 ? schedule1.Classes.GetAt(x) : schedule2.Classes.GetAt(x)); //each class have a 50% chance of being chosen from one of the parents
        }
        return crossoverSchedule;
    }

    //Mutate Population of Schedules -> Randomly change one of the schedules. effected by mutation rate in main program
    public Population MutatePopulation(Population population)
    {
        Population mutatePopulation = new Population(population.Schedules);
        TempSchedules = mutatePopulation.Schedules;// TempSchedules points to mutatePopulation.Schedules just to make it easier to read
        for (int x = 0; x < MainProgram.NUM_OF_ELITE_SCHEDULES; x++)// it does not change the best schedules - "NUM_OF_ELITE_SCHEDULES"
        {
            TempSchedules.SetAt(population.Schedules.GetAt(x), x);
        }
        for (int x = MainProgram.NUM_OF_ELITE_SCHEDULES; x < population.Schedules.Size; x++) // it mutates the other schedules by the mutation rate
        {
            TempSchedules.SetAt(MutateSchedule(population.Schedules.GetAt(x)), x);
        }
        return mutatePopulation;
    }

    //Mutate on Schedules -> Randomly change one of the classes
    public Schedules MutateSchedule(Schedules schedule)
    {
        Schedules mutateSchedule = new Schedules(schedule);

        for (int x = 0; x < mutateSchedule.Classes.Size; x++)
        {
            if (MainProgram.MUTATION_RATE > rand.NextDouble())
            {
                mutateSchedule.Classes.SetAt(schedule.Classes.GetAt(x), x);
            }
        }
        return mutateSchedule;
    }

    //Select Tournament Population -> Randomly select schedules from the top of the population
    public Population SelectTournamentPopulation(Population population)
    {
        Population tournamentPopulation = new Population(MainProgram.TOURNAMENT_SELECTION_SIZE, rand);
        for (int x = 0; x < MainProgram.TOURNAMENT_SELECTION_SIZE; x++)
        {
            int randomIndex = rand.Next(population.Schedules.Size); //choses random schedule from best top 3 in population(because is is sorted)
            tournamentPopulation.Schedules.SetAt(population.Schedules.GetAt(randomIndex), x);
        }
        return tournamentPopulation;
    }
}