using UnityEngine;

namespace Commands
{
    public class P2Command : Command
    {
        public P2Command(Enm_CommandType _type) : base(_type)
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