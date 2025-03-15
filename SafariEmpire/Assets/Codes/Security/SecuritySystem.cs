namespace Codes.Security
{
    public abstract class SecuritySystem
    {
        protected int range;

        public bool Detect()
        {
            //If poacher in range return true.
            return false;
        } 
    }
}