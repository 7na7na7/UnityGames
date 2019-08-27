using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OrderManager : MonoBehaviour
{
    static public OrderManager instance;
    private PlayerManager thePlayer; //이벤트 도중 키입력 처리 방지
    private List<movingobj> characters;
    
    //Add(), Remove(),Clear() 이 함수들로 리스트는 배열과 인수 값 조정 가능
    void Start()
    { 
        if (instance == null)
        {
            thePlayer = FindObjectOfType<PlayerManager>();
            DontDestroyOnLoad(this);
            instance = this; 
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }
    public void PreLoadCharacter()
    {
        characters = ToList();
    }
    public List<movingobj> ToList()
    {
        List<movingobj> tempList = new List<movingobj>();
        movingobj[] temp = FindObjectsOfType<movingobj>();

        for (int i = 0; i < temp.Length; i++) //for문을 이용해 리스트에 값을 넣어줌
        {
            tempList.Add(temp[i]);
        }
        Debug.Log(temp.Length);
        return tempList;
    }

    public void NotMove()
    {
        thePlayer.notMove = true;
    }

    public void Move()
    {
        thePlayer.notMove = false;
    }
    public void SetTransparent(string _name)
    {
        for (int i = 0; i < characters.Count; i++) //.count를 사용해 리스트의 크기 반환(배열의 length개념)
        {
            if (_name == characters[i].characterName)
            {
                characters[i].gameObject.SetActive(false);
            }
        }
    }

    public void SetUnTransparent(string _name)
    {
        for (int i = 0; i < characters.Count; i++) //.count를 사용해 리스트의 크기 반환(배열의 length개념)
        {
            if (_name == characters[i].characterName)
            {
                characters[i].gameObject.SetActive(true);
            }
        }
    }

    public void SetThorought(string _name)
    {
        for (int i = 0; i < characters.Count; i++) //.count를 사용해 리스트의 크기 반환(배열의 length개념)
        {
            if (_name == characters[i].characterName)
            {
                characters[i].boxCollider.enabled = false;
            }
        }
    }
    public void SetUnThorought(string _name)
    {
        for (int i = 0; i < characters.Count; i++) //.count를 사용해 리스트의 크기 반환(배열의 length개념)
        {
            if (_name == characters[i].characterName)
            {
                characters[i].boxCollider.enabled = true;
            }
        }
    }
    public void Move(string _name, string _dir)
    {
        for (int i = 0; i < characters.Count; i++) //.count를 사용해 리스트의 크기 반환(배열의 length개념)
        {
            if (_name == characters[i].characterName)
            {
                characters[i].Move(_dir);
            }
        }
    }

    public void Turn(string _name, string _dir)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (_name == characters[i].characterName)
            {
                characters[i].animator.SetFloat("DirX",0f);
                characters[i].animator.SetFloat("DirY",0f);
                switch (_dir)
                {
                    case "UP":
                        characters[i].animator.SetFloat("DirY",1f);
                        break;
                    case "DOWN":
                        characters[i].animator.SetFloat("DirY",-1f);
                        break;
                    case "LEFT": 
                        characters[i].animator.SetFloat("DirX",-1f);
                        break;
                    case "RIGHT":
                        characters[i].animator.SetFloat("DirX",1f);
                        break;
                }
            }
        }
    }
}
