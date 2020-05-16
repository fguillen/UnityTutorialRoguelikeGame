using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableController : MonoBehaviour
{
  public GameObject[] pieces;

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
}
