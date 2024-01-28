using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Commands
{
    public abstract class Command 
    {
        public Enm_CommandType type;

        public Command(Enm_CommandType _type)
        {
            type = _type;
        }
        public abstract void Apply(GameObject gameObject);
        public abstract bool CanApply();
        
        public virtual void End()
        {
            Debug.Log("on end Called");
            onEnd?.Invoke();
        }
        public Action onEnd;
    }

    public enum Enm_CommandType
    {
        None=0,
        Move=1,
        Jump=2,
        Light=3,
        RotateLeft=4,
        RotateRight=5,
        P1=6,
        P2=7
    }
}