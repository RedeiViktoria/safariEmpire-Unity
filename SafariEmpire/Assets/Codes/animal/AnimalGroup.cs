using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

namespace Codes.animal
{
    public class AnimalGroup : Entity
    {
        protected List<Vector2> waterPlaces;
        protected int vision;
        protected int femaleCount;
        protected int maleCount;
        protected double averageAge;
        public List<Animal> animals;
        public AnimalType animalType;
        protected Animal father;
        protected bool merged;

        public AnimalGroup(Vector2 spawnPosition, AnimalType type) : base(spawnPosition)
        {
            this.merged = false;
            this.animals = new List<Animal>();

            this.animals.Add(Born(type));
            this.waterPlaces = new List<Vector2>();
            this.femaleCount = CountGender(this, 0);
            this.maleCount = CountGender(this, 1);
            this.averageAge = CountAverage(this);
            this.animalType = type;
            this.targetPosition = new Vector2(UnityEngine.Random.Range(-14, 15), UnityEngine.Random.Range(-14, 15));
        }
        public void SortAnimals()
        {
            animals.Sort((a, b) => a.Age.CompareTo(b.Age));
        }
        private Animal ElectFather()
        {
            SortAnimals();
            return animals[-1];

        }
        private static int CountGender(AnimalGroup animalGroup, int gender)
        {
            int c = 0;
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
            foreach (var a in animalGroup.animals)
            {
                sum += a.Age;
            }
            return sum / animalGroup.animals.Count;
        }
        public static double CountAverageHunger(AnimalGroup animalGroup)
        {
            double sum = 0;
            foreach (var a in animalGroup.animals)
            {
                sum += a.Hunger;
            }
            return sum / animalGroup.animals.Count;
        }
        public bool AbleToMate()
        {
            if (femaleCount >= 1 && maleCount >= 1)
            {
                bool healthyMale = false;
                bool healthyFemale = false;
                foreach (Animal a in animals)
                {
                    if (a.Hunger > 80 && a.Thirst > 80)
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
                Random rand = new Random();
                int chance = rand.Next(101);
                if (chance > 75)
                {
                    this.animals.Add(Born(this.animalType));
                    this.femaleCount = CountGender(this, 0);
                    this.maleCount = CountGender(this, 1);
                }
            }

        }

        public void Die()
        {
            bool removed = false;
            foreach (var a in animals)
            {
                if (a.Age >= 100)
                {
                    animals.Remove(a);
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
            foreach (Animal animal in animals)
            {
                sum += animal.Age;
            }

            averageAge = sum / animals.Count;
        }

        public int Eat(int foodAmount)
        {
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

        public void Drink(int waterAmount)
        {
            foreach (Animal animal in animals)
            {
                animal.Thirst += waterAmount;
                if (animal.Thirst > 100)
                {
                    animal.Thirst = 100;
                }
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
            return false;
        }
        public void MergeGroups(AnimalGroup animalGroup)
        {
            if (FatherForMerge(animalGroup))
            {
                foreach (var a in animalGroup.animals)
                {
                    this.animals.Add(a);
                }

                foreach (var p in animalGroup.waterPlaces)
                {
                    this.waterPlaces.Add(p);
                }
                this.averageAge = CountAverage(animalGroup);
                this.femaleCount = CountGender(animalGroup, 0);
                this.maleCount = CountGender(animalGroup, 1);
            }
            else
            {
                this.Decay();
            }
        }

        public void TryToEat()
        {
            if (this.father.IsCarnivore())
            {

            }
            else if (this.father.IsHerbivore())
            {

            }
        }
        public bool IsHungry()
        {
            return CountAverageHunger(this) <= 0.6;
        }
    }
}
    