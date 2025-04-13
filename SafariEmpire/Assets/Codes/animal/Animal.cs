using System;
using UnityEngine.EventSystems;

namespace Codes.animal
{
	public abstract class Animal
	{
		protected int age;
		protected int gender;
		protected int hunger;
		protected int thirst;

		public Animal(int gender)
		{
			this.age = 0;
			this.gender = gender;
			this.hunger = 100;
			this.thirst = 100;
		}

		public abstract AnimalType GetAnimalType();

		public void IncAge()
		{
			this.age++;
		}


		public int Age
		{
			get => age;
			set => age = value;
		}

		public int Gender
		{
			get => gender;
			set => gender = value;
		}

		public int Hunger
		{
			get => hunger;
			set => hunger = value;
		}

		public int Thirst
		{
			get => thirst;
			set => thirst = value;
		}
		public virtual  bool IsCarnivore()
        {
			return false;
        }
		public virtual bool IsHerbivore()
		{
			return false;
		}
    }

}