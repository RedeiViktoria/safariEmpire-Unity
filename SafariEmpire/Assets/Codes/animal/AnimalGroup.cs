using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using Random = System.Random;

namespace Codes.animal
{
    public class AnimalGroup : Entity
    {
        protected HashSet<Vector2> waterPlaces;
        protected HashSet<Plant> plantPlaces;
        protected int femaleCount;
        protected int maleCount;
        protected double averageAge;
        public List<Animal> animals;
        public AnimalType animalType;
        protected Animal father;
        protected bool merged;
        protected int vision;
        protected int reach;
        protected int sleep;

        public AnimalGroup(Vector2 spawnPosition, AnimalType type) : base(spawnPosition)
        {
            this.merged = false;
            this.vision = 3;
            this.animals = new List<Animal>();
            this.father = Born(type);
            this.animals.Add(father);
            this.waterPlaces = new HashSet<Vector2>();
            this.femaleCount = CountGender(this, 0);
            this.maleCount = CountGender(this, 1);
            this.averageAge = CountAverage(this);
            this.animalType = type;
            this.targetPosition = new Vector2(UnityEngine.Random.Range(-14, 15), UnityEngine.Random.Range(-14, 15));
            this.plantPlaces = new HashSet<Plant>();
        }
        public void SortAnimals()
        {
            animals.Sort((a, b) => a.Age.CompareTo(b.Age));
        }
        private Animal ElectFather()
        {
            SortAnimals();
            if (animals.Count > 0) return animals[animals.Count-1];
            return null;

        }
        private static int CountGender(AnimalGroup animalGroup, int gender)
        {
            int c = 0;
            if (animalGroup.animals.Count <= 0)
            {
                animalGroup.merged = true;
                return 0;
            }
            foreach (var a in animalGroup.animals)
            {
                if (a.Gender == gender)
                {
                    c++;
                }
            }
            return c;
        }
        public static double CountAverage(AnimalGroup animalGroup)
        {
            double sum = 0;
            if (animalGroup.animals.Count <= 0)
            {
                animalGroup.merged = true;
                return 0;
            }
            foreach (var a in animalGroup.animals)
            {
                sum += a.Age;
            }
            return sum / animalGroup.animals.Count;
        }
        public static double CountAverageHunger(AnimalGroup animalGroup)
        {
            double sum = 0;
            if (animalGroup.animals.Count <= 0)
            {
                animalGroup.merged = true;
                return 0;
            }
            foreach (var a in animalGroup.animals)
            {
                sum += a.Hunger;
            }
            return sum / animalGroup.animals.Count;
        }
        public bool AbleToMate()
        {
            //Debug.Log(this.femaleCount +" Nő és Hím: "+ this.maleCount);
            if (femaleCount >= 1 && maleCount >= 1)
            {

                bool healthyMale = false;
                bool healthyFemale = false;
                foreach (Animal a in animals)
                {
                    if (a.Hunger > 70 && a.Thirst > 70)
                    {
                        if (a.Gender == 1)
                        {
                            healthyMale = true;
                        }
                        else
                        {
                            healthyFemale = true;
                        }
                    }
                }
                return (healthyFemale && healthyMale);
            }
            return false;
        }
        public Animal Born(AnimalType type)
        {
            Random rand = new Random();
            int gender = rand.Next(2);
            switch (type)
            {
                case AnimalType.Crocodile:
                    return new Crocodile(gender);

                case AnimalType.Gazella:
                    return new Gazella(gender);

                case AnimalType.Gepard:
                    return new Gepard(gender);

                case AnimalType.Hippo:
                    return new Hippo(gender);

                default:
                    animals = new List<Animal>();
                    break;
            }

            return null;
        }
        public void Mate()
        {
            if (AbleToMate())
            {
               // Debug.Log("Group is able To mate");
                Random rand = new Random();
                int chance = rand.Next(101);
                if (chance > 75)
                {
                    this.animals.Add(Born(this.animalType));
                    this.femaleCount = CountGender(this, 0);
                    this.maleCount = CountGender(this, 1);
                   // Debug.Log("Baby " + animalType + " is born.");
                }
            }

        }
        public void Age()
        {
            foreach (Animal a in this.animals)
            {
                a.Age += 1;
            }
        }

        public void Die()
        {
            bool removed = false;
            if (animals.Count <= 0)
            {
                this.merged = true;
                return;
            } 
            
            for (int i = this.animals.Count-1; i >= 0; i--)
            {
                if (this.animals[i].Age >= 1000 || this.animals[i].Thirst < 0 || this.animals[i].Hunger < 0)
                {
                    animals.Remove(this.animals[i]);
                    removed = true;
                }

            }
            if (removed)
            {
                if (animals.Count < 1)
                {
                    this.merged = true;
                }
                else
                {
                    this.averageAge = CountAverage(this);
                    this.maleCount = CountGender(this, 1);
                    this.femaleCount = CountGender(this, 0);
                    father = ElectFather();
                }

            }

        }
        private void Decay()
        {
            this.merged = true;
        }
        public int killAnimal()
        {
            if (animals.Count() == 1)
            {
                animals.Clear();
                this.Decay();
                return 0;
            }
            animals.Remove(father);
            father = ElectFather();
            return animals.Count();
        }
        public void AverageAge()
        {
            double sum = 0;
            if (this.animals.Count <= 0)
            {
                this.merged = true;
                return;
            }
            foreach (Animal animal in animals)
            {
                sum += animal.Age;
            }

            averageAge = sum / animals.Count;
        }

        public int Eat(int foodAmount)
        {
            Debug.Log("Animal is eating");
            int foodForEach = (int)(System.Math.Round(foodAmount / (double)(animals.Count), 0));
            int leftOver = 0;
            List<Animal> stillHungry = new List<Animal>();
            foreach (Animal animal in animals)
            {
                animal.Hunger += foodForEach;
                if (animal.Hunger <= 100)
                {
                    stillHungry.Add(animal);
                }
                ;
                leftOver = animal.Hunger - 100;
                animal.Hunger -= leftOver;
                foodForEach += leftOver;
            }

            if (leftOver <= 0) return leftOver;
            stillHungry.OrderBy(animal => animal.Hunger);
            foreach (Animal animal in stillHungry)
            {
                animal.Hunger += leftOver;
                if (animal.Hunger > 100)
                {
                    leftOver = animal.Hunger - 100;
                    animal.Hunger = 100;
                }

                if (leftOver == 0)
                {
                    return 0;
                }
            }

            return leftOver;
        }
        public int AverageThirst()
        {
            int sum = 0;
            if (this.animals.Count <= 0)
            {
                this.merged = true;
                return 0;
            }
            foreach (Animal animal in this.animals)
            {
                sum += animal.Thirst;
            }
            return sum / this.animals.Count;
        }
        public bool IsThirsty()
        {
            return AverageThirst() < 50;
        }
        public void Drink()
        {
            foreach (Animal animal in animals)
            {
                animal.Thirst = 100;
            }
        }

        public bool FatherForMerge(AnimalGroup animalGroup)
        {
            if (this.animals.Count != animalGroup.animals.Count)
                return this.animals.Count > animalGroup.animals.Count;
            if (this.father.Age != animalGroup.father.Age)
                return this.father.Age > animalGroup.father.Age;
            if (this.averageAge != animalGroup.averageAge)
                return this.averageAge > animalGroup.averageAge;
            if (this.father.Gender != animalGroup.father.Gender)
                return this.father.Gender < animalGroup.father.Gender;
            if (this.AverageThirst() != animalGroup.AverageThirst())
                return this.AverageThirst() < animalGroup.AverageThirst();
            if (CountAverageHunger(this) != CountAverageHunger(animalGroup))
                return CountAverageHunger(this) < CountAverageHunger(animalGroup);
            return false;
        }
        public bool MergeGroups(AnimalGroup animalGroup)
        {
            if (FatherForMerge(animalGroup))
            {
                foreach (Animal a in animalGroup.animals)
                {
                    this.animals.Add(a);
                }
                this.averageAge = CountAverage(animalGroup);
                this.femaleCount += animalGroup.femaleCount;
                this.maleCount = animalGroup.maleCount;
                this.waterPlaces.UnionWith(animalGroup.WaterPlaces);
                if (this.IsHerbivore())
                {
                    this.plantPlaces.UnionWith(animalGroup.PlantPlaces);
                }
                return true;
            }
            else
            {
                this.Decay();
                return false;
            }
        }
        public bool IsHungry()
        {
            return CountAverageHunger(this) <= 50;
        }
        public bool IsHerbivore()
        {
            return father.IsHerbivore();
        }
        public bool IsCarnivore()
        {
            return father.IsCarnivore();
        }
        public int Vision
        {
            get => vision;
            set => vision = value;
        }
        public int Panicked
        {
            get => sleep;
            set => sleep = value;
        }
        public int Reach
        {
            get => reach;
        }
        public bool Merged
        {
            get => merged;
        }
        public HashSet<Vector2> WaterPlaces
        {
            get => waterPlaces;
        }
        public HashSet<Plant> PlantPlaces
        {
            get => plantPlaces;
        }
        public bool addWaterPlace(Vector2 place)
        {
            return this.waterPlaces.Add(place);
        }
        public bool addPlantPlace(Plant p)
        {
            return this.plantPlaces.Add(p);
        }
        public void changePlaceValue(Vector2 place)
        {

        }
        public int AmountToEat()
        {
            if (this.IsCarnivore())
            {
                if (animals.Count < 5)
                {
                    return 1;
                }
                return (int)Math.Round(1 + 0.2 * animals.Count, 0);
            }
            else if (this.IsHerbivore())
            {
                if (animals.Count >= 10)
                {
                    return animals.Count / 10;
                }
                return 1;
            }
            return 0;
        }
        public void Starve()
        {
            foreach (Animal a in this.animals)
            {
                a.Hunger -= (int) Math.Round(1 + 0.5 * a.Age, 0);
                a.Thirst -= (int)Math.Round(1 + 0.3 * a.Age, 0);
            }
        }
        public AnimalType GetGroupType()
        {
            return this.father.GetAnimalType();
        }
    }
}
    