using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


namespace Codes.Security
{
    public class AirBalloon : SecuritySystem
    {
        private List<Vector2> _waypoints;
        private int _waypoint_index;
        private float fspeed;

        public AirBalloon(Vector2 spawnpoint, List<Vector2> waypoints) : base(spawnpoint)
        {
            //example range (idk)
            range = 10;
            _waypoints = waypoints;
            _waypoint_index = 0;
            fspeed = 1f;
        }

        public void Travel()
        {
            this.obj.transform.position = Vector2.MoveTowards(this.obj.transform.position, _waypoints[_waypoint_index], fspeed * Time.deltaTime);

            if (this.obj.transform.position.x == _waypoints[_waypoint_index].x && this.obj.transform.position.y == _waypoints[_waypoint_index].y)
            {
                if (_waypoint_index + 1 < _waypoints.Count)
                    _waypoint_index++;
                else _waypoint_index = 0;
            }

        }
    }
}