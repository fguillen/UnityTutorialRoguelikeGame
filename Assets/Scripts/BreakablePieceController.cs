using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePieceController : MonoBehaviour
{
  private Vector3 moveDirection;
  public float moveSpeed = 3f;
  public float decelaration = 5f;
  public float lifeTime = 3f;
  public float fadeSpeed = 2f;
  public SpriteRenderer theRS;

  // Start is called before the first frame update
  void Start()
  {
    moveDirection.x = Random.Range(-1f, 1f);
    moveDirection.y = Random.Range(-1f, 1f);
    moveDirection.Normalize();
  }

  // Update is called once per frame
  void Update()
  {
    transform.position += moveDirection * moveSpeed * Time.deltaTime;

    moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, decelaration * Time.deltaTime);

    lifeTime -= Time.deltaTime;

    if(lifeTime <= 0)
    {
      theRS.color = new Color(theRS.color.r, theRS.color.g, theRS.color.b, Mathf.MoveTowards(theRS.color.a, 0f, fadeSpeed));

      if(theRS.color.a == 0)
      {
        Destroy(gameObject);
      }
    }
  }
}
