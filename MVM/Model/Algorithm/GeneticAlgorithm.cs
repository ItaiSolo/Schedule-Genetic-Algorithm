using System;

/*
 * this class contains the logic for the genetic algorithm
 * includes the crossover SelectTournament and mutation of the population of schedules
 */
public class GeneticAlgorithm
{
    readonly Random rand;
    Schedules schedulePerent1;
    Schedules schedulePerent2;
    MyList<Schedules> TempSchedules;
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
        Population crossoverPopulation = new Population(population.Schedules.Size, rand)
        {
            Schedules = new MyList<Schedules>(population.Schedules)
        };

        for (int x = MainProgram.NUM_OF_ELITE_SCHEDULES; x < population.Schedules.Size; x++)
        {
            if (MainProgram.CROSSOVER_RATE > rand.NextDouble())
            {
                schedulePerent1 = SelectTournamentPopulation(population).Schedules.GetAt(0);
                schedulePerent2 = SelectTournamentPopulation(population).Schedules.GetAt(0);
                crossoverPopulation.Schedules.SetAt(CrossoverSchedule(schedulePerent1, schedulePerent2), x);
            }
        }
        return crossoverPopulation;
    }

    //Crossover per Schedule
    private Schedules CrossoverSchedule(Schedules schedule1, Schedules schedule2)
    {
        Schedules crossoverSchedule = new Schedules().Initialize(rand);//problem line

        for (int x = 0; x < crossoverSchedule.Classes.Size; x++)
        {
            crossoverSchedule.Classes.SetAt(rand.NextDouble() > 0.5 ? schedule1.Classes.GetAt(x) : schedule2.Classes.GetAt(x), x);
        }
        return crossoverSchedule;
    }

    //Mutate Population of Schedules -> Randomly change one of the schedules. effected by mutation rate in main program
    public Population MutatePopulation(Population population)
    {
        Population mutatePopulation = new Population(population.Schedules.Size, rand);
        TempSchedules = mutatePopulation.Schedules;
        for (int x = 0; x < MainProgram.NUM_OF_ELITE_SCHEDULES; x++)
        {
            TempSchedules.SetAt(population.Schedules.GetAt(x), x);
        }
        for (int x = MainProgram.NUM_OF_ELITE_SCHEDULES; x < population.Schedules.Size; x++)
        {
            TempSchedules.SetAt(MutateSchedule(population.Schedules.GetAt(x)), x);
        }
        return mutatePopulation;
    }

    //need to be refactored and made more efficient
    //Mutate on Schedules -> Randomly change one of the classes
    public Schedules MutateSchedule(Schedules schedule)
    {
        Schedules mutateSchedule = new Schedules().Initialize(rand);

        for (int x = 0; x < mutateSchedule.Classes.Size; x++)
        {
            if (MainProgram.MUTATION_RATE > rand.NextDouble())
            {
                mutateSchedule.Classes.SetAt(schedule.Classes.GetAt(x), x);
            }
        }
        return mutateSchedule;
    }

    //Select Tournament Population -> Randomly select schedules from the population
    public Population SelectTournamentPopulation(Population population)
    {
        Population tournamentPopulation = new Population(MainProgram.TOURNAMENT_SELECTION_SIZE, rand);
        for (int x = 0; x < MainProgram.TOURNAMENT_SELECTION_SIZE; x++)
        {
            int randomIndex = rand.Next(population.Schedules.Size);
            tournamentPopulation.Schedules.SetAt(population.Schedules.GetAt(randomIndex), x);
        }
        return tournamentPopulation;
    }
}