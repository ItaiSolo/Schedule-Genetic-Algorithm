using System;
using System.Collections.Generic;

//This class creates a population of schedules and sorts them
public class Population
{
    public MyList<Schedules> Schedules { get; set; }

    //init the population with random schedules
    public Population(int size, Random rand)
    {
        Schedules = new MyList<Schedules>(size);

        for (int i = 0; i < size; i++)
        {
            Schedules.Add(new Schedules().Initialize(rand));
        }
    }

    //sorts MyList - schedules by fitness
    public Population SortSchedulesByFitness()
    {
        Schedules.Sort(new ScheduleFitnessComparer());
        return this;
    }
    //sorts MyList - schedules by fitness but for the last generation so it will include conflicts as well
    public Population SortSchedulesByFitnessLast()
    {
        Schedules.Sort(new ScheduleFitnessComparerLast());
        return this;
    }

    //comparator for sorting
    private class ScheduleFitnessComparer : IComparer<Schedules>
    {
        public int Compare(Schedules x, Schedules y)
        {
            return y.Fitness.CompareTo(x.Fitness);
        }
    }

    private class ScheduleFitnessComparerLast : IComparer<Schedules>
    {
        public int Compare(Schedules x, Schedules y)
        {
            return y.AddLastGenerationConflicts().CompareTo(x.AddLastGenerationConflicts());
        }
    }
}