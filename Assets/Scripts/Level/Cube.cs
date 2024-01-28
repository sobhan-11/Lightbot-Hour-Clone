using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [Header("Components"), Space(5)] 
    public MeshRenderer meshRenderer;

    
    [Header("Config"), Space(5)] 
    public Enm_CubeType type;
    [HideInInspector] public bool isLighted;


    #region LightUp
    
    public void LightUp()
    {
        if (isLighted)
        {
            //Remove Light Cube
            ResetLightableCube();
        }
        else
        {
            //Light Up Cube
            meshRenderer.material = LevelManager.instance.GameDataHolder.lightCubeMaterial;
            isLighted = true;
        }
        LevelManager.instance.onCubeLightUp?.Invoke();
    }

    public void ResetLightableCube()
    {
        meshRenderer.material = LevelManager.instance.GameDataHolder.blueCubeMaterial;
        isLighted = false;
    }

    #endregion

}

public enum Enm_CubeType
{
    Default=0,
    Lightable=1
}
