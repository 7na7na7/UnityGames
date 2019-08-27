using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class moveMap : MonoBehaviour
{
    public string transferMapName; //이동할 맵의 이름

    private PlayerManager thePlayer;
    private CameraManager theCamera;
    private FadeManager theFade;
    private OrderManager theOrder;
    void Start()
    {
        theCamera= FindObjectOfType<CameraManager>();
        thePlayer = FindObjectOfType<PlayerManager>(); //다수객체
        theFade = FindObjectOfType<FadeManager>();
        theOrder = FindObjectOfType<OrderManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "player")
        {
            StartCoroutine(TransferCoroutine());
        }
    }

    IEnumerator TransferCoroutine()
    {
        //theFade.Flash();
        theFade.FadeOut();
        theOrder.NotMove();
        yield return new WaitForSeconds(1f);
        thePlayer.currentMapName = transferMapName;
        SceneManager.LoadScene(transferMapName);
        theCamera.CameraSpeedUp();
        //theCamera.transform.position = new Vector3(theCamera.target.transform.position.x,theCamera.target.transform.position.y,theCamera.transform.position.z);
        theOrder.Move();
        theFade.FadeIn();
    }
}

