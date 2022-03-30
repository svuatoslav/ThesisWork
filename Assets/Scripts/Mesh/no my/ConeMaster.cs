using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ConeMaster : ScriptableWizard
{
    public float height = 3;
    public float radius = 1;
    public int nrad = 10;
    public int nhei = 5;
    public Color color = Color.green;
    public bool setka = false;

    Vector3[] vertices;
    int[] triangles;
    Mesh cone;

    [MenuItem("GameObject/3D Object/Cone")]
    static void ShowWisard()
    {
        ScriptableWizard.DisplayWizard<ConeMaster>("Create a cone", "Create");
    }

    private void OnWizardCreate()
    {

        float deltaHeight = 1.0f / nhei;
        List<Vector3> tempVert = new List<Vector3>();
        List<int> tempTriang = new List<int>();
        List<int> oldVal = new List<int>();
        for (float i = 0; i <= height; i += deltaHeight * height)
        {
            //int ni = (int)((height - i) * nrad / height);
            //if (i < height && ni < 3)
            //{
            //    ni = 3;
            //}
            float angle = i < height ? 360 / nrad : 1000;
            float radi = (height - i) * radius / height;
            List<int> curVal = new List<int>();
            for (float j = 0; j < 360; j += angle)
            {
                tempVert.Add(new Vector3(radi * Mathf.Cos(Mathf.Deg2Rad * j),
                    i, radi * Mathf.Sin(Mathf.Deg2Rad * j)));
                curVal.Add(tempVert.Count - 1);
            }
            if (i > 0)
            {
                for (int j = 0; j < curVal.Count - 1; j++)
                {
                    tempTriang.AddRange(new int[] { oldVal[j],curVal[j],oldVal[j+1],
                    oldVal[j+1],curVal[j],curVal[j+1]});
                }
                for (int j = 0; j < oldVal.Count - curVal.Count; j++)
                {
                    tempTriang.AddRange(new int[] { oldVal[curVal.Count+j-1],
                        curVal[curVal.Count-1],oldVal[curVal.Count+j] });
                }
                int endold = oldVal.Count - 1;
                int endcur = curVal.Count - 1;
                tempTriang.AddRange(new int[] { oldVal[endold],curVal[endcur],oldVal[0],
                    oldVal[0],curVal[endcur],curVal[0]});
            }
            oldVal.Clear();
            oldVal.AddRange(curVal);
        }
        //vertices = tempVert.ToArray();
        //triangles = tempTriang.ToArray();
        cone = new Mesh
        {
            vertices = tempVert.ToArray(),
            triangles = tempTriang.ToArray()
        };
        cone.RecalculateNormals();
        GameObject go = new GameObject();
        MeshFilter mf = go.AddComponent<MeshFilter>();
        MeshRenderer mr = go.AddComponent<MeshRenderer>();
        mf.mesh = cone;
        mr.material = new Material(Shader.Find("Standard"));
        mr.material.color = color;
        if (setka)
        {
            LineRenderer lr = go.AddComponent<LineRenderer>();
            lr.positionCount = tempTriang.Count;
            for (int i = 0; i < tempTriang.Count; i++)
            {
                lr.SetPosition(i, tempVert[tempTriang[i]]);
            }
            //lr.SetPosition(0, new Vector3(radius, 0, 0));
            //lr.SetPosition(1, new Vector3(0, height, 0));
            lr.widthCurve = new AnimationCurve(new Keyframe(0, 0.1f));
            lr.sharedMaterial = new Material(Shader.Find("Sprites/Default"));
            lr.colorGradient = new Gradient();
            lr.colorGradient.SetKeys(new GradientColorKey[] {
        new GradientColorKey(Color.black,0),new GradientColorKey(Color.black,0)},
            new GradientAlphaKey[] { new GradientAlphaKey(1, 0), new GradientAlphaKey(1, 1) });
            lr.useWorldSpace = false;
        }
    }
}
