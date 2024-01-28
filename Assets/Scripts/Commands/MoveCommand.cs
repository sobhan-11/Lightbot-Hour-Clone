using System;
using DG.Tweening;
using UnityEngine;

namespace Commands
{
    public class MoveCommand : Command
    {
        public MoveCommand(Enm_CommandType _type) : base(_type)
        {
        }
        
        private GameObject targetGameObject;
        private Vector3 targetPosition;
        
        #region Abstraction

        public override void Apply(GameObject gameObject)
        {
            targetGameObject = gameObject;
            if(!CanApply()) return;
            
            Move();
        }

        public override bool CanApply()
        {
            return CanMove();
        }

        #endregion

        #region UtilityFunctions

        private void Move()
        {
            targetGameObject.transform.DOMove(targetPosition, 1f).OnComplete(End);
        }

        private bool CanMove()
        {
            //raycast for check if block is in front ==> return false : raycast a little down check for ground
            if (IsWallInfront())
            {
                End();
                return false;
            }
            return IsGrounded();
        }
        
        private bool IsGrounded()
        {
            //raycast a little down check for ground
            var hit= new RaycastHit();
            Vector3 origin = targetGameObject.transform.position + (targetGameObject.transform.forward * 2);
            var isHit=Physics.Raycast(origin, -targetGameObject.transform.up, out hit, 2f ,LayerMask.GetMask("Cube"));
            if (hit.collider == null)
            {
                End();
                return false;
            }
            
            targetPosition=hit.collider.gameObject.transform.position + (Vector3.up*1.5f);
            return true;
        }

        private bool IsWallInfront()
        {
            //raycast for check if block is in front
            bool isHitWall = Physics.Raycast(targetGameObject.transform.position,targetGameObject.transform.forward,1.5f,LayerMask.GetMask("Cube"));
            return isHitWall;
        }

        #endregion
    }
}