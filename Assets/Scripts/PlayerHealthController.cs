using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
  public static PlayerHealthController instance;
  public int currenHealth;
  public int maxHealth;
  public float invincibleTime = 1f;

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
    

    if (!PlayerController.instance.IsInvincible())
    {
      AudioManager.instance.playSFX("Player Hurt");

      PlayerController.instance.MakeInvincible(invincibleTime);

      currenHealth--;
      if (currenHealth <= 0)
      {
        PlayerDeath();
      }

      UIController.instance.SetCurrentHealth(currenHealth);
    }
  }

  public void PlayerDeath()
  {
    AudioManager.instance.playSFX("Player Death");
    PlayerController.instance.gameObject.SetActive(false);
    UIController.instance.ActivateDeathScreen();
    AudioManager.instance.PlayGameOverMusic();
  }

  public void HealPlayer(int amount)
  {
    currenHealth += amount;
    if(currenHealth > maxHealth)
    {
      currenHealth = maxHealth;
    }

    UIController.instance.SetCurrentHealth(currenHealth);
  }
}
