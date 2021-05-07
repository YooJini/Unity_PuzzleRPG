using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public Image fire;
    [HideInInspector]
    public Button fireBtn;
    public Image leaf;
    [HideInInspector]
    public Button leafBtn;
    public Image water;
    [HideInInspector]
    public Button waterBtn;
    public Image heart;
    [HideInInspector]
    public Button heartBtn;

    private void Awake()
    {
        
       fireBtn = fire.GetComponent<Button>();
       leafBtn = leaf.GetComponent<Button>();
       waterBtn = water.GetComponent<Button>();
       heartBtn = heart.GetComponent<Button>();
    }
 
}
