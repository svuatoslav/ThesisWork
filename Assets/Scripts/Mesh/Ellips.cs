using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class Ellips : MonoBehaviour
{
    //[SerializeField] private Material _material = null;
    private const float _funHeight = 0.5f;
    private const int _numbDivisions = 20; //количество разделений
    private const float _a = 0.1f;
    private const float _b = 0.1f;
    
    private List<Vector3> _vertex = new List<Vector3>();
    private float _summAngle = 0f;
    private List<int> _triangl = new List<int>();
    private Mesh _mesh;
    private Vector3[] _vertices;
    private int[] _triangles;

    private delegate float _x_t(float a, float phi);
    private delegate float _z_t(float b, float phi);
    private void Start()
    {
        Generate();
    }
    private void Generate()
    {
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;
        _mesh.name = "Ellips";
        _x_t xt = x_t;
        _z_t zt = z_t;
        // для двух оснований
        _vertex.Add(new Vector3(0f, 0f, 0f));
        for (_summAngle = 0f; _summAngle < 360f; _summAngle += 360f / _numbDivisions)
        {
            _vertex.Add(new Vector3(xt(_a, _summAngle), 0f, zt(_b, _summAngle)));
        }
        for (_summAngle = 0f; _summAngle < 360f; _summAngle += 360f / _numbDivisions)
        {
            _vertex.Add(new Vector3(xt(_a, _summAngle), _funHeight, zt(_b, _summAngle)));
        }
        _vertex.Add(new Vector3(0f, _funHeight, 0f));
        // для боковых сторон
        for (_summAngle = 0f; _summAngle < 360f; _summAngle += 360f / _numbDivisions)
        {
            _vertex.Add(new Vector3(xt(_a, _summAngle), 0f, zt(_b, _summAngle)));
        }
        for (_summAngle = 0f; _summAngle < 360f; _summAngle += 360f / _numbDivisions)
        {
            _vertex.Add(new Vector3(xt(_a, _summAngle), _funHeight, zt(_b, _summAngle)));
        }
        _vertices = new Vector3[_vertex.Count];
        for (int i = 0; i < _vertex.Count; i++)
        {
            _vertices[i] = _vertex[i];
        }
        _mesh.vertices = _vertices;
        //нижнее основание
        for (int i = 0; i < _numbDivisions - 1; i++)
        {
            _triangl.Add(0);
            _triangl.Add(i + 1);
            _triangl.Add(i + 2);
        }
        _triangl.Add(0);
        _triangl.Add(_numbDivisions);
        _triangl.Add(1);
        //верхнее основание
        for (int i = 0; i < _numbDivisions - 1; i++)
        {
            _triangl.Add(_numbDivisions * 2 + 1);
            _triangl.Add(i + 2 + _numbDivisions);
            _triangl.Add(i + 1 + _numbDivisions);
        }
        _triangl.Add(_numbDivisions * 2 + 1);
        _triangl.Add(_numbDivisions + 1);
        _triangl.Add(_numbDivisions * 2);
        // боковая сторона
        for (int i = _numbDivisions * 2 + 2; i <= _numbDivisions * 3; i++)
        {
            _triangl.Add(i);
            _triangl.Add(i + _numbDivisions);
            _triangl.Add(i + 1);
            _triangl.Add(i + 1);
            _triangl.Add(i + _numbDivisions);
            _triangl.Add(i + _numbDivisions + 1);
        }
        _triangl.Add(_numbDivisions * 3 + 1);
        _triangl.Add(_numbDivisions * 4 + 1);
        _triangl.Add(_numbDivisions * 2 + 2);
        _triangl.Add(_numbDivisions * 2 + 2);
        _triangl.Add(_numbDivisions * 4 + 1);
        _triangl.Add(_numbDivisions * 3 + 2);
        _triangles = new int[_triangl.Count];
        for (int i = 0; i < _triangl.Count; i++)
        {
            _triangles[i] = _triangl[i];
        }
        _mesh.triangles = _triangles;
        _mesh.RecalculateNormals();
        GetComponent<MeshCollider>().sharedMesh = _mesh;
        AssetDatabase.CreateAsset(_mesh, "Assets/Meshs/Ellips.asset");
    }

    private float x_t(float a, float t)
    {
        return a * Mathf.Cos(t * Mathf.Deg2Rad);
    }
    private float z_t(float b, float t)
    {
        return b * Mathf.Sin(t * Mathf.Deg2Rad);
    }
    private void OnDrawGizmos()
    {
        if (_vertex == null)
            return;
        Gizmos.color = Color.red;
        for (int i = 0; i < _vertex.Count; i++)
        {
            Gizmos.DrawSphere(_vertex[i], 0.1f);
        }
    }
}
