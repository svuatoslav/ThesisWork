using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObjects : MonoBehaviour
{
    [SerializeField] private float _speedOnRim = 0f;
    [SerializeField] private Material _select = null;
    //[SerializeField] private Material _new = null;
    [SerializeField] private Material _old = null;
    private Ray _ray;
    private RaycastHit _hit;
    public GameObject NewObject { private get; set; } = null;
    public float RadiusNewObject { private get; set; } = 0f;
    private float _radiusSelectObject = 0f;
    private float _heightSelectObject = 0f;
    private Transform[] _relatedObjects;
    private bool _attach = false;
    private int i = 0;
    private Coroutine _choice;
    private WasherScr _washerScr;
    private Ring _ring;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(_attach)
            {
                StopCoroutine(_choice);
                if (_hit.transform.tag == "Disk" || _hit.transform.tag == "Washer")
                {
                    if (NewObject.tag == "Washer")
                        _relatedObjects[i] = NewObject.transform.parent.transform;
                    else
                        _relatedObjects[i] = NewObject.transform;
                    if (NewObject.transform.tag == "Disk")
                        _relatedObjects = NewObject.GetComponent<WasherScr>().RelatedObjects;
                    else if (NewObject.transform.tag == "Ring")
                        _relatedObjects = NewObject.GetComponent<Ring>().RelatedObjects;
                }
                else if (_hit.transform.tag == "Ring" || _hit.transform.tag == "RingWithBottom")
                {
                    if (NewObject.tag == "RingWithBottom")
                        _relatedObjects[i] = NewObject.transform.parent.transform;
                    else
                        _relatedObjects[i] = NewObject.transform;
                    if (NewObject.transform.tag == "Disk")
                        _relatedObjects = NewObject.GetComponent<WasherScr>().RelatedObjects;
                    else if (NewObject.transform.tag == "Ring")
                        _relatedObjects = NewObject.GetComponent<Ring>().RelatedObjects;
                }
                if (i % 2 == 1)
                    _relatedObjects[i - 1] = _hit.transform;
                else
                    _relatedObjects[i + 1] = _hit.transform;
                _attach = false;
                _hit.transform.GetComponent<Renderer>().material = _old;
            }
            else
            {
                _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(_ray, out _hit, Mathf.Infinity))
                {
                    Debug.LogWarning(_hit.transform.tag);
                    if (_hit.transform.tag == "Disk" || _hit.transform.tag == "Washer")//возможны баги!!!!! (newObjects и т.д.) _hit.transform.tag == "Ellips"
                        DiskSelected();
                    else if (_hit.transform.tag == "Ring" || _hit.transform.tag == "RingWithBottom")
                        RingSelected();
                    //_hit.transform.gameObject.GetComponent<PointInformation>().CreateInformation();
                }
            }
        }
        else if(Input.GetMouseButtonDown(1))
        {
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out _hit, Mathf.Infinity))
            {
                if (_hit.transform.tag == "Disk" || _hit.transform.tag == "Washer")//возможны баги!!!!! (newObjects и т.д.)
                {
                    _hit.transform.parent.GetComponent<WasherScr>().SpeedOnRim = _speedOnRim;
                }
            }
        }
    }
    private void DiskSelected()
    {
        _washerScr = _hit.transform.parent.GetComponent<WasherScr>();
        Activation();
        _radiusSelectObject = _washerScr.Radius;
        _heightSelectObject = _washerScr.Height;
        _relatedObjects = _washerScr.RelatedObjects;
        _hit.transform.GetComponent<Renderer>().material = _select;
        _choice = StartCoroutine(Choice());
        //_newObject.GetComponent<Renderer>().material = _new;
        _attach = true; // для одного объекта
    }
    private void RingSelected()
    {
        _ring = _hit.transform.parent.GetComponent<Ring>();
        Activation();
        _radiusSelectObject = _ring.Radius;
        _heightSelectObject = _ring.Height;
        _relatedObjects = _ring.RelatedObjects;
        _hit.transform.GetComponent<Renderer>().material = _select;
        _choice = StartCoroutine(Choice());
        //_newObject.GetComponent<Renderer>().material = _new;
        _attach = true; // для одного объекта

    }
    private void Activation()
    {
        if (NewObject.activeSelf)
        {
            NewObject = Instantiate(NewObject, Vector3.zero, Quaternion.identity);
            if (_hit.transform.tag == "Disk" || _hit.transform.tag == "Washer")
                NewObject.GetComponent<WasherScr>().Radius = RadiusNewObject;
            else if (_hit.transform.tag == "Ring" || _hit.transform.tag == "RingWithBottom")
                NewObject.GetComponent<Ring>().Radius = RadiusNewObject;
        }
        else
            NewObject.SetActive(true);
    }
    //public void SetObjects(GameObject newObject, float _radiusNewObject)
    //{
    //    NewObject = newObject;
    //    this.RadiusNewObject = _radiusNewObject;
    //}

    private Vector3 PositionNewObjects(Vector3 old, int i, float radiusobject1)// через enum попробовать написать для безопасности и связать с объектом
    {
        if (_hit.transform.tag == "Disk" || _hit.transform.tag == "Washer")
        {
            if (NewObject.transform.tag == "Disk" || NewObject.transform.tag == "Washer")
            {
                //функция позиций
                Vector3[] iPosition = { new Vector3(radiusobject1 + RadiusNewObject, 0f, 0f), new Vector3(-(radiusobject1 + RadiusNewObject), 0f, 0f),
                new Vector3(0f, 0f, (radiusobject1 + RadiusNewObject)), new Vector3(0f, 0f, -(radiusobject1 + RadiusNewObject)),
                new Vector3(0f, -_heightSelectObject, 0f), new Vector3(0f, _heightSelectObject, 0f) };
                this.i = i % iPosition.Length;
                return old + iPosition[i % iPosition.Length];
            }
            else
            {
                //if(radius)
                //{
                //    Vector3[] iPosition = { new Vector3(radiusobject1 + RadiusNewObject, 0f, 0f), new Vector3(-(radiusobject1 + RadiusNewObject), 0f, 0f),
                //new Vector3(0f, 0f, (radiusobject1 + RadiusNewObject)), new Vector3(0f, 0f, -(radiusobject1 + RadiusNewObject)),
                //new Vector3(0f, -_heightSelectObject, 0f), new Vector3(0f, _heightSelectObject, 0f) };

                //}
                //else
                //{
                Vector3[] iPosition = { new Vector3(radiusobject1 + RadiusNewObject, 0f, 0f), new Vector3(-(radiusobject1 + RadiusNewObject), 0f, 0f),
                new Vector3(0f, 0f, (radiusobject1 + RadiusNewObject)), new Vector3(0f, 0f, -(radiusobject1 + RadiusNewObject)),
                new Vector3(0f, -_heightSelectObject, 0f), new Vector3(0f, _heightSelectObject, 0f) };
                this.i = i % iPosition.Length;
                return old + iPosition[i % iPosition.Length];

                //}
            }

        }
        else if (_hit.transform.tag == "Ring" || _hit.transform.tag == "RingWithBottom")
        {
            if (NewObject.transform.tag == "Disk" || NewObject.transform.tag == "Washer")
            {
                Vector3[] iPosition = { new Vector3(radiusobject1 + RadiusNewObject, 0f, 0f), new Vector3(-(radiusobject1 + RadiusNewObject), 0f, 0f),
                new Vector3(0f, 0f, (radiusobject1 + RadiusNewObject)), new Vector3(0f, 0f, -(radiusobject1 + RadiusNewObject)),
                new Vector3(radiusobject1 - RadiusNewObject, _heightSelectObject / 2, 0f),
                new Vector3(-(radiusobject1 - RadiusNewObject), _heightSelectObject / 2, 0f),
                new Vector3(radiusobject1 - RadiusNewObject, _heightSelectObject / 2, 0f),
                new Vector3(-(radiusobject1 - RadiusNewObject), _heightSelectObject / 2, 0f) };
                this.i = i % iPosition.Length;
                return old + iPosition[i % iPosition.Length];

            }
            else
            {
                Vector3[] iPosition = { new Vector3(radiusobject1 + RadiusNewObject, 0f, 0f), new Vector3(-(radiusobject1 + RadiusNewObject), 0f, 0f),
                new Vector3(0f, 0f, (radiusobject1 + RadiusNewObject)), new Vector3(0f, 0f, -(radiusobject1 + RadiusNewObject)) };
                this.i = i % iPosition.Length;
                return old + iPosition[i % iPosition.Length];
            }
        }
        else
            return Vector3.zero;
    }
    private IEnumerator Choice()
    {
        for (int i = 0; i < _relatedObjects.Length * 1000; i++)
        {
            
            if (_relatedObjects[i % _relatedObjects.Length] == null)
            {
                NewObject.transform.position = PositionNewObjects(_hit.transform.position, i, _radiusSelectObject);
            }
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

//находим диск
//for(int i =0;i< _hit.transform.childCount; i++)
//{
//    if(_hit.transform.GetChild(i).tag == "Disk")
//    {
//        i = _hit.transform.childCount+1;

//    }
//}
