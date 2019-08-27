using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    public string startPoint; //플레이어가 시작될 위치

    private PlayerManager thePlayer;
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerManager>();
        if (startPoint == thePlayer.currentMapName)
        {
            thePlayer.transform.position = this.transform.position;
        }
    }
}
