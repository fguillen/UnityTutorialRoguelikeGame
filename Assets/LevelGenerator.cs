using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
  public GameObject templateRoom;
  public Color firstRoomColor;
  public Color lastRoomColor;
  public Transform generatorPoint;
  private enum Direction {up, right, down, left};
  public int numOfRooms;
  private float xOffset = 18;
  private float yOffset = 10;

  // Start is called before the first frame update
  void Start()
  {
    Instantiate(templateRoom, generatorPoint.position, generatorPoint.rotation).GetComponent<SpriteRenderer>().color = firstRoomColor;

    for (int i = 0; i < numOfRooms; i++)
    {
      var direction = NewDirection();
      GenerateRoom(direction);
    }
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKey(KeyCode.Space))
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
  }

  void GenerateRoom(Direction direction)
  {
    switch (direction)
    {
      case Direction.up:
        generatorPoint.position += new Vector3(0f, yOffset, 0f);
        break;

      case Direction.right:
        generatorPoint.position += new Vector3(xOffset, 0f, 0f);
        break;

      case Direction.down:
        generatorPoint.position += new Vector3(0f, -yOffset, 0f);
        break;

      case Direction.left:
        generatorPoint.position += new Vector3(-xOffset, 0f, 0f);
        break;
    }

    Instantiate(templateRoom, generatorPoint.position, generatorPoint.rotation);
  }

  Direction NewDirection() {
    return (Direction)Random.Range(0, 4);
  }
}
