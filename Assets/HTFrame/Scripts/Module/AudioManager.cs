using System;
using UnityEngine;
using System.Reflection;
using System.Collections.Generic;
using DG.Tweening;

[InternalModule(HTFrameworkModuleType.Audio)]
public class AudioManager : InternalBaseModule
{
    private AudioSource mWorldBackground;
    private Dictionary<string, AudioSource> dic = new Dictionary<string, AudioSource>();


    public AudioClip worldClip;
    public float worldVolume;

    public override void OnInitialization()
    {
        base.OnInitialization();
        mWorldBackground = CreateAudio("worldBackground", 1, 10, 1);
        PlayerWorldBackground(worldClip, worldVolume, true);
    }

    public override void OnPause()
    {
        base.OnPause();
    }

    public override void OnPreparatory()
    {
        base.OnPreparatory();
        Debug.Log("audio preparatory");
    }

    public override void OnRefresh()
    {
        base.OnRefresh();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnPauseWorldBackground(true);
        }
    }

    public override void OnResume()
    {
        base.OnResume();
    }

    public override void OnTermination()
    {
        base.OnTermination();
    }

    public void OnPauseWorldBackground(bool isGradual)
    {
        if (mWorldBackground.isPlaying && mWorldBackground != null)
        {
            if (isGradual)
            {
                mWorldBackground.DOFade(0, 5).OnComplete(() =>
                {
                    mWorldBackground.volume = worldVolume;
                    mWorldBackground.Pause();
                });
            }
            else
            {
                mWorldBackground.Pause();
            }
        }
    }

    public void PlayerWorldBackground(AudioClip audioClip, float speed, bool loop)
    {
        if (mWorldBackground != null)
        {
            if (!mWorldBackground.isPlaying)
            {
                mWorldBackground.clip = audioClip;
                mWorldBackground.pitch = speed;
                mWorldBackground.loop = loop;
                mWorldBackground.Play();
            }
        }
    }


    private AudioSource CreateAudio(string goName, float volume, int priority, float speed)
    {
        GameObject go = new GameObject(goName);
        go.transform.SetParent(gameObject.transform);
        go.transform.position = Vector3.zero;
        go.transform.rotation = Quaternion.identity;
        go.transform.localScale = Vector3.one;

        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.volume = volume;
        audioSource.priority = priority;
        audioSource.pitch = speed;

        if (!dic.ContainsKey(goName))
            dic[goName] = audioSource;
        return audioSource;
    }
}
