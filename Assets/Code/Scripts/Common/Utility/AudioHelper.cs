using UnityEngine;

public static class AudioHelper
{
    public static float VolumeModifier = 1f;

    public static AudioSource PlayClip2D(AudioClip clip, float volume)
    {
        //create
        GameObject audioObject = new GameObject("Audio2D");
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        //configure
        audioSource.clip = clip;
        audioSource.volume = volume * VolumeModifier;
        //activate
        audioSource.Play();
        Object.Destroy(audioObject, clip.length);
        //return it
        return audioSource;
    }
}