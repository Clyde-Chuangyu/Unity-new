using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("音效音源设置")]
    [SerializeField] private AudioSource musicSource; // 背景音乐专用
    [SerializeField] private AudioSource sfxSource;   // 音效专用

    [Header("玩家动作音效")]
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip secondJumpSound;
    [SerializeField] private AudioClip landSound;
    [SerializeField] private AudioClip walkSound;
    [SerializeField] private AudioClip dashSound;
    [SerializeField] private AudioClip wallSlideSound;
    [SerializeField] private AudioClip wallJumpSound;

    [Header("战斗音效")]
    [SerializeField] private AudioClip attackSound1;
    [SerializeField] private AudioClip attackSound2;
    [SerializeField] private AudioClip attackSound3;
    [SerializeField] private AudioClip counterAttackSound;
    [SerializeField] private AudioClip hitTakenSound;
    [SerializeField] private AudioClip deathSound;

    [Header("其他音效")]
    [SerializeField] private AudioClip swordThrowSound;
    [SerializeField] private AudioClip swordCatchSound;
    [SerializeField] private AudioClip swordAimSound;

    [SerializeField] private AudioClip boxSound;

    [Header("胜利音效")]
    [SerializeField] private AudioClip VictoryMusic;

    [Header("音量设置")]
    [Range(0f, 1f)] public float masterVolume = 1f;
    [Range(0f, 1f)] public float sfxVolume = 1f;
    [Range(0f, 1f)] public float musicVolume = 0.5f;

    private bool isWalking = false;

    private void Awake()
    {
        // 单例模式
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // 如果没有指定音源，自动创建
        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;
            musicSource.playOnAwake = false;
        }

        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.playOnAwake = false;
        }
    }

 
    #region 音效播放方法
    /// <summary>
    /// 播放音效
    /// </summary>
    private void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip, volume * sfxVolume * masterVolume);
        }
    }

    /// <summary>
    /// 播放循环音效
    /// </summary>
    private void PlayLoopingSFX(AudioClip clip, float volume = 1f)
    {
        if (clip != null && sfxSource != null)
        {
            if (sfxSource.clip != clip)
            {
                sfxSource.clip = clip;
                sfxSource.volume = volume * sfxVolume * masterVolume;
                sfxSource.loop = true;
                sfxSource.Play();
            }
        }
    }

    /// <summary>
    /// 停止循环音效
    /// </summary>
    public void StopLoopingSFX()
    {
        if (sfxSource != null && sfxSource.loop)
        {
            sfxSource.Stop();
            sfxSource.loop = false;
        }
    }
    #endregion

    #region 玩家动作音效
    public void PlayJumpSound()
    {
        PlaySFX(jumpSound);
    }

    public void PlaySecondJumpSound()
    {
        PlaySFX(secondJumpSound);
    }

    public void PlayLandSound()
    {
        PlaySFX(landSound);
    }

    public void PlayDashSound()
    {
        PlaySFX(dashSound);
    }

    public void PlayWallJumpSound()
    {
        PlaySFX(wallJumpSound);
    }

    public void PlayWalkSound()
    {
        if (!isWalking)
        {
            isWalking = true;

            // 直接播放，不依赖条件检查
            if (walkSound != null && sfxSource != null)
            {
                sfxSource.clip = walkSound;
                sfxSource.volume = 0.6f * sfxVolume * masterVolume;
                sfxSource.loop = true;
                sfxSource.Play();
            }
        }
    }

    public void StopWalkSound()
    {
        if (isWalking)
        {
            isWalking = false;
            StopLoopingSFX();
        }
    }

    public void PlayWallSlideSound()
    {
        PlayLoopingSFX(wallSlideSound, 0.4f);
    }

    public void StopWallSlideSound()
    {
        StopLoopingSFX();
    }
    #endregion

    #region 战斗音效
    public void PlayAttackSound(int comboIndex)
    {
        AudioClip attackClip = comboIndex switch
        {
            0 => attackSound1,
            1 => attackSound2,
            2 => attackSound3,
            _ => attackSound1
        };
        PlaySFX(attackClip);
    }

    public void PlayCounterAttackSound()
    {
        PlaySFX(counterAttackSound);
    }

    public void PlayHitTakenSound()
    {
        PlaySFX(hitTakenSound);
    }

    public void PlayDeathSound()
    {
        PlaySFX(deathSound);
    }
    #endregion

    #region 技能音效
    public void PlaySwordThrowSound()
    {
        PlaySFX(swordThrowSound);
    }
  public void PlayVictoryMusic()
    {
        PlaySFX(VictoryMusic);
    }
    public void PlaySwordCatchSound()
    {
        PlaySFX(swordCatchSound);
    }

    public void PlaySwordAimSound()
    {
        PlaySFX(swordAimSound);
    }

    public void PlayboxSound()
    {
        PlaySFX(boxSound);
    }
    #endregion

    

    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        if (musicSource != null)
        {
            musicSource.volume = musicVolume * masterVolume;
        }
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
    }

    public void SetMasterVolume(float volume)
    {
        masterVolume = Mathf.Clamp01(volume);
        if (musicSource != null)
        {
            musicSource.volume = musicVolume * masterVolume;
        }
    }
   
    
    
}