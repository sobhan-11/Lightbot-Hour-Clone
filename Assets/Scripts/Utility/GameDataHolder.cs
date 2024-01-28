using UnityEngine;

namespace Utility
{
    public class GameDataHolder : MonoBehaviour
    {
        [Header("Scriptables-Level"), Space(5)] 
        public SOLevelData levelData;

        [Header("Scriptables-UI"), Space(5)]
        public SOCommandUICardData commandUICardData;
        
        [Header("Game Data")] 
        public Material blueCubeMaterial;
        public Material lightCubeMaterial;
        
    }
}