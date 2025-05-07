using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Codes;

namespace Codes.Security
{
    public abstract class SecuritySystem : Entity
    {
        //egyelőre a range-eket 5, 10, és 15-re állítom, mert nem tudom ez hogy fog menni unityben
        //majd pontosat a megbeszélésen
        protected int range;
        public int Range {  get { return range; } }
        public char pathOption;
        public List<Vector2> waypoints;
        public SecuritySystem(Vector2 spawnpoint) : base(spawnpoint) { }

        public void toggleSecurityPath()
        {
            this.waypoints.Clear();
            switch (this.pathOption)
            {
                case 'a':
                    {
                        this.waypoints.Add(new Vector2(6, 5));
                        this.waypoints.Add(new Vector2(-4, 2));
                        this.waypoints.Add(new Vector2(4, 2));
                        this.pathOption = 'b';
                    }
                    break;
                case 'b':
                    {
                        this.waypoints.Add(new Vector2(10, -3));
                        this.waypoints.Add(new Vector2(-10, -3));
                        this.waypoints.Add(new Vector2(-10, 3));
                        this.waypoints.Add(new Vector2(10, 3));
                        this.pathOption = 'c';
                    }
                    break;
                case 'c':
                    {
                        this.waypoints.Add(new Vector2(5, 10));
                        this.waypoints.Add(new Vector2(10, 10));
                        this.waypoints.Add(new Vector2(8, 8));
                        this.waypoints.Add(new Vector2(-5, -5));
                        this.waypoints.Add(new Vector2(-3, -7));
                        this.pathOption = 'a';
                    }
                    break;
            }
        }
    }
}