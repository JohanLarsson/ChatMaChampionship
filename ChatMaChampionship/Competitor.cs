using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatMaChampionship
{
    public class Competitor
    {
        public Competitor(string name, Func<IEnumerable<double>,int, IEnumerable<double>> code, Func<double[],IEnumerable<double>> transform )
        {
            Name = name;
            Code = code;
            Transform = transform;
        }

        public string Name { get; private set; }
        public Func<double[], IEnumerable<double>> Transform { get; private set; } 
        public Func<IEnumerable<double>,int, IEnumerable<double>> Code { get; private set; }

        public IEnumerable<double> Run(IEnumerable<double> data,int p)
        {
            return Code(data,p);
        }
    }
}
