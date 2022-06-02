using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace K2
{
    public class SandboxMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _panelGreateObject = null;
        [SerializeField] private GameObject _panelGame = null;
        [SerializeField] private GameObject _panelParameters = null;
        [SerializeField] private GameObject _panelNewTask = null;
        public void OpenPanelCreate()
        {
            _panelGame.SetActive(false);
            _panelGreateObject.SetActive(true);
            _panelGreateObject.GetComponent<SandboxPanelCreate>().GetPosition(Camera.main.transform.position);
            Camera.main.transform.position = new Vector3(1000f, 1000f, 1000f);
        }
        public void OpenPanelParametrs()
        {
            _panelGame.SetActive(false);
            _panelParameters.SetActive(true);
        }
        public void OpenPanelNewTask()
        {
            _panelGame.SetActive(false);
            _panelNewTask.SetActive(true);
        }
    }
}
