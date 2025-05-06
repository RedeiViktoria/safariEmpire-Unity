using System.Collections.Generic;
using static UnityEngine.EventSystems.EventTrigger;
using UnityEngine;
namespace Codes.Security
{
    public class Drone : SecuritySystem
    {
        public List<Vector2> waypoints;
        public Vector2 charger;
        public int battery;
        public int waypoint_index;
        public float fspeed;
        public int MAX_BATTERY;

        public Drone(Vector2 spawnpoint, List<Vector2> waypoints, Vector2 charger) : base(spawnpoint)
        {
            //example range (idk)
            range = 5;
            this.waypoints = waypoints;
            this.waypoint_index = 0;
            this.charger = charger;
            battery = 200;
            MAX_BATTERY = 200;
            fspeed = 1f;
        }

        public int Travel()
        {
            obj.transform.position = Vector2.MoveTowards(obj.transform.position, waypoints[waypoint_index], fspeed * Time.deltaTime);
            while (obj.transform.position.x != waypoints[waypoint_index].x && obj.transform.position.y != waypoints[waypoint_index].y)
            {
                battery -= 1;
            }
            if (waypoint_index + 1 < waypoints.Count)
            {
                waypoint_index++;
            }
            else waypoint_index = 0;

            return battery;
        }

        public void GoBack()
        {

            obj.transform.position = Vector2.MoveTowards(obj.transform.position, charger, fspeed * Time.deltaTime);
            /*
            while (obj.transform.position.x != waypoints[waypoint_index].x && obj.transform.position.y != waypoints[waypoint_index].y)
            {
                battery -= 1;
            }
            */

        }

        public int Charge()
        {
            battery += 10;
            return battery;
        }    
    }
}