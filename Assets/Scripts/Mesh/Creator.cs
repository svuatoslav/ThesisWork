using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator : MonoBehaviour
{
    [SerializeField] private Mesh _ellips = null;
    [SerializeField] private Material _material = null;
    private const float _funHeightWasher = 0.5f;
    private const float _funHeightRingWithBotton = 1.1f;
    private const int _numbDivisions = 50;
    // ���������� ����������
    private const float _aLittle = 0.1f;
    private const float _bLittle = 0.1f;
    private float _aBig = 0f;
    private float _bBig = 0f;
    private const float _around = 360f;

    private List<Vector3> _vertex;
    private float _summAngle = 0f;
    private List<int> _triangl;
    private Mesh _mesh;
    private Vector3[] _vertices;
    private int[] _triangles;
    private GameObject _parent = null;
    private GameObject _washer = null;
    private GameObject _ringWithBottom = null;
    private GameObject _ellipsGO = null;

    private delegate float _x_t(float a, float phi);
    private delegate float _z_t(float b, float phi);

    public GameObject GetData(float radius, GameObject gameObject, bool FirstObject)
    {
        //_ = new Disk(0.5f, 1f);
        _ellipsGO = new GameObject();
        _ellipsGO.AddComponent<MeshFilter>().mesh = _ellips;
        _ellipsGO.AddComponent<MeshRenderer>().material = _material;
        _parent = Instantiate(gameObject, Vector3.zero, Quaternion.identity);
        _aBig = radius;
        _bBig = _aBig;
        if (gameObject.CompareTag("Disk"))
        {
            _parent.GetComponent<Disk>().Radius = radius;
            // ������� �������� ��� ����������� ��� ������
            _washer = new GameObject();
            _washer = GenerateWasher(_washer);
            _washer.transform.parent = _parent.transform;
            // ������� �������
            _washer.name = "Washer";
            _washer.tag = "Washer";
        }
        else if (gameObject.CompareTag("Ring"))
        {
            _parent.GetComponent<Ring>().Radius = radius;
            _ringWithBottom = new GameObject();
            _ringWithBottom = GenerateRingWithBottom(_ringWithBottom);
            _ringWithBottom.transform.parent = _parent.transform;
            _ringWithBottom.name = "RingWithBottom";
            _ringWithBottom.tag = "RingWithBottom";
        }
        _ellipsGO.transform.parent = _parent.transform;
        _ellipsGO.name = "Ellips";
        _ellipsGO.tag = "Ellips";
        if (!FirstObject)
            _parent.SetActive(false);
        return _parent;
    }
    private GameObject GenerateWasher(GameObject _washer)
    {
        _vertex = new List<Vector3>();
        _triangl = new List<int>();
        _mesh = new Mesh();
        _washer.AddComponent<MeshFilter>().mesh = _mesh;
        _mesh.name = "Washer";
        _x_t xt = X_t;
        _z_t zt = Z_t;
        // ��� ������� ���������
        for (_summAngle = 0f; _summAngle < _around; _summAngle += _around / _numbDivisions)
        {
            _vertex.Add(new Vector3(xt(_aBig, _summAngle), 0f, zt(_bBig, _summAngle)));
        }
        for (_summAngle = 0f; _summAngle < _around; _summAngle += _around / _numbDivisions)
        {
            _vertex.Add(new Vector3(xt(_aLittle, _summAngle), 0f, zt(_bLittle, _summAngle)));
        }
        // ��� �������� ���������
        for (_summAngle = 0f; _summAngle < _around; _summAngle += _around / _numbDivisions)
        {
            _vertex.Add(new Vector3(xt(_aBig, _summAngle), _funHeightWasher, zt(_bBig, _summAngle)));
        }
        for (_summAngle = 0f; _summAngle < _around; _summAngle += _around / _numbDivisions)
        {
            _vertex.Add(new Vector3(xt(_aLittle, _summAngle), _funHeightWasher, zt(_bLittle, _summAngle)));
        }
        // ��� ������� ������ �� 40
        for (_summAngle = 0f; _summAngle < _around; _summAngle += _around / _numbDivisions)
        {
            _vertex.Add(new Vector3(xt(_aBig, _summAngle), 0f, zt(_bBig, _summAngle)));
        }
        for (_summAngle = 0f; _summAngle < _around; _summAngle += _around / _numbDivisions)
        {
            _vertex.Add(new Vector3(xt(_aBig, _summAngle), _funHeightWasher, zt(_bBig, _summAngle)));
        }
        for (_summAngle = 0f; _summAngle < _around; _summAngle += _around / _numbDivisions)
        {
            _vertex.Add(new Vector3(xt(_aLittle, _summAngle), 0f, zt(_bLittle, _summAngle)));
        }
        for (_summAngle = 0f; _summAngle < _around; _summAngle += _around / _numbDivisions)
        {
            _vertex.Add(new Vector3(xt(_aLittle, _summAngle), _funHeightWasher, zt(_bLittle, _summAngle)));
        }
        _vertices = new Vector3[_vertex.Count];
        for (int i = 0; i < _vertex.Count; i++)
        {
            _vertices[i] = _vertex[i];
        }
        _mesh.vertices = _vertices;
        // ������ ��������� �������
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
        // ������� ��������� �������
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
        // ������� ������� �������
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
        // ���������� ������� �������
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

        _triangles = new int[_triangl.Count];
        for (int i = 0; i < _triangl.Count; i++)
        {
            _triangles[i] = _triangl[i];
        }
        _mesh.triangles = _triangles;
        _mesh.RecalculateNormals();
        _washer.AddComponent<MeshRenderer>().material = _material;
        _washer.AddComponent<MeshCollider>().sharedMesh = _mesh;
        // ��� ����������
        //AssetDatabase.CreateAsset(_mesh, "Assets/Meshs/Washer.asset");
        return _washer;
    }
    private GameObject GenerateRingWithBottom(GameObject ringWithBottom)
    {
        _vertex = new List<Vector3>();
        _triangl = new List<int>();
        _mesh = new Mesh();
        ringWithBottom.AddComponent<MeshFilter>().mesh = _mesh;
        _mesh.name = "Ring";
        _x_t xt = X_t;
        _z_t zt = Z_t;
        // ��� ������� ���������
        for (_summAngle = 0f; _summAngle < _around; _summAngle += _around / _numbDivisions)
        {
            _vertex.Add(new Vector3(xt(_aBig, _summAngle), 0f, zt(_bBig, _summAngle)));
        }
        for (_summAngle = 0f; _summAngle < _around; _summAngle += _around / _numbDivisions)
        {
            _vertex.Add(new Vector3(xt(_aLittle, _summAngle), 0f, zt(_bLittle, _summAngle)));
        }
        // ��� �������� ��������� �����
        for (_summAngle = 0f; _summAngle < _around; _summAngle += _around / _numbDivisions)
        {
            _vertex.Add(new Vector3(xt(_aBig, _summAngle), _funHeightWasher, zt(_bBig, _summAngle)));
        }
        for (_summAngle = 0f; _summAngle < _around; _summAngle += _around / _numbDivisions)
        {
            _vertex.Add(new Vector3(xt(_aLittle, _summAngle), _funHeightWasher, zt(_bLittle, _summAngle)));
        }
        // ��� ������� ������
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
        // ������ ��������� �������
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
        // ������� ��������� �������
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
        // ������� ������� ������� � ������� �������
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
        // ���������� ������� �������
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
        // ������� ������� ������� � ���������� �������
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
        ringWithBottom.AddComponent<MeshRenderer>().material = _material;
        ringWithBottom.AddComponent<MeshCollider>().sharedMesh = _mesh;
        return ringWithBottom;
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
