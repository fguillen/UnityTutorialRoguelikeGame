using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
  public static LevelManager instance;
  public string sceneToLoad;
  public float secondsToWait = 4f;
  public bool isPaused = false;  


  private void Awake()
  {
    instance = this;
  }

  // Start is called before the first frame update
  void Start()
  {
        
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      PauseToogle();
    }
  }

  public IEnumerator LevelEnd()
  {
    PlayerController.instance.SetCanMove(false);
    AudioManager.instance.PlayWinMusic();
    UIController.instance.FadeToBlack();

    yield return new WaitForSeconds(secondsToWait);

    SceneManager.LoadScene(sceneToLoad);
  }

  public void PauseToogle()
  {
    isPaused = !isPaused;

    if (isPaused)
    {
      UIController.instance.pauseScreen.SetActive(true);
      Time.timeScale = 0f;
    } else
    {
      UIController.instance.pauseScreen.SetActive(false);
      Time.timeScale = 1f;
    }
  }

}
