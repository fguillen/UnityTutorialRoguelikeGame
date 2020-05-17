using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
  public static AudioManager instance;

  public AudioSource winMusic;
  public AudioSource gameOverMusic;
  public AudioSource gameMusic;

  public AudioSource[] sfxs;
  private Hashtable sfxsTable;

  // Start is called before the first frame update
  void Start()
  {
    instance = this;

    sfxsTable = new Hashtable();
    foreach (AudioSource audioSource in sfxs)
    {
      sfxsTable.Add(audioSource.name, audioSource);
    }
  }

  public void playSFX(string name)
  {
    AudioSource sfx = (AudioSource)sfxsTable[name];
    sfx.Stop();
    sfx.Play();
  }


  // Update is called once per frame
  void Update()
  {
        
  }


  public void PlayWinMusic()
  {
    gameMusic.Stop();
    winMusic.Play();
  }

  public void PlayGameOverMusic()
  {
    gameMusic.Stop();
    gameOverMusic.Play();
  }
}
