using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace K2
{
    public class Creator : MonoBehaviour
    {
        [SerializeField] private Mesh _ellips = null;
        [SerializeField] private Material _material = null;
        [SerializeField] private GameObject _tape = null;
        private const float _funHeightWasher = 0.5f;
        private const float _funHeightCylinder = 5f;
        private const float _funHeightRingWithBotton = 1.1f;
        private const int _numbDivisions = 50;
        // Количество разделений
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

        public GameObject GetData(float number, GameObject gameObject, bool FirstObject)
        {
            if (_parent.CompareTag("Cargo"))
            {
                _parent = new GameObject();
                var cargo = Instantiate(gameObject, new Vector3(0f, 0f, -number), Quaternion.identity);
                cargo.transform.parent = _parent.transform;
                for (int i = 0; i <= number / _tape.transform.localScale.z; i++)
                {
                    var tape = Instantiate(_tape, new Vector3(0f, 0f, (_tape.transform.localScale.z * i) - _tape.transform.localScale.z / 2), Quaternion.identity);
                    tape.transform.parent = _parent.transform;
                }
            }
            else if (_parent.CompareTag("Cylinder"))//add a dependency on the radius of the object
            {
                _parent = Instantiate(gameObject, Vector3.zero, Quaternion.identity);
                _aBig = number;
                _bBig = _aBig;
                //_parent.GetComponent<MyCylinder>().Radius = number;!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                _washer = new GameObject();
                GenerateWasher(_washer, _funHeightCylinder);
                _washer.transform.parent = _parent.transform;
                GenerateEllips(true);
            }
            if (!FirstObject)
                _parent.SetActive(false);
            return _parent;
        }
        public GameObject GetData(float radius, GameObject gameObject, bool connectionType, bool FirstObject)
        {
            _parent = Instantiate(gameObject, Vector3.zero, Quaternion.identity);
            _aBig = radius;
            _bBig = _aBig;
            if (_parent.CompareTag("Disk"))
            {
                _parent.GetComponent<Disk>().Radius = radius;
                // Сделать родителя для наследников для кольца
                _washer = new GameObject();
                GenerateWasher(_washer, _funHeightWasher);
                _washer.transform.parent = _parent.transform;
            }
            else if (_parent.CompareTag("Ring"))
            {
                _parent.GetComponent<Ring>().Radius = radius;
                _ringWithBottom = new GameObject();
                GenerateRingWithBottom(_ringWithBottom);
                _ringWithBottom.transform.parent = _parent.transform;
                _ringWithBottom.name = "RingWithBottom";
                _ringWithBottom.tag = "RingWithBottom";
            }
            GenerateEllips(false);
            if (!FirstObject)
                _parent.SetActive(false);
            if (connectionType)
            {

            }
            return _parent;
        }
        private void AddCargo()
        {

        }
        public void GenerateType(float lenght, Vector3 position, float angle)
        {

        }
        private void GenerateEllips(bool high)
        {
            _ellipsGO = new GameObject();
            _ellipsGO.AddComponent<MeshFilter>().mesh = _ellips;
            _ellipsGO.AddComponent<MeshRenderer>().material = _material;
            _ellipsGO.transform.position -= new Vector3(0f, 0f, 0.1f);
            if (high)
            {
                _ellipsGO.transform.localScale += new Vector3(0f, 0f, _ellipsGO.transform.localScale.z) * 10;
            }
            _ellipsGO.transform.parent = _parent.transform;
            _ellipsGO.name = "Ellips";
            _ellipsGO.tag = "Ellips";
        }
        private void GenerateWasher(GameObject _washer, float height)
        {
            _vertex = new List<Vector3>();
            _triangl = new List<int>();
            _mesh = new Mesh();
            _washer.AddComponent<MeshFilter>().mesh = _mesh;
            _mesh.name = "Washer";
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
            // Для верхнего основания
            for (_summAngle = 0f; _summAngle < _around; _summAngle += _around / _numbDivisions)
            {
                _vertex.Add(new Vector3(xt(_aBig, _summAngle), height, zt(_bBig, _summAngle)));
            }
            for (_summAngle = 0f; _summAngle < _around; _summAngle += _around / _numbDivisions)
            {
                _vertex.Add(new Vector3(xt(_aLittle, _summAngle), height, zt(_bLittle, _summAngle)));
            }
            // Для боковых сторон от 40
            for (_summAngle = 0f; _summAngle < _around; _summAngle += _around / _numbDivisions)
            {
                _vertex.Add(new Vector3(xt(_aBig, _summAngle), 0f, zt(_bBig, _summAngle)));
            }
            for (_summAngle = 0f; _summAngle < _around; _summAngle += _around / _numbDivisions)
            {
                _vertex.Add(new Vector3(xt(_aBig, _summAngle), height, zt(_bBig, _summAngle)));
            }
            for (_summAngle = 0f; _summAngle < _around; _summAngle += _around / _numbDivisions)
            {
                _vertex.Add(new Vector3(xt(_aLittle, _summAngle), 0f, zt(_bLittle, _summAngle)));
            }
            for (_summAngle = 0f; _summAngle < _around; _summAngle += _around / _numbDivisions)
            {
                _vertex.Add(new Vector3(xt(_aLittle, _summAngle), height, zt(_bLittle, _summAngle)));
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
            // Внешняя боковая сторона
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

            _triangles = new int[_triangl.Count];
            for (int i = 0; i < _triangl.Count; i++)
            {
                _triangles[i] = _triangl[i];
            }
            _mesh.triangles = _triangles;
            _mesh.RecalculateNormals();
            _washer.AddComponent<MeshRenderer>().material = _material;
            _washer.AddComponent<MeshCollider>().sharedMesh = _mesh;
            _washer.name = "Washer";
            _washer.tag = "Washer";
            // Для сохранения
            //AssetDatabase.CreateAsset(_mesh, "Assets/Meshs/Washer.asset");
        }
        private void GenerateRingWithBottom(GameObject ringWithBottom)
        {
            _vertex = new List<Vector3>();
            _triangl = new List<int>();
            _mesh = new Mesh();
            ringWithBottom.AddComponent<MeshFilter>().mesh = _mesh;
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
            ringWithBottom.AddComponent<MeshRenderer>().material = _material;
            ringWithBottom.AddComponent<MeshCollider>().sharedMesh = _mesh;
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
}
