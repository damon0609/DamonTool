using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Damon.Tool;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestGizmos : MonoBehaviour {
    [SerializeField]
    private bool mIsShow;
    public bool isShow {
        set {
            mIsShow = value;
        }
    }

    [SerializeField]
    private Color mColor = Color.white;
    public Color color { set { mColor = value; } }

    [ContextMenu ("GenternalQuad")]
    public void GenternalQuad () {

        List<Vector3> verts = new List<Vector3> ();
        List<int> indexes = new List<int> ();
        GizmosTool.AddQuad (verts, indexes, Vector3.right, Vector3.up, Vector3.zero);

        Mesh mesh = new Mesh ();
        mesh.name = "[Quad]";
        mesh.hideFlags = HideFlags.HideAndDontSave;
        mesh.SetVertices (verts);
        mesh.SetIndices (indexes.ToArray (), MeshTopology.Quads, 0);
        mesh.RecalculateNormals ();
        mesh.RecalculateBounds ();
        mesh.UploadMeshData (true);

        GameObject go = new GameObject ("Quad");
        MeshRenderer renderer = go.AddComponent<MeshRenderer> ();
        renderer.material = new Material (Shader.Find ("Standard"));
        MeshFilter meshFilter = go.AddComponent<MeshFilter> ();
        meshFilter.mesh = mesh;
    }

    [ContextMenu ("GenternalCube")]
    public void GenternalCube () {

        List<Vector3> faces = new List<Vector3> { Vector3.right, Vector3.up, Vector3.forward };
        List<Vector3> verts = new List<Vector3> ();
        List<int> indexes = new List<int> ();
        for (int i = 0; i < 1; i++) {
            GizmosTool.AddQuad (verts, indexes, Vector3.forward, faces[0], faces[1]);
            GizmosTool.AddQuad (verts, indexes, Vector3.forward, faces[0], -faces[1]);
        }
        Mesh mesh = new Mesh ();
        mesh.name = "[Cube]";
        mesh.hideFlags = HideFlags.HideAndDontSave;
        mesh.SetVertices (verts);
        mesh.SetIndices (indexes.ToArray (), MeshTopology.Quads, 0);
        mesh.RecalculateNormals ();
        mesh.RecalculateBounds ();
        mesh.UploadMeshData (true);

        GameObject temp = new GameObject ("Cube");
        MeshRenderer renderer = temp.AddComponent<MeshRenderer> ();
        renderer.material = new Material (Shader.Find ("Standard"));

        MeshFilter meshFilter = temp.AddComponent<MeshFilter> ();
        meshFilter.mesh = mesh;
    }

    [ContextMenu ("GetRootName")]
    private void GetRootGameObjectName () {
        // Scene scene = SceneManager.GetActiveScene ();
        // GameObject[] goes = scene.GetRootGameObjects (); //不返回对象的子对象需自己手动查找
        // foreach (GameObject go in goes) {
        //     Debug.Log (go.name);
        // }

        Mesh _cubeMesh = new Mesh ();
        _cubeMesh.name = "RuntimeGizmoCube";
        _cubeMesh.hideFlags = HideFlags.HideAndDontSave;

        List<Vector3> verts = new List<Vector3> ();
        List<int> indexes = new List<int> ();

        Vector3[] faces = new Vector3[] { Vector3.forward, Vector3.right, Vector3.up };
        for (int i = 0; i < 3; i++) {
            addQuad (verts, indexes, faces[(i + 0) % 3], -faces[(i + 1) % 3], faces[(i + 2) % 3]);
            addQuad (verts, indexes, -faces[(i + 0) % 3], faces[(i + 1) % 3], faces[(i + 2) % 3]);
        }

        _cubeMesh.SetVertices (verts);
        _cubeMesh.SetIndices (indexes.ToArray (), MeshTopology.Quads, 0);
        _cubeMesh.RecalculateNormals ();
        _cubeMesh.RecalculateBounds ();
        _cubeMesh.UploadMeshData (true);

        GameObject temp = new GameObject ("Cube");
        MeshRenderer renderer = temp.AddComponent<MeshRenderer> ();
        renderer.material = new Material (Shader.Find ("Standard"));

        MeshFilter meshFilter = temp.AddComponent<MeshFilter> ();
        meshFilter.mesh = _cubeMesh;
    }

    private void addQuad (List<Vector3> verts, List<int> indexes, Vector3 normal, Vector3 axis1, Vector3 axis2) {
        indexes.Add (verts.Count + 0);
        indexes.Add (verts.Count + 1);
        indexes.Add (verts.Count + 2);
        indexes.Add (verts.Count + 3);

        //在各轴上移动的距离都是0.5f -axis 表示负半轴 axis 表示正半轴
        Vector3 r1 = (normal + axis1 + axis2) * 0.5f;
        Vector3 r2 = (normal + axis1 - axis2) * 0.5f;
        Vector3 r3 = (normal - axis1 - axis2) * 0.5f;
        Vector3 r4 = (normal - axis1 + axis2) * 0.5f;

        verts.Add (r1);
        verts.Add (r2);
        verts.Add (r3);
        verts.Add (r4);
    }

    [ContextMenu ("CubeWire")]
    public void cubeWire () {
        List<Vector3> verts = new List<Vector3> ();
        List<int> indexes = new List<int> ();
        for (int dx = 1; dx >= -1; dx -= 2) {
            for (int dy = 1; dy >= -1; dy -= 2) {
                for (int dz = 1; dz >= -1; dz -= 2) {
                    verts.Add (0.5f * new Vector3 (dx, dy, dz));
                }
            }
        }
        addCorner (indexes, 0, 1, 2, 4);
        addCorner (indexes, 3, 1, 2, 7);
        addCorner (indexes, 5, 1, 4, 7);
        addCorner (indexes, 6, 2, 4, 7);

        Mesh _wireCubeMesh = new Mesh ();
        _wireCubeMesh.name = "CubeWire";
        _wireCubeMesh.hideFlags = HideFlags.HideAndDontSave;

        _wireCubeMesh.SetVertices (verts);
        _wireCubeMesh.SetIndices (indexes.ToArray (), MeshTopology.Lines, 0);
        _wireCubeMesh.RecalculateBounds ();
        _wireCubeMesh.UploadMeshData (true);

        GameObject temp = new GameObject ("CubeWire");
        MeshRenderer renderer = temp.AddComponent<MeshRenderer> ();
        renderer.material = new Material (Shader.Find ("Standard"));

        MeshFilter meshFilter = temp.AddComponent<MeshFilter> ();
        meshFilter.mesh = _wireCubeMesh;

    }
    private void addCorner (List<int> indexes, int a, int b, int c, int d) {
        indexes.Add (a);
        indexes.Add (b);
        indexes.Add (a);
        indexes.Add (c);
        indexes.Add (a);
        indexes.Add (d);
    }

    [ContextMenu ("SphereWire")]
    private void SphereWire () {
        List<Vector3> verts = new List<Vector3> ();
        List<int> indexes = new List<int> ();
        int totalVerts = 32 * 3;
        for (int i = 0; i < 32; i++) {
            float angle = Mathf.PI * 2 * i / 32;
            float dx = 0.5f * Mathf.Cos (angle);
            float dy = 0.5f * Mathf.Sin (angle);

            for (int j = 0; j < 3; j++) {
                indexes.Add ((i * 3 + j + 0) % totalVerts);
                indexes.Add ((i * 3 + j + 3) % totalVerts);
            }

            verts.Add (new Vector3 (dx, dy, 0));
            verts.Add (new Vector3 (0, dx, dy));
            verts.Add (new Vector3 (dx, 0, dy));
        }
        Mesh _wireSphereMesh = new Mesh ();
        _wireSphereMesh.name = "SphereWire";
        _wireSphereMesh.hideFlags = HideFlags.HideAndDontSave;
        _wireSphereMesh.SetVertices (verts);
        _wireSphereMesh.SetIndices (indexes.ToArray (), MeshTopology.Lines, 0);
        _wireSphereMesh.RecalculateBounds ();
        _wireSphereMesh.UploadMeshData (true);
        
        GameObject temp = new GameObject ("SphereWire");
        MeshRenderer renderer = temp.AddComponent<MeshRenderer> ();
        renderer.material = new Material (Shader.Find ("Standard"));

        MeshFilter meshFilter = temp.AddComponent<MeshFilter> ();
        meshFilter.mesh = _wireSphereMesh;
    }
    
    public Transform quad;

    void Awake () { }
    void Start () {
        for (int i = 0; i < 6; i++) {
            int a = (i + 0) % 4;
            int b = (i + 1) % 4;
            int c = (i + 2) % 4;
            int d = (i + 3) % 4;
            Debug.Log (a + "-" + b + "-" + c + "-" + d);
        }
    }

    void Update () {

    }
}