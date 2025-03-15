using System;

namespace Codes.Animal
{
	public abstract class Animal
	{
		protected int age;
		protected int gender;
		protected int hunger;
		protected int thirst;
		
		protected Animal(int age, int gender, int hunger, int thirst)
		{
			this.age = age;
			this.gender = gender;
			this.hunger = hunger;
			this.thirst = thirst;
		}

		public void IncAge()
		{
			this.age++;
		}

		public void AssignGroup(Animal animal)
		{
			
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
	}
}
