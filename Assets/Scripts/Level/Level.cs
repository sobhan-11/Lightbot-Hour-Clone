using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level : MonoBehaviour
{
    public LevelData Data;

    public bool IsAllLightableCubeLighted()
    {
        if(Data==null) return false;

        return !Data.lightableCubes.Any(x => !x.isLighted);
    }

    public void ResetLevel()
    {
        for (int i = 0; i < Data.lightableCubes.Length; i++)
        {
            Data.lightableCubes[i].ResetLightableCube();
        }
    }
    
    public void SetLightableCube(Cube cube)
    {
        if(Data==null) return;
        
        Data.lightableCubes.First(x=>x.id==cube.id).isLighted = true;
    }
}

[Serializable]
public class LevelData
{
    [Header("Config-Level"), Space(5)]
    public Vector3 levelSpawnPlace;
    public Enm_PlayerStartRotationType playerStartRotationType;

    [Header("Config-Cubes"), Space(5)] 
    public Cube[] lightableCubes;
    public Transform startCube;
    public float cubeheight;
}

public enum Enm_PlayerStartRotationType
{ 
    Forward=0,
    Back=1,
    Right=2,
    Left=3,
}
