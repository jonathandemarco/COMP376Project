using UnityEngine;
using System.Collections.Generic;

public class InputSystem{

	public string callName;
	public int id;
	public PlayerManager manager;

	public float timeAtPress;
	public float timeAtLastPress;
	public float holdTime;
	public float netHoldTime;
	public ACTION lastState;

	public InputSystem(){}

	public void setName(string name) {
		callName = name;
	}
	public void setManager(PlayerManager m) {
		manager = m;
	}

	public virtual void check(){
	}

	public enum ACTION {PRESS,HOLD,RELEASE};

	public void press(){
		holdTime = 0;
		timeAtPress = Time.time;
		lastState = ACTION.PRESS;
	}
	public void hold(){
		holdTime += Time.deltaTime;
		netHoldTime += Time.deltaTime;
		lastState = ACTION.HOLD;
	}
	public void release(){
		lastState = ACTION.RELEASE;
		timeAtLastPress = timeAtPress;
	}
	public float getTimeAtPress() {
		return timeAtPress;
	}
	public float getTimeAtLastPress() {
		return timeAtPress;
	}
	public float getHoldTime() {
		return holdTime;
	}
	public float getNetHoldTime() {
		return netHoldTime;
	}
	public void resetNetHoldTime() {
		netHoldTime = 0;
	}
	public int getID() {
		return id;
	}
	public ACTION getLastState() {
		return lastState;
	}

}
public class ControlButton:InputSystem {


	public ControlButton(string name,int ID, PlayerManager m) {
		setName(name);
		setManager(m);
		id = ID;
	}



	public override void check() {

		if (Input.GetButton(callName))
		{
			hold ();
			manager.getButton(this);

		}
		if (Input.GetButtonDown(callName))
		{
			press ();
			manager.getButton(this);

		}

		if (Input.GetButtonUp(callName))
		{
			release ();
			manager.getButton(this);

		}

	}


}

public class ControlAxis:InputSystem {

	private bool wasPressed = false;
	private float axisThreshold;
	public ControlAxis(string name, int ID, PlayerManager m, float thresh)
    {
		setName(name);
		setManager(m);
		id = ID;
		axisThreshold = thresh;
    }


	public override void check()
    {
		float input = Input.GetAxis(callName);
		if (input > axisThreshold && wasPressed == false) {
			wasPressed = true;
			press ();
			Debug.Log ("PRESS");
		}
		if (input > axisThreshold && wasPressed == true) {
			Debug.Log ("HOLD");

		}
		if (input < axisThreshold && wasPressed == true) {
			wasPressed = false;
			release ();
			Debug.Log ("RELEASE");

		}
    }

}
public class PlayerControls : MonoBehaviour {

    public float moveSpeed;
    public float rotationSpeed;
    public int numOfButtons;
	public int numOfAxes;
	public float axisThreshold;

    private PlayerManager player;

    private string joystickCallNameHorizontal;
    private string joystickCallNameVertical;


    private float horizontal;
    private float vertical;


    private List<ControlButton> buttons;
	private List<ControlAxis> axes;


    public void readInput() {
        horizontal = Input.GetAxis(joystickCallNameHorizontal);
        vertical = Input.GetAxis(joystickCallNameVertical);
        checkButtons();
		checkAxes ();


    }


    public void setPlayerChar(char c) {
        buttons = new List<ControlButton>();
        for (int i = 0; i < numOfButtons; i++)
        {
            ControlButton b = new ControlButton("" + c + i, i, player.GetComponent<PlayerManager>());
            buttons.Add(b);
        }
		axes = new List<ControlAxis>();
		for (int i = 0; i < numOfAxes; i++)
		{
			ControlAxis a = new ControlAxis("" + c+"-AXIS-" + i, i, player.GetComponent<PlayerManager>(),axisThreshold);
			axes.Add(a);
		}
		joystickCallNameHorizontal = ""+c+"H";
		joystickCallNameVertical = ""+c+"V";
			
    }
    public void setPlayer(PlayerManager p) {
        player = p;
        setPlayerChar(p.getPlayerChar());
    }

    public void move() {
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
    public void moveInAir() {
        if (!Mathf.Approximately(vertical, 0.0f) || !Mathf.Approximately(horizontal, 0.0f))
        {
            float rotateStep = player.settings.rotateSpeed * Time.deltaTime;
            Quaternion previous = player.transform.rotation;
            Vector3 direction = new Vector3(horizontal, 0.0f, vertical);
            direction = direction.normalized;
            Quaternion target = Quaternion.LookRotation(direction, Vector3.up);
            player.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
            player.GetComponent<Rigidbody>().AddForce(new Vector3(horizontal, 0, vertical).normalized * player.settings.airMoveForce * Time.deltaTime);

            player.transform.rotation = Quaternion.Slerp(previous, target, rotateStep);
        }
    }

	private void checkAxes(){
		for (int i = 0; i <axes.Count;i++){
			axes[i].check();

		}

	}
    private void checkButtons() {
        for (int i = 0; i <buttons.Count;i++){
            buttons[i].check();

        }

    }

}
