using System;
using System.Collections.Generic;
using System.Linq;
using Commands;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class PanelHandler : MonoBehaviour
    {
        [Header("Component"), Space(5)] 
        private PanelManager panelManager;
        
        [Header("Congif"), Space(5)]
        [HideInInspector] public List<CommandUICard> commandList;
        public Enm_PanelHandlerType type;
        public int maxCardCapacity;

        [Header("UI-ObjectPool"), Space(5)] 
        [SerializeField] private ObjectPool commandUICardPool;
        [SerializeField] private Transform cardParent;
        
        [Header("UI"), Space(5)] 
        [SerializeField] private Button selectionButton;
        [SerializeField] private GameObject selectionBorder;

        public void Init(PanelManager panelManager)
        {
            this.panelManager = panelManager;
            selectionButton.onClick.RemoveAllListeners();
            selectionButton.onClick.AddListener((() =>
            {
                panelManager.SetSelectedPanel(this);
            }));

            panelManager.onSelectedPanelChange += HandlePanelSelection;
        }

        private void OnDisable()
        {
            panelManager.onSelectedPanelChange -= HandlePanelSelection;
        }

        #region InitCardsToPanels
        
        public void AddCard(Enm_CommandType newCommand)
        {
            if(commandList.Count>=maxCardCapacity) return;
            
            switch (newCommand)
            {
                case Enm_CommandType.Move:
                    Command moveCommand = new MoveCommand(Enm_CommandType.Move);
                    InitCommandCard(moveCommand);
                    break;
                case Enm_CommandType.Jump:
                    Command jumpCommand = new JumpCommand(Enm_CommandType.Jump);
                    InitCommandCard(jumpCommand);
                    break;
                case Enm_CommandType.Light:
                    Command lightCommand = new LightCommand(Enm_CommandType.Light);
                    InitCommandCard(lightCommand);
                    break;
                case Enm_CommandType.RotateLeft:
                    Command rotateCommandLeft = new RotateCommand(Enm_CommandType.RotateLeft, Enm_RotateCommandType.Left);
                    InitCommandCard(rotateCommandLeft);
                    break;
                case Enm_CommandType.RotateRight:
                    Command rotateCommandRight = new RotateCommand(Enm_CommandType.RotateRight, Enm_RotateCommandType.Right);
                    InitCommandCard(rotateCommandRight);
                    break;
                case Enm_CommandType.P1:
                    if(panelManager.selectedPanel.type!=Enm_PanelHandlerType.Main) return; //avoid infinite loop
                    Command p1Command = new P1Command(Enm_CommandType.P1);
                    InitCommandCard(p1Command);
                    break;            
                case Enm_CommandType.P2:
                    if(panelManager.selectedPanel.type==Enm_PanelHandlerType.Proc2) return; //avoid infinite loop
                    Command p2Command = new P2Command(Enm_CommandType.P2);
                    InitCommandCard(p2Command);
                    break;
            
            }
        }

        private void InitCommandCard(Command command)
        {
            var card = GetCommandCard();
            if(card==null) return;
        
            card.gameObject.transform.SetParent(cardParent);
            card.image.sprite = LevelManager.instance.GameDataHolder.commandUICardData.GetSprite(command.type);
            card.command = command;
            card.button.onClick.AddListener((() =>
            {
                commandList.Remove(card);
                card.BackToPool();
            }));
            card.gameObject.SetActive(true);
            
            commandList.Add(card);
        }

        public void ClearPanel()
        {
            for (int i = 0; i < commandList.Count; i++)
            {
                commandList[i].BackToPool();
            }
            commandList.Clear();
        }
        
        #endregion

        #region Utility
        
        private CommandUICard GetCommandCard()
        {
            return commandUICardPool.GetObject<CommandUICard>();
        }
        private void HandlePanelSelection(PanelHandler selectedPanelHandler)
        {
            selectionBorder.SetActive(this==selectedPanelHandler);
        }

        #endregion
    }

    public enum Enm_PanelHandlerType
    {
        None=0,
        Main=1,
        Proc1=2,
        Proc2=3
    }
}