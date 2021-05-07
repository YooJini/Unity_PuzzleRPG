using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        //해상도를 비율에 맞춰 화면에 딱맞게 보이도록 함
        Screen.SetResolution(Screen.width, Screen.width / 9 * 16, true);
    }

}
