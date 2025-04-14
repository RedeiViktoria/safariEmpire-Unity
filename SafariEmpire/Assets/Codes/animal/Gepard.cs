namespace Codes.animal
{
    public class Gepard : Carnivore
    {
        public Gepard(int gender):base(gender)
        {

        }
        override
        public AnimalType GetAnimalType()
        {
            return AnimalType.Gepard;
        }
    }


}