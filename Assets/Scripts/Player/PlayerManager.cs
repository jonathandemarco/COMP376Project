using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

    // Use this for initialization

    public float maxHealth;
    public int numLives;
    public float respawnTime;

    private float health;
    private bool isAlive;
    private bool isEliminated;
    private int score;
    private float timeSinceDeath;

    void Start () {
        isAlive = true;
        health = maxHealth;
        isEliminated = false;
        score = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (!isEliminated)
        {
            if (!isAlive)
            {
                timeSinceDeath += Time.deltaTime;
                if (timeSinceDeath >= respawnTime)
                    respawn();
            }
        }

	
	}

    public void takeDamage(float damage) {
        health -= damage;
        if (health <= 0)
            die();
    }
    private void die() {

        isAlive = false;
        numLives--;
        timeSinceDeath = 0;
        if (numLives <= 0)
            isEliminated = true;
    }
    private void respawn() {
        isAlive = true;
        health = maxHealth;
    }



    public int getScore() {
        return score;
    }
    public int getNumLives() {
        return numLives;
    }


    public void getMessage(int buttonID, ControlButton.ACTION action)
    {
        Debug.Log("ButtonID: "+buttonID);
    }        
		



}
