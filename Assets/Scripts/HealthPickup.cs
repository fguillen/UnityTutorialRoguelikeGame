using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
  public int healAmount = 1;
  public float idleTime = .5f;

  // Start is called before the first frame update
  void Start()
  {
    
  }

  // Update is called once per frame
  void Update()
  {

    CheckIdle();
  }

  private void CheckIdle()
  {
    if(idleTime > 0) {
      idleTime -= Time.deltaTime;
    }
  }

  private bool IsIdle()
  {
    return idleTime > 0;
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player") && !IsIdle())
    {
      PlayerHealthController.instance.HealPlayer(healAmount);
      Destroy(gameObject);
    }
  }
}
