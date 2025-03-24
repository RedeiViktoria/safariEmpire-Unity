using System;

namespace Codes.animal
{
	public class Animal
	{
		protected int age;
		protected int gender;
		protected int hunger;
		protected int thirst;

		public Animal()
		{
			this.age = 0;
			this.gender = 1; //randomizálni
			this.hunger = 100;
			this.thirst = 100;
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