using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSyncer : MonoBehaviour
{
    public float bias;
    public float timeStep;
    public float timeToBeat;
    public float restSmoothTime;

    private float m_previousAudioValue;
    private float m_audioValue;
    private float m_timer;

    protected bool m_isBeat;

    private void Update()
    {
        OnUpdate();
    }

    public virtual void OnUpdate()
    {
        // update audio value
        m_previousAudioValue = m_audioValue;
        m_audioValue = AudioSpectrum.spectrumValue;

        // if audio value went below the bias during this frame
        if (m_previousAudioValue > bias &&
            m_audioValue <= bias)
        {
            // if minimum beat interval is reached
            if (m_timer > timeStep)
                OnBeat();
        }

        // if audio value went above the bias during this frame
        if (m_previousAudioValue <= bias &&
            m_audioValue > bias)
        {
            // if minimum beat interval is reached
            if (m_timer > timeStep)
                OnBeat();
        }

        m_timer += Time.deltaTime;
    }

    public virtual void OnBeat()
    {
        Debug.Log("beat");
        m_timer = 0;
        m_isBeat = true;
    }
}
