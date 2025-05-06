using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Codes.Security
{
    public abstract class SecuritySystem : Entity
    {
        //egyelőre a range-eket 5, 10, és 15-re állítom, mert nem tudom ez hogy fog menni unityben
        //majd pontosat a megbeszélésen
        protected int range;
        public int Range { get { return range; } }
        public SecuritySystem(Vector2 spawnpoint) : base(spawnpoint) { }

        public bool Detect()
        {
            //If poacher in range return true.
            //amíg nem tudni mizu a general detecttel, addig ezt így hagyom
            return false;
        } 
    }
}