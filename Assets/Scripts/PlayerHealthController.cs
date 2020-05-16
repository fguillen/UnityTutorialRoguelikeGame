using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
  public static PlayerHealthController instance;
  public int currenHealth;
  public int maxHealth;
  private bool isInvincible;
  public float invicibleTime = 1f;
  private float invicibleCounter;

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
    CheckInvic();
  }

  public void DamagePlayer()
  {
    if (!isInvincible)
    {
      MakeInvic();

      currenHealth--;
      if (currenHealth <= 0)
      {
        PlayerController.instance.gameObject.SetActive(false);
        UIController.instance.ActivateDeathScreen();
      }

      UIController.instance.SetCurrentHealth(currenHealth);
    }
  }

  private void MakeInvic()
  {
    invicibleCounter = invicibleTime;
    isInvincible = true;
    PlayerController.instance.theBodySR.color = new Color(PlayerController.instance.theBodySR.color.r, PlayerController.instance.theBodySR.color.g, PlayerController.instance.theBodySR.color.b, 0.5f);
  }

  private void RemoveInvic()
  {
    isInvincible = false;
    PlayerController.instance.theBodySR.color = new Color(PlayerController.instance.theBodySR.color.r, PlayerController.instance.theBodySR.color.g, PlayerController.instance.theBodySR.color.b, 1f);
  }

  private void CheckInvic()
  {
    if (invicibleCounter > 0) {
      invicibleCounter -= Time.deltaTime;
      if (invicibleCounter <= 0)
      {
        RemoveInvic();
      }
    }
  }
}
