using DG.Tweening;
using UnityEngine;

namespace Commands
{
    public class JumpCommand : Command
    {
        public JumpCommand(Enm_CommandType _type) : base(_type)
        {
        }

        private GameObject targetGameObject;
        private Vector3 targetPosition;

        #region Abstraction

        public override void Apply(GameObject gameObject)
        {
            targetGameObject = gameObject;
            if (!CanApply()) return;

            Jump();
        }

        public override bool CanApply()
        {
            return CanJump();
        }

        #endregion

        #region UtilityFunctions

        private void Jump()
        {
            targetGameObject.transform.DOMove(targetPosition, 1f).OnComplete(End);
        }

        private bool CanJump()
        {
            //raycast for check if block is in front
            if (CanJumpUp())
            {
                return true;
            }
            else
            {
                //check for can jump down or not
                return CanJumpDown();
            }
        }

        private bool CanJumpUp()
        {
            //raycast for check if block is in front so can jump up
            var hit = new RaycastHit();

            Physics.Raycast(targetGameObject.transform.position, targetGameObject.transform.forward, out hit, 2f,
                LayerMask.GetMask("Cube"));
            
            if (hit.collider != null && !Physics.Raycast(targetGameObject.transform.position+(Vector3.up * 1.5f),
                    targetGameObject.transform.forward,2f,LayerMask.GetMask("Cube"))) //wall is infront && Not too High For Jump
            {
                targetPosition = hit.collider.gameObject.transform.position + (Vector3.up * 1.5f);
                return true;
            }
            
            return false;
        }

        private bool CanJumpDown()
        {
            //check for can jump down or not
            var raycastHits = Physics.RaycastAll(targetGameObject.transform.position + (targetGameObject.transform.forward * 2),
                -targetGameObject.transform.up, 10f, LayerMask.GetMask("Cube"));
            
            if (raycastHits.Length==0) // No Ground infront of You
            {
                End();
                return false;
            }
            
            var hit = new RaycastHit();
            if (Physics.Raycast(targetGameObject.transform.position + (targetGameObject.transform.forward * 2f),
                    -targetGameObject.transform.up,out hit, 1.5f, LayerMask.GetMask("Cube")))//Ground is infront of you! you can MOVE not Jump!
            {
                End();
                return false;
            }
            
            //one ground is in front of you 
            targetPosition= raycastHits[0].collider.gameObject.transform.position + (Vector3.up * 1.5f);
            return true;
        }

        #endregion
    }
}