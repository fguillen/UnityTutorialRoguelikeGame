using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
  public static LevelManager instance;
  public string sceneToLoad;
  public float secondsToWait = 4f;

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
        
  }

  public IEnumerator LevelEnd()
  {
    AudioManager.instance.PlayWinMusic();

    yield return new WaitForSeconds(secondsToWait);

    SceneManager.LoadScene(sceneToLoad);
  }

}
