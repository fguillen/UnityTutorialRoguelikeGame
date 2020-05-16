using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
  public static UIController instance;
  public Slider healthSlider;
  public GameObject deathScreen;
  public Text healthText;
  private int maxHealth;
  private int currentHealth;

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
        
  }
}
