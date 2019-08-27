using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : movingobj
{
    static public PlayerManager instance; //movingobj라는 스크립트가 적용된 모든 객체가 instance변수의 값을 공유
    
    
    
    public int playMusicTrack;
    public string walksound_1, walksound_2, walksound_3, walksound_4;
    public string currentMapName; //moveMap 스크립트에 잇는 transferMapName의 값을 저장
    public float runspeed; //달릴 때의 이동 속도  증가값
    
    
    private float applyRunSpeed;
    public bool canMove = true;
    private bool applyRunFlag = false;
    private float animatorspeed;
    private AudioManager theAudio; //오디오매니저
    private float soundcount;
    private BGMManager bgm; //브금매니저 

    public bool notMove = false;
    void Start()
    {
        queue= new Queue<string>();
        if (instance == null)
        {
            DontDestroyOnLoad(this);
            boxCollider = GetComponent<BoxCollider2D>();
            animator = GetComponent<Animator>(); //애니메이터 컴포넌트 받아옴
            animatorspeed = animator.speed;
            theAudio = FindObjectOfType<AudioManager>();
            bgm = FindObjectOfType<BGMManager>();
            bgm.Play(playMusicTrack);
            instance = this; //instance가 null이 아니게 됨
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    IEnumerator MoveCoroutine() //플레이어를 움직이게 하는 함수, 픽셀 단위 움직임을 구현하기 위해 딜레이를 넣어야 하여 코루틴으로 만들었다.
    {
        while (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0&&!notMove)
            {
                if (Input.GetKey(KeyCode.LeftShift)) //왼쪽 시프트 키를 누를 시 달리기
                {
                    animator.speed = animator.speed + animator.speed * runspeed;
                    applyRunSpeed = speed * runspeed;
                    applyRunFlag = true;
                }
                else
                {
                    animator.speed = animatorspeed;
                    applyRunSpeed = 0;
                    applyRunFlag = false;
                }

                vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"),
                    transform.position.z); //vector.Set을 이용하여 vector의 X에 Horizontal값, Y에 Vertical값을 넣어 주고 있다.

                if (vector.x != 0) //아래로 걷고 있을 때 방향전환되는 버그 수정
                    vector.y = 0;

                animator.SetFloat("DirX", vector.x); //파라미터 DirX에 vector.x값을 넣음
                animator.SetFloat("DirY", vector.y); //파라미터 DirY에 vector.y값을 넣음

                bool checkCollisionFlag = base.CheckCollision();
                if (checkCollisionFlag == true)
                {
                    animator.speed = animatorspeed; //반복문에 있는 필수 실행 연산문, 반복문 실행이 안될시 이상해짐
                    break;
                }
                
                animator.SetBool("Walking", true); //Walking을 true로 변경

                
                while (currentWalkCount < walkCount) //실제로 이동하는 코드
                {
                    if (vector.x != 0)
                    {
                        transform.Translate(vector.x * (speed + applyRunSpeed), 0, 0);
                    }
                    else if (vector.y != 0)
                    {
                        transform.Translate(0, vector.y * (speed + applyRunSpeed), 0);
                    }

                    if (applyRunFlag)
                    {
                        soundcount += runspeed;
                        currentWalkCount++;
                    }

                    currentWalkCount++;
                    soundcount += 1;
                    yield return new WaitForSeconds(0.01f);

                    if (soundcount > 20)
                    {
                        theAudio.Play(walksound_1);
                        soundcount = 0;
                    }
                }

                currentWalkCount = 0;
                animator.speed = animatorspeed;
            }
            canMove = true;
    }

        void Update()
        {
            if (canMove&&!notMove)
                {
                    if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
                    {
                        canMove = false;
                        StartCoroutine(MoveCoroutine());
                    }
                    else
                    {
                        animator.SetBool("Walking", false); //Walking을 false로 변경
                    }
                }

            if (notMove)
            {
                animator.SetBool("Walking", false); //Walking을 false로 변경
            }
        }
}
