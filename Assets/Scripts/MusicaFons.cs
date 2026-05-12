using UnityEngine;

// Attach to a GameObject with an AudioSource (Loop = true, clip assigned in Inspector).
public class MusicaFons : MonoBehaviour
{
    void Start()
    {
        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null && !audio.isPlaying)
            audio.Play();
    }
}
