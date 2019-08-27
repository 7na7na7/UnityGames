using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;
    #region Singleton
    void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
    }
    #endregion Singleton

    public Text text;
    public SpriteRenderer rendererSprite;
    public SpriteRenderer rendererDialogueWindow;

    private List<string> listSentences;
    private List<Sprite> listSprites;
    private List<Sprite> listDialogueWindows; //커스텀 클래스 Dialogue의 배열을 리스트에 넣는다.
    //리스트는 확장, 삭제가 자유로워서 좋다!
    private int count; //대화 진행 상황 카운트.

    public Animator animSprite;
    public Animator animDialogueWindow;

    public bool talking = false;
    private bool keyActivated = false;
    
    public string typeSound;
    public string enterSound;
    
    private AudioManager theAudio;
    private OrderManager theOrder;

    void Start()
    {
        count = 0;
        text.text = "";
        listSentences = new List<string>(); 
        listSprites = new List<Sprite>();
        listDialogueWindows = new List<Sprite>();
        theAudio = FindObjectOfType<AudioManager>();
        theOrder = FindObjectOfType<OrderManager>();
    }

    public void ShowDialogue(Dialogue dialogue)
    {
        talking = false;
        theOrder.NotMove();
        for (int i = 0; i < dialogue.sentences.Length; i++)
        {
            listSentences.Add(dialogue.sentences[i]); //리스트에 배열값 넣기
            listSprites.Add(dialogue.sprites[i]);
            listDialogueWindows.Add(dialogue.dialogueWindows[i]);
        } 
        animSprite.SetBool("Appear",true);
        animDialogueWindow.SetBool("Appear",true);
        StartCoroutine(StartDialogueCoroutine());
        talking = true;
    }

    public void ExitDialogue()
    {
        count = 0;
        text.text = "";
        listSentences.Clear();
        listSprites.Clear();
        listDialogueWindows.Clear();
        animSprite.SetBool("Appear",false);
        animDialogueWindow.SetBool("Appear",false);
        StartCoroutine(StartDialogueCoroutine());
        theOrder.Move();
    }
    IEnumerator StartDialogueCoroutine()
    {
        if (count > 0)
        {
            if (listDialogueWindows[count] != listDialogueWindows[count - 1]) //대사바가 달라질 경우 대사바와 캐릭터 이미지 교체
            {
                animSprite.SetBool("Change",true);
                animDialogueWindow.SetBool("Appear", false);
                yield return new WaitForSeconds(0.2f);
                rendererDialogueWindow.GetComponent<SpriteRenderer>().sprite = listDialogueWindows[count];
                rendererSprite.GetComponent<SpriteRenderer>().sprite = listSprites[count];
                animDialogueWindow.SetBool("Appear", true);
                animSprite.SetBool("Change",false);
            }
            else //대사바가 똑같으면
            {
                if (listSprites[count] != listSprites[count - 1]) //스프라이트가 교체될 경우
                {
                    animSprite.SetBool("Change",true);
                    yield return new WaitForSeconds(0.1f);
                    rendererSprite.GetComponent<SpriteRenderer>().sprite = listSprites[count];
                    animSprite.SetBool("Change",false);
                }
                else
                {
                    yield return new WaitForSeconds(0.05f);
                }
            }
        }
        else //count가 0일 경우 첫 이미지이므로 무조건 교체
        {
            rendererDialogueWindow.GetComponent<SpriteRenderer>().sprite = listDialogueWindows[count];
            rendererSprite.GetComponent<SpriteRenderer>().sprite = listSprites[count];
        }

        keyActivated = true;
        
        for (int i = 0; i < listSentences[count].Length; i++)
        {
            text.text += listSentences[count][i]; //1글자씩 출력
            if (i % 7 == 1)
            {
                theAudio.Play(typeSound);
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
    void Update()
    {
        if (talking==true&& keyActivated==true)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                keyActivated = false;
                count++;
                text.text = "";
                theAudio.Play(enterSound);
                if (count == listSentences.Count) //카운트가 문장개수와 같을 경우
                {
                    StopAllCoroutines(); //코루틴 종료
                    ExitDialogue();
                }
                else
                {
                    StopAllCoroutines();
                    StartCoroutine(StartDialogueCoroutine());
                }
            }
        }
    }
}

// 오늘 면접 힘내라구!