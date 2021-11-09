using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathSystem : MonoBehaviour {

    public enum SeedType { RANDOM, CUSTOM }
    [Header("Random Related Stuff")]
    public SeedType seedType = SeedType.RANDOM;
    System.Random random;
    public int seed = 0;

    [Space]
    public bool animatedPath;
    public List<MyGridCell> gridCellList = new List<MyGridCell>();
    public int pathLength = 10;
    [Range(1.0f, 10.0f)]
    public float cellSize = 1.0f;

    public Transform startLocation;
    public float pauseTime = 1.0f;
    public float buddySpeed = 1.0f;
    public CircleBuddy buddy;
     public int spawnCount = 10;

    // Start is called before the first frame update
    void Start() {
      //  for(int i = 0; i < spawnCount; i++) {
          //  Instantiate(buddy, GetRandomLocation(), Quaternion.identity);
       // }
    }

    void SetSeed() {
        if (seedType == SeedType.RANDOM) {
            random = new System.Random();
        }
        else if (seedType == SeedType.CUSTOM) {
            random = new System.Random(seed);
        }
    }

    void CreatePath() {

        gridCellList.Clear();
        Vector2 currentPosition = startLocation.transform.position;
        gridCellList.Add(new MyGridCell(currentPosition));

        for (int i = 0; i < pathLength; i++) {

            int n = random.Next(100);

            if (n.IsBetween(0, 25)) {
                currentPosition = new Vector2(currentPosition.x + cellSize, currentPosition.y);
            }
            else if (n.IsBetween(26, 50))
            {
                currentPosition = new Vector2(currentPosition.x, currentPosition.y + cellSize);
            }
            else if (n.IsBetween(51, 74))
            {
                currentPosition = new Vector2(currentPosition.x - cellSize, currentPosition.y);
            }
            else if(n.IsBetween(75,100))
            {
                currentPosition = new Vector2(currentPosition.x, currentPosition.y - cellSize);
            }

            gridCellList.Add(new MyGridCell(currentPosition));

        }
    }

    IEnumerator CreatePathRoutine() {

        gridCellList.Clear();
        Vector2 currentPosition = startLocation.transform.position;
        gridCellList.Add(new MyGridCell(currentPosition));

        for (int i = 0; i < pathLength; i++) {

            int n = random.Next(100);

            if (n.IsBetween(0, 25)) {
                currentPosition = new Vector2(currentPosition.x + cellSize, currentPosition.y);
            }
            else if (n.IsBetween(26, 50))
            {
                currentPosition = new Vector2(currentPosition.x, currentPosition.y + cellSize);
            }
            else if (n.IsBetween(51, 74))
            {
                currentPosition = new Vector2(currentPosition.x - cellSize, currentPosition.y);
            }
            else if(n.IsBetween(75,100))
            {
                currentPosition = new Vector2(currentPosition.x, currentPosition.y - cellSize);
            }

            gridCellList.Add(new MyGridCell(currentPosition));
            yield return null;
        }
    }

    //public Vector2 GetRandomLocation() {
      //  return gridCellList[random.Next(gridCellList.Count)].location;
   // }


    private void OnDrawGizmos() {
        for (int i = 0; i < gridCellList.Count; i++) {
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(gridCellList[i].location, Vector3.one * cellSize);
            Gizmos.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            Gizmos.DrawCube(gridCellList[i].location, Vector3.one * cellSize);
        }
    }



    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SetSeed();

            if (animatedPath)
                StartCoroutine(CreatePathRoutine());
            else
                CreatePath();
        }
    }


}
