using System.Numerics;

namespace Codes.animal
{
    public abstract class Carnivore : Animal
    {
        public Carnivore(int gender) : base(gender)
        {

        } 
        public override bool IsCarnivore()
        {
            return true;
        }
    }
}