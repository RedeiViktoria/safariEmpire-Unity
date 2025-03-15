using System.Collections.Generic;
using Codes.utility;
namespace Codes.Security
{
    public class Drone
    {
       private List<Position> _waypoints;
       private Position _charger;
       private int _battery;

       public Drone(List<Position> waypoints, Position charger)
       {
           this._waypoints = waypoints;
           this._charger = charger;
           _battery = 100;
       }

       public int Travel()
       {
           _battery -= 2;
           return _battery;
       }

       public void GoBack()
       {
           
       }

       public int Charge()
       {
           _battery += 20;
           return _battery;
       }
    }
    
    
}