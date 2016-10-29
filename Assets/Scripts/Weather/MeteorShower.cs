using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeteorShower : MonoBehaviour, Weather {

    public GameObject meteorPrefab;
    public int meteorCount;
    public float radius; //area of effect size
    public float meteorTimeStep; //rate of meteor drop

    private Vector3 castPoint; //where player casts it
    private float timeSinceLast = 0;
    private Vector3 source = new Vector3(30, 50, 0); //where meteors fall from
    private List<GameObject> meteorList = new List<GameObject>();
    private Vector3 direction;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (meteorCount <= 0) {
            Destroy(gameObject);
        }

        timeSinceLast += Time.deltaTime;
        if(timeSinceLast > meteorTimeStep) {
            launch();
            timeSinceLast = 0;
        }

	}

    public Vector3 getCastPoint() {
        return castPoint;
    }

    public void setCastPoint(Vector3 castPoint) {
        this.castPoint = castPoint;
    }

    public void setRadius(float radius) {
        this.radius = radius;
    }

    public void launch() {
        Vector2 randPoint = Random.insideUnitCircle * radius;
        Vector3 randCastPoint = castPoint + new Vector3(randPoint.x, 0, randPoint.y);
        
        direction = (randCastPoint - source).normalized;
        GameObject meteor = Instantiate(meteorPrefab);
        ((Meteor)meteor.GetComponent(typeof(Meteor))).setDirection(direction);
        meteor.transform.position = source;
        meteorCount--;
    }
}
