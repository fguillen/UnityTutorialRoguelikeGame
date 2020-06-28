using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
  public bool closeWhenEnter = true;

  public bool isActive = false;
  public bool doorsOpen = true;

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

  private void OnTriggerExit2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      DeactivateRoom();
    }
  }

  private void ActivateRoom()
  {
    isActive = true;
    CameraController.instance.GoToTarget(transform);

    if (closeWhenEnter)
    {
      CloseDoors();
    }
  }

  private void DeactivateRoom()
  {
    isActive = false;
  }

  private void CloseDoors()
  {
    foreach (GameObject door in doors)
    {
      door.SetActive(true);
    }
    doorsOpen = false;
  }

  public void OpenDoors()
  {
    Debug.Log("XXX: OpenDoors");

    foreach (GameObject door in doors)
    {
      door.SetActive(false);
    }
    doorsOpen = true;
    closeWhenEnter = false;
  }
}
