using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    //데이터베이스가 필요한 이유 :
    //1. 씬 이동, 이벤트가 실행되었었는지 여부
    //2. 세이브/로드가 간편해짐, 데이터베이스만 불러보면 되므로
    //3. 미리 만들어두면 편한 아이템 관리가 쉽다
    public static DatabaseManager instance;
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

    public string[] var_name;
    public float[] var;

    public string[] switch_name;
    public bool[] switches;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
