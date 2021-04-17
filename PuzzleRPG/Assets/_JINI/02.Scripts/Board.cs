using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class Board : MonoBehaviour
{
    public Block[,] blocks;
    List<Block> ableBlockList;
    List<Block> selectedBlockList;

    static public int width = 7;
    static public int height = 9;
    static public int typeSize = 4;

    GameObject[] type = new GameObject[typeSize];
    GameObject playerObj;

    public bool isAble = false;

    Vector2 mousePos;

    Player player;
    bool isAbleBlock = false;
    private void Awake()
    {
        blocks = new Block[width, height];
        ableBlockList = new List<Block>();
        selectedBlockList = new List<Block>();

        //타입별 블록 프리팹 로드
        for (int i = 0; i < type.Length; i++)
        {
            type[i] = Resources.Load("Prefabs/Block_" + i.ToString()) as GameObject;
        }
        //플레이어 블록 로드
        playerObj = Resources.Load("Prefabs/Player") as GameObject;

    }
 
    private void OnEnable()
    {
        StartCoroutine(SetStage());
       
    }
    private void Update()
    {
        if (!isAbleBlock)
        {
            AbleAroundPlayer();
        }
    }

    //스테이지 세팅
    //1. 플레이어 배치
    //2. 블록 배치
    IEnumerator SetStage()
    {

     SetPlayer();
     yield return null;

     for (int i = 0; i < width ; i++)
       {
           for (int j = 0; j < height; j++)
           {
                if (i == 3 && j == 1)
                {
                    blocks[i, j] = null;
                    continue;
                }
             int index = Random.Range(0, typeSize);
             GameObject go= Instantiate(type[index]);
             go.transform.localPosition = new Vector2(i - 3, j - 4);
             go.name = index.ToString();
             Block block = go.GetComponent<Block>();
             block.Index_X = i;
             block.Index_Y = j;
             blocks[i, j] = block;
             yield return null;
           }
           yield return null;
       }
      
    }
   
    //플레이어 배치
    public void SetPlayer()
    {
        GameObject go_p = Instantiate(playerObj);
        go_p.transform.localPosition = new Vector2(0, -3);
        go_p.name = "player";
        player = go_p.GetComponent<Player>();
        player.X = 0;
        player.Y = -3;
        player.Index_X = 3;
        player.Index_Y = 1;
    }

    //플레이어 주변에 있는 모든 블록 활성화
    public void AbleAroundPlayer()
    {
        if (player.State == Player.STATE.SELECT)
        {
            int x = player.Index_X;
            int y = player.Index_Y;
            isAbleBlock = true;

           for (int i = x-1; i < x+2; i++)
           {
               for (int j = y-1; j < y+2; j++)
               {
                   if (i == x && j == y) continue;

                    ableBlockList.Add(blocks[i, j]);
                    blocks[i, j].Able();
               }
           }
        }
    }

    public void Search(int index_x, int index_y,int type)
    {
        for (int i = 0; i < ableBlockList.Count; i++)
        {
            ableBlockList[i].Disable();
        }
        ableBlockList.Clear();
        int x = index_x;
        int y = index_y;

        //리스트
        //리스트는 원소를 삭제하면 뒤에 있는 원소가 앞으로 당겨져 동적이다.

        //인접, 같은 타입
        for (int i = x - 1; i < x + 2; i++)
        {
            for (int j = y - 1; j < y + 2; j++)
            {
                if (i == x && j == y || !blocks[i,j]) continue;
                if((int)(blocks[i,j].Type)==type)
                ableBlockList.Add(blocks[i, j]);
            }
        }
    
       
       for (int i = 0; i < ableBlockList.Count; i++)
       {
            //Debug.Log(i);
           ableBlockList[i].Able();

       }
    }
    public void ReleaseSelect()
    {
        selectedBlockList.Last().SelectToAble();
        selectedBlockList.Remove(selectedBlockList.Last());
    }
    public void AddSelectedBlock(Block block)
    {
        selectedBlockList.Add(block);
    }

}//
 //