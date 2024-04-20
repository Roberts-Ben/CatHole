using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour
{
    public PolygonCollider2D holeCollider;
    public PolygonCollider2D groundCollider;
    public MeshCollider GeneratedMeshCollider;
    Mesh generatedMesh;

    public float speed;

    void FixedUpdate()
    {
        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed * Time.deltaTime);

        holeCollider.transform.localScale = transform.localScale;

        if (transform.hasChanged)
            holeCollider.transform.position =  new Vector2(transform.position.x, transform.position.z);
        CreateHole();
        CreateMeshCollider();
    }

    void CreateHole()
    {
        Vector2[] pointPositions = holeCollider.GetPath(0);

        for(int i = 0; i < pointPositions.Length; i++)
        {
            pointPositions[i] = holeCollider.transform.TransformPoint(pointPositions[i]);
        }

        groundCollider.pathCount = 2;
        groundCollider.SetPath(1, pointPositions);
    }

    void CreateMeshCollider()
    {
        generatedMesh = groundCollider.CreateMesh(true, false);
        GeneratedMeshCollider.sharedMesh = generatedMesh;
    }
}
