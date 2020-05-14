using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
  public float moveSpeed;
  public Rigidbody2D theRB;
  private Vector3 moveDirection;
  public float rangeToChasePlayer;

  // Start is called before the first frame update
  void Start()
  {
        
  }

  // Update is called once per frame
  void Update()
  {
    if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer)
    {
      moveDirection = PlayerController.instance.transform.position - transform.position;
      
    } else
    {
      moveDirection = Vector3.zero;
    }

    moveDirection.Normalize();
    theRB.velocity = moveDirection * moveSpeed;
  }
}
