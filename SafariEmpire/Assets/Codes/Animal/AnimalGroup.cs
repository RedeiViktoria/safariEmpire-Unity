using Codes.utility;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Codes.Animal
{
    public abstract class AnimalGroup
    {
        protected List<Position> places;
        protected int vision;
        protected int femaleCount;
        protected int maleCount;
        protected double averageAge;
        protected List<Animal> animals;

        protected AnimalGroup(List<Position> places, int vision, int femaleCount, int maleCount, double averageAge)
        {
            this.places = places;
            this.vision = vision;
            this.femaleCount = femaleCount;
            this.maleCount = maleCount;
            this.averageAge = averageAge;
        }

        public void Mate()
        {
            
            if (femaleCount >= 1 && maleCount >= 1)
            {
                Random rand = new Random();
                rand.Next(100);
                
            }
            
        }

        public void Die()
        {
            
        }
        public void Decay()
        {
            
        }

        public void AverageAge()
        {
            double sum = 0;
            foreach(Animal animal in animals)
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
                }; 
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
        
        
        
    }
}