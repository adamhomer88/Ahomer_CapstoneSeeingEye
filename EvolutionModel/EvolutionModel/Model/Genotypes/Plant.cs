﻿using EvolutionModel.Model.Environment;
using EvolutionModel.Model.Mutation;
using EvolutionModel.Model.PhenoTypes.Energy_Factory;
using EvolutionModel.Model.PhenoTypes.Water_Absorbtion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.Genotypes
{
    [Serializable]
    public class Plant : Organism
    {
        public EnvironmentTile localEnvironment { get; set; }

        #region BasicPhenotypes
        public double growthThresholdToNutrients { get; set; }
        public double growthRate { get; set; }
        public int NutrientTotal { get; set; }
        public int MaxNutrient { get; set; }
        public int WaterTotal { get; set; }
        public int MaxWater { get; set; }
        public int ReproductionRate { get; set; }
        private int seasonsWaited = 0;
        #endregion

        #region DefaultBasePhenotypes
        public const double DEFAULT_GROWTH_THRESHOLD = .6;
        public const double DEFAULT_GROWTH_RATE = 1.05;
        public const int DEFAULT_REPRODUCTION_RATE = 3;
        #endregion

        #region Phenotypes
        public IEnergyFactory EnergyFactory { get; set; }
        public INutrientAbsorber NutrientAbsorbtion { get; set; }
        #endregion

        public Plant()
        {
            this.MaxEnergy = 50;
            this.EnergyTotal = (int)(this.MaxEnergy * .6);
            this.Mass = 1;
            this.MaxNutrient = 50;
            this.MaxWater = 50;
            this.WaterTotal = (int)(this.MaxWater * .6);
            this.NutrientTotal = (int)(this.MaxNutrient*.6);
            this.ReproductionRate = DEFAULT_REPRODUCTION_RATE;
            this.Parasites = new List<Parasite>();
        }
        
        public void Grow()
        {
            this.EnergyTotal -= this.MaxEnergy * this.Mass;
            this.Mass += (int)(this.growthRate * this.Mass);
        }

        public override void doTurn()
        {
            AbsorbFromEnvironment();

            if ((NutrientTotal / MaxNutrient) > growthThresholdToNutrients)
                Grow();
        }

        public Plant resolveReproduction()
        {
            Plant childPlant = null;
            if (seasonsWaited >= ReproductionRate)
            {
                childPlant = (Plant)Reproduce();
                seasonsWaited = 0;
            }
            else
                seasonsWaited++;
            return childPlant;
        }

        private void AbsorbFromEnvironment()
        {
            int waterLevel = localEnvironment.WaterLevel;
            int fertilityLevel = localEnvironment.FertilityLevel;

            int nutrientsAbsorbed = NutrientAbsorbtion.absorbNutrients(localEnvironment, this.Mass);
            int newWaterTotal = WaterTotal;
            int energyCreated = EnergyFactory.createEnergy(out newWaterTotal, this.Mass);
        }

        private void resolveParasites()
        {
            foreach (Parasite p in Parasites)
                p.doTurn();
        }

        public override Organism basicMutate(Organism baseOrganism)
        {
            Mutator basicMutator = Mutator.GetBasicInstance();
            Organism newMutatedOrganism = basicMutator.Mutate(baseOrganism);
            return newMutatedOrganism;
        }

        public override Organism complexMutate(Organism baseOrganism)
        {
            Mutator complexMutator = Mutator.GetComplexInstance();
            Organism newMutatedOrganism = complexMutator.Mutate(baseOrganism);
            return newMutatedOrganism;
        }
    }
}
