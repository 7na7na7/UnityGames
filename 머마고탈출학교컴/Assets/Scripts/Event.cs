using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{
    public string direction;
    public Dialogue dialogue;
    
    private PlayerManager thePlayer; //animator.getFloat "DirY" == 1f 조건용도
    private DialogManager theDM;
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerManager>();
        theDM = FindObjectOfType<DialogManager>();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        //플래그가 켜지지 않은 상태이고, Z키를 누르고 있으며, 플레이어가 위를 향하고 있을 경우 이벤트 실행
            if (Input.GetKeyDown(KeyCode.Z) && col.gameObject.name=="player")
            {
                switch (direction)
                {
                    case "UP":
                        if (thePlayer.animator.GetFloat("DirY") == 1f)
                        {
                            theDM.ShowDialogue(dialogue);
                        }
                        break;
                    case "DOWN":
                        if (thePlayer.animator.GetFloat("DirY") == -1f)
                        {
                            theDM.ShowDialogue(dialogue);
                        }
                        break;
                    case "RIGHT":
                        if (thePlayer.animator.GetFloat("DirX") == 1f)
                        {
                            theDM.ShowDialogue(dialogue);
                        }
                        break;
                    case "LEFT":
                        if (thePlayer.animator.GetFloat("DirX") == -1f)
                        {
                            theDM.ShowDialogue(dialogue);
                        }
                        break;
                }
                //StartCoroutine(EventCoroutine());
            }
    }

    /*
    public IEnumerator EventCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        theOrder.PreLoadCharacter(); //리스트를 채워 주는 작업
        
        theOrder.NotMove();
        
        theDM.ShowDialogue(dialogue_1);

        yield return new WaitUntil(() => theDM.talking == false); //대화가 끝날때까지 대기
        
        theOrder.Move("player","RIGHT");
        theOrder.Move("player","RIGHT");
        theOrder.Move("player","DOWN");
        
        //yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => thePlayer.queue.Count == 0);//큐가 0개일 때만 실행
        //이동을 다 마치면 큐가 0이 된다.
        theDM.ShowDialogue(dialogue_2);
        yield return new WaitUntil(() => theDM.talking == false); //대화가 끝날때까지 대기
        theOrder.Move();
    }
    */
}
