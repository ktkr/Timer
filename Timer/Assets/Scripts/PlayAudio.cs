
using UnityEngine;

public class PlayAudio : MonoBehaviour
{

    public AudioSource track;

    private void Awake()
    {
        track = GetComponent<AudioSource>();
        track.Play();
    }

    private void Update()
    {
        if (!track.isPlaying) //self-destruct once done playing to reduce clutter
        {
            Destroy(gameObject);
        }
    }

}
