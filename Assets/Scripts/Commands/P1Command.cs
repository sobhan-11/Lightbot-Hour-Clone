﻿using System.Collections.Generic;
using UnityEngine;

namespace Commands
{
    public class P1Command : Command
    {
        public P1Command(Enm_CommandType _type) : base(_type)
        {
        }

        public override void Apply(GameObject gameObject)
        {
            return;
        }

        public override bool CanApply()
        {
            return true;
        }
    }
}