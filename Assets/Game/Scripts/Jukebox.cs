using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Jukebox : MonoBehaviour
{
    [SerializeField]
    private AudioSource _musicSummon;

    [SerializeField]
    private AudioSource _musicFight;

    public void PlayFightMusic()
    {
        _musicSummon.DOFade(0.0f, 0.5f);
        _musicFight.DOFade(1.0f, 0.5f);
    }
}
