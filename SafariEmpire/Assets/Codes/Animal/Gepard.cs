namespace Codes.Animal
{
    public class Gepard : Carnivore
    {
        public Gepard(int age, int gender, int hunger, int thirst) : base(age, gender, hunger, thirst)
        {
            base.age = age;
            base.gender = gender;
            base.hunger = hunger;
            base.thirst = thirst;
        }
    }


}