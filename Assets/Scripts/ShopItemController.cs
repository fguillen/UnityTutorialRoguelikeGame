using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemController : MonoBehaviour
{
  public int coinsCost;
  public int upgradeAmount;
  public Text buyText;
  private bool inArea;
  public bool isHealthRestore;
  public bool isHealthUpgrade;
  public bool isWeapon;

  // Start is called before the first frame update
  void Start()
  {
    buyText.gameObject.SetActive(false);
  }

  // Update is called once per frame
  void Update()
  {
    if(inArea && Input.GetKeyDown(KeyCode.E))
    {
      if(LevelManager.instance.coins >= coinsCost)
      {
        PurchaseItem();
      }
      else
      {
        AudioManager.instance.playSFX("Shop Not Enough");
      }
    }
  }

  void PurchaseItem(){
    AudioManager.instance.playSFX("Shop Buy");
    LevelManager.instance.SpendCoins(coinsCost);

    if(isHealthRestore)
    {
      PlayerHealthController.instance.HealPlayer(PlayerHealthController.instance.maxHealth);
    }

    if(isHealthUpgrade)
    {
      PlayerHealthController.instance.UpgradeHealth(upgradeAmount);
    }

    gameObject.SetActive(false);
    inArea = false;
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if(other.CompareTag("Player"))
    {
      buyText.gameObject.SetActive(true);
      inArea = true;
    }
  }

  void OnTriggerExit2D(Collider2D other)
  {
    if(other.CompareTag("Player"))
    {
      buyText.gameObject.SetActive(false);
      inArea = false;
    }
  }
}
