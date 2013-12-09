using System;
using System.Collections.Generic;

namespace ChatMaChampionship
{
    public interface ICompetitor
    {
        string Name { get; }
        Func<double[], IEnumerable<double>> Transform { get; }
    }
}