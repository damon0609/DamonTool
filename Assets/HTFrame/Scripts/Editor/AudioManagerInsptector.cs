using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[GitHubURL ("https://github.com/")]
[CSDNURL ("https://passport.csdn.net/login?code=public", "Assets/HTFrame/Assets/Texture/02.png")]
[CustomEditor (typeof (AudioManager))]
public class AudioManagerInsptector : HTBaseEditor<AudioManager> {
    private AudioManager audioManager;

    protected override void OnDefaultEnable () {
        base.OnDefaultEnable ();
        audioManager = e as AudioManager;
    }

    protected override void OnDefaultInspectorGUI () {
        base.OnDefaultInspectorGUI ();
        audioManager.worldClip = ObjectField<AudioClip> ("background Clip", audioManager.worldClip);
        audioManager.worldVolume = FloatField ("worldVolue", 2.0f);
    }
}