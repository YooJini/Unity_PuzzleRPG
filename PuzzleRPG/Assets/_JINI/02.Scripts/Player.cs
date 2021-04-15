using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

    public class Player : MonoBehaviour,IPointerDownHandler
    {
        public enum STATE { IDLE, SELECT, MOVE }
        STATE state;
        public STATE State { get { return state; } set { state = value; } }
        public int X { get; set; }
        public int Y { get; set; }

         Board board;
    // Start is called before the first frame update
    void Start()
    {
       
    }
        // Update is called once per frame
        void Update()
        {

        }
    private void OnMouseDown()
    {
      
       
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("PP");
    }
}
