using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Codes.animal
{
    public class AnimalGroup : Entity
        {
            protected List<int> places;
            protected int vision;
            protected int femaleCount;
            protected int maleCount;
            protected double averageAge;
            public List<Animal> animals;

            public Vector2 targetPosition;

            public AnimalGroup(Vector2 spawnPosition, string type) : base(spawnPosition)
            {
                this.places = new List<int>();
                this.animals = new List<Animal>();
                this.vision = 1;
                switch (type)
                {
                    case "cheetah": Gepard cheetah = new Gepard(); this.animals.Add(cheetah); break;
                    case "hippo": Hippo hippo = new Hippo();  this.animals.Add(hippo); break;
                    case "gazelle": Gazella gazelle = new Gazella(); this.animals.Add(gazelle); break;
                    case "crocodile": Crocodile crocodile = new Crocodile(); this.animals.Add(crocodile); break;
                }
                this.targetPosition = new Vector2(UnityEngine.Random.Range(-14, 15), UnityEngine.Random.Range(-14, 15));
            }
            
            /*
            protected AnimalGroup(List<int> places, int vision, int femaleCount, int maleCount, double averageAge)
            {
                this.places = places;
                this.vision = vision;
                this.femaleCount = femaleCount;
                this.maleCount = maleCount;
                this.averageAge = averageAge;
            }*/
    
            public void Mate()
            {
                
                if (femaleCount >= 1 && maleCount >= 1)
                {
                    //Random rand = new Random();
                    //rand.Next(100);
                    
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
    