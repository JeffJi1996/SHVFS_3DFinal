using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class BlurBehaviour : PlayableBehaviour
{
    //public PostEffects postEffects;
    public int times;
    //public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    //{
    //    //base.ProcessFrame(playable, info, playerData);
    //    //postEffects.times = times;
    //    var postEffect = (PostEffects) playerData;

    //    if (postEffect)
    //    {
    //        postEffect.times = times;
    //    }
    //}
}
