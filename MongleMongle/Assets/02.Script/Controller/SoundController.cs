using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    private static SoundController instance = null;
    public static SoundController Inst
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<SoundController>();

            return instance;
        }
    }

    [SerializeField]
    private List<AudioClip> m_aclistBGM = new List<AudioClip>();
    private List<AudioClip> m_aclistSound = new List<AudioClip>();

    private AudioSource m_asSound;
    [SerializeField]
    private AudioSource m_asBgm;

	// Use this for initialization
	void Awake ()
    {
        AudioClip[] acArr = Resources.LoadAll<AudioClip>("ThemeBGM");

        for(int i=0;i<acArr.Length;i++)
        {
            m_aclistBGM.Add(acArr[i]);
        }

        m_asSound = GetComponent<AudioSource>();
        m_asBgm = transform.GetChild(0).GetComponent<AudioSource>();
    }

    public void PlaySound(string sSoundName)
    {
        AudioClip ac = m_aclistSound.Find(a => a.name == sSoundName);

        m_asSound.PlayOneShot(ac);
    }

    public void PlaySoundAbleStop(string sSoundName)
    {
        AudioClip ac = m_aclistSound.Find(a => a.name == sSoundName);

        m_asSound.clip = ac;
        m_asSound.loop = false;
        m_asSound.Play();
    }

    public void StopPlayingSound()
    {
        m_asSound.Stop();
        m_asSound.clip = null;
    }

    public void PlayBGM(string sSoundName)
    {

        AudioClip ac = m_aclistBGM.Find(a => a.name == sSoundName);

        m_asBgm.clip = ac;
        m_asBgm.loop = true;
        m_asBgm.Play();
    }

    public void StopBGM()
    {
        m_asBgm.Stop();
    }

    public void ChangeBGM(string sSoundName)
    {
        StopBGM();
        PlayBGM(sSoundName);
    }
}
