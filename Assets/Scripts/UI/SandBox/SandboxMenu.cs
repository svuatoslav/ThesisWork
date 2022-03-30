using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SandboxMenu : MonoBehaviour
{
    [SerializeField] private GameObject _panelGreateObject = null;
    [SerializeField] private GameObject _panelGame = null;
    public void OpenPanelCreate()
    {
        _panelGame.SetActive(false);
        _panelGreateObject.SetActive(true);
        _panelGreateObject.GetComponent<SandboxPanellCreate>().GetPosition(Camera.main.transform.position);
        Camera.main.transform.position = new Vector3(1000f, 1000f, 1000f);
    }
}
