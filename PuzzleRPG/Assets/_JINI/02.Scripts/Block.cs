using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

    public class Block : MonoBehaviour, IPointerEnterHandler
    {
   
     public enum STATE { DISABLE, ABLE, SELECT, SEARCH, WAIT, PANG, HINT }
     public enum TYPE { RED, GREEN, BLUE, PURPLE }

     STATE state = STATE.DISABLE;
     TYPE type;
     public STATE State { get { return state; } set { state = value; } }
     public TYPE Type { get { return type; } set { type = value; } }

      public int Index_X{ get; set; }
      public int Index_Y { get; set; }

     static Board board;


    private void Start()
    {
        board = GameObject.Find("Board").GetComponent<Board>();
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
 
     public void OnPointerEnter(PointerEventData eventData)
     {
        Debug.Log(State);
        if (State == STATE.ABLE)
        {
            State = STATE.SELECT;
            board.AddSelectedBlock(this);
            //이동할 수 있는 블록 찾기
            board.Search(Index_X, Index_Y,(int)Type);

            //라인 그리기
            Camera.main.GetComponent<DrawLine>().DrawLineFunc(transform.position);
            //라인의 시작점을 현재 블록의 위치로 설정
            Camera.main.GetComponent<DrawLine>().SetStartPoint(transform.position);           
        }
        else if(State==STATE.SELECT)
        {
            //라인 지우기
            Debug.Log("SSS");
            Camera.main.GetComponent<DrawLine>().RemoveLine();
            Camera.main.GetComponent<DrawLine>().SetStartPoint(transform.position);
            board.ReleaseSelect();
            board.Search(Index_X, Index_Y, (int)Type);
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
}
