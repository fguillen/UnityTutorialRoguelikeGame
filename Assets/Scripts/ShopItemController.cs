using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemController : MonoBehaviour
{
  public int coinsCost;
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
      Debug.Log("XXX: 1");

      if(LevelManager.instance.coins >= coinsCost)
      {
        Debug.Log("XXX: 2");
        PurchaseItem();
      }
    }
  }

  void PurchaseItem(){
    LevelManager.instance.SpendCoins(coinsCost);

    if(isHealthRestore)
    {
      Debug.Log("XXX: 3");
      PlayerHealthController.instance.HealPlayer(PlayerHealthController.instance.maxHealth);
    }
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
