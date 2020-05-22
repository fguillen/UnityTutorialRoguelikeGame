using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
  public static UIController instance;
  public Slider healthSlider;
  public GameObject deathScreen;
  public Text healthText;
  private int maxHealth;
  private int currentHealth;
  public string startAgainScene;
  public string mainMenuScene;

  // FadeOut
  public Image fadeOutImage;
  public float fadeOutSeconds = 1f;
  private bool fadeToBlack = false;
  private bool fadeOutBlack = true;

  private void Awake()
  {
    instance = this;
  }

  public void SetMaxHealth(int value)
  {
    maxHealth = value;
    renderHealth();
  }

  public void SetCurrentHealth(int value)
  {
    currentHealth = value;
    renderHealth();
  }

  public void ActivateDeathScreen()
  {
    deathScreen.SetActive(true);
  }

  private void renderHealth()
  {
    healthSlider.maxValue = maxHealth;
    healthSlider.value = currentHealth;
    healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();
  }

  // Start is called before the first frame update
  void Start()
  {
        
  }

  // Update is called once per frame
  void Update()
  {
    Fading();
  }

  public void FadeToBlack()
  {
    fadeToBlack = true;
    fadeOutBlack = false;
  }

  void Fading()
  {
    if (fadeToBlack)
    {
      fadeOutImage.color = new Color(fadeOutImage.color.r, fadeOutImage.color.g, fadeOutImage.color.b, Mathf.MoveTowards(fadeOutImage.color.a, 1f, fadeOutSeconds * Time.deltaTime));

      if(fadeOutImage.color.a >= 1f)
      {
        fadeToBlack = false;
      }
    }

    if (fadeOutBlack)
    {
      fadeOutImage.color = new Color(fadeOutImage.color.r, fadeOutImage.color.g, fadeOutImage.color.b, Mathf.MoveTowards(fadeOutImage.color.a, 0f, fadeOutSeconds * Time.deltaTime));

      if (fadeOutImage.color.a <= 0f)
      {
        fadeOutBlack = false;
      }
    }
  }

  public void StartAgain()
  {
    SceneManager.LoadScene(startAgainScene);
  }

  public void MainMenu()
  {
    SceneManager.LoadScene(mainMenuScene);
  }
}
