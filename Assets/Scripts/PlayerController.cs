using System;
using System.Collections;
using System.Collections.Generic;
using Commands;
using Managers;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components"), Space(5)] 
    [SerializeField] private ActionHandler actionHandler;


    public void PlayActions(Queue<Command> commands)
    {
        actionHandler.StartActions(commands);
    }

    public void StopActions()
    {
        actionHandler.StopActions();
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 origin = transform.position + (transform.forward * 2);
        Vector3 dir = -transform.up * 1.5f;
        Gizmos.color=Color.red;
        Gizmos.DrawRay(origin,dir);
    }
}
