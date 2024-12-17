using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPosManager : MonoBehaviour
{
    [SerializeField] private GameObject _cam;

    [SerializeField] private List<GameObject> _camPositions;

    private int _currentCam;

    private bool _moveCamRight = false;
    private bool _moveCamLeft = false;

    // Start is called before the first frame update
    void Start()
    {
        //_cam.transform.position = _camPositions[_currentCam].transform.position;
        //_cam.transform.rotation = _camPositions[0].transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        _cam.transform.position = _camPositions[_currentCam].transform.position;
        _cam.transform.rotation = _camPositions[_currentCam].transform.rotation;
        
        MoveCam();
    }

    /// <summary>
    /// Calls the proper attacking logic when the checks pass
    /// </summary>
    private void MoveCam()
    {
        if (_moveCamRight == true)
        {
            if(_currentCam == _camPositions.Count - 1) _currentCam = 0;
            else _currentCam++;

            _moveCamRight = false;
        }

        if (_moveCamLeft == true)
        {
            if (_currentCam == 0) _currentCam = _camPositions.Count - 1;
            else _currentCam--;

            _moveCamLeft = false;
        }
    }

    /// <summary>
    /// When input is received, modify the checks
    /// </summary>
    private void OnCamMoveRight()
    {
        _moveCamRight = true;
    }

    /// <summary>
    /// When input is received, modify the checks
    /// </summary>
    private void OnCamMoveLeft()
    {
        _moveCamLeft = true;
    }


    public GameObject GetCurrentCam()
    {
        return _camPositions[_currentCam];
    }
}
