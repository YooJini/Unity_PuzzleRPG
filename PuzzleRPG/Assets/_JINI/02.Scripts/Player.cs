using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

    public class Player : MonoBehaviour,IPointerDownHandler
    {
        public enum STATE { IDLE, SELECT, MOVE }
        STATE state = STATE.IDLE;
        public STATE State { get { return state; } set { state = value; } }
        public int X { get; set; }
        public int Y { get; set; }
        public int Index_X { get; set; }
        public int Index_Y { get; set; }

   
    // Start is called before the first frame update
    void Start()
    {
       
    }
        // Update is called once per frame
    void Update()
    {

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        State = STATE.SELECT;
        Camera.main.GetComponent<DrawLine>().SetStartPoint(transform.position);
        
        //라인 시작점 
        //플레이어와 인접한 블록만 활성화
       // Debug.Log("PP");
    }
}
