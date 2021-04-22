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
        public int X { get; set; }
        public int Y { get; set; }
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
        StartCoroutine(CoMove(list, duration));
    }

    //선택된 블록따라 플레이어 이동, 선택된 블록 비활성화
    IEnumerator CoMove(List<Block> list, float duration)
    {
        WaitForSeconds ws = new WaitForSeconds(0.5f);

        while (list.Count > 0)
        {
            Block first = list.First();
            //플레이어 이동
            transform.DOMove(first.transform.position, duration);
            yield return ws;
            //블록 비활성화
            first.gameObject.SetActive(false);
            //선택된 블록 리스트에서 삭제
            first.State = Block.STATE.PANG;
            list.Remove(first);

        }
    }
}
