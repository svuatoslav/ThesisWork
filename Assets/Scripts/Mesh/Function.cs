using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace K2
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]

    public class Function : MonoBehaviour
    {
        [SerializeField] private float _maxT = 10f;
        [SerializeField] private int _numbDivisions = 10; //количество разделений
        [SerializeField] private float _tStep = 0.5f; //шаг по функции

        private List<Vector3> _vertex = new List<Vector3>();

        private float _radius = 1f;
        private float _summAngle = 0f;
        private List<int> _triangl = new List<int>();
        private Mesh _mesh;
        private Vector3[] _vertices;
        private int[] _triangles;

        private delegate float _x_t(float x);
        private delegate float _z_t(float z);
        private void Start()
        {
            transform.position = new Vector3(0f, 0f, 0f);
            Generate();
        }
        private void Generate()
        {
            _mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = _mesh;
            _mesh.name = "Fuction";
            GetComponent<MeshCollider>().sharedMesh = _mesh;
            _x_t xt = x_t;
            _z_t zt = z_t;
            _vertex.Add(new Vector3(0f, 0f, 0f));
            for (float i = _tStep; i <= _maxT; i += _tStep)
            {
                _radius = (new Vector3(0f, 0f, zt(i)) - new Vector3(xt(i), 0f, zt(i))).magnitude;
                for (_summAngle = 0f; _summAngle < 360f; _summAngle += 360f / _numbDivisions)
                {
                    _vertex.Add(new Vector3(_radius * Mathf.Cos(_summAngle * Mathf.Deg2Rad), _radius * Mathf.Sin(_summAngle * Mathf.Deg2Rad), zt(i)));
                }
            }
            //крышка (по новым вершинам)
            _radius = (new Vector3(0f, 0f, zt(_maxT)) - new Vector3(xt(_maxT), 0f, zt(_maxT))).magnitude;
            for (_summAngle = 0f; _summAngle < 360f; _summAngle += 360f / _numbDivisions)
            {
                _vertex.Add(new Vector3(_radius * Mathf.Cos(_summAngle * Mathf.Deg2Rad), _radius * Mathf.Sin(_summAngle * Mathf.Deg2Rad), zt(_maxT)));
            }

            _vertex.Add(new Vector3(0f, 0f, zt(_maxT)));

            _vertices = new Vector3[_vertex.Count];
            for (int i = 0; i < _vertex.Count; i++)
            {
                _vertices[i] = _vertex[i];
            }
            _mesh.vertices = _vertices;
            // от начала координат до первой окружности
            for (int i = 0; i < _numbDivisions - 1; i++)
            {
                _triangl.Add(0);
                _triangl.Add(i + 2);
                _triangl.Add(i + 1);
            }
            _triangl.Add(0);
            _triangl.Add(1);
            _triangl.Add(_numbDivisions);
            int k = 0;
            for (float i = _tStep; i < _maxT; i += _tStep, k += _numbDivisions)
            {
                for (int j = 1; j < _numbDivisions; j++)
                {
                    _triangl.Add(j + (int)k);
                    _triangl.Add(j + 1 + (int)k);
                    _triangl.Add(j + _numbDivisions + (int)k);
                    _triangl.Add(j + _numbDivisions + (int)k);
                    _triangl.Add(j + 1 + (int)k);
                    _triangl.Add(j + _numbDivisions + 1 + (int)k);
                }
            }
            k = 10;
            for (float i = _tStep; i < _maxT; i += _tStep, k += _numbDivisions)
            {
                _triangl.Add(k);
                _triangl.Add(1 + k - _numbDivisions);
                _triangl.Add(_numbDivisions + k);
                _triangl.Add(_numbDivisions + k);
                _triangl.Add(1 + k - _numbDivisions);
                _triangl.Add(1 + k);
            }
            /*
            //крышка
            for (int i = 0; i < _numbDivisions - 1; i++)
            {

                _triangl.Add((int)(_funHeight / _tStep) * _numbDivisions - _numbDivisions + 1 + i);
                _triangl.Add((int)(_funHeight / _tStep) * _numbDivisions - _numbDivisions + 2 + i);
                _triangl.Add((int)(_funHeight / _tStep) * _numbDivisions + 1);
            }
            _triangl.Add((int)(_funHeight / _tStep) * _numbDivisions);
            _triangl.Add((int)(_funHeight / _tStep) * _numbDivisions - _numbDivisions + 1);
            _triangl.Add((int)(_funHeight / _tStep) * _numbDivisions + 1);*/

            //крышка (по новым вершинам)
            for (int i = 0; i < _numbDivisions - 1; i++)
            {

                _triangl.Add((int)(_maxT / _tStep) * _numbDivisions + 1 + i);
                _triangl.Add((int)(_maxT / _tStep) * _numbDivisions + 2 + i);
                _triangl.Add((int)(_maxT / _tStep) * _numbDivisions + _numbDivisions + 1);
            }
            _triangl.Add((int)(_maxT / _tStep) * _numbDivisions + _numbDivisions);
            _triangl.Add((int)(_maxT / _tStep) * _numbDivisions + 1);
            _triangl.Add((int)(_maxT / _tStep) * _numbDivisions + _numbDivisions + 1);

            _triangles = new int[_triangl.Count];
            for (int i = 0; i < _triangl.Count; i++)
            {
                _triangles[i] = _triangl[i];
            }
            _mesh.triangles = _triangles;
            _mesh.RecalculateNormals();
            GetComponent<MeshCollider>().sharedMesh = _mesh;
            AssetDatabase.CreateAsset(_mesh, "Assets/Meshs/Function.asset");
        }
        private float x_t(float t)
        {
            return t * (t + 5 - (8 / t) / t);

        }
        private float z_t(float t)
        {
            return t * t * t - 7 * t + 2 / t;
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
}
