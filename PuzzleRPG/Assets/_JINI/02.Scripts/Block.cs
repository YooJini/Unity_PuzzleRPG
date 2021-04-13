using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Block : MonoBehaviour
{
    enum STATE { IDLE, WAIT, PANG }
    STATE state;


    private void OnMouseDown()
    {
       gameObject.SetActive(false);
    }
}
