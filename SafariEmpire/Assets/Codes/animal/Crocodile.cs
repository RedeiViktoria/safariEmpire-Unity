namespace Codes.animal
{
    public class Crocodile : Carnivore
    {
        public Crocodile(int gender) : base(gender)
        {
        }
        override
        public AnimalType GetAnimalType()
        {
            return AnimalType.Crocodile;
        }
    }
}