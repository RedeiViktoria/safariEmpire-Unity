using System;

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
    }
}
