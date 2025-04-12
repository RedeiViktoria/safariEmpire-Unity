namespace Codes.animal
{
    public class Hippo : Herbivore
    {
        public Hippo(int gender) : base(gender)
        {
        }
        override
        public AnimalType GetAnimalType()
        {
            return AnimalType.Hippo;
        }
    }
}