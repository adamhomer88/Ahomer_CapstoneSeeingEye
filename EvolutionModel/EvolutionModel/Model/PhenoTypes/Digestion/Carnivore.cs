﻿using EvolutionModel.Model.Genotypes;
using EvolutionModel.Model.PhenoTypes.Digestion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.PhenoTypes.Digestion
{
    [Serializable]
    public class Carnivore : DigestiveSystem
    {
        public Carnivore()
        {
            this.OrganismHungryFor = typeof(Animal);
        }

        public override int Digest(Genotypes.Plant plant)
        {
            return (int)(plant.EnergyTotal * DigestiveSystem.NOT_DIGESTIBLE);
        }

        public override int Digest(Genotypes.Animal animal)
        {
            return (int)(animal.EnergyTotal * DigestiveSystem.FULLY_DIGESTIBLE);
        }
    }
}
