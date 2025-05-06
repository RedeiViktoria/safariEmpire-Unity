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
        public SecuritySystem(Vector2 spawnpoint) : base(spawnpoint) { }
    }
}