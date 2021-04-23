using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Linq;

public class Player : MonoBehaviour,IPointerDownHandler,IPointerEnterHandler,IPointerUpHandler
    {
        public enum STATE { IDLE, SELECT, MOVE }
        STATE state = STATE.IDLE;
        public STATE State { get { return state; } set { state = value; } }

        public float X { get; set; }
        public float Y { get; set; }
        public int Index_X { get; set; }
        public int Index_Y { get; set; }

     static Board board;
     static DrawLine drawLine;

    // Start is called before the first frame update
    void Start()
    {
        board = GameObject.Find("Board").GetComponent<Board>();
        drawLine = Camera.main.GetComponent<DrawLine>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (State == STATE.SELECT)
        {
            drawLine.RemoveLine();
            board.ReleaseSelect();
            drawLine.SetStartPoint(transform.position);
        }

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        State = STATE.SELECT;
       
        board.AbleAroundPlayer();
        drawLine.SetStartPoint(transform.position);
        
     //라인 시작점 
     //플레이어와 인접한 블록만 활성화
     // Debug.Log("PP");
    }
    public void OnPointerUp(PointerEventData eventData)
    {
      
        board.Pang();
    }
  
    public void InitPlayer(int x, int y)
    {
        State = STATE.IDLE;
        Index_X = x;
        Index_Y = y;
    }
    public void Move(List<Block> list,  float duration)
    {
        int originX = Index_X;
        int originY = Index_Y;

        StartCoroutine(CoMove(list, duration,originX,originY));
    }

    //선택된 블록따라 플레이어 이동, 선택된 블록 비활성화
    IEnumerator CoMove(List<Block> list, float duration,int index_x, int index_y)
    {

        WaitForSeconds ws = new WaitForSeconds(0.5f);

        //인수로 받아온 선택블록 리스트를 새로운 리스트에 저장
        //List<T>의 경우 참조형식이다.
        //따라서 단순히 tmpList=list; 이렇게 대입하면
        //주소값이 복사되는 것이기 때문에
        //list의 값이 변경될때 tmpList값도 같이 변경된다.
        //따라서 ToList()를 이용한다. (단 ToList()는 Linq에 정의되어 있다는 점을 주의할 것)
         List<Block> tmpList = list.ToList();
       
        while (list.Count > 0)
        {
            Block first = list.First();
            //플레이어 이동
            transform.DOMove(first.transform.position, duration);
            yield return ws;
            //블록 비활성화
            //first.gameObject.SetActive(false);
            first.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            //블록 상태 변경
            first.State = Block.STATE.PANG;
            //선택된 블록 리스트에서 삭제
            list.Remove(first);
        }

        //플레이어의 이동이 끝난 뒤에 벌어질 일..

        board.AfterPang(index_x, index_y);

     
        for (int i = 0; i < tmpList.Count-1; i++)
        {
            int x = tmpList[i].Index_X;
            int y = tmpList[i].Index_Y;
            board.AfterPang(x,y);
        }
       
       
    }
}
