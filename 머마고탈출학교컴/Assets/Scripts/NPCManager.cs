using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class NPCMove
{
    [Tooltip("NPCMove를 체크하면 NPC가 움직임")]
    public bool NPCmove;
    public string[] direction; //npc가 움직일 방향 설정
    [Range(1,5)] [Tooltip(("1=천천히, 2=조금 천천히, 3=보통, 4=빠르게, 5=연속적으로"))]//frequency에 스크롤바 1부터 5까지를 설정할 수 있도록 해 줌
    public int frequency; //npc가 움직일 방향으로 얼마나 빠른 속도로 움직일 것인가(빈도)
}
public class NPCManager : movingobj
{
    [SerializeField]
    public NPCMove npc;
    void Start()
    {
        queue= new Queue<string>();
    }

    public void SetMove()
    {
        StartCoroutine(MoveCoroutine());
    }

    public void SetnotMove()
    {
        StopAllCoroutines();
    }

    IEnumerator MoveCoroutine()
    {
        if (npc.direction.Length != 0)
        {
            for (int i = 0; i < npc.direction.Length; i++)
            {
                switch (npc.frequency)
                {
                    case 1:
                        yield return new WaitForSeconds(4f);
                        break;
                    case 2:
                        yield return new WaitForSeconds(3f);
                        break;
                    case 3 :
                        yield return new WaitForSeconds(2f);
                        break;
                    case 4:
                        yield return new WaitForSeconds(1f);
                        break;
                    case 5: 
                        yield return new WaitUntil(()=>queue.Count<2);
                        break;
                } 
                base.Move(npc.direction[i],npc.frequency);
                if (i == npc.direction.Length - 1)
                {
                    i = -1;
                    //i++로 가산이 이루어지면 i가 다시 0이 되어 반복이 됨
                }
            }
        }
    }
}
