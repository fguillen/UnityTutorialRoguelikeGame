using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableController : MonoBehaviour
{
  public GameObject[] pieces;
  public GameObject[] pickupsToDrop;
  public float percentageOfChancesToDrop = .3f;
  public int health = 2;

  // Start is called before the first frame update
  void Start()
  {
        
  }

  // Update is called once per frame
  void Update()
  {
        
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player") && PlayerController.instance.IsDashing())
    {
      Break();
      
    } else if (other.CompareTag("PlayerBullet"))
    {
      health--;
      if(health <= 0)
      {
        Break();
      }
    }
  }

  private void Break()
  {
    AudioManager.instance.playSFX("Box Breaking");

    Destroy(gameObject);

    int piecesCount = Random.Range(1, pieces.Length);
    for (int i = 0; i < piecesCount; i++)
    {
      int pieceIndex = Random.Range(0, pieces.Length);
      Instantiate(pieces[pieceIndex], transform.position, transform.rotation);
    }

    DropPickup();
  }

  private void DropPickup()
  {
    bool dropPickup = (Random.Range(0f, 1f) > percentageOfChancesToDrop);
    if(dropPickup)
    {
      int pickupIndex = Random.Range(0, pickupsToDrop.Length);
      GameObject pickup = pickupsToDrop[pickupIndex];

      Instantiate(pickup, transform.position, transform.rotation);
    }
  }
}
