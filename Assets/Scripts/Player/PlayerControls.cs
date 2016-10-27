using UnityEngine;
using System.Collections.Generic;


public class ControlButton {
    public string buttonCallName;
    
    private int buttonID;
    private PlayerManager manager;

    public enum ACTION {PRESS,HOLD,RELEASE};

    public ControlButton(string name,int id, PlayerManager m) {
        setName(name);
        setManager(m);
        buttonID = id;
   }
    public void setName(string name) {
        buttonCallName = name;
    }
    private void setManager(PlayerManager m) {
        manager = m;
    }

    public void check() {

        if (Input.GetButton(buttonCallName))
        {
            manager.getMessage(buttonID, ACTION.HOLD);
            
        }
        if (Input.GetButtonDown(buttonCallName))
        {
            manager.getMessage(buttonID, ACTION.PRESS);
        }
        if (Input.GetButtonUp(buttonCallName))
        {
            manager.getMessage(buttonID, ACTION.RELEASE);
        }

    }

}

public class ControlJoysitck {
    public string joystickCallName;
    public float deadzone;
    private PlayerManager manager;
    private int stickID;

    public ControlJoysitck(string name, int id, PlayerManager m)
    {
        setName(name);
        setManager(m);
        stickID = id;
        Debug.Log("STICK MADE");
    }
    public void setName(string name)
    {
        joystickCallName = name;
    }
    private void setManager(PlayerManager m)
    {
        manager = m;
    }

    public void check()
    {
        float horizontal = Input.GetAxis(joystickCallName+"H");
        float vertical = Input.GetAxis(joystickCallName + "V");

        Vector3 stickDirection = new Vector3(0, 0, 0);

        if (Mathf.Abs(horizontal) > deadzone)
            stickDirection += horizontal * Vector3.right;

        if (Mathf.Abs(vertical) > deadzone)
            stickDirection += horizontal * Vector3.forward;

       // manager.getMessage(stickID, stickDirection);

    }

}
public class PlayerControls : MonoBehaviour {

    public float moveSpeed;
    public float rotationSpeed;
    public int numOfButtons;

    private PlayerManager player;

    private string joystickCallNameHorizontal;
    private string joystickCallNameVertical;


    private float horizontal;
    private float vertical;


    private List<ControlButton> buttons;

    public void update() {
        horizontal = Input.GetAxis(joystickCallNameHorizontal);
        vertical = Input.GetAxis(joystickCallNameVertical);
        move();
        checkButtons();

    }


    public void setPlayerChar(char c) {
        buttons = new List<ControlButton>();
        for (int i = 0; i < numOfButtons; i++)
        {
            ControlButton b;
            b = new ControlButton("" + c + i, i, player.GetComponent<PlayerManager>());
            buttons.Add(b);
        }

            joystickCallNameHorizontal = c + "H";
            joystickCallNameVertical = c + "V";
    }
    public void setPlayer(PlayerManager p) {
        player = p;
        setPlayerChar(p.getPlayerChar());
    }
    private void move() {
        if (!Mathf.Approximately(vertical, 0.0f) || !Mathf.Approximately(horizontal, 0.0f))
        {
            float rotateStep = player.settings.rotateSpeed * Time.deltaTime;
            Quaternion previous = player.transform.rotation;
            Vector3 direction = new Vector3(horizontal, 0.0f, vertical);
            direction = direction.normalized;
            Quaternion target = Quaternion.LookRotation(direction, Vector3.up);
            player.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
            player.transform.Translate(new Vector3(horizontal, 0, vertical).normalized * player.settings.moveSpeed * Time.deltaTime);

            player.transform.rotation = Quaternion.Slerp(previous, target, rotateStep);
        }
    }


    private void checkButtons() {
        for (int i = 0; i <buttons.Count;i++){
            buttons[i].check();

        }

    }

}
