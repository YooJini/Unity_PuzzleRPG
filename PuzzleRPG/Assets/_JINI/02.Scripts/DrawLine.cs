using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    GameObject linePrefab;
    LineRenderer lr;

    Vector2 _startPoint;

    private void Awake()
    {
        linePrefab = Resources.Load("Prefabs/Line") as GameObject;
      
    }
    // Start is called before the first frame update
    private void Start()
    {

       
        // DrawLineFunc(new Vector2(0, -3));
    }
    public void SetStartPoint(Vector2 startPoint)
    {
        _startPoint = startPoint;
    }
    public void DrawLineFunc(Vector2 endPoint)
    {
        Debug.Log("Draw");
        GameObject go = Instantiate(linePrefab);
        lr = go.GetComponent<LineRenderer>();
        lr.SetPosition(0, _startPoint);
        lr.SetPosition(1, endPoint);
        lr.startWidth = 0.5f;
        lr.endWidth = 0.5f;
        
    }
}
