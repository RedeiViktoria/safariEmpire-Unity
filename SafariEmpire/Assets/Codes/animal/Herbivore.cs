using System;
using System.Collections.Generic;
using UnityEngine;

namespace Codes.animal{
	public abstract class Herbivore : Animal
	{
		
		public Herbivore(int gender) : base(gender)
		{
	
		}
        public override bool IsHerbivore()
        {
            return true;
        }
        public static Plant SelectClosestPlantTarget(AnimalGroup group)
        {
            Plant closest = null;
            HashSet<Plant> plants = group.PlantPlaces;
            float minDistance = float.MaxValue;

            foreach (Plant plant in plants)
            {
                if (plant.Value >= 70)
                {
                    float distance = Vector2.Distance(plant.obj.transform.position, group.obj.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closest = plant;
                    }
                }
            }

            return closest;
        }
    }
}
