using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Buttons")] 
    [SerializeField] private Button[] inputButtons;
    [SerializeField] private Button playButton;
    [SerializeField] private Button rePlayButton;
    [SerializeField] private Button nextLevelButton;

    private void Awake()
    {
        DeActivateInputButtonsIntractable();
    }

    private void OnEnable()
    {
        LevelManager.instance.onSpawnLevel += ActivateInputButtonsIntractable;
        LevelManager.instance.onSuccessLevel += OnSuccessLevel;
        LevelManager.instance.onFailLevel += OnFailLevel;
    }

    private void OnDisable()
    {
        LevelManager.instance.onSpawnLevel -= ActivateInputButtonsIntractable;
        LevelManager.instance.onSuccessLevel -= OnSuccessLevel;
        LevelManager.instance.onFailLevel -= OnFailLevel;
    }

    #region LevelEvents
    
    public void ActivateInputButtonsIntractable()
    {
        for (int i = 0; i < inputButtons.Length; i++)
        {
            inputButtons[i].interactable = true;
        }
    }   
    
    private void OnSuccessLevel()
    {
        nextLevelButton.gameObject.SetActive(true);
        rePlayButton.gameObject.SetActive(true);
        playButton.gameObject.SetActive(false);
    }   
    
    private void OnFailLevel()
    {
        nextLevelButton.gameObject.SetActive(false);  
        playButton.gameObject.SetActive(false);
        rePlayButton.gameObject.SetActive(true);
    }    
    public void DeActivateInputButtonsIntractable()
    {
        for (int i = 0; i < inputButtons.Length; i++)
        {
            inputButtons[i].interactable = false;
        }
    }
    

    #endregion

    #region UIInput
    
    public void PressPlayButton()
    {
        LevelManager.instance.onStartPlayLevel?.Invoke();
        DeActivateInputButtonsIntractable();
    }

    public void PressReplayButton()
    {
        LevelManager.instance.ResetLevel();
        //UI
        rePlayButton.gameObject.SetActive(false);
        nextLevelButton.gameObject.SetActive(false);
        playButton.gameObject.SetActive(true);
    }  
    
    public void PressNextLevelButton()
    {
        LevelManager.instance.NextLevel();
        //UI
        nextLevelButton.gameObject.SetActive(false); 
        rePlayButton.gameObject.SetActive(false);
        playButton.interactable = false;
        playButton.gameObject.SetActive(true);
    }

    public void SelectLevel(string value)
    {
        var data = value.ToString();
        LevelManager.instance.GameDataHolder.levelData.currentSeason = int.Parse(data[0].ToString()); // Season
        LevelManager.instance.GameDataHolder.levelData.currentLevel = int.Parse(data[1].ToString()); // Level
        
        LevelManager.instance.SelectLevel();
    }
    #endregion

}
