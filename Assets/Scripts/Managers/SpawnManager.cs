using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonobehaviourSingleton<SpawnManager>
{
    [SerializeField] private GameObject[] collectableObjects;
    [SerializeField] private GameObject staticSpawnCube;
    [SerializeField] private GameObject floorObject;

    [Header("Static Cube Spawn Settings")]
    [Range(0f, 20f),SerializeField] private float cubeSpawnMinInterval;
    [Range(0f, 20f),SerializeField] private float cubeSpawnMaxInterval;

    private Mesh floorMesh;

    private Coroutine lastCubeCor;
    private GameObject lastSpawnedCube;

    protected override void Awake()
    {
        base.Awake();

        floorMesh = floorObject.GetComponent<MeshFilter>().mesh;
    }

    private void Start()
    {
        StartCoroutine(SpawnRandomCollectable());
        lastCubeCor = StartCoroutine(SpawnStaticCube());

    }

    public void SpawnRandomObject()
    {
        StartCoroutine(SpawnRandomCollectable());
    }

    IEnumerator SpawnRandomCollectable()
    {

        Vector3 randomPos = GetRandomPosWithinFloor();

        int randomSpawnIndex = Random.Range(0, collectableObjects.Length);

        Instantiate(collectableObjects[randomSpawnIndex],randomPos, Quaternion.identity);


        yield return null;
    }

    IEnumerator SpawnStaticCube()
    {

        Vector3 randomPos = GetRandomPosWithinFloor();

        lastSpawnedCube = Instantiate(staticSpawnCube, randomPos, Quaternion.identity);

        yield return new WaitForSeconds(Random.Range(cubeSpawnMinInterval, cubeSpawnMaxInterval));

        StopCoroutine(lastCubeCor); // Making sure that no other coroutine is still alive

        lastCubeCor = StartCoroutine(SpawnStaticCube());
    }


    private Vector3 GetRandomPosWithinFloor()
    {

        Bounds bounds = floorMesh.bounds;

        float minX = floorObject.transform.position.x - floorObject.transform.localScale.x * bounds.size.x * 0.5f;
        float minZ = floorObject.transform.position.z - floorObject.transform.localScale.z * bounds.size.z * 0.5f;


        return new Vector3(Random.Range(minX, -minX),
                           floorObject.transform.position.y + 1f,
                           Random.Range(minZ, -minZ));
    }
}
