using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(0.874f,120/255f,0)]
[TrackBindingType(typeof(PostEffects))]
[TrackClipType(typeof(BlurClip))]
public class BlurTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<BlurMixerBehaviour>.Create(graph,inputCount);
    }
}
