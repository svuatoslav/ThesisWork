using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer), typeof(MeshCollider))]
public class ManyAngle : MonoBehaviour
{
    [SerializeField] private float _hight = 1;
    [SerializeField] private List<float> _sideLenght = new List<float>();
    [SerializeField] private List<float> _angle = new List<float>();
    [SerializeField] private List<Vector2> _vertex2 = new List<Vector2>();
    [SerializeField] private bool _conve = false;
    [SerializeField] private bool _choiceAngle = false;

    private Vector2 _SummCoordinate = new Vector2(0f, 0f);
    private List<Vector3> _vertex = new List<Vector3>();
    private List<int> _triangl = new List<int>();
    private Mesh _mesh;
    private Vector3[] _vertices;
    private int[] _triangles;

    private void Start()
    {
        transform.position = new Vector3(0f, 0f, 0f);
        if (_choiceAngle)
        {
            if (Chek(_angle.Count, _angle, _conve))
                Generate();
        }
        else
            Generate();
    }
    private void Generate()
    {
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;
        _mesh.name = "Many";
        GetComponent<MeshCollider>().sharedMesh = _mesh;
        if (_choiceAngle)
        {
            float SummAngle = 0f;
            _vertex.Add(new Vector3(0f, 0f, 0f));
            _angle[0] = 0f;
            for (int i = 0; i < _angle.Count - 1; i++)
            {
                _vertex.Add(new Vector3(Mathf.Cos((SummAngle + _angle[i]) * Mathf.Deg2Rad) * _sideLenght[i] + _vertex[i].x, 0f, Mathf.Sin((SummAngle + _angle[i]) * Mathf.Deg2Rad) * _sideLenght[i] + _vertex[i].z));
                SummAngle += _angle[i];
            }
            SummAngle = 0f;
            for (int i = 0; i < _angle.Count - 1; i++)
            {
                _vertex.Add(new Vector3(Mathf.Cos((SummAngle + _angle[i]) * Mathf.Deg2Rad) * _sideLenght[i] + _vertex[i].x, _hight, Mathf.Sin((SummAngle + _angle[i]) * Mathf.Deg2Rad) * _sideLenght[i] + _vertex[i].z));
                SummAngle += _angle[i];
            }
            _vertex.Add(new Vector3(0f, _hight, 0f));
            for (int i = 0; i < _angle.Count - 1; i++)
            {
                _triangl.Add(i);
                _triangl.Add(i + _angle.Count);
                _triangl.Add(i + 1);
                _triangl.Add(i + 1);
                _triangl.Add(i + _angle.Count);
                _triangl.Add(i + _angle.Count + 1);
            }
            _triangl.Add(_angle.Count - 1);
            _triangl.Add(_angle.Count * 2 - 1);
            _triangl.Add(0);
            _triangl.Add(0);
            _triangl.Add(_angle.Count * 2 - 1);
            _triangl.Add(_angle.Count);
        }
        else
        {
            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < _vertex2.Count; i++)
                {
                    _vertex.Add(new Vector3(_vertex2[i].x, _vertex2[i].y, 0f));
                }
            }
            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < _vertex2.Count; i++)
                {
                    _vertex.Add(new Vector3(_vertex2[i].x, _vertex2[i].y, _hight));
                }
            }
            for (int i = 0; i < _vertex2.Count; i++)
            {
                _SummCoordinate += _vertex2[i];
            }
            _vertex.Add(new Vector3(_SummCoordinate.x / _vertex2.Count, _SummCoordinate.y / _vertex2.Count, 0f));
            _vertex.Add(new Vector3(_SummCoordinate.x / _vertex2.Count, _SummCoordinate.y / _vertex2.Count, _hight));
            //нижнее основание
            for (int i = 0; i < _vertex2.Count-1; i++)
            {
                _triangl.Add(i);
                _triangl.Add(_vertex.Count - 2);
                _triangl.Add(i + 1);
            }
            _triangl.Add(_vertex2.Count - 1);
            _triangl.Add(_vertex.Count - 2);
            _triangl.Add(0);
            //верхнее основание
            for (int i = _vertex.Count - _vertex2.Count - 2; i < _vertex.Count - 3; i++)
            {
                _triangl.Add(i+1);
                _triangl.Add(_vertex.Count - 1);
                _triangl.Add(i);
            }
            _triangl.Add(_vertex.Count - _vertex2.Count - 2);
            _triangl.Add(_vertex.Count - 1);
            _triangl.Add(_vertex.Count - 3);
            //боковая сторона
            for (int i = _vertex2.Count; i < _vertex2.Count * 2 - 1; i++)
            {
                _triangl.Add(i);
                _triangl.Add(i + 1 + _vertex2.Count);
                _triangl.Add(i + _vertex2.Count * 2);
                _triangl.Add(i + _vertex2.Count * 2);
                _triangl.Add(i + 1 + _vertex2.Count);
                _triangl.Add(i + _vertex2.Count * 2 + 1);
            }
            _triangl.Add(_vertex2.Count * 2);
            _triangl.Add(_vertex2.Count * 4 - 1);
            _triangl.Add(_vertex2.Count * 2 - 1);
            _triangl.Add(_vertex2.Count * 4);
            _triangl.Add(_vertex2.Count * 4 - 1);
            _triangl.Add(_vertex2.Count * 2);
        }
        _vertices = new Vector3[_vertex.Count];
        for (int i = 0; i < _vertex.Count; i++) 
        {
            _vertices[i] = _vertex[i];
        }
        _mesh.vertices = _vertices;
        
        _triangles = new int[_triangl.Count];
        //Debug.LogWarning(_triangl.Count);
        for (int i = 0; i < _triangl.Count; i++) 
        {
            _triangles[i] = _triangl[i];
            //Debug.Log(_triangles[i]);
        }
        //Debug.LogWarning(_triangles.Length);
        _mesh.triangles = _triangles;
        _mesh.RecalculateNormals();
        GetComponent<MeshCollider>().sharedMesh = _mesh;
        AssetDatabase.CreateAsset(_mesh, "Assets/Meshs/ManyAngle.asset");
    }
    private bool Chek(int count, List<float> angel,bool conve)
    {
        if(conve)
        {
            float SummAngel = 0f;
            for (int i = 0; i < angel.Count; i++)
            {
                SummAngel += 180f - angel[i];
            }
            Debug.Log(Mathf.Abs((angel.Count - 2)));
            if (Mathf.Abs((angel.Count - 2) * 180 - SummAngel) < 2)
            {
                Debug.Log("ок");
                return true;
            }
            else
            {
                Debug.LogError("Не выпуклый!");
                return false;
            }
                
        }
        return true;
    }
    /*private void OnDrawGizmos()
    {
        if (_vertex == null)
            return;
        Gizmos.color = Color.red;
        for (int i = 0; i < _vertex.Count; i++) 
        {
            Gizmos.DrawSphere(_vertex[i], 0.2f);
        }
    }*/
}
