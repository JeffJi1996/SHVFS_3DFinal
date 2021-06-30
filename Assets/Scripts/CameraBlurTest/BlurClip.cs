using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BlurClip : PlayableAsset
{
   // public ExposedReference<PostEffects> postEffects;
    public int times;
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<BlurBehaviour>.Create(graph);

        var blurBehaviour = playable.GetBehaviour();
        //blurBehaviour.postEffects = postEffects.Resolve(graph.GetResolver());

        blurBehaviour.times = times;

        return playable;
    }
}
