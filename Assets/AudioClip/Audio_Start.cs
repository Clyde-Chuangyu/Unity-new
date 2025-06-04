using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    [Header("Background Music")]
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] [Range(0f, 1f)] private float volume = 0.5f;
    
    private AudioSource audioSource;
    
    // 单例 - 确保只有一个背景音乐
    public static BackgroundMusicController instance;
    
    private void Awake()
    {
        
        
        // 设置音频组件
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;
        audioSource.volume = volume;
        audioSource.playOnAwake = false;
    }
    
    private void Start()
    {
        // 开始播放背景音乐
        if (backgroundMusic != null)
        {
            audioSource.Play();
        }
    }
}