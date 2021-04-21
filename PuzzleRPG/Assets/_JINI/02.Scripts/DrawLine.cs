using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    GameObject linePrefab;
    LineRenderer lr;

    Vector2 _startPoint;
    //라인 오브젝트풀링
    Queue<GameObject> unusingLine;
    Stack<GameObject> usingLine;

    private void Awake()
    {
        linePrefab = Resources.Load("Prefabs/Line") as GameObject;
        unusingLine = new Queue<GameObject>();
        usingLine = new Stack<GameObject>();
      
    }
    // Start is called before the first frame update
    private void Start()
    {
        
        for (int i = 0; i < 10; i++)
        {
            
            GameObject go = Instantiate(linePrefab);
            go.SetActive(false);
            unusingLine.Enqueue(go);
           
        }

    }
    public void SetStartPoint(Vector2 startPoint)
    {
        _startPoint = startPoint;
    }
    public void DrawLineFunc(Vector2 endPoint)
    {
        GameObject go = unusingLine.Dequeue();
        go.SetActive(true);
        lr = go.GetComponent<LineRenderer>();
        lr.SetPosition(0, _startPoint);
        lr.SetPosition(1, endPoint);
        lr.startWidth = 0.5f;
        lr.endWidth = 0.5f;
        usingLine.Push(go);
        
    }
    public void RemoveLine()
    {
        if (usingLine.Count > 0)
        {
            GameObject go = usingLine.Pop();
            go.SetActive(false);
            unusingLine.Enqueue(go);
            
        }
        
    }
    public void RemoveAll()
    {
        //리스트를 앞에서부터 순차적으로 삭제하면
        //앞으로 당겨지면서 인덱스가 동적으로 변하기 때문에
        //뒤에서부터 순차적으로 삭제한다.
        for (int i = usingLine.Count; i > 0; i--)
        {
            RemoveLine();
        }
    }
}
