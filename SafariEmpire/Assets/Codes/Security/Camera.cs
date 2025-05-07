using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Codes.Security
{
    public class Camera : SecuritySystem
    {
        public Camera(Vector2 spawnpoint) : base(spawnpoint)
        {
            //example range (idk)
            range = 5;
        }

        //detect later
    }
}