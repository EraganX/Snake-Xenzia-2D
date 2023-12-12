using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : MonoBehaviour
{
    public BoxCollider2D gridArea;


    void Start()
    {
        RandomSpawnArea();
    }

    private void RandomSpawnArea()
    {
        Bounds bounds = this.gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        transform.position = new Vector3(
            Mathf.Round(x),
            Mathf.Round(y),
            0
        );
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            RandomSpawnArea();
        }
    }
}
