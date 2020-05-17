using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  public static CameraController instance;

  private Transform target;
  public float moveSpeed = 30f;

  private void Awake()
  {
    instance = this;
  }

  // Start is called before the first frame update
  void Start()
  {
       
  }

  // Update is called once per frame
  void Update()
  {
    if (target != null)
    {
      transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), moveSpeed * Time.deltaTime);
    }
  }

  public void GoToTarget(Transform newTarget)
  {
    target = newTarget;
  }
}
