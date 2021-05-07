using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

    public class Block : MonoBehaviour, IPointerEnterHandler
    {
   
     public enum STATE { DISABLE, ABLE, SELECT, SEARCH, WAIT, PANG, HINT }
     public enum TYPE { RED, GREEN, BLUE, PURPLE }

     STATE state = STATE.DISABLE;
     TYPE type;
     public STATE State { get { return state; } set { state = value; } }
     public TYPE Type { get { return type; } set { type = value; } }

     public float X { get { return transform.position.x; } }
     public float Y { get { return transform.position.y; } }

     public int Index_X{ get; set; }
     public int Index_Y { get; set; }

    static Board board;
    static DrawLine drawLine;
    protected static SkillManager skillMgr;

    protected float gauge_unit = 0.1f;
    protected bool pang = false;
    SpriteRenderer sr;

    private void Start()
    {
        board = GameObject.Find("Board").GetComponent<Board>();
        drawLine = Camera.main.GetComponent<DrawLine>();
        skillMgr = GameObject.Find("GameObject").GetComponent<SkillManager>();
        sr = gameObject.GetComponent<SpriteRenderer>();
    }
  
       

     // public void Spawn()
     // {
     //     transform.localPosition = new Vector2(X, Y);
     //     state = State.IDLE;
     // }

     // public void SetPosition(int x, int y)
     // {
     //     X = x;
     //     Y = y;
     // }
    
     public virtual void Skill()
     {

     }
 
    //포인터가 해당블록을 가리키고 있을 때
    public void OnPointerEnter(PointerEventData eventData)
     {
        if (State == STATE.ABLE)
        {
            //select상태로 변경
            State = STATE.SELECT;
            //선택블록 리스트에 추가
            board.AddSelectedBlock(this);
            //이동할 수 있는 블록 찾기
            board.Search(Index_X, Index_Y,(int)Type,this);

            //라인 그리기
            drawLine.DrawLineFunc(transform.position);
            //라인의 시작점을 현재 블록의 위치로 설정
            drawLine.SetStartPoint(transform.position);           
        }
        else if(State==STATE.SELECT)
        {
            //라인 지우기
            drawLine.RemoveLine();
            drawLine.SetStartPoint(transform.position);
            board.ReleaseSelect();
            board.Search(Index_X, Index_Y, (int)Type,this);
        }
     }


    public void Able()
    {
        if(State==STATE.DISABLE)
        State = STATE.ABLE;
    }
    public void Disable()
    {
        if(State==STATE.ABLE)
       State = STATE.DISABLE;
    }
    public void SelectToAble()
    {
        State = STATE.ABLE;
    }
    //Pang!! -> 비활성화 되었던 블록이 
    //빈자리에 채워지도록 함 (재사용)
    public void Reuse()
    {
        
    }
   // public void Down()
   // {
   //     
   //     Debug.Log(Index_Y);
   // }
    public void SetIndex()
    {
        Index_X =(int) X + 3;
        Index_Y =(int) Y + 4;
    }
    public IEnumerator Down()
    {
        transform.DOMoveY(transform.position.y - 1, 0.5f);
        yield return new WaitForSeconds(1f);
        SetIndex();
    }
  //  public void Blind()
  //  {
  //      sr.enabled = false;
  //  }
}
//인덱스가 겹치면 플레이어 인덱스는 그대로 두고
//블록의 인덱스를 변경시킨다.
