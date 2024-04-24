using System;

public class GeneticAlgorithm
{
    private Data data;
    Random rand;
    Schedules schedulePerent1;
    Schedules schedulePerent2;
    MyList<Schedules> Tempschedules;
    public GeneticAlgorithm(Data data, Random mainRand)
    {
        this.data = data;
        rand = mainRand;
    }

    public Population EvolvePopulation(Population population)
    {
        return MutatePopulation(CrossoverPopulation(population));
    }

    //need to be refactored and made more efficient!!!
    public Population CrossoverPopulation(Population population)
    {
        Population crossoverPopulation = new Population(population.Schedules.Size, data, rand);
        // Use system-level cloning or ensure deep copy if necessary
        crossoverPopulation.Schedules = new MyList<Schedules>(population.Schedules);

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

    //need to be refactored and made more efficient
    private Schedules CrossoverSchedule(Schedules schedule1, Schedules schedule2)
    {
        Schedules crossoverSchedule = new Schedules(data).Initialize(rand);//problem line

        for (int x = 0; x < crossoverSchedule.Classes.Size; x++)
        {
            crossoverSchedule.Classes.SetAt(rand.NextDouble() > 0.5 ? schedule1.Classes.GetAt(x) : schedule2.Classes.GetAt(x),x);
        }
        return crossoverSchedule;
    }

    //Mutate Population of Schedules
    public Population MutatePopulation(Population population)
    {
        Population mutatePopulation = new Population(population.Schedules.Size, data, rand);
        Tempschedules = mutatePopulation.Schedules;
        for (int x = 0; x < MainProgram.NUM_OF_ELITE_SCHEDULES; x++)
        {
            Tempschedules.SetAt(population.Schedules.GetAt(x),x);
        }
        for (int x = MainProgram.NUM_OF_ELITE_SCHEDULES; x < population.Schedules.Size; x++)
        {
            Tempschedules.SetAt(MutateSchedule(population.Schedules.GetAt(x)), x);
        }
        return mutatePopulation;
    }

    //need to be refactored and made more efficient
    //Mutate on Schedules
    public Schedules MutateSchedule(Schedules schedule)
    {
        Schedules mutateSchedule = new Schedules(data).Initialize(rand);
        
        for (int x = 0; x < mutateSchedule.Classes.Size; x++)
        {
            if (MainProgram.MUTATION_RATE > rand.NextDouble())
            {
                mutateSchedule.Classes.SetAt(schedule.Classes.GetAt(x),x);
            }
        }
        return mutateSchedule;
    }


    public Population SelectTournamentPopulation(Population population)
    {
        Population tournamentPopulation = new Population(MainProgram.TOURNAMENT_SELECTION_SIZE, data,rand);
        for (int x = 0; x < MainProgram.TOURNAMENT_SELECTION_SIZE; x++)
        {
            int randomIndex = rand.Next(population.Schedules.Size);
            tournamentPopulation.Schedules.SetAt(population.Schedules.GetAt(randomIndex),x);
        }
        return tournamentPopulation;
    }
}