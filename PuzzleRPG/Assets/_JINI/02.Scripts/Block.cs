using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

    public class Block : MonoBehaviour, IPointerEnterHandler
    {
   
     public enum STATE { DISABLE, ABLE, SELECT, SEARCH, WAIT, PANG, HINT }
     public enum Type { RED, GREEN, BLUE, PURPLE }

     STATE state;
     public STATE State { get { return state; } set { state = value; } }
     protected Type type;

     // public float X{ get { return transform.position.x; } set {  X= value; } }
     // public float Y { get { return transform.position.y; } set { Y = value; } }

     static Board board;

     private void Awake()
     {
          State = STATE.DISABLE;
     }
     private void Start()
     {

     }
     public void Search(int x, int y)
     { //이동할 수 있는 블록 탐색
         if (State == STATE.SEARCH) return;

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
     public void Select(out Block block)
     {
         if (State == STATE.SELECT)
         {
             block = null;
             return;
         }
         block = this;
         State = STATE.SELECT;
     }
     public virtual void Skill()
     {

     }
 
     public void OnPointerEnter(PointerEventData eventData)
     {
        Debug.Log("a");
     }
}
