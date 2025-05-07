using System.Collections.Generic;
using static UnityEngine.EventSystems.EventTrigger;
using UnityEngine;
namespace Codes.Security
{
    public class Drone : SecuritySystem
    {

        public Vector2 charger;
        public int battery;
        public int waypoint_index;
        public float fspeed;
        public int MAX_BATTERY;


        public Drone(Vector2 spawnpoint) : base(spawnpoint)
        {
            //example range (idk)
            range = 4;
            this.waypoint_index = 0;
            this.charger = new Vector2(0,1);
            battery = 200;
            fspeed = 3f;
            MAX_BATTERY = 200;
            this.pathOption = 'a';
            this.waypoints = new List<Vector2>();
            toggleSecurityPath();
        }

        public void Travel()
        {
            this.obj.transform.position = Vector2.MoveTowards(this.obj.transform.position, waypoints[waypoint_index], fspeed * Time.deltaTime);
            if (this.obj.transform.position.x != waypoints[waypoint_index].x && this.obj.transform.position.y != waypoints[waypoint_index].y)
            {
                battery -= 1;
            }
            if (this.obj.transform.position.x == waypoints[waypoint_index].x && this.obj.transform.position.y == waypoints[waypoint_index].y)
            {
                if (waypoint_index + 1 < waypoints.Count)
                    waypoint_index++;
                else waypoint_index = 0;
            }
        }

        public void GoBack()
        {

            this.obj.transform.position = Vector2.MoveTowards(this.obj.transform.position, charger, fspeed * Time.deltaTime);
            /*
            while (obj.transform.position.x != waypoints[waypoint_index].x && obj.transform.position.y != waypoints[waypoint_index].y)
            {
                battery -= 2;
            }
            */

        }

        public void Charge()
        {
            battery += 5;
            if (battery > MAX_BATTERY) battery = MAX_BATTERY;
        }

        
    }
}