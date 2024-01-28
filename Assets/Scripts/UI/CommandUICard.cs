using System.Collections;
using System.Collections.Generic;
using Commands;
using UnityEngine;
using UnityEngine.UI;
using Utility;

public class CommandUICard : MonoBehaviour , IPooledObject
{
    public Command command;
    private ObjectPool pool;

    [Header("UI"), Space(5)] 
    public Image image;
    public Button button;

    public void SetPool(ObjectPool _pool)
    {
        pool = _pool;
    }

    public void BackToPool()
    {
        command = null;
        button.onClick.RemoveAllListeners();
        transform.SetParent(pool.transform);
        gameObject.SetActive(false);
    }
}
