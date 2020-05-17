using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableController : MonoBehaviour
{
  public GameObject[] pieces;
  public GameObject[] pickupsToDrop;
  public float percentageOfChancesToDrop = .3f;

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
      DropPickup();
    }
  }

  private void Break()
  {
    Destroy(gameObject);

    int piecesCount = Random.Range(1, pieces.Length);
    for (int i = 0; i < piecesCount; i++)
    {
      int pieceIndex = Random.Range(0, pieces.Length);
      Instantiate(pieces[pieceIndex], transform.position, transform.rotation);
    }
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
