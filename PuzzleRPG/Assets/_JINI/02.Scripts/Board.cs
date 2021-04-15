using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Board : MonoBehaviour
{
    public Block[,] blocks;

    static public int width = 7;
    static public int height = 9;
    static public int typeSize = 4;

    GameObject[] type = new GameObject[typeSize];
    GameObject playerObj;

    public bool isAble = false;

    Vector2 mousePos;

    Player player;

    private void Awake()
    {
        blocks = new Block[width, height];

        //타입별 블록 프리팹 로드
        for (int i = 0; i < type.Length; i++)
        {
            type[i] = Resources.Load("Prefabs/Block_" + i.ToString()) as GameObject;
        }
        //플레이어 블록 로드
        playerObj = Resources.Load("Prefabs/Player") as GameObject;
    }
   
    private void Start()
    {
        
    }

    private void OnEnable()
    {
        StartCoroutine(SetStage());     
    }
    
    private void OnMouseDrag()
    {
        
    }
    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //
    //  if (Input.GetMouseButtonDown(0))
    //  {
    //      if (player.X + 0.5f > mousePos.x && mousePos.x > player.X - 0.5f && mousePos.y > player.Y - 0.5f && mousePos.y < player.Y + 0.5f)
    //          print("AA");
    //  }
    //  if (Input.GetMouseButton(0))
    //  {
    //     Block block =  blocks[(int)mousePos.x + 3, (int)mousePos.y + 4];
    //     
    //      
    //  }
    //  if (Input.GetMouseButtonUp(0))
    //  {
    //
    //  }
    }
 
      
    
    
      IEnumerator SetStage()
      {
          for (int i = 0; i < width ; i++)
          {
              for (int j = 0; j < height; j++)
              {
                int index = Random.Range(0, typeSize);
                GameObject go= Instantiate(type[index]);
                go.transform.localPosition = new Vector2(i - 3, j - 4);
                go.name = index.ToString();
                Block block = go.GetComponent<Block>();
                blocks[i, j] = block;
                yield return null;
              }
              yield return null;
          }
        SetPlayer();
      }
    public void SetPlayer()
    {
        GameObject go_p = Instantiate(playerObj);
        go_p.transform.localPosition = new Vector2(0, -3);
        go_p.name = "player";
        player = go_p.GetComponent<Player>();
        player.X = 0;
        player.Y = -3;
        blocks[3, 1].gameObject.SetActive(false);
        blocks[3, 1] = null;
    }
}//
 //