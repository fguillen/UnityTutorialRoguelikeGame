using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
  public static AudioManager instance;

  public AudioSource winMusic;
  public AudioSource gameOverMusic;
  public AudioSource gameMusic;

  // Start is called before the first frame update
  void Start()
  {
    instance = this;
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
