using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
//[RequireComponent(typeof(WheelCollider))]
public class Washer : MonoBehaviour
{
    [SerializeField] private float _funHeight = 0.1f;
    [SerializeField] private int _numbDivisions = 20; //количество разделений
    [SerializeField] private float _aLittle = 0f;
    [SerializeField] private float _bLittle = 0f;
    [SerializeField] private float _aBig = 0f;
    [SerializeField] private float _bBig = 0f;
    private Coroutine _coroutine;
    private int _i = -5;
    private bool _flag = true;
    private bool _end = false;

    private List<Vector3> _vertex = new List<Vector3>();
    private float _summAngle = 0f;
    private List<int> _triangl = new List<int>();
    private Mesh _mesh;
    private Vector3[] _vertices;
    private int[] _triangles;
    //private GameObject _washer;

    private delegate float _x_t(float a, float phi);
    private delegate float _z_t(float b, float phi);
    private void Start()
    {
        Generate();
        //gameObject.GetComponent<WheelCollider>().radius = _aBig;
        /*Debug.Log(gameObject.transform.position);
        Debug.Log(gameObject.GetComponent<WheelCollider>().center);
        gameObject.GetComponent<WheelCollider>().center = gameObject.transform.position;
        Debug.LogWarning(gameObject.transform.position);
        Debug.LogWarning(gameObject.GetComponent<WheelCollider>().center);*/
        
    }
    private void Update()
    {
        if (_i >= _vertex.Count && _flag && !_end)
        {
            //StopCoroutine(_coroutine);
            _flag = false;
            _i = 0;
            Debug.Log("!");
            Triangle();
        }
        else if (_i >= _triangl.Count && !_flag && !_end)
        {
            _i = 0;
            Debug.Log("!)");
            end();
        }
        //gameObject.GetComponent<WheelCollider>().center = gameObject.transform.position;
        //Debug.LogError(gameObject.GetComponent<WheelCollider>().center);
    }
    private void Generate()
    {
        /*_washer = new GameObject();
        _washer.transform.position = new Vector3(110f, 110f, 110f);
        _washer.AddComponent<MeshFilter>();
        _washer.AddComponent<MeshRenderer>();
        _washer.AddComponent<MeshCollider>();
        _washer.AddComponent<WheelCollider>();*/
        _mesh = new Mesh();
        //_washer.GetComponent<MeshFilter>().mesh = _mesh;
        //_washer.GetComponent<WheelCollider>().radius = _aBig;
        _mesh.name = "Washer";
        _x_t yt = y_t;
        _z_t zt = z_t;
        // для нижнего основания
        for (_summAngle = 0f; _summAngle < 360f; _summAngle += 360f / _numbDivisions)
        {
            _vertex.Add(new Vector3(_funHeight, yt(_aBig, _summAngle), zt(_bBig, _summAngle)));
        }
        for (_summAngle = 0f; _summAngle < 360f; _summAngle += 360f / _numbDivisions)
        {
            _vertex.Add(new Vector3(_funHeight, yt(_aLittle, _summAngle), zt(_bLittle, _summAngle)));
        }
        // для верхнего основания
        for (_summAngle = 0f; _summAngle < 360f; _summAngle += 360f / _numbDivisions)
        {
            _vertex.Add(new Vector3(0f, yt(_aBig, _summAngle), zt(_bBig, _summAngle)));
        }
        for (_summAngle = 0f; _summAngle < 360f; _summAngle += 360f / _numbDivisions)
        {
            _vertex.Add(new Vector3(0f, yt(_aLittle, _summAngle), zt(_bLittle, _summAngle)));
        }
        // для боковых сторон от 40
        for (_summAngle = 0f; _summAngle < 360f; _summAngle += 360f / _numbDivisions)
        {
            _vertex.Add(new Vector3(_funHeight, yt(_aBig, _summAngle), zt(_bBig, _summAngle)));
        }
        for (_summAngle = 0f; _summAngle < 360f; _summAngle += 360f / _numbDivisions)
        {
            _vertex.Add(new Vector3(0f, yt(_aBig, _summAngle), zt(_bBig, _summAngle)));
        }
        for (_summAngle = 0f; _summAngle < 360f; _summAngle += 360f / _numbDivisions)
        {
            _vertex.Add(new Vector3(_funHeight, yt(_aLittle, _summAngle), zt(_bLittle, _summAngle)));
        }
        for (_summAngle = 0f; _summAngle < 360f; _summAngle += 360f / _numbDivisions)
        {
            _vertex.Add(new Vector3(0f, yt(_aLittle, _summAngle), zt(_bLittle, _summAngle)));
        }
        _vertices = new Vector3[_vertex.Count];
        //for (int i = 0; i < _vertex.Count; i++)
        //{
        //    _vertices[i] = _vertex[i];
        //}
        _i = 0;
        _coroutine = StartCoroutine(Vert());
    }
    private void Triangle()
    {
        _mesh.vertices = _vertices;
        //нижнее основание сторона
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
        //верхнее основание сторона
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
        //внешняя боковая сторона
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
        //внутренняя боковая сторона
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
        _coroutine = StartCoroutine(Triangl());
        //for (int i = 0; i < _triangl.Count; i++)
        //{
        //    _triangles[i] = _triangl[i];
        //}

    }

    private float y_t(float a, float t)
    {
        return a * Mathf.Cos(t * Mathf.Deg2Rad);
    }
    private float z_t(float b, float t)
    {
        return b * Mathf.Sin(t * Mathf.Deg2Rad);
    }
    /*public void ButtonSetting()
    {
        Generate();
    }*/
    private void OnDrawGizmos()
    {
        if (_vertices == null)
            return;
        Gizmos.color = Color.red;
        for (int i = 0; i < _vertices.Length; i++) 
        {
            Gizmos.DrawSphere(_vertices[i], 0.1f);
        }
    }
    IEnumerator Vert()
    {
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        for (int i = 0; i < _vertex.Count; i++)
        {
            _vertices[_i] = _vertex[_i];
            _i++;
            yield return new WaitForSeconds(0.025f);//0.05f
        }
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
    IEnumerator Triangl()
    {
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        for (int i = 0; i < _triangl.Count; i++)
        {
            _triangles[_i] = _triangl[_i];
            _i++;
            if (_i % 3 == 0)
            {
                _mesh.triangles = _triangles;
                gameObject.GetComponent<MeshFilter>().mesh = _mesh;
            }
            yield return new WaitForSeconds(0.02f);
        }
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
    private void end()
    {
        _mesh.triangles = _triangles;
        _mesh.RecalculateNormals();
        gameObject.GetComponent<MeshFilter>().mesh = _mesh;
        //для сохранения
        //_washer.GetComponent<MeshCollider>().sharedMesh = _mesh;
        //AssetDatabase.CreateAsset(_mesh, "Assets/Meshs/Washer.asset");
        //_washer.transform.position = new Vector3(110f, 110f, 110f);
        _end = true;
    }
}