using DG.Tweening;
using UnityEngine;

namespace Commands
{
    public class LightCommand : Command
    {
        public LightCommand(Enm_CommandType _type) : base(_type)
        {
        }
        
        private GameObject targetGameObject;
        private Cube lightableCube;
        private Material lightMaterial;

        #region Abstraction

        public override void Apply(GameObject gameObject)
        {
            targetGameObject = gameObject;

            if(!CanApply()) return;
            
            LightUp();
        }

        public override bool CanApply()
        {
            return CanLightUp();
        }

        #endregion

        #region UtilityFunctions

        private void LightUp()
        {
            lightableCube.LightUp();
            End();
        }

        private bool CanLightUp()
        {
            //raycast for check if ground is Lightable

            var hit = new RaycastHit();
            Physics.Raycast(targetGameObject.transform.position, -targetGameObject.transform.up,out hit, 2f,
                LayerMask.GetMask("Cube"));

            if (hit.collider != null)
            {
                var cube=hit.collider.gameObject.GetComponent<Cube>();
                if (cube.type == Enm_CubeType.Lightable)
                {
                    Debug.Log("Cube IS Lightable");
                    
                    lightableCube = cube;
                    return true;
                }
            }
            Debug.Log("Cube Not Lightable");
            End();
            return false;
        }

        #endregion
    }
}