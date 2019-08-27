using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceManager : MonoBehaviour
{
    public static ChoiceManager instance;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private AudioManager theAudio; //사운드 재생
    public string keySound;
    public string enterSound;

    private string question;
    private List<string> answerList;

    public GameObject go; //평소에 비활성화시킬 목적으로 선언 setActive
    public Text question_Text;
    public Text[] answer_Text;
    public GameObject[] answer_Panel;

    public Animator anim;

    public bool choiceing; //대기, ()=>c= !choiceing사용
    private bool keyInput; //키처리 활성화, 비활성화

    private int count; //배열의 크기
    private int result; //선택한 선택창

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    private void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
        answerList = new List<string>(); //리스트 초기화
        for (int i = 0; i <= answer_Text.Length; i++) //텍스트 초기화
        {
            answer_Text[i].text = "";
            answer_Panel[i].gameObject.SetActive(false); //처음엔 패널 비활성화
        }

        question_Text.text = "";
    }

    public void ShowChoice(Choice _choice)
    {
        choiceing = true;
        result = 0;
        question = _choice.question;
        for (int i = 0; i < _choice.answers.Length; i++) //대답 배열의 크기만큼 반복
        {
            answerList.Add(_choice.answers[i]);
            answer_Panel[i].SetActive(true); //배열의 크기만큼 반복하므로 활성화되는 패널 수가 대답배열만큼만 만들어짐
            count = i;
        }
        anim.SetBool("Appear", true);
        StartCoroutine(ChoiceCoroutine());
    }

    IEnumerator ChoiceCoroutine()
    {
        yield return new WaitForSeconds(0.15f); //애니메이션이 실행될 동안 기다림을 줌
    }

    IEnumerator TypingQuestion()
    {
        for (int i = 0; i < question.Length; i++) //질문 개수만큼 반복
        {
            question_Text.text += question[i]; //한글자씩 들어감
            yield return waitTime; //아까만든 waitTime
        }
    }
    //동시에 질문/대답(1~4)텍스트를 출력할 것이기 때문에 코루틴을 그만큼 추가해줌.
    //하나의 코루틴으로 하고 싶다면, 그 코루틴에 파라미터로 넘겨 줌
    IEnumerator TypingAnswer_1()
    {
        for (int i = 0; i < answerList[0].Length; i++)
        {
            answer_Text[0].text += answerList[0][i];
            yield return waitTime;
        }
    }
    IEnumerator TypingAnswer_1()
    {
        for (int i = 0; i < answerList[0].Length; i++)
        {
            answer_Text[0].text += answerList[0][i];
            yield return waitTime;
        }
    }
}
