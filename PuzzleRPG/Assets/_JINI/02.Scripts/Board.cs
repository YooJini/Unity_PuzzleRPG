using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    static public int x = 7;
    static public int y = 9;
    static public int typeSize = 4;

    GameObject[,] blocks = new GameObject[x, y];
    GameObject[] type = new GameObject[typeSize];

    private void Awake()
    {
        for (int i = 0; i < type.Length; i++)
        {
            type[i]= Resources.Load("Prefabs/Block_" + i.ToString())as GameObject;
        }
       
    }
    private void OnEnable()
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                GameObject block =  Instantiate(type[Random.Range(0,typeSize)]).gameObject;
                blocks[i, j] = block;
                block.transform.position = new Vector2(i-3, j-4);
            }
        }
    }
   
    private void OnMouseDrag()
    {

    }
    private void OnMouseUp()
    {

    }
}
