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

    public GameObject KeytoSpawn;
    public GameObject MonstertoSpawn;
    public GameObject ButtontoSpawn;
    public Transform startLocation;
    public GameObject Player;

    public List<GameObject> keyList = new List<GameObject>(); 
    public List<GameObject> MonsterList = new List<GameObject>(); 
    public List<GameObject> ButtonList = new List<GameObject>(); 
    public List<GameObject> BlockList = new List<GameObject>();
    
    //public float pauseTime = 1.0f;
    //public float buddySpeed = 1.0f;
    //public CircleBuddy buddy;
    //public int spawnCount = 10;

    // Start is called before the first frame update
    void Start() {
      //  for(int i = 0; i < spawnCount; i++) {
          //  Instantiate(buddy, GetRandomLocation(), Quaternion.identity);
       // }
    }

    public void SetSeed() {
        if (seedType == SeedType.RANDOM) {
            random = new System.Random();
        }
        else if (seedType == SeedType.CUSTOM) {
            random = new System.Random(seed);
        }
    }

    public void CreatePath() {

        gridCellList.Clear();
        Vector2 currentPosition = startLocation.transform.position;
        MyGridCell cp = new MyGridCell(currentPosition);
        gridCellList.Add(cp);

        for (int i = 0; i < BlockList.Count; i++) {
            Destroy(BlockList[i]);
        }
        BlockList.Clear();

        GameObject bk = new GameObject("Starting Block");
        bk.transform.position = currentPosition;
        BlockList.Add(bk);

        BoxCollider2D bcLeft = bk.AddComponent<BoxCollider2D>();
        bcLeft.transform.position = new Vector3(bk.transform.position.x - (0.5f * cellSize), bcLeft.transform.position.y);
        bcLeft.size = new Vector2(0.1f, cellSize);

        Player.transform.position = currentPosition;

        for (int i = 0; i<keyList.Count; i++){
            Destroy(keyList[i]);
        }
        keyList.Clear();

        for(int i = 0; i<MonsterList.Count; i++){
            Destroy(MonsterList[i]);
        }
        MonsterList.Clear();

        for(int i = 0; i<ButtonList.Count; i++){
            Destroy(ButtonList[i]);
        }
       ButtonList.Clear();

       for(int i = 0; i<BlockList.Count; i++){
            Destroy(BlockList[i]);
        }
       BlockList.Clear();
        

        for (int i = 0; i < pathLength; i++) {

            int n = random.Next(100);

            if (n.IsBetween(0, 30)) {
                currentPosition = new Vector2(currentPosition.x + cellSize, currentPosition.y);
            }
            else if (n.IsBetween(31, 60))
            {
                currentPosition = new Vector2(currentPosition.x, currentPosition.y + cellSize);
            }
            else if (n.IsBetween(61, 74))
            {
                currentPosition = new Vector2(currentPosition.x - cellSize, currentPosition.y);
            }
            else if(n.IsBetween(75,100))
            {
                currentPosition = new Vector2(currentPosition.x, currentPosition.y - cellSize);
            }

            gridCellList.Add(new MyGridCell(currentPosition));

            GameObject bkTwo = new GameObject($"Block - {i}");
            bkTwo.transform.position = currentPosition;
            BlockList.Add(bkTwo);

            int y = random.Next(100);
            if(y >= 0 && y <=30){
                GameObject ky = Instantiate(KeytoSpawn, currentPosition, Quaternion.identity);
                keyList.Add(ky);

            }
            else if (y >=31 && y<= 60){
                GameObject ms = Instantiate(MonstertoSpawn, currentPosition, Quaternion.identity);
                MonsterList.Add(ms);
            }
            else if (y >= 61 && y <= 63){
                GameObject bt = Instantiate(ButtontoSpawn, currentPosition, Quaternion.identity);
                MonsterList.Add(bt);
            }
            else{

            }

        }

        
    }

    IEnumerator CreatePathRoutine() {

        gridCellList.Clear();
        Vector2 currentPosition = startLocation.transform.position;
        MyGridCell cp = new MyGridCell(currentPosition);
        gridCellList.Add(cp);

        for (int i = 0; i<keyList.Count; i++){
            Destroy(keyList[i]);
        }
        keyList.Clear();

        for(int i = 0; i<MonsterList.Count; i++){
            Destroy(MonsterList[i]);
        }
        MonsterList.Clear();

        for (int i = 0; i < pathLength; i++) {

            int n = random.Next(100);

            if (n.IsBetween(0, 30)) {
                currentPosition = new Vector2(currentPosition.x + cellSize, currentPosition.y);
            }
            else if (n.IsBetween(31, 60))
            {
                currentPosition = new Vector2(currentPosition.x, currentPosition.y + cellSize);
            }
            else if (n.IsBetween(61, 74))
            {
                currentPosition = new Vector2(currentPosition.x - cellSize, currentPosition.y);
            }
            else if(n.IsBetween(75,100))
            {
                currentPosition = new Vector2(currentPosition.x, currentPosition.y - cellSize);
            }

            gridCellList.Add(new MyGridCell(currentPosition));

            int y = random.Next(100);
            if(y >= 0 && y <=30){
            GameObject ky = Instantiate(KeytoSpawn, currentPosition, Quaternion.identity);
            keyList.Add(ky);

            }
            else if (y >=31 && y<= 60){
            GameObject ms = Instantiate(MonstertoSpawn, currentPosition, Quaternion.identity);
            MonsterList.Add(ms);
            }
            else{

            };

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
        if(Input.GetKeyDown(KeyCode.C)) {
            if (seedType == SeedType.RANDOM){
                seedType = SeedType.CUSTOM;
            }
            else if(seedType == SeedType.CUSTOM){
                seedType = SeedType.RANDOM;
            }
        }
    }


}
