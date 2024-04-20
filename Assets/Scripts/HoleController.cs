using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HoleController : MonoBehaviour
{
    public PolygonCollider2D holeCollider;
    public PolygonCollider2D groundCollider;
    public MeshCollider GeneratedMeshCollider;
    Mesh generatedMesh;

    public float speed;

    private float score = 1;
    private Vector3 initialSize = new(1, 1, 1);

    private void FixedUpdate()
    {
        transform.Translate(speed * Time.deltaTime * new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));

        transform.localScale = initialSize * score;
        holeCollider.transform.localScale = initialSize * score;

        if (transform.hasChanged)
            holeCollider.transform.position =  new Vector2(transform.position.x, transform.position.z);
        CreateHole();
        CreateMeshCollider();
    }

    private void CreateHole()
    {
        Vector2[] pointPositions = holeCollider.GetPath(0);

        for(int i = 0; i < pointPositions.Length; i++)
        {
            pointPositions[i] = holeCollider.transform.TransformPoint(pointPositions[i]);
        }

        groundCollider.pathCount = 2;
        groundCollider.SetPath(1, pointPositions);
    }

    private void CreateMeshCollider()
    {
        generatedMesh = groundCollider.CreateMesh(true, false);
        GeneratedMeshCollider.sharedMesh = generatedMesh;
    }

    public void AwardScore(float scoreToAdd)
    {
        score += scoreToAdd;
    }
}
