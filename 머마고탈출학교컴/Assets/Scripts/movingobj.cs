using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class movingobj : MonoBehaviour
{
    private bool notCoroutine = false;
    public string characterName;
    public float speed; //이동속도
    protected Vector3 vector; //벡터값 저장
    public int walkCount; //픽셀 단위 이동 시 이 값이 크면 더 많은 픽셀을 한번에 건너뜀
    protected int currentWalkCount;
    public BoxCollider2D boxCollider; //충돌감지영역
    public LayerMask layerMask; //통과가 불가능한 레이어 설정
    public Animator animator;
    public Queue<string> queue;
    //FIFO, 선입선출 자료구조. queue <- a넣고 b넣고 c넣은 다음 빼면
    //뺀 순서대로(a,b,c)순서대로 빠진다
    //queue.Enqueue로 넣고
    //queue.Dequeue로 빼고
    
    protected bool CheckCollision()
    {
        RaycastHit2D hit;
        //RaycastHit2D란?
        //A지점에서 B지점으로 레이저를 발사했을 때, 레이저가 도달하면 hit에 Null이 반환,
        //레이저가 도달하지 못하면 hit에 레이저와 충돌한 방해물이 반환된다!
        Vector2 start = transform.position; //A지점은 현재 위치
        Vector2 end =
            start + new Vector2(vector.x * speed * walkCount, vector.y * speed * walkCount); //B지점은 자신이 이동할 위치

        boxCollider.enabled = false; //플레이어가 자기 자신과의 충돌 판정을 하는 것을 막기 위해 잠시 콜라이더 비활성화
        hit = Physics2D.Linecast(start, end, layerMask);
        boxCollider.enabled = true; //다시 활성화
        if (hit.transform != null) //만약 플레이어의 위치와 플레이어가 이동할 위치 사이에 장애물이 있을 경우
            return true;
        return false;
    }
    public void Move(string _dir, int _frequency=5) //frequency를 생략해도 됨, 생략하면 값은 5가 됨
    {
        queue.Enqueue(_dir);
        if (!notCoroutine)
        {
            notCoroutine = true;
            StartCoroutine(MoveCoroutine(_dir, _frequency));
        }
    }

    IEnumerator MoveCoroutine(string _dir, int _frequency)
    {
        while (queue.Count != 0)
        {
            string direction = queue.Dequeue();
            vector.Set(0,0,vector.z);
            switch (direction)
            {
                case "UP" :
                    vector.y = 1f;
                    break;
                case "DOWN":
                    vector.y = -1f;
                    break;
                case "RIGHT":
                    vector.x = 1f;
                    break;
                case "LEFT":
                    vector.x = -1;
                    break;
            }
            animator.SetFloat("DirX", vector.x); //파라미터 DirX에 vector.x값을 넣음
            animator.SetFloat("DirY", vector.y); //파라미터 DirY에 vector.y값을 넣음

            while (true)
            {
                bool checkCollisionFlag = CheckCollision();
                if (checkCollisionFlag)
                {
                    animator.SetBool("Walking",false);
                    yield return new WaitForSeconds(1f);
                }
                else
                {
                    break;
                }
            }


            animator.SetBool("Walking",true);
            while (currentWalkCount < walkCount)
            {
                transform.Translate(vector.x*speed, vector.y * speed, 0); //x와 y중 하나가 무조건 0이 되기 때문에 대각선 이동은 이루어지지 않는다
                currentWalkCount++;
                yield return new WaitForSeconds(0.01f);
            }
            currentWalkCount = 0;
            if(_frequency!=5) 
                animator.SetBool("Walking",false);
        }
        animator.SetBool("Walking",false);
        notCoroutine = false;
    }
}
