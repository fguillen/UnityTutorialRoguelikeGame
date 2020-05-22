using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
  public float waitToShowPressKeyMessageSeconds = 2f;
  public GameObject pressKeyText;
  public string sceneToLoad;

  // Start is called before the first frame update
  void Start()
  {
        
  }

  // Update is called once per frame
  void Update()
  {
    if(waitToShowPressKeyMessageSeconds > 0)
    {
      waitToShowPressKeyMessageSeconds -= Time.deltaTime;
      if(waitToShowPressKeyMessageSeconds <= 0)
      {
        pressKeyText.SetActive(true);
      }
    }

    if(waitToShowPressKeyMessageSeconds <= 0 && Input.anyKeyDown)
    {
      SceneManager.LoadScene(sceneToLoad);
    }
    
  }
}
