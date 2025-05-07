using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


namespace Codes.Security
{
    public class AirBalloon : SecuritySystem
    {
        private int _waypoint_index;
        private float fspeed;

        public AirBalloon(Vector2 spawnpoint) : base(spawnpoint)
        {
            //example range (idk)
            range = 4;
            _waypoint_index = 0;
            fspeed = 1f;
            this.pathOption = 'a';
            this.waypoints = new List<Vector2>();
            toggleSecurityPath();
        }

        public void Travel()
        {
            this.obj.transform.position = Vector2.MoveTowards(this.obj.transform.position, waypoints[_waypoint_index], fspeed * Time.deltaTime);

            if (this.obj.transform.position.x == waypoints[_waypoint_index].x && this.obj.transform.position.y == waypoints[_waypoint_index].y)
            {
                if (_waypoint_index + 1 < waypoints.Count)
                    _waypoint_index++;
                else _waypoint_index = 0;
            }

        }
    }
}