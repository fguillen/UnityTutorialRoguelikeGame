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
  public LayerMask roomLayer;
  private GameObject firstRoom;
  private GameObject lastRoom;
  private List<GameObject> rooms = new List<GameObject>();
  public RoomsPrefabs roomsPrefabs;

  // Start is called before the first frame update
  void Start()
  {
    GenerateRooms();
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKey(KeyCode.Space))
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
  }

  void GenerateRooms()
  {
    firstRoom = Instantiate(templateRoom, generatorPoint.position, generatorPoint.rotation);
    firstRoom.GetComponent<SpriteRenderer>().color = firstRoomColor;

    for (int i = 0; i < numOfRooms; i++)
    {
      var direction = NewDirection();

      MoveGeneratorPoint(direction);

      while (IsNewPositionTaken())
      {
        MoveGeneratorPoint(direction);
      }

      GameObject newRoom = GenerateRoom();

      if (i == numOfRooms - 1)
      {
        lastRoom = newRoom;
        lastRoom.GetComponent<SpriteRenderer>().color = lastRoomColor;
      }
      else
      {
        rooms.Add(newRoom);
      }
    }
  }

  bool IsNewPositionTaken()
  {
    return (Physics2D.OverlapCircle(generatorPoint.position, .2f, roomLayer));
  }

  GameObject GenerateRoom()
  {
    return Instantiate(templateRoom, generatorPoint.position, generatorPoint.rotation);
  }

  void MoveGeneratorPoint(Direction direction)
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
  }

  Direction NewDirection() {
    return (Direction)Random.Range(0, 4);
  }
}

[System.Serializable]
public class RoomsPrefabs
{
  public GameObject roomR;
  public GameObject roomU;
  public GameObject roomL;
  public GameObject roomD;
  public GameObject roomRL;
  public GameObject roomUD;
  public GameObject roomUR;
  public GameObject roomRD;
  public GameObject roomDL;
  public GameObject roomUL;
  public GameObject roomURDL;
  public GameObject roomURL;
  public GameObject roomUDL;
  public GameObject roomRDL;
  public GameObject roomURD;
}