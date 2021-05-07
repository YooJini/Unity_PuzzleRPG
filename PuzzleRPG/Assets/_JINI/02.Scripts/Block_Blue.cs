using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Blue : Block
{
    private void Awake()
    {
        Type = TYPE.BLUE;
    }
    // Start is called before the first frame update
    private void Update()
    {
        if (State == STATE.PANG && !pang)
            Skill();
    }
    public override void Skill()
    {
        base.Skill();
        skillMgr.water.fillAmount += gauge_unit;
        pang = true;
        if (skillMgr.water.fillAmount >= 1f) skillMgr.waterBtn.interactable = true;
    }
}
