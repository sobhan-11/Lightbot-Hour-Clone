using System;
using UnityEngine;

namespace Utility
{
    [CreateAssetMenu(fileName = "LevelsData",menuName = "Data/LevelData")]
    public class SOLevelData : ScriptableObject
    {
        public SeasonData[] SeasonDatas;
        public int currentLevel=1;
        public int currentSeason=1;
        
        public Level GetCurrentLevel()
        {
            if (currentLevel > SeasonDatas[currentSeason - 1].Levels.Length)
            {
                currentSeason++;
                if (currentSeason > SeasonDatas.Length) 
                { 
                    currentSeason = 1;
                }
                currentLevel = 1;
            }
            return SeasonDatas[currentSeason - 1].Levels[currentLevel - 1];
        }
    }

    [Serializable]
    public class SeasonData
    {
        public Level[] Levels;
    }
}