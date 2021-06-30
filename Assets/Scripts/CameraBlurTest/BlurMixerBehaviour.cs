using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BlurMixerBehaviour : PlayableBehaviour
{
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        //拿到引用对象
        var trackBinding = playerData as PostEffects;
        float finalTimes = 0;

        //如果为空，直接返回
        if (!trackBinding)return;

        //拿到当前帧的clip个数
        int inputCount = playable.GetInputCount();

        for (int i = 0; i < inputCount; i++)
        {
            //拿到融合权重
            float inputWeight = playable.GetInputWeight(i);

            //拿到当前clip
            ScriptPlayable<BlurBehaviour> inputScriptPlayable = (ScriptPlayable<BlurBehaviour>) playable.GetInput(i);
            //拿到Behaviour
            BlurBehaviour input = inputScriptPlayable.GetBehaviour();
            //融合scale
            finalTimes += input.times * inputWeight;
        }

        trackBinding.times = (int)finalTimes;

    }
}
