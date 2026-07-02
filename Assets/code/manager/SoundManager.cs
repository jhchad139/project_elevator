using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // 싱글
    public static SoundManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgmSource; // BGM용 스피커
    [SerializeField] private AudioSource sfxSource; // 효과음용 스피커

    [Header("Audio Clips")]
    [SerializeField] private AudioClip[] bgmClips;  // 배경음악들을 담아둘 배열
    [SerializeField] private AudioClip[] sfxClips;  // 효과음들을 담아둘 배열

    public float MasterVolume { get; private set; } = 0.1f; // 기본값 10%
    public float BgmVolume { get; private set; } = 0.1f; // 기본값 10%
    public float SfxVolume { get; private set; } = 0.1f; // 기본값 10%

    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }


    public void PlayBGM(int index, bool loop = true)
    {
    
        if (bgmSource == null || index < 0 || index >= bgmClips.Length) return;

        AudioClip clip = bgmClips[index];
        if (clip == null) return;

        // 이미 똑같은 배경음악이 나오고 있다면 중복 재생 안 하고 무시
        if (bgmSource.clip == clip && bgmSource.isPlaying) return;

        bgmSource.clip = clip;
        bgmSource.loop = loop;
        bgmSource.Play();
    }


    public void StopBGM()
    {
        if (bgmSource != null) bgmSource.Stop();
    }


    public void PlaySFX(int index, float speed = 1f)
    {
        if (sfxSource == null || index < 0 || index >= sfxClips.Length) return;

        AudioClip clip = sfxClips[index];
        if (clip == null) return;
        sfxSource.pitch = speed;
        sfxSource.PlayOneShot(clip);

    }

    public void SetBgmVolume(float value)
    {
        BgmVolume = value;
        UpdateBgmVolumes();
    }

    public void SetSfxVolume(float value)
    {
        SfxVolume = value;
        UpdateSfxVolumes();
    }
    public void SetMasterVolume(float value)
    {
        MasterVolume = value;
        UpdateBgmVolumes();
    }

    private void UpdateBgmVolumes()
    {
        if (bgmSource != null)
        {
            bgmSource.volume = BgmVolume * MasterVolume;
        }

    }
    private void UpdateSfxVolumes()
    {
        if (sfxSource != null)
        {
            sfxSource.volume = SfxVolume * MasterVolume;
        }

    }
}
