using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXPlayer : MonoBehaviour
{
    private AudioSource _source;

    private void Awake() => _source = GetComponent<AudioSource>();

    public void PlayClip(AudioClip clip) => _source.PlayOneShot(clip);
}
