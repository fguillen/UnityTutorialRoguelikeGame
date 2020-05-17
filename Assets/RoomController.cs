using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
  private bool closeWhenEnter = true;
  public GameObject[] doors;

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
    if (other.CompareTag("Player"))
    {
      ActivateRoom();
    }
  }

  private void ActivateRoom()
  {
    CameraController.instance.GoToTarget(transform);

    if (closeWhenEnter)
    {
      foreach (GameObject door in doors)
      {
        door.SetActive(true);
      }
    }
  }
}
