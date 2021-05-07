using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Purple : Block
{
    private void Awake()
    {
        Type = TYPE.PURPLE;
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
        skillMgr.heart.fillAmount += gauge_unit;
        pang = true;
        if (skillMgr.heart.fillAmount >= 1f) skillMgr.heartBtn.interactable = true;
    }

}
