using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Utility;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;
    public static LevelManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<LevelManager>();
            }

            return _instance;
        }
    }

    [Header("Components")] 
    public PanelManager PanelManager;
    public GameDataHolder GameDataHolder;
    public PlayerController playerController;

    [Header("Level")] 
    [SerializeField] private Transform levelSpawnPlace;
    private Level currentLevel;


    [Header("Actions")] 
    public Action onSpawnLevel;
    public Action onStartPlayLevel;
    public Action onSuccessLevel;
    public Action onFailLevel;
    public Action OnPlayerActionEnd;
    public Action onCubeLightUp;

    private void OnEnable()
    {
        onCubeLightUp+=OnCubeLightUp;
        onStartPlayLevel += OnPlayerActionsStart;
        OnPlayerActionEnd += OnPlayerActionsEnd;
        
        //Spawn Level
        SpawnLevel();
    }
    
    private void OnDisable()
    {
        onCubeLightUp-=OnCubeLightUp;
        onStartPlayLevel -= OnPlayerActionsStart;
        OnPlayerActionEnd -= OnPlayerActionsEnd;

    }

    #region HandleLevelSpawn
    
    public void NextLevel()
    {
        if(!currentLevel) return;
        
        playerController.gameObject.SetActive(false);
        PanelManager.ResetPanels();
        
        currentLevel.transform.DOMove(levelSpawnPlace.transform.position, 1f).OnComplete((() =>
        {
            Destroy(currentLevel.gameObject);
            currentLevel = null;
            SpawnLevel();
        }));
        GameDataHolder.levelData.currentLevel++;
    }
    
    private void SpawnLevel()
    {
        var levelGO = Instantiate(GameDataHolder.levelData.GetCurrentLevel().gameObject,levelSpawnPlace.transform.position,Quaternion.identity);
        currentLevel = levelGO.GetComponent<Level>();
        currentLevel.transform.DOMove(currentLevel.Data.levelSpawnPlace, 1f).OnComplete((() =>
        {
            //spawn player
            Debug.Log("Start Level");
            playerController.transform.position = currentLevel.Data.startCube.position + (Vector3.up * 1.5f);
            SetPlayerRotation(currentLevel.Data.playerStartRotationType);
            playerController.gameObject.SetActive(true);
            onSpawnLevel?.Invoke();
        }));
    }

    public void SelectLevel()
    { 
        playerController.gameObject.SetActive(false); 
        PanelManager.ResetPanels();

        if (currentLevel)
        {
            currentLevel.transform.DOMove(levelSpawnPlace.transform.position, 1f).OnComplete((() => 
            { 
                Destroy(currentLevel.gameObject); 
                currentLevel = null; 
                SpawnLevel();
            }));
        }
        else
        {
            SpawnLevel();
        }
    }

    public void ResetLevel()
    {
        //Level Reset
        currentLevel.ResetLevel();
        //Player Reset
        playerController.transform.position = currentLevel.Data.startCube.position + (Vector3.up * 1.5f);
        SetPlayerRotation(currentLevel.Data.playerStartRotationType);
        playerController.gameObject.SetActive(true);
        
        onSpawnLevel?.Invoke();
    }
    
    private void SetPlayerRotation(Enm_PlayerStartRotationType rotationType)
    {
        switch (rotationType)
        {
            case Enm_PlayerStartRotationType.Forward:
                playerController.transform.rotation=Quaternion.Euler(Vector3.zero);
                break;
            case Enm_PlayerStartRotationType.Back:
                playerController.transform.rotation=Quaternion.Euler(new Vector3(0,180,0));
                break;
            case Enm_PlayerStartRotationType.Right:
                playerController.transform.rotation=Quaternion.Euler(new Vector3(0,90,0));
                break;
            case Enm_PlayerStartRotationType.Left:
                playerController.transform.rotation=Quaternion.Euler(new Vector3(0,270,0));
                break;
        }
    }
    
    #endregion
    
    #region Level-Events
    
    private void OnCubeLightUp()
    {
        if (currentLevel.IsAllLightableCubeLighted())
        {
            onSuccessLevel?.Invoke();
            playerController.StopActions();
        }
    }

    private void OnPlayerActionsStart()
    {
        playerController.PlayActions(PanelManager.GatherPanelCommandQueue());
    }
    
    public void OnPlayerActionsEnd()
    {
        if (currentLevel.IsAllLightableCubeLighted())
        {
            onSuccessLevel?.Invoke();
        }
        else
        {
            onFailLevel?.Invoke();
        }
    }
    
    #endregion

}
