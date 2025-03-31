using System.Collections.Generic;
using static UnityEngine.EventSystems.EventTrigger;
using UnityEngine;
namespace Codes.Security
{
    public class Drone : SecuritySystem
    {
        private List<Vector2> _waypoints;
        private Vector2 _current_waypoint;
        private Vector2 _charger;
        private int _battery;
        private int _waypoint_index;
        private float fspeed;
        private GameObject obj;

        public Drone(Vector2 spawnpoint, List<Vector2> waypoints, Vector2 charger) : base(spawnpoint)
        {
            //example range (idk)
            range = 5;
            this._waypoints = waypoints;
            this._current_waypoint = charger;
            this._waypoint_index = 0;
            this._charger = charger;
            _battery = 100;
            fspeed = 1f;
        }

        public int Travel()
        {
            obj.transform.position = Vector2.MoveTowards(obj.transform.position, _waypoints[_waypoint_index], fspeed * Time.deltaTime);
            while (obj.transform.position.x != _waypoints[_waypoint_index].x && obj.transform.position.y != _waypoints[_waypoint_index].y)
            {
                _battery -= 2;
            }
            if (_waypoint_index + 1 < _waypoints.Count)
            {
                _waypoint_index++;
            }
            else _waypoint_index = 0;

            return _battery;
        }

        public void GoBack()
        {

            obj.transform.position = Vector2.MoveTowards(obj.transform.position, _charger, fspeed * Time.deltaTime);
            /*
            while (obj.transform.position.x != _waypoints[_waypoint_index].x && obj.transform.position.y != _waypoints[_waypoint_index].y)
            {
                _battery -= 2;
            }
            */

        }

        public int Charge()
        {
            _battery += 20;
            return _battery;
        }    
    }
}