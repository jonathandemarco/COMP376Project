using UnityEngine;
using System.Collections.Generic;


public class ControlButton {

    private int buttonID;
    private string buttonName;
    private PlayerManager manager;

    public enum ACTION {PRESS,HOLD,RELEASE};

    public ControlButton(string name,int id, PlayerManager m) {
        setName(name);
        setManager(m);
        buttonID = id;
        Debug.Log("BUTTON MADE");

    }
    private void setName(string name) {
        buttonName = name;
    }
    private void setManager(PlayerManager m) {
        manager = m;
    }

    public void check() {

        if (Input.GetButton(buttonName))
        {
            press();

        }
        if (Input.GetButtonDown(buttonName))
        {
            hold();
        }
        if (Input.GetButtonUp(buttonName))
        {
            release();
        }

    }
    void press() {
        manager.getMessage(buttonID, ACTION.PRESS);
    }

    void hold() {
        manager.getMessage(buttonID, ACTION.HOLD);

    }

    void release() {
        manager.getMessage(buttonID, ACTION.RELEASE);

    }
}

public class PlayerControls : MonoBehaviour {

    public float moveSpeed;
    public float rotationSpeed;
    public int numOfButtons = 5;

    public GameObject player;
    


    private float horizontal;
    private float vertical;


    private List<ControlButton> buttons;
    void Awake () {
        buttons = new List<ControlButton>();
        for (int i = 1; i <= numOfButtons; i++) {
            ControlButton b = new ControlButton("B"+i,i,player.GetComponent<PlayerManager>());
            buttons.Add(b);
            Debug.Log("BUTTON " + i +" size is "+numOfButtons);

        }
    }

    void Update() {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

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
    // I hate this method, If I figure out how to pass functions as parameters this will be fixed
    /**private void checkButtons()
    {         // I hate this, If I figure out how to pass functions as parameters this will be fixed
       
        if (Input.GetButton("B1")) {
            b1Hold();
        }
        if (Input.GetButtonDown("B1")) {
            b1Press();
        }
        if (Input.GetButtonUp("B1"))
        {
            b1Release();
        }



        if (Input.GetButton("B2"))
        {
            b2Hold();
        }
        if (Input.GetButtonDown("B2"))
        {
            b2Press();
        }
        if (Input.GetButtonUp("B2"))
        {
            b2Release();
        }



        if (Input.GetButton("B3"))
        {
            b3Hold();
        }
        if (Input.GetButtonDown("B3"))
        {
            b3Press();
        }
        if (Input.GetButtonUp("B3"))
        {
            b3Release();
        }


        if (Input.GetButton("B4"))
        {
            b4Hold();
        }
        if (Input.GetButtonDown("B4"))
        {
            b4Press();
        }
        if (Input.GetButtonUp("B4"))
        {
            b4Release();
        }


        if (Input.GetButton("B5"))
        {
            b5Hold();
        }
        if (Input.GetButtonDown("B5"))
        {
            b5Press();
        }
        if (Input.GetButtonUp("B5"))
        {
            b5Release();
        }
    }
    */
    //b1,b2,b3 are for attacks

    private void b1Press()
    {
        //get player inventory -> getWeapon(1)->pressAttack();
        GetComponentInParent<PlayerManager>();
        
    }
    private void b1Hold()
    {
        //get player inventory -> getWeapon(1)->holdAttack();

    }
    private void b1Release()
    {
        //get player inventory -> getWeapon(1)->releaseAttack();

    }


    private void b2Press()
    {

    }
    private void b2Hold()
    {

    }
    private void b2Release()
    {

    }


    private void b3Press()
    {
        Debug.Log("B Pressed");
    }
    private void b3Hold()
    {
        Debug.Log("B hold");

    }
    private void b3Release()
    {
        Debug.Log("B release");

    }


    //b4,b5 are to cycle through weapons
    private void b4Press()
    {
        Debug.Log("B Pressed");
    }
    private void b4Hold()
    {
        Debug.Log("B hold");

    }
    private void b4Release()
    {
        Debug.Log("B release");

    }


    private void b5Press()
    {
        Debug.Log("B Pressed");
    }
    private void b5Hold()
    {
        Debug.Log("B hold");

    }
    private void b5Release()
    {
        Debug.Log("B release");

    }


}
