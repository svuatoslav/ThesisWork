using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ObjectMaster : ScriptableWizard
{
    public float a = 2;
    public float b = -1;
    public float c = 1;
    public float y1 = 0;
    public float y2 = 2;
    public float nrad = 50;
    public float ny = 10;
    public Color color = Color.green;
    public bool setka = false;

    Mesh obj;

    [MenuItem("GameObject/3D Object/ObjectWizard")]
    static void ShowWisard()
    {
        ScriptableWizard.DisplayWizard<ObjectMaster>("Create an object", "Create");
    }
    private void OnWizardCreate()
    {
        float epsilon = 0.00001f;
        List<Vector3> tempVert = new List<Vector3>();
        List<int> tempTriang = new List<int>();
        List<int> oldVal = new List<int>();
        //Делаем дно, если нужно
        if (b * y1 + c != 0)
        {
            Debug.Log("Floor");
            tempVert.Add(new Vector3(0, y1, 0));
            for (float phi = 0; phi < 360; phi+= 360.0f / nrad)
            {
                tempVert.Add(new Vector3((b * y1 + c) / a * Mathf.Cos(Mathf.Deg2Rad * phi),
                                    y1, (b * y1 + c) / a * Mathf.Sin(Mathf.Deg2Rad * phi)));
                oldVal.Add(tempVert.Count - 1);
            }
            for (int j = 0; j < oldVal.Count - 1; j++)
            {
                tempTriang.AddRange(new int[] { oldVal[oldVal.Count - j - 1],
                        0, oldVal[oldVal.Count - j - 2] });
            }
            int endold = oldVal.Count - 1;
            tempTriang.AddRange(new int[] { oldVal[0],0,oldVal[endold] });
            oldVal.Clear();
        }
        //Делаем боковой обход
        for (float y = y1; y <= y2+epsilon; y += (y2 - y1) / ny)
        {
            List<int> curVal = new List<int>();
            for (float phi = 0; phi < 360; phi += 360.0f / nrad)
            {
                tempVert.Add(new Vector3((b * y + c) / a * Mathf.Cos(Mathf.Deg2Rad * phi),
                                    y, (b * y + c) / a * Mathf.Sin(Mathf.Deg2Rad * phi)));
                curVal.Add(tempVert.Count - 1);
            }
            if (y > 0)
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
        //Делаем крышку, если нужно
        if (b * y2 + c != 0)
        {
            Debug.Log("Roof");
            oldVal.Clear();
            tempVert.Add(new Vector3(0, y2, 0));
            int lastPoint = tempVert.Count - 1;
            for (float phi = 0; phi < 360; phi+= 360.0f / nrad)
            {
                tempVert.Add(new Vector3((b * y2 + c) / a * Mathf.Cos(Mathf.Deg2Rad * phi),
                                    y2, (b * y2 + c) / a * Mathf.Sin(Mathf.Deg2Rad * phi)));
                oldVal.Add(tempVert.Count - 1);
            }
            for (int j = 0; j < oldVal.Count - 1; j++)
            {
                tempTriang.AddRange(new int[] { oldVal[j],
                        lastPoint, oldVal[j+1] });
            }
            int endold = oldVal.Count - 1;
            tempTriang.AddRange(new int[] { oldVal[endold], lastPoint, oldVal[0] });
        }
        obj = new Mesh
        {
            vertices = tempVert.ToArray(),
            triangles = tempTriang.ToArray()
        };
        obj.RecalculateNormals();
        GameObject go = new GameObject();
        MeshFilter mf = go.AddComponent<MeshFilter>();
        MeshRenderer mr = go.AddComponent<MeshRenderer>();
        mf.mesh = obj;
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

            lr.widthCurve = new AnimationCurve(new Keyframe(0, 0.1f));
            lr.material = new Material(Shader.Find("Sprites/Default"));
            lr.colorGradient = new Gradient();
            lr.colorGradient.SetKeys(
                new GradientColorKey[] { new GradientColorKey(Color.black,0),
                    new GradientColorKey(Color.black,0)},
                new GradientAlphaKey[] { new GradientAlphaKey(1, 0),
                    new GradientAlphaKey(1, 1) });
            lr.useWorldSpace = false;
        }
    }
}

