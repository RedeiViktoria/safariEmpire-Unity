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
        public char pathOption;

        public AirBalloon(Vector2 spawnpoint, List<Vector2> waypoints) : base(spawnpoint)
        {
            //example range (idk)
            range = 10;
            _waypoints = waypoints;
            _waypoint_index = 0;
            fspeed = 1f;
            this.pathOption = 'a';
            toggleSecurityPath();
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