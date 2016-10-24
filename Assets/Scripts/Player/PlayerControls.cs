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
            hold();
        }
        if (Input.GetButtonDown(buttonCallName))
        {
            press();
        }
        if (Input.GetButtonUp(buttonCallName))
        {
            release();
        }

    }
    void press() {
        manager.getMessage(buttonID, ACTION.PRESS);
        Debug.Log("PRESS: " + buttonID);
    }

    void hold() {
        manager.getMessage(buttonID, ACTION.HOLD);
        Debug.Log("HOLD: " + buttonID);

    }

    void release() {
        manager.getMessage(buttonID, ACTION.RELEASE);
        Debug.Log("RELEASE: " + buttonID);

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

    public char playerLetter;
    public GameObject player;

    private string joystickCallNameHorizontal;
    private string joystickCallNameVertical;


    private float horizontal;
    private float vertical;
    public bool useKeyboard; // set outside of play modes


    private List<ControlButton> buttons;
    void Awake () {
        buttons = new List<ControlButton>();
        for (int i = 0; i < numOfButtons; i++) {
            ControlButton b;
            if(!useKeyboard)
                b = new ControlButton(""+playerLetter+i,i,player.GetComponent<PlayerManager>());
            else
                b = new ControlButton("K"+ i, i, player.GetComponent<PlayerManager>());

            buttons.Add(b);
            Debug.Log("BUTTON " + i +" size is "+numOfButtons);
        }
        if (useKeyboard)
        {
            joystickCallNameHorizontal = "Horizontal";
            joystickCallNameVertical = "Vertical";
        }
        else
        {
            joystickCallNameHorizontal = playerLetter+"H";
            joystickCallNameVertical = playerLetter+"V";
        }
    }

    void Update() {
        horizontal = Input.GetAxis(joystickCallNameHorizontal);
        vertical = Input.GetAxis(joystickCallNameVertical);


        move();
        checkButtons();

    }


    private void move() {
        if (!Mathf.Approximately(vertical, 0.0f) || !Mathf.Approximately(horizontal, 0.0f))
        {
            float rotateStep = rotationSpeed * Time.deltaTime;
            Quaternion previous = player.transform.rotation;
            Vector3 direction = new Vector3(horizontal, 0.0f, vertical);
            direction = direction.normalized;
            Quaternion target = Quaternion.LookRotation(direction, Vector3.up);
            player.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
            player.transform.Translate(new Vector3(horizontal, 0, vertical).normalized * moveSpeed * Time.deltaTime);

            player.transform.rotation = Quaternion.Slerp(previous, target, rotateStep);
        }
    }


    private void checkButtons() {
        for (int i = 0; i <buttons.Count;i++){
            buttons[i].check();

        }

    }


   


}
