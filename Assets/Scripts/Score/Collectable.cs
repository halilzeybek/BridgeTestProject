using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private ShapeType shapeType;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            SpawnManager.Instance.SpawnRandomObject();
            ScoreManager.Instance.AddScore(score, shapeType);
            Destroy(gameObject);            
        }

    }
}
