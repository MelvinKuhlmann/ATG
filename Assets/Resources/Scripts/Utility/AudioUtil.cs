using UnityEngine;

namespace Resources.Scripts.Utility
{
    public static class AudioUtil
    {
        public static void PlayClip(GameObject gameObject, AudioClip clip)
        {
            var audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}