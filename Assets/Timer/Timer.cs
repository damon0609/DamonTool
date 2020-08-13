using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Timer {
    private MonoBehaviour m_CoroutineContainer;

    private class TimerProgressEvent : UnityEvent<float> { }
    private class TimerEndEvent:UnityEvent{}

    private TimerProgressEvent m_Target;
    private TimerEndEvent m_TimerEnd;
    private float m_Duration;
    private bool m_ignoreTimeScale;
    public bool ignoreTimeScale {
        get { return m_ignoreTimeScale; }
        set { m_ignoreTimeScale = value; }
    }

    public Timer (MonoBehaviour mono, float duration) {
        this.m_CoroutineContainer = mono;
        this.m_Duration = duration;
        this.m_Target = new TimerProgressEvent();
        this.m_TimerEnd = new TimerEndEvent();
    }

    private IEnumerator Run () {
        if (m_CoroutineContainer == null) yield break;
        float elapsedTime = 0.0f;
        while (elapsedTime < m_Duration) {
            elapsedTime += m_ignoreTimeScale?Time.unscaledDeltaTime : Time.deltaTime;
            float precentage = Mathf.Clamp01 (elapsedTime / m_Duration);
            if (m_Target != null) {
                m_Target.Invoke (precentage);
            }
            yield return null;
        }
        m_Target.Invoke (1.0f);
        m_TimerEnd.Invoke();
    }

    public void Start (UnityAction<float> progress,UnityAction end) {
        m_Target.AddListener (progress);
        m_TimerEnd.AddListener(end);
        if(m_CoroutineContainer.gameObject == null) return;
        Stop ();
        if (!m_CoroutineContainer.gameObject.activeInHierarchy) {
            return;
        }
        m_CoroutineContainer.StartCoroutine(Run());
    }

    private void Stop () {
        if (m_CoroutineContainer != null) {
            m_CoroutineContainer.StartCoroutine (Run ());
        }
    }

}