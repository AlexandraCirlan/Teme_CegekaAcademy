using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PetShelterDemo.Domain
{
    public class Donor_fund: INamedEntity
    {
        public string Name { get; }

        public string Id { get; }

        public Donor_fund(string name, string id)
        {
           Name = name;
            Id = id;
   
        }
    }
}
