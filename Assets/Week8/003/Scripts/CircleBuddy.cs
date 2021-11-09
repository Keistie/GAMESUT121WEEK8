using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBuddy : MonoBehaviour {

    public float speed = 1.0f;

    Vector2 nextLocation = new Vector2();
    GridSystem gridSystem;
    PathSystem pathSystem;
    SpriteRenderer spriteRenderer;

    bool isActive;
    float pauseTime = 1.0f;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start() {
        gridSystem = GameObject.FindGameObjectWithTag("GridSystem").GetComponent<GridSystem>();
        pathSystem = GameObject.FindGameObjectWithTag("PathSystem").GetComponent<PathSystem>();
        isActive = true;
        //StartCoroutine(MoveToLocation());
        MoveToLocationSolid();
    }

    
    IEnumerator MoveToLocation() {

        while (isActive) {

            float t = 0.0f;
            nextLocation = gridSystem.GetRandomLocation();
            Vector2 startLocation = transform.position;
            spriteRenderer.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
           // while(t < 1.0f) {
             //  t += Time.deltaTime * gridSystem.buddySpeed;
             //   transform.position = Vector2.Lerp(startLocation, nextLocation, t);
             //   yield return null;
           // }


            yield return new WaitForSeconds(gridSystem.pauseTime);
        }


    }

    void MoveToLocationSolid() {
         while (isActive) {

            float t = 0.0f;
            //nextLocation = gridSystem.GetRandomLocation();
            Vector2 startLocation = transform.position;
            spriteRenderer.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
         }


    }

}
