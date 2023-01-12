using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using InGamePacket;

public class GameManager : Singleton<GameManager>
{
    // - Member Variable
    public GameObject _TempMovingObj;


// - Method
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        InputKey();
    }

    private void InputKey()
    {
        C_MOVE _packet = new C_MOVE();

        // - MoveTest
        if (Input.GetKeyUp(KeyCode.A))
        {
            _packet._input = KeyCode.A;
            PTPServerManager.Instance.SendMessage(_packet);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            _packet._input = KeyCode.D;
            PTPServerManager.Instance.SendMessage(_packet);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            _packet._input = KeyCode.W;
            PTPServerManager.Instance.SendMessage(_packet);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            _packet._input = KeyCode.S;
            PTPServerManager.Instance.SendMessage(_packet);
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            PTPServerManager.Instance.DisConnectServer();
        }
    }

    public bool Move(KeyCode input)
    {
        switch(input)
        {
            case KeyCode.A:
                _TempMovingObj.transform.localPosition += Vector3.left;
                break;
            case KeyCode.D:
                _TempMovingObj.transform.localPosition += Vector3.right;
                break;
            case KeyCode.W:
                _TempMovingObj.transform.localPosition += Vector3.up;
                break;
            case KeyCode.S:
                _TempMovingObj.transform.localPosition += Vector3.down;
                break;
        };
        return true;
    }
}
