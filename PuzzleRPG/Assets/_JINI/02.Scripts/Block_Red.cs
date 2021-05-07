using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block_Red : Block
{
    
   
    private void Awake()
    {
        Type = TYPE.RED;
    }
    private void Update()
    {
        if (State == STATE.PANG && !pang)
            Skill();
    }
    //빨간색 블록이 터질 때마다 게이지 상승
    //게이지가 full되었을 떄
    //Fire 버튼을 누를 수 있게 됨
    //버튼을 눌렀을 때 발생하는 스킬
    public override void Skill()
    {
        base.Skill();
        skillMgr.fire.fillAmount += gauge_unit;
        pang = true;
        if (skillMgr.fire.fillAmount >= 1f) skillMgr.fireBtn.interactable = true;
    }

}
