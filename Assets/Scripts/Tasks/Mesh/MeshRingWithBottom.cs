using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class MeshRingWithBottom : MonoBehaviour
{
    [SerializeField] private Material _material = null;
    private const float _funHeightWasher = 0.5f;
    private const float _funHeightRingWithBotton = 2.5f;
    private const int _numbDivisions = 50;
    private const float _aLittle = 0.1f;
    private const float _bLittle = 0.1f;
    [SerializeField] private float _aBig = 0f;
    [SerializeField] private float _bBig = 0f;
    private const float _around = 360f;

    private List<Vector3> _vertex = new List<Vector3>();
    private float _summAngle = 0f;
    private List<int> _triangl = new List<int>();
    private Mesh _mesh;
    private Vector3[] _vertices;
    private int[] _triangles;

    private delegate float _x_t(float a, float phi);
    private delegate float _z_t(float b, float phi);
    void Start()
    {
        GenerateRingWithBottom();
    }
    private void GenerateRingWithBottom()
    {
        _mesh = new Mesh();
        gameObject.GetComponent<MeshFilter>().mesh = _mesh;
        _mesh.name = "Ring";
        _x_t xt = X_t;
        _z_t zt = Z_t;
        // Для нижнего основания
        for (_summAngle = 0f; _summAngle < _around; _summAngle += _around / _numbDivisions)
        {
            _vertex.Add(new Vector3(xt(_aBig, _summAngle), 0f, zt(_bBig, _summAngle)));
        }
        for (_summAngle = 0f; _summAngle < _around; _summAngle += _around / _numbDivisions)
        {
            _vertex.Add(new Vector3(xt(_aLittle, _summAngle), 0f, zt(_bLittle, _summAngle)));
        }
        // Для верхнего основания шайбы
        for (_summAngle = 0f; _summAngle < _around; _summAngle += _around / _numbDivisions)
        {
            _vertex.Add(new Vector3(xt(_aBig, _summAngle), _funHeightWasher, zt(_bBig, _summAngle)));
        }
        for (_summAngle = 0f; _summAngle < _around; _summAngle += _around / _numbDivisions)
        {
            _vertex.Add(new Vector3(xt(_aLittle, _summAngle), _funHeightWasher, zt(_bLittle, _summAngle)));
        }
        // Для боковых сторон
        for (_summAngle = 0f; _summAngle < _around; _summAngle += _around / _numbDivisions)
        {
            _vertex.Add(new Vector3(xt(_aBig, _summAngle), 0f, zt(_bBig, _summAngle)));
        }
        for (_summAngle = 0f; _summAngle < _around; _summAngle += _around / _numbDivisions)
        {
            _vertex.Add(new Vector3(xt(_aBig, _summAngle), _funHeightRingWithBotton, zt(_bBig, _summAngle)));
        }
        for (_summAngle = 0f; _summAngle < _around; _summAngle += _around / _numbDivisions)
        {
            _vertex.Add(new Vector3(xt(_aLittle, _summAngle), 0f, zt(_bLittle, _summAngle)));
        }
        for (_summAngle = 0f; _summAngle < _around; _summAngle += _around / _numbDivisions)
        {
            _vertex.Add(new Vector3(xt(_aLittle, _summAngle), _funHeightWasher, zt(_bLittle, _summAngle)));
        }
        for (_summAngle = 0f; _summAngle < _around; _summAngle += _around / _numbDivisions)
        {
            _vertex.Add(new Vector3(xt(_aBig, _summAngle), _funHeightWasher, zt(_bBig, _summAngle)));
        }
        for (_summAngle = 0f; _summAngle < _around; _summAngle += _around / _numbDivisions)
        {
            _vertex.Add(new Vector3(xt(_aBig, _summAngle), _funHeightRingWithBotton, zt(_bBig, _summAngle)));
        }

        _vertices = new Vector3[_vertex.Count];
        for (int i = 0; i < _vertex.Count; i++)
        {
            _vertices[i] = _vertex[i];
        }
        _mesh.vertices = _vertices;
        // Нижнее основание сторона
        for (int i = 0; i < _numbDivisions - 1; i++)
        {
            _triangl.Add(i + _numbDivisions);
            _triangl.Add(i);
            _triangl.Add(i + 1);
            _triangl.Add(i + 1);
            _triangl.Add(i + _numbDivisions + 1);
            _triangl.Add(i + _numbDivisions);
        }
        _triangl.Add(_numbDivisions * 2 - 1);
        _triangl.Add(_numbDivisions - 1);
        _triangl.Add(0);
        _triangl.Add(0);
        _triangl.Add(_numbDivisions);
        _triangl.Add(_numbDivisions * 2 - 1);
        // Верхнее основание сторона
        for (int i = _numbDivisions * 2; i < _numbDivisions * 3 - 1; i++)
        {
            _triangl.Add(i);
            _triangl.Add(i + _numbDivisions);
            _triangl.Add(i + 1);
            _triangl.Add(i + 1);
            _triangl.Add(i + _numbDivisions);
            _triangl.Add(i + _numbDivisions + 1);
        }
        _triangl.Add(_numbDivisions * 3 - 1);
        _triangl.Add(_numbDivisions * 4 - 1);
        _triangl.Add(_numbDivisions * 2);
        _triangl.Add(_numbDivisions * 2);
        _triangl.Add(_numbDivisions * 4 - 1);
        _triangl.Add(_numbDivisions * 3);
        // Внешняя боковая сторона с внешней стороны
        for (int i = _numbDivisions * 4; i < _numbDivisions * 5 - 1; i++)
        {
            _triangl.Add(i);
            _triangl.Add(i + _numbDivisions);
            _triangl.Add(i + 1);
            _triangl.Add(i + 1);
            _triangl.Add(i + _numbDivisions);
            _triangl.Add(i + _numbDivisions + 1);
        }
        _triangl.Add(_numbDivisions * 5 - 1);
        _triangl.Add(_numbDivisions * 6 - 1);
        _triangl.Add(_numbDivisions * 4);
        _triangl.Add(_numbDivisions * 4);
        _triangl.Add(_numbDivisions * 6 - 1);
        _triangl.Add(_numbDivisions * 5);
        // Внутренняя боковая сторона
        for (int i = _numbDivisions * 6; i < _numbDivisions * 7 - 1; i++)
        {
            _triangl.Add(i + _numbDivisions);
            _triangl.Add(i);
            _triangl.Add(i + 1);
            _triangl.Add(i + 1);
            _triangl.Add(i + _numbDivisions + 1);
            _triangl.Add(i + _numbDivisions);
        }
        _triangl.Add(_numbDivisions * 8 - 1);
        _triangl.Add(_numbDivisions * 7 - 1);
        _triangl.Add(_numbDivisions * 6);
        _triangl.Add(_numbDivisions * 6);
        _triangl.Add(_numbDivisions * 7);
        _triangl.Add(_numbDivisions * 8 - 1);
        // Внешняя боковая сторона с внутренней стороны
        for (int i = _numbDivisions * 8; i < _numbDivisions * 9 - 1; i++)
        {
            _triangl.Add(i + _numbDivisions);
            _triangl.Add(i);
            _triangl.Add(i + 1);
            _triangl.Add(i + 1);
            _triangl.Add(i + _numbDivisions + 1);
            _triangl.Add(i + _numbDivisions);
        }
        _triangl.Add(_numbDivisions * 10 - 1);
        _triangl.Add(_numbDivisions * 9 - 1);
        _triangl.Add(_numbDivisions * 8);
        _triangl.Add(_numbDivisions * 8);
        _triangl.Add(_numbDivisions * 9);
        _triangl.Add(_numbDivisions * 10 - 1);

        _triangles = new int[_triangl.Count];
        for (int i = 0; i < _triangl.Count; i++)
        {
            _triangles[i] = _triangl[i];
        }
        _mesh.triangles = _triangles;
        _mesh.RecalculateNormals();
        gameObject.GetComponent<MeshRenderer>().material = _material;
        gameObject.GetComponent<MeshCollider>().sharedMesh = _mesh;
    }
    private float X_t(float a, float t)
    {
        return a * Mathf.Cos(t * Mathf.Deg2Rad);
    }
    private float Z_t(float b, float t)
    {
        return b * Mathf.Sin(t * Mathf.Deg2Rad);
    }
}
