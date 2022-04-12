using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class RectangleRecord : MonoBehaviour
{
    private void Start()
    {
        Mesh _mesh = new Mesh();
        Vector3[] _vertices = { Vector3.zero, Vector3.up, Vector3.right, Vector3.up + Vector3.right };
        _mesh.vertices = _vertices;
        int[] _triangles = { 0, 1, 2, 2, 1, 3 };
        _mesh.triangles = _triangles;
        _mesh.RecalculateNormals();
        gameObject.GetComponent<MeshFilter>().mesh = _mesh;
    }
}
