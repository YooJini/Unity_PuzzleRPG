using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Board : MonoBehaviour
{
    //블록 이차원 배열
    public Block[,] blocks;
    //선택가능한 블록 리스트
    List<Block> ableBlockList;
    //선택된 블록 리스트
    List<Block> selectedBlockList;
    //터져서 사라진 블록 리스트
    List<Block> pangBlockList;

    static public int width = 7;
    static public int height = 9;
    static public int typeSize = 4;

    //블록을 타입별로 나누어 랜덤으로 할당하기 위한 배열
    GameObject[] type = new GameObject[typeSize];
    GameObject playerObj;

    public bool isAble = false;

    Vector2 mousePos;

    static Player player;
    static DrawLine drawLine;
    static Enemy enemy;

    GameObject enemyObj;


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
        //에너미 프리팹 로드
        enemyObj = Resources.Load("Prefabs/Enemy1") as GameObject;

        drawLine = Camera.main.GetComponent<DrawLine>();

    }
 
    private void OnEnable()
    {
        StartCoroutine(SetStage());
       
    }

    //스테이지 세팅
    //1. 플레이어 배치
    //2. 블록 배치
    IEnumerator SetStage()
    {

     SetPlayer();
     SetEnemy();
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
             block.SetIndex();
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
        //플레이어 오브젝트 좌표 설정
        go_p.transform.localPosition = new Vector2(0, -3f);
        go_p.name = "player";
        player = go_p.GetComponent<Player>();
        player.X = 0f;
        player.Y = -3f;
        player.Index_X = 3;
        player.Index_Y = 1;
    }

    //플레이어 주변에 있는 모든 블록을
    //선택가능한 블록 리스트에 넣고 able
    public void AbleAroundPlayer()
    { 
            int x = player.Index_X;
            int y = player.Index_Y;

           for (int i = x-1; i < x+2; i++)
           {
            if (i < width && i >= 0 )
            {
                for (int j = y - 1; j < y + 2; j++)
                {
                    if (i == x && j == y ) continue;
                    if (j < height && j >= 0)
                    {
                        if (!blocks[i, j]) continue;
                        ableBlockList.Add(blocks[i, j]);
                        blocks[i, j].Able();////////
                    }
                }
            }
           }
        
    }

    //에너미 배치
    public void SetEnemy()
    {
        GameObject go_e = Instantiate(enemyObj);
        enemy = go_e.GetComponent<Enemy>();
        enemy.Init(3, 0.5f);
        
    }
    public void Search(int index_x, int index_y,int type,Block block)
    {
        //선택가능한 블록 리스트에 있는 것들을 모두 disable 시키고
        //리스트를 초기화시킴
        for (int i = 0; i < ableBlockList.Count; i++)
        {
            ableBlockList[i].Disable();
        }
        //플레이어와 인접한 블록리스트 초기화
        ableBlockList.Clear();

        int x = index_x;
        int y = index_y;

        //리스트
        //리스트는 원소를 삭제하면 뒤에 있는 원소가 앞으로 당겨져 동적이다.

        //인접
        for (int i = x - 1; i < x + 2; i++)
        {
            if (i < width && i >= 0)
            {
                for (int j = y - 1; j < y + 2; j++)
                {
                    if (i == x && j == y) continue;
                    if (j < height && j >= 0)
                    {
                        if (!blocks[i, j]) continue;
                        //같은 타입
                        if ((int)(blocks[i, j].Type) == type)
                            ableBlockList.Add(blocks[i, j]);
                    }
                }
            }
        }
   
        //선택가능 블록리스트에 있는 블록들 able
       for (int i = 0; i < ableBlockList.Count; i++)
       {
           ableBlockList[i].Able();

       }
    }
    //선택 해제
    public void ReleaseSelect()
    {
        if (selectedBlockList.Count > 0)
        {
            selectedBlockList.Last().SelectToAble();
            selectedBlockList.Remove(selectedBlockList.Last());
        }
    }
    //선택블록 리스트에 추가
    public void AddSelectedBlock(Block block)
    {
        selectedBlockList.Add(block);
    }
    public void Pang()
    {
        if (selectedBlockList.Count > 0)
        {
            //선택된 블록 리스트의 마지막 블록 저장
            Block lastBlock = selectedBlockList.Last();


            //플레이어 이동
            //이동이 끝나고 나면 해당 함수 내에서 빈 자리를 계산하는 함수를 호출한다.
            player.Move(selectedBlockList, 0.5f);

            //선택블록 리스트에 있는 블록들의 인덱스를 이용하여
            //전체 블록 배열의 해당 인덱스 블록 값을 null로 바꿔줌


            //플레이어 상태를 초기화 시켜줌
            //선택된 블록중 마지막 원소의 인덱스값을 넘겨준다.
            //해당 인덱스값이 플레이어의 인덱스값이 된다.
            if (selectedBlockList.Count > 0)
            {
                player.InitPlayer(lastBlock.Index_X, lastBlock.Index_Y);
            }

            

            //인덱스를 인수로 넘겨주어 빈 공간을 계산하게 함
            //단, 마지막 블록은 제외함
            
             

            //라인 지우기
            drawLine.RemoveAll();
        }
    }

    //블록이 터지고 난 뒤에 벌어질 일들..
    //터진 팡들의 인덱스 정보를 이용하여 빈 공간을 채운다.
    //마지막 팡은 제외
    //그자리에 플레이어가 들어가 있으므로 빈 공간이 생기지 않기 때문임

    public void  AfterPang(int x, int y)
    {
        for (int i = y+1; i < height; i++)
        {
            StartCoroutine(blocks[x, i].Down());
            blocks[x, i - 1] = blocks[x, i];
        }

        StartCoroutine(blocks[player.Index_X, player.Index_Y].Down());
    }
    //pang 블록을 pang리스트에 추가하는 함수
    //재사용될 블록들임
    public void AddPangList(Block block)
    {
        pangBlockList.Add(block);
    }
 
   
}//
 //