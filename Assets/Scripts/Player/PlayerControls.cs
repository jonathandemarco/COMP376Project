using UnityEngine;
using System.Collections.Generic;

public class InputSystem{

	public string callName;
	public int id;
	public PlayerManager manager;
	public PlayerControls controller;

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
	public void setController(PlayerControls c) {
		controller = c;
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


	public ControlButton(string name,int ID, PlayerManager m, PlayerControls c) {
		setName(name);
		setManager(m);
		id = ID;
		controller = c;
	}



	public override void check() {

		if (Input.GetButton(callName))
		{
			hold ();
			controller.getButton(this);

		}
		if (Input.GetButtonDown(callName))
		{
			press ();
			Debug.Log ("Boop");
			controller.getButton(this);

		}

		if (Input.GetButtonUp(callName))
		{
			release ();
			controller.getButton(this);

		}

	}


}

public class ControlAxis:InputSystem {

	private bool wasPressed = false;
	private float axisThreshold;
	public ControlAxis(string name, int ID, PlayerManager m,PlayerControls c, float thresh)
    {
		setName(name);
		setManager(m);
		id = ID;
		controller = c;
	
		axisThreshold = thresh;
    }


	public override void check()
    {
		float input = Input.GetAxis(callName);
		if (input > axisThreshold && wasPressed == false) {
			wasPressed = true;
			press ();
			controller.getAxis (this);
		}
		if (input > axisThreshold && wasPressed == true) {
			hold ();
			controller.getAxis (this);


		}
		if (input < axisThreshold && wasPressed == true) {
			wasPressed = false;
			release ();
			controller.getAxis (this);


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

	private bool actionButton_1 = false;
	private bool actionButton_2 = false;
	private float nextButtonPress = 0;

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
            ControlButton b = new ControlButton("" + c + i, i, player.GetComponent<PlayerManager>(), this);
            buttons.Add(b);
        }

		axes = new List<ControlAxis>();
		for (int i = 0; i < numOfAxes; i++)
		{
			ControlAxis a = new ControlAxis("" + c+"-AXIS-" + i, i, player.GetComponent<PlayerManager>(),this, axisThreshold);
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


	public void getButton(ControlButton button)
	{
		int buttonID = button.getID();
		ControlButton.ACTION action = button.getLastState();
		switch (buttonID)
		{
		case 0:
			button0(button, action); // jump
			break;
		case 1:
			button1(button, action); // dash
			break;
		case 2:
			button2(button, action); 
			break;
		case 3:
			button3(button, action);
			break;
		case 4:
			button4(button, action);
			break;
		case 5:
			button5(button, action);
			break;
		}
	}


	public void getAxis(ControlAxis axis)
	{
		int id = axis.getID();
		ControlAxis.ACTION action = axis.getLastState();
		switch (id) {
		case 0:
			axis0 (axis, action); // wep1
			break;
		case 1:
			axis1 (axis, action); // wep2
			break;
		}
	}

	public void button0(ControlButton butto, ControlButton.ACTION action)
	{
		if (action == ControlButton.ACTION.PRESS)
		{
			player.jump();
		}
		else if (action == ControlButton.ACTION.HOLD)
		{
			//hold Button0;
		}
		else if (action == ControlButton.ACTION.RELEASE)
		{
			Rigidbody rb = GetComponentInParent<Rigidbody>();
			if (rb.velocity.y > 0)
				rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y / 2, rb.velocity.z);

		}
	}
	public void button1(ControlButton button, ControlButton.ACTION action)
	{
		if (action == ControlButton.ACTION.PRESS)
		{
			//press Button1;
			player.dash();
		}
		else if (action == ControlButton.ACTION.HOLD)
		{
			//hold Button1;
		}
		else if (action == ControlButton.ACTION.RELEASE)
		{
			//release Button1;
		}
	}
	public void button2(ControlButton button, ControlButton.ACTION action)
	{

		if (!actionButton_2 && (Time.time > nextButtonPress || actionButton_1))
		{
			if (action == ControlButton.ACTION.PRESS)
			{
				player.getInventory().GetWeapon(0).PressAttack(button);
				actionButton_1 = true;
				nextButtonPress = Time.time + player.settings.buttonCooldown;
			}
			else if (action == ControlButton.ACTION.HOLD)
			{
				player.getInventory().GetWeapon(0).HoldAttack(button);


			}
			else if (action == ControlButton.ACTION.RELEASE)
			{
				player.getInventory().GetWeapon(0).ReleaseAttack(button);
				actionButton_1 = false;
			}
		}

	}
	public void button3(ControlButton button, ControlButton.ACTION action)
	{
		if (!actionButton_1 && (Time.time > nextButtonPress ||actionButton_2))
		{
			if (action == ControlButton.ACTION.PRESS)
			{
				player.getInventory().GetWeapon(1).PressAttack(button);
				actionButton_2 = true;
				nextButtonPress = Time.time + player.settings.buttonCooldown;

			}
			else if (action == ControlButton.ACTION.HOLD)
			{
				player.getInventory().GetWeapon(1).HoldAttack(button);


			}
			else if (action == ControlButton.ACTION.RELEASE)
			{
				player.getInventory().GetWeapon(1).ReleaseAttack(button);
				actionButton_2 = false;
			}
		}

	}
	public void button4(ControlButton button, ControlButton.ACTION action)
	{
		if (action == ControlButton.ACTION.PRESS)
		{
			Debug.Log ("DROP 0");

			player.drop(0);

		}
		else if (action == ControlButton.ACTION.HOLD)
		{
			//hold Button4;
		}
		else if (action == ControlButton.ACTION.RELEASE)
		{
			//release Button4;
		}
	}

	public void button5(ControlButton button, ControlButton.ACTION action)
	{
		if (action == ControlButton.ACTION.PRESS)
		{
			Debug.Log ("DROP 1");
			player.drop(1);

		}
		else if (action == ControlButton.ACTION.HOLD)
		{
			//hold Button4;
		}
		else if (action == ControlButton.ACTION.RELEASE)
		{
			//release Button4;
		}
	}
	public void axis0(ControlAxis axis, ControlAxis.ACTION action)
	{
		if (!actionButton_2 && (Time.time > nextButtonPress || actionButton_1))
		{
			if (action == ControlAxis.ACTION.PRESS)
			{
				Debug.Log ("PRESS");
				player.getInventory().GetWeapon(0).PressAttack(axis);
				actionButton_1 = true;
				nextButtonPress = Time.time + player.settings.buttonCooldown;

			}
			else if (action == ControlAxis.ACTION.HOLD)
			{
				Debug.Log ("HOLD");

				player.getInventory().GetWeapon(0).HoldAttack(axis);


			}
			else if (action == ControlAxis.ACTION.RELEASE)
			{
				Debug.Log ("RELEASE");

				player.getInventory().GetWeapon(0).ReleaseAttack(axis);
				actionButton_1 = false;
			}
		}
	}

	public void axis1(ControlAxis axis, ControlAxis.ACTION action)
	{
		if (!actionButton_1 && (Time.time > nextButtonPress ||actionButton_2))
		{
			if (action == ControlAxis.ACTION.PRESS)
			{
				Debug.Log ("PRESS");

				player.getInventory().GetWeapon(1).PressAttack(axis);
				actionButton_2 = true;
				nextButtonPress = Time.time + player.settings.buttonCooldown;
			}
			else if (action == ControlAxis.ACTION.HOLD)
			{
				Debug.Log ("HOLD");

				player.getInventory().GetWeapon(1).HoldAttack(axis);


			}
			else if (action == ControlAxis.ACTION.RELEASE)
			{
				Debug.Log ("RELEASE");

				player.getInventory().GetWeapon(1).ReleaseAttack(axis);
				actionButton_2 = false;

			}
		}
	}

}
