using DG.Tweening;
using UnityEngine;

namespace Commands
{
    public class RotateCommand : Command
    {
        public RotateCommand(Enm_CommandType _type , Enm_RotateCommandType _rotateCommandType) : base(_type)
        {
            rotateCommandType = _rotateCommandType;
        }

        private GameObject targetGameObject;
        private Enm_RotateCommandType rotateCommandType;
        
        #region Abstraction

        public override void Apply(GameObject gameObject)
        {
            targetGameObject = gameObject;
            if(!CanApply()) return;
            
            Rotate();
        }

        public override bool CanApply()
        {
            return true;
        }

        #endregion

        #region UtilityFunctions

        private void Rotate()
        {
            Vector3 targetRot = new Vector3();
            switch (rotateCommandType)
            {
                case Enm_RotateCommandType.Right:
                    targetRot = targetGameObject.transform.eulerAngles + new Vector3(0, 90, 0);
                    break;
                case Enm_RotateCommandType.Left:
                    targetRot = targetGameObject.transform.eulerAngles - new Vector3(0, 90, 0);
                    break;
            }
            DOVirtual.Vector3(targetGameObject.transform.eulerAngles, targetRot, 1f,
                x => targetGameObject.transform.rotation = Quaternion.Euler(x)).OnComplete(End);
        }

        #endregion
    }

    public enum Enm_RotateCommandType
    {
        Right=0,
        Left=1
    }
}