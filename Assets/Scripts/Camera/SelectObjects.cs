using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace K2
{
    public class SelectObjects : MonoBehaviour
    {
        [SerializeField] private Material _select = null;
        //[SerializeField] private Material _new = null;
        [SerializeField] private Material _old = null;
        private Ray _ray;
        private GameObject _selectGameObject;
        private RaycastHit _hit;
        public GameObject NewObject { private get; set; } = null;
        public float RadiusNewObject { private get; set; } = 0f;
        public float AngelSpeed { private get; set; } = 0f;
        private float _radiusSelectObject = 0f;
        private float _heightSelectObject = 0f;
        private Transform[] _relatedObjects;
        private bool _attach = false;
        private int _Id = 0;
        private Coroutine _choice;
        private Disk _washer;
        private Ring _ring;
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (_attach)
                {
                    StopCoroutine(_choice);
                    AddNewNeighbows();
                    _attach = false;
                    _selectGameObject.transform.GetChild(0).GetComponent<Renderer>().material = _old;
                }
                else
                {
                    _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(_ray, out _hit, Mathf.Infinity))
                    {
                        _selectGameObject = _hit.transform.parent.gameObject;
                        GameObjectSelected();
                        //_hit.transform.gameObject.GetComponent<PointInformation>().CreateInformation();
                    }
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(_ray, out _hit, Mathf.Infinity))
                {
                    _selectGameObject = _hit.transform.parent.gameObject;
                    if (_selectGameObject.CompareTag("Disk"))//возможны баги!!!!! (newObjects и т.д.)
                        _selectGameObject.GetComponent<Disk>().AngelSpeed = this.AngelSpeed;
                    else if (_selectGameObject.CompareTag("Ring"))
                        _selectGameObject.GetComponent<Ring>().AngelSpeed = this.AngelSpeed;
                }
            }
        }
        private void AddNewNeighbows()
        {
            _relatedObjects[_Id] = NewObject.transform;
            if (_selectGameObject.transform.CompareTag("Disk"))
            {
                if (NewObject.transform.CompareTag("Disk"))
                    _relatedObjects = NewObject.GetComponent<Disk>().RelatedObjects;
                else if (NewObject.transform.CompareTag("Ring"))
                    _relatedObjects = NewObject.GetComponent<Ring>().RelatedObjects;
                Debug.Log(NewObject.tag);
                Debug.LogWarning(_relatedObjects);
                Debug.LogError(NewObject.GetComponent<Ring>().gameObject);
                Debug.Log(NewObject.GetComponent<Ring>().RelatedObjects);
                Debug.Log(_relatedObjects[_Id - 1]);
                Debug.Log(_relatedObjects[_Id + 1]);
                if (_Id % 2 == 1)
                    _relatedObjects[_Id - 1] = _selectGameObject.transform;
                else
                    _relatedObjects[_Id + 1] = _selectGameObject.transform;//error 
                                                                           //NullReferenceException: Object reference not set to an instance of an object
            }
            else if (_selectGameObject.transform.CompareTag("Ring"))
            {
                if (NewObject.transform.CompareTag("Disk"))
                    _relatedObjects = NewObject.GetComponent<Disk>().RelatedObjects;
                else if (NewObject.transform.CompareTag("Ring"))
                    _relatedObjects = NewObject.GetComponent<Ring>().RelatedObjects;
                if (_Id <= 3)// magic
                {
                    if (_Id % 2 == 1)
                        _relatedObjects[_Id - 1] = _selectGameObject.transform;
                    else
                        _relatedObjects[_Id + 1] = _selectGameObject.transform;
                }
                else
                    _relatedObjects[_Id % 4] = _selectGameObject.transform;
            }
        }
        private void GameObjectSelected()
        {
            if (_selectGameObject.transform.CompareTag("Disk"))
            {
                _washer = _selectGameObject.GetComponent<Disk>();
                _radiusSelectObject = _washer.Radius;
                _heightSelectObject = _washer.Height;
                _relatedObjects = _washer.RelatedObjects;
            }
            else if (_selectGameObject.transform.CompareTag("Ring"))
            {
                _ring = _selectGameObject.transform.GetComponent<Ring>();
                _radiusSelectObject = _ring.Radius;
                _heightSelectObject = _ring.Height;
                _relatedObjects = _ring.RelatedObjects;
            }
            Activation();
            _selectGameObject.transform.GetChild(0).GetComponent<Renderer>().material = _select;
            _choice = StartCoroutine(Choice());
            _attach = true;// для одного объекта
        }
        private void Activation()
        {
            if (NewObject.activeSelf)
            {
                NewObject = Instantiate(NewObject, new Vector3(_radiusSelectObject + RadiusNewObject, 0f, 0f), Quaternion.identity);
                if (NewObject.transform.CompareTag("Disk"))
                    NewObject.GetComponent<Disk>().Radius = RadiusNewObject;
                else if (NewObject.transform.CompareTag("Ring"))
                    NewObject.GetComponent<Ring>().Radius = RadiusNewObject;
            }
            else
                NewObject.SetActive(true);
        }
        private Vector3 LocationNewFacility(int i, float radiusobject1)// через enum попробовать написать для безопасности и связать с объектом
        {
            if (_selectGameObject.transform.CompareTag("Disk"))
            {
                if (NewObject.transform.CompareTag("Disk"))
                {
                    Vector3[] iPosition = { new Vector3(radiusobject1 + RadiusNewObject, 0f, 0f), new Vector3(-(radiusobject1 + RadiusNewObject), 0f, 0f),
                    new Vector3(0f, 0f, radiusobject1 + RadiusNewObject), new Vector3(0f, 0f, -(radiusobject1 + RadiusNewObject)),
                    new Vector3(0f, -_heightSelectObject, 0f), new Vector3(0f, _heightSelectObject, 0f) };
                    _Id = i % iPosition.Length;
                    return _selectGameObject.transform.position + iPosition[i % iPosition.Length];
                }
                else if (NewObject.transform.CompareTag("Ring"))
                {
                    Vector3[] iPosition = { new Vector3(radiusobject1 + RadiusNewObject, 0f, 0f), new Vector3(-(radiusobject1 + RadiusNewObject), 0f, 0f),
                    new Vector3(0f, 0f, radiusobject1 + RadiusNewObject), new Vector3(0f, 0f, -(radiusobject1 + RadiusNewObject)),
                    new Vector3(0f, -_heightSelectObject, 0f), new Vector3(0f, _heightSelectObject, 0f) };
                    _Id = i % iPosition.Length;
                    return _selectGameObject.transform.position + iPosition[i % iPosition.Length];
                }
            }
            else if (_selectGameObject.transform.CompareTag("Ring"))
            {
                if (NewObject.transform.CompareTag("Disk"))
                {
                    Vector3[] iPosition = { new Vector3(radiusobject1 + RadiusNewObject, 0f, 0f),
                    new Vector3(-(radiusobject1 + RadiusNewObject), 0f, 0f),
                    new Vector3(0f, 0f, radiusobject1 + RadiusNewObject),
                    new Vector3(0f, 0f, -(radiusobject1 + RadiusNewObject)),
                    new Vector3(radiusobject1 - RadiusNewObject, _heightSelectObject / 2, 0f),
                    new Vector3(-(radiusobject1 - RadiusNewObject), _heightSelectObject / 2, 0f),
                    new Vector3(0f, _heightSelectObject / 2, (radiusobject1 - RadiusNewObject)),
                    new Vector3(0f, _heightSelectObject / 2, -(radiusobject1 - RadiusNewObject)) };
                    _Id = i % iPosition.Length;
                    return _selectGameObject.transform.position + iPosition[i % iPosition.Length];
                }
                else if (NewObject.transform.CompareTag("Ring"))
                {
                    Vector3[] iPosition = { new Vector3(radiusobject1 + RadiusNewObject, 0f, 0f), new Vector3(-(radiusobject1 + RadiusNewObject), 0f, 0f),
                    new Vector3(0f, 0f, radiusobject1 + RadiusNewObject), new Vector3(0f, 0f, -(radiusobject1 + RadiusNewObject)) };
                    _Id = i % iPosition.Length;
                    return _selectGameObject.transform.position + iPosition[i % iPosition.Length];
                }
            }
            return Vector3.zero;
        }
        private IEnumerator Choice()
        {
            Debug.Log(_relatedObjects);
            for (int i = 0; i < _relatedObjects.Length * 1000; i++)
            {
                if (_relatedObjects[i % _relatedObjects.Length] == null)
                    NewObject.transform.position = LocationNewFacility(i, _radiusSelectObject);
                else
                    continue;
                yield return new WaitForSeconds(1);
            }
            //for (int i = 0; i < (int)RelativePositionDisk._yBot; i++)
            //{
            //    RelativePositionDisk i1 = (RelativePositionDisk)i;
            //    Debug.LogError((RelativePositionDisk)i);
            //    if ()
            //}
        }
        enum RelativePositionDisk
        {
            _xRighr,
            _xLeft,
            _zRighr,
            _zLeft,
            _yTop,
            _yBot
        }
        enum RelativePositionRWB
        {
            _xRighrOut,
            _xLeftOut,
            _zRighrOut,
            _zLeftOut,
            _xRighrIn,
            _xLeftIn,
            _zRighrIn,
            _zLeftIn,
        }
    }
}
