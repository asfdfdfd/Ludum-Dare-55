using UnityEngine;
using DG.Tweening;
using System;
using Unity.Mathematics;

public class Jukebox : MonoBehaviour
{
    [SerializeField]
    private AudioSource _musicSummon;

    [SerializeField]
    private AudioSource _musicFight;

    [SerializeField]
    private AudioSource _musicWin;

    [SerializeField]
    private AudioSource _musicLose;    

    [SerializeField]
    private AudioSource _soundKick1;    

    [SerializeField]
    private AudioSource _soundKick2;   

    [SerializeField]
    private AudioSource _soundKickShield1;       

    [SerializeField]
    private AudioSource _soundKickShield2;      

    [SerializeField]
    private AudioSource _soundSummoningPositive;

    [SerializeField]
    private AudioSource _soundSummoningNegative;

    [SerializeField]
    private AudioSource _soundBookOpen;    

    [SerializeField]
    private AudioSource _soundPaperSheets;    

    [SerializeField]
    private AudioSource _soundCoins;      

    public void PlayFightMusic()
    {
        _musicSummon.DOFade(0.0f, 0.5f);
        _musicFight.DOFade(1.0f, 0.5f);
    }

    public void PlaySummonMusic()
    {
        _musicSummon.DOFade(1.0f, 0.5f);
        _musicFight.DOFade(0.0f, 0.5f);      
    }    

    public void PlayWinMusic()
    {
        _musicWin.Play();
    }   

    public void PlayLoseMusic()
    {
        _musicLose.Play();
    } 

    public void PlayKickPlayer()
    {
        _soundKick1.Play();
    }      

    public void PlayKickShieldPlayer()
    {
        _soundKickShield1.Play();
    }    

    public void PlayKickEnemy()
    {
        _soundKick2.Play();
    }      

    public void PlayKickShieldEnemy()
    {
        _soundKickShield2.Play();
    }     

    public void PlaySummoningPositive()
    {
        _soundSummoningPositive.Play();
    }

    public void PlaySummoningNegative()
    {
        _soundSummoningNegative.Play();
    }    

    public void PlaySoundBookOpen()
    {
        _soundBookOpen.Play();
    }

    public void PlayBookSheets()
    {
        _soundPaperSheets.Play();
    }

    public void PlayCoins()
    {
        _soundCoins.Play();
    }
}
