using System.Collections;
using UnityEngine;

public class RepeatingSound : MonoBehaviour
{
    public AudioSource source;
    public int playsPerCycle = 2;
    public float pauseSeconds = 5f;

    void Start()
    {
        StartCoroutine(SoundRoutine());
    }

    IEnumerator SoundRoutine()
    {
        while (true)
        {
            for (int i = 0; i < playsPerCycle; i++)
            {
                source.Play();
                yield return new WaitForSeconds(source.clip.length);
            }

            yield return new WaitForSeconds(pauseSeconds);
        }
    }
}
