using System;
using System.Collections;
using System.Collections.Generic;
using Commands;
using UnityEngine;

namespace Managers
{
    public class ActionHandler : MonoBehaviour
    {
        private Queue<Command> actionsQueue;
        private Coroutine delayCoroutine;
        private bool isPlaying;

        private void Start()
        {
            actionsQueue=new Queue<Command>();
        }

        public void StartActions(Queue<Command> commandsQueue)
        {
            if(actionsQueue.Count>0) actionsQueue.Clear();
            
            actionsQueue = commandsQueue;

            isPlaying = true;
            ApplyActions();
        }

        public void StopActions()
        {
            if(!isPlaying) 
                return;

            isPlaying = false;
            
            if(delayCoroutine!=null) 
                StopCoroutine(delayCoroutine);
        }
        
        public void ApplyActions()
        {
            if(!isPlaying) return;
            
            Debug.Log("Player Actions with : "+actionsQueue.Count);
            
            if (actionsQueue.Count == 0)
            {
                Debug.Log("Actions Done !!");
                if(delayCoroutine!=null) StopCoroutine(delayCoroutine);
                delayCoroutine=StartCoroutine(EndDelayCoroutine());
                return;
            }
            
            var command = actionsQueue.Dequeue();
            command.onEnd = null;
            command.onEnd += ApplyActions;
            command.Apply(this.gameObject);
        }

        private IEnumerator EndDelayCoroutine()
        {
            yield return new WaitForSeconds(1f);
            LevelManager.instance.OnPlayerActionEnd?.Invoke();
        } 
    }
}