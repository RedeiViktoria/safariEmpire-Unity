namespace Codes.animal
{
    public class Gazella : Herbivore
    {
        public Gazella(int gender) : base(gender)
        {
        }
        override
        public AnimalType GetAnimalType()
        {
            return AnimalType.Gazella;
        }
    }
}