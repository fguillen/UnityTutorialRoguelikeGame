using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
  public static PlayerHealthController instance;
  public int currenHealth;
  public int maxHealth;

  private void Awake()
  {
    instance = this;
  }

  // Start is called before the first frame update
  void Start()
  {
    currenHealth = maxHealth;
    UIController.instance.SetMaxHealth(maxHealth);
    UIController.instance.SetCurrentHealth(currenHealth);
  }

  // Update is called once per frame
  void Update()
  {
        
  }

  public void DamagePlayer()
  {
    currenHealth--;
    if(currenHealth <= 0)
    {
      PlayerController.instance.gameObject.SetActive(false);
      UIController.instance.ActivateDeathScreen();
    }

    UIController.instance.SetCurrentHealth(currenHealth);
  }
}
