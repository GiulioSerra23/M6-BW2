using UnityEngine;

public class AudioManager : GenericSingleton<AudioManager>
{
    [Header ("Sound Data")]
    [SerializeField] private SoundData[] _sounds;
    
    private AudioSource _audioSource;

    protected override void Awake()
    {
        base.Awake();
        _audioSource = GetComponent<AudioSource>();
    }

    public void Play(SoundID id)
    {
        foreach (var sound in _sounds)
        {
            if (sound.ID == id)
            {
                if (sound.Clips.Length == 0) return;

                AudioClip clip = sound.Clips[Random.Range(0, sound.Clips.Length)];
                _audioSource.pitch = Random.Range(0.95f, 1.05f);
                _audioSource.PlayOneShot(clip);
                return;
            }
        }
    }
}
