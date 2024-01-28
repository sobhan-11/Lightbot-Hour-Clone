using System;
using System.Collections.Generic;
using System.Linq;
using Commands;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CommandUICardData",menuName = "Data/CommandUICardData")]
public class SOCommandUICardData : ScriptableObject
{
    [SerializeField] private UICardData[] Data;

    public Sprite GetSprite(Enm_CommandType type)
    {
        return Data.First(x => x.Type == type).Sprite;
    }
}

[Serializable]
public class UICardData
{
    public Enm_CommandType Type;
    public Sprite Sprite;
}
