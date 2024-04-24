using System;
using System.Collections.Generic;
using System.Linq;

//remove this class and marge with othrs when possible
public class Population
{
    private bool isSorted = false;
    public MyList<Schedules> Schedules { get; set; }

    public Population(int size, Data data, Random rand)
    {
        Schedules = new MyList<Schedules>(size);
        
        for (int i = 0; i < size; i++)
        {
            Schedules.Add(new Schedules(data).Initialize(rand));
        }
    }

   
    public Population SortSchedulesByFitness()
    {
        if (!isSorted)
        {
            Schedules.Sort(new ScheduleFitnessComparer());
            isSorted = true;
        }
        return this;
    }

    private class ScheduleFitnessComparer : IComparer<Schedules>
    {
        public int Compare(Schedules x, Schedules y)
        {
            return y.Fitness.CompareTo(x.Fitness);
        }
    }
}