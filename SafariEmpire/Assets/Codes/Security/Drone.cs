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
        public char pathOption;

        public Drone(Vector2 spawnpoint, List<Vector2> waypoints, Vector2 charger) : base(spawnpoint)
        {
            //example range (idk)
            range = 5;
            this.waypoints = waypoints;
            this.waypoint_index = 0;
            this.charger = charger;
            battery = 200;
            fspeed = 1f;
            MAX_BATTERY = 200;
            this.pathOption = 'a';
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

        public void toggleSecurityPath()
        {
            char i = 'a';
            switch (this.pathOption)
            {
                case 'a': this.pathOption = 'b'; break;
                case 'b': this.pathOption = 'c'; break;
                case 'c': this.pathOption = 'a'; break;
            }
            this.waypoints.Clear();
            switch (i)
            {
                case 'a':
                    {
                        this.waypoints.Add(new Vector2(1, 1));
                        this.waypoints.Add(new Vector2(2, 1));
                        this.waypoints.Add(new Vector2(1, 2));

                    }
                    break;
                case 'b':
                    {
                        this.waypoints.Add(new Vector2(0, 2));
                        this.waypoints.Add(new Vector2(1, 2));
                        this.waypoints.Add(new Vector2(2, 1));
                        this.waypoints.Add(new Vector2(2, 2));

                    }
                    break;
                case 'c':
                    {
                        this.waypoints.Add(new Vector2(0, 1));
                        this.waypoints.Add(new Vector2(1, 1));
                        this.waypoints.Add(new Vector2(1, 2));
                        this.waypoints.Add(new Vector2(2, 2));
                        this.waypoints.Add(new Vector2(2, 1));
                    }
                    break;
            }
        }
    }
}