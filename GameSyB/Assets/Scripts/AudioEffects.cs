using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffects: MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _pistolShot;
    [SerializeField] private AudioClip _bazukaShot;
    [SerializeField] private AudioClip _rocketBoom;
    [SerializeField] private AudioClip _destroyBase;
    [SerializeField] private AudioClip _nazatieNaKnopky;
    [SerializeField] private AudioClip _zombieAttack;
    [SerializeField] private AudioClip _woodBoxDestroy;
    [SerializeField] private AudioClip _zombieDead;
    [SerializeField] private AudioClip _perezaryadka;
    public static AudioSource audioSource;
    public static AudioClip pistolShoot;
    public static AudioClip bazukaShoot;
    public static AudioClip rocketboom;
    public static AudioClip destroyBase;
    public static AudioClip nazatieNaKnopky;
    public static AudioClip zombieAttack;
    public static AudioClip woodBoxDestroy;
    public static AudioClip zombieDead;
    public static AudioClip perezaryadka;

    private void Start()
    {
        audioSource = _audioSource;
        pistolShoot = _pistolShot;
        bazukaShoot = _bazukaShot;
        rocketboom = _rocketBoom;
        destroyBase = _destroyBase;
        nazatieNaKnopky = _nazatieNaKnopky;
        zombieAttack = _zombieAttack;
        woodBoxDestroy = _woodBoxDestroy;
        zombieDead = _zombieDead;
        perezaryadka = _perezaryadka;   
    }
}
