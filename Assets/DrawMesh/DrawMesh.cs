using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DrawMesh : MonoBehaviour {


    [SerializeField] protected MapManager map;


    [SerializeField] protected float lineThickness = 1f;

    [SerializeField] protected ObjectPoolScript fogManager;


    List<Mesh> meshes = new List<Mesh>();

    bool erase = false;

    Mesh currentMesh;
    [SerializeField] protected GameObject meshPrefab;
    private Vector3 lastMousePosition;

    private void OnDrawGizmos()
    {

    }


    public void EraseToggle()
    {
        erase = !erase;
    }

    void Erase()
    {
        Debug.Log("ite ");

        foreach (var item in meshes)
        {

            Vector3[] vertices = item.vertices;
            Debug.Log("ite " + vertices.Length);
            Debug.Log("ite " + item.colors.Length);
            Color[] colors = item.colors;
            for (int i = 0; i < vertices.Length; i++)
            {
  
                Debug.Log("item: " + Vector3.Distance(vertices[i], map.GetMouseWorldPosition()));
                if (Vector3.Distance(vertices[i], map.GetMouseWorldPosition()) <= lineThickness)
                {
                    colors[i] = Color.clear;
                }
            }
            item.colors = colors;
        }
    }


    protected virtual void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (erase)
            {
                Erase();
            }
            else
            {
                // Mouse Pressed
                GameObject meshObject = Instantiate(meshPrefab, transform, true);
                currentMesh = new Mesh();
                meshes.Add(currentMesh);

                Vector3[] vertices = new Vector3[4];
                Vector2[] uv = new Vector2[4];
                int[] triangles = new int[6];

                vertices[0] = map.GetMouseWorldPosition();
                vertices[1] = map.GetMouseWorldPosition();
                vertices[2] = map.GetMouseWorldPosition();
                vertices[3] = map.GetMouseWorldPosition();

                uv[0] = Vector2.zero;
                uv[1] = Vector2.zero;
                uv[2] = Vector2.zero;
                uv[3] = Vector2.zero;

                triangles[0] = 0;
                triangles[1] = 3;
                triangles[2] = 1;

                triangles[3] = 1;
                triangles[4] = 3;
                triangles[5] = 2;

                currentMesh.vertices = vertices;
                currentMesh.uv = uv;
                currentMesh.triangles = triangles;
                currentMesh.MarkDynamic();

                meshObject.GetComponent<MeshFilter>().mesh = currentMesh;

                lastMousePosition = map.GetMouseWorldPosition();
            }
        }

        if (Input.GetMouseButton(0)) {
            // Mouse held down
            if (erase)
            {
                Erase();
            }
            else
            {
                float minDistance = .1f;
                if (!map.CheckWithinMap())
                    return;
                if (Vector3.Distance(map.GetMouseWorldPosition(), lastMousePosition) > minDistance)
                {
                    Vector3[] vertices = new Vector3[currentMesh.vertices.Length + 2];
                    Vector2[] uv = new Vector2[currentMesh.uv.Length + 2];
                    int[] triangles = new int[currentMesh.triangles.Length + 6];

                    currentMesh.vertices.CopyTo(vertices, 0);
                    currentMesh.uv.CopyTo(uv, 0);
                    currentMesh.triangles.CopyTo(triangles, 0);

                    int vIndex = vertices.Length - 4;
                    int vIndex0 = vIndex + 0;
                    int vIndex1 = vIndex + 1;
                    int vIndex2 = vIndex + 2;
                    int vIndex3 = vIndex + 3;

                    Vector3 mouseForwardVector = (map.GetMouseWorldPosition() - lastMousePosition).normalized;
                    Vector3 normal2D = new Vector3(0, 0, -1f);

                    Vector3 newVertexUp = map.GetMouseWorldPosition() + Vector3.Cross(mouseForwardVector, normal2D) * lineThickness;
                    Vector3 newVertexDown = map.GetMouseWorldPosition() + Vector3.Cross(mouseForwardVector, normal2D * -1f) * lineThickness;

                    vertices[vIndex2] = newVertexUp;
                    vertices[vIndex3] = newVertexDown;

                    uv[vIndex2] = Vector2.zero;
                    uv[vIndex3] = Vector2.zero;

                    int tIndex = triangles.Length - 6;

                    triangles[tIndex + 0] = vIndex0;
                    triangles[tIndex + 1] = vIndex2;
                    triangles[tIndex + 2] = vIndex1;

                    triangles[tIndex + 3] = vIndex1;
                    triangles[tIndex + 4] = vIndex2;
                    triangles[tIndex + 5] = vIndex3;

                    currentMesh.vertices = vertices;
                    currentMesh.uv = uv;
                    currentMesh.triangles = triangles;

                    lastMousePosition = map.GetMouseWorldPosition();
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            Color[] colors = new Color[currentMesh.vertexCount];
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = Color.green;
            }
            currentMesh.colors = colors;
        }
    }

  
}