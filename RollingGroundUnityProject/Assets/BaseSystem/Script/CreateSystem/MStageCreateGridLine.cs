using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MStageCreateGridLine : MonoBehaviour
{
    const int kGridSize = 50;       // グリッドのサイズ（セル数）
    const float kCellSize = 1f;       // セルサイズ
    const float kLineWidth = 0.05f;    // 線の太さ

    private void Awake()
    {
        GetComponent<MeshFilter>().mesh = GenerateGridMesh();
    }

    Mesh GenerateGridMesh()
    {
        var mesh = new Mesh();
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

        var vertices = new System.Collections.Generic.List<Vector3>();
        var triangles = new System.Collections.Generic.List<int>();
        int vertCount = 0;

        float size = kGridSize * kCellSize;

        // X方向の線
        for (int x = 0; x <= kGridSize; x++)
        {
            float px = x * kCellSize;

            vertices.Add(new Vector3(px - kLineWidth, 0, 0));
            vertices.Add(new Vector3(px + kLineWidth, 0, 0));
            vertices.Add(new Vector3(px + kLineWidth, 0, size));
            vertices.Add(new Vector3(px - kLineWidth, 0, size));

            // CCW（上向きに見えるように）
            triangles.AddRange(new int[] { vertCount, vertCount + 2, vertCount + 1, vertCount, vertCount + 3, vertCount + 2 });
            vertCount += 4;
        }

        // Z方向の線
        for (int z = 0; z <= kGridSize; z++)
        {
            float pz = z * kCellSize;

            vertices.Add(new Vector3(0, 0, pz - kLineWidth));
            vertices.Add(new Vector3(size, 0, pz - kLineWidth));
            vertices.Add(new Vector3(size, 0, pz + kLineWidth));
            vertices.Add(new Vector3(0, 0, pz + kLineWidth));

            triangles.AddRange(new int[] { vertCount, vertCount + 2, vertCount + 1, vertCount, vertCount + 3, vertCount + 2 });
            vertCount += 4;
        }

        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);

        // 全部上向きの法線をセット
        mesh.SetNormals(System.Linq.Enumerable.Repeat(Vector3.up, vertices.Count).ToList());

        return mesh;
    }
}
