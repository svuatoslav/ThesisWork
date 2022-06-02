using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace K2
{
    public class PanelMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _menu = null;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_menu.activeSelf)
                    _menu.SetActive(false);
                else
                    _menu.SetActive(true);
            }

        }
        public void QuitSandbox()
        {
            // clear meshs
            SceneManager.LoadScene(0);
        }
    }
}