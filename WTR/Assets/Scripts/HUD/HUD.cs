using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VHS;

public class HUD : MonoBehaviour
{
    public GameObject Player;
    private PlayerManager _player;
    public Image SprintBarLevelImage;
    public GameObject menuEchap;
    private bool isMenuOpen;
    public GameObject menuOption;
    public GameObject blur;
    public GameObject[] objectToDisabled;
    public Camera cam;

    public CameraController cameraController;

    public Quaternion lastCameraRotation; // Pour caméra

    void Start()
    {
        _player = Player.GetComponent<PlayerManager>();
        menuEchap.SetActive(false);
    }

    void Update()
    {
        if (_player.m_sprint || _player.m_run) SprintBar();
        if (Input.GetKeyDown(KeyCode.Escape)) StateManager();
    }

    public void StateManager()
    {
        blur.SetActive(!isMenuOpen);
        isMenuOpen = !isMenuOpen;
        menuEchap.SetActive(isMenuOpen);
        Cursor.visible = isMenuOpen;
        Cursor.lockState = isMenuOpen ? CursorLockMode.Confined : CursorLockMode.Locked;
        cameraController.enabled = !isMenuOpen;
        Time.timeScale = isMenuOpen ? 0 : 1;
        ToggleAllObjects(!isMenuOpen);
    }

    public void ResetLockState()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void SprintBar()
    {
        SprintBarLevelImage.fillAmount = _player.sprint / _player.maxSprint;
        _player.sprint = Mathf.Clamp(_player.sprint, 0f, _player.maxSprint);
    }

    void ToggleAllObjects(bool active)
    {
        foreach (GameObject obj in objectToDisabled)
        {
            if (obj != null)
            {
                obj.SetActive(active);
            }
        }
    }
}
