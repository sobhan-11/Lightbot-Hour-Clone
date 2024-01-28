using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Commands;
using Managers;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    
    [Header("Config"), Space(5)] 
    public PanelHandler[] panelHandlers;
    [HideInInspector] public PanelHandler selectedPanel;
    private PanelHandler mainPanel;
    
    [Header("Actions")]
    public Action<PanelHandler> onSelectedPanelChange;

    private void OnEnable()
    {
        for (int i = 0; i < panelHandlers.Length; i++)
        {
            panelHandlers[i].Init(this);
            if (panelHandlers[i].type == Enm_PanelHandlerType.Main)//Set default selected panel Main
            {
                mainPanel = panelHandlers[i];
                SetSelectedPanel(panelHandlers[i]);
            }
        }
    }

    #region AddCommandToSelectedPanel

    public void AddCommandToSelectedPanel(int commandNum) //Add with command inputs buttons
    { 
        selectedPanel.AddCard((Enm_CommandType)commandNum);
    }

    #endregion
    
    #region PanelSelection
    
    public void SetSelectedPanel(PanelHandler panelHandler)
    {
        selectedPanel = panelHandler;
        onSelectedPanelChange?.Invoke(panelHandler);
    }

    public void ResetPanels()
    {
        for (int i = 0; i < panelHandlers.Length; i++)
        {
            panelHandlers[i].ClearPanel();
        }
    }
    
    #endregion
    
    public Queue<Command> GatherPanelCommandQueue() //MOST IMPORTANT FUNC IN GAME ! :)
    {
        List<Command> panelCommandsListMain = new List<Command>();
        List<Command> panelCommandsListP1 = new List<Command>();
        List<Command> panelCommandsListP2 = new List<Command>();

        #region Gather Proc2 Commands
        var panelHandler2 = panelHandlers.FirstOrDefault(x => x.type == Enm_PanelHandlerType.Proc2);
        if (panelHandler2)
        {
            var P2 = panelHandler2.commandList;
            for (int i = 0; i < P2.Count; i++)
            {
                if (P2[i].command.type == Enm_CommandType.P1 || P2[i].command.type == Enm_CommandType.P1) continue;
                panelCommandsListP2.Add(P2[i].command);
            }
        }
        #endregion
        
        #region Gather Proc1 Commands
        var panelHandler1 = panelHandlers.FirstOrDefault(x => x.type == Enm_PanelHandlerType.Proc1);
        if (panelHandler1)
        {
            var P1=panelHandler1.commandList;
            for (int i = 0; i < P1.Count; i++)
            {
                if (P1[i].command.type != Enm_CommandType.P1 || P1[i].command.type != Enm_CommandType.P1)
                {
                    panelCommandsListP1.Add(P1[i].command);
                    continue;
                }

                if (P1[i].command.type == Enm_CommandType.P2)
                    panelCommandsListP1.AddRange(panelCommandsListP2);
            }
        }
        #endregion
        
        #region Gather Main Commands
        
        for (int i = 0; i < mainPanel.commandList.Count; i++)
        {
            switch (mainPanel.commandList[i].command.type)
            {
                case Enm_CommandType.P1:
                    panelCommandsListMain.AddRange(panelCommandsListP1);
                    break;
                case Enm_CommandType.P2:
                    panelCommandsListMain.AddRange(panelCommandsListP2);
                    break;
                default:
                    panelCommandsListMain.Add(mainPanel.commandList[i].command);
                    break;
            }
        }
        
        #endregion

        //Create Queue
        Queue<Command> mainQueue = new Queue<Command>();
        for (int i = 0; i < panelCommandsListMain.Count; i++)
        {
            mainQueue.Enqueue(panelCommandsListMain[i]);
        }

        return mainQueue;
    } 

}
