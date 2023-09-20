
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("AudioSource")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clips")]
    public AudioClip background;
    public AudioClip AttackSound;
    public AudioClip Pain;
    public AudioClip GunShot;
    public AudioClip Portal;
    public AudioClip Blast;
    public AudioClip Spell;
    public AudioClip Shield;

    public static AudioManager me;

    private void Awake()
    {
        if (me)
        {
            Destroy(this.gameObject);

        }
        else
        {
            me = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
