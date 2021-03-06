﻿using EvolutionModel.Model.Genotypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EvolutionModel.Model.Mutation
{
    [Serializable]
    public class BaseMutationProcessor : IMutationProcessor
    {
        private const int DEFAULT_BASE_MUTATION_MARGIN = 5;
        private static BaseMutationProcessor processor;

        public Organism MutateAnimal(Organism organism)
        {
            Animal mutatee = (Animal)mutateBaseOrganismPhenotypes(organism);
            mutatee = mutateBaseAnimalPhenotypes(mutatee);
            return mutatee;
        }

        public Organism MutatePlant(Organism organism)
        {
            Plant mutatee = (Plant)mutateBaseOrganismPhenotypes(organism);
            mutatee = mutateBasePlantPhenotypes(mutatee);
            return mutatee;
        }

        public Organism MutateParasite(Organism organism)
        {
            Parasite mutatee = (Parasite)mutateBaseOrganismPhenotypes(organism);
            mutatee = mutateBaseParasitePhenotypes(mutatee);
            return mutatee;
        }

        private Organism mutateBaseOrganismPhenotypes(Organism organism)
        {
            organism.MaximumMass += OrganismFactory.random.Next(-(DEFAULT_BASE_MUTATION_MARGIN+1), DEFAULT_BASE_MUTATION_MARGIN);
            organism.MaxEnergy += OrganismFactory.random.Next(-(DEFAULT_BASE_MUTATION_MARGIN + 1), DEFAULT_BASE_MUTATION_MARGIN);
            organism.EnergyPerTurn += OrganismFactory.random.Next(-(DEFAULT_BASE_MUTATION_MARGIN + 1), DEFAULT_BASE_MUTATION_MARGIN);
            return organism;
        }

        private Plant mutateBasePlantPhenotypes(Plant mutatee)
        {
            MessageBox.Show("Plant Evolved");
            mutatee.growthThresholdToNutrients += OrganismFactory.random.Next(-DEFAULT_BASE_MUTATION_MARGIN + 1, DEFAULT_BASE_MUTATION_MARGIN);
            mutatee.growthRate += OrganismFactory.random.Next(-DEFAULT_BASE_MUTATION_MARGIN + 1, DEFAULT_BASE_MUTATION_MARGIN);
            mutatee.MaxNutrient += OrganismFactory.random.Next(-DEFAULT_BASE_MUTATION_MARGIN + 1, DEFAULT_BASE_MUTATION_MARGIN);
            mutatee.MaxWater += OrganismFactory.random.Next(-DEFAULT_BASE_MUTATION_MARGIN + 1, DEFAULT_BASE_MUTATION_MARGIN);
            mutatee.ReproductionRate += OrganismFactory.random.Next(-DEFAULT_BASE_MUTATION_MARGIN + 1, DEFAULT_BASE_MUTATION_MARGIN);
            return mutatee;
        }

        private Parasite mutateBaseParasitePhenotypes(Parasite mutatee)
        {
            MessageBox.Show("Parasite Evolved");
            return mutatee;
        }

        private Animal mutateBaseAnimalPhenotypes(Animal mutatee)
        {
            MessageBox.Show("Animal Evolved");
            mutatee.favoredHungerThreshold += OrganismFactory.random.Next(-DEFAULT_BASE_MUTATION_MARGIN+1, DEFAULT_BASE_MUTATION_MARGIN);
            mutatee.unfavoredHungerThreshold += OrganismFactory.random.Next(-DEFAULT_BASE_MUTATION_MARGIN + 1, DEFAULT_BASE_MUTATION_MARGIN);
            mutatee.reproductionThreshold += OrganismFactory.random.Next(-DEFAULT_BASE_MUTATION_MARGIN + 1, DEFAULT_BASE_MUTATION_MARGIN);
            return mutatee;
        }

        public static IMutationProcessor GetInstance()
        {
            if (processor == null)
                processor = new BaseMutationProcessor();
            return processor;
        }
    }
}
