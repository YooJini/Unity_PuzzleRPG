using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Green : Block
{

    private void Awake()
    {
        Type = TYPE.GREEN;
    }
    // Update is called once per frame
    void Update()
    {
        if (State == STATE.PANG && !pang)
            Skill();
    }
    public override void Skill()
    {
        base.Skill();
        skillMgr.leaf.fillAmount += gauge_unit;
        pang = true;
        if (skillMgr.leaf.fillAmount >= 1f) skillMgr.leafBtn.interactable = true;
    }
}
