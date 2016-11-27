using UnityEngine;
using System.Collections;

public class XboxController : MonoBehaviour {

    public GameObject a;
    public GameObject b;
    public GameObject x;
    public GameObject y;
    public GameObject rb;
    public GameObject lb;

    public GameObject a2;
    public GameObject b2;
    public GameObject x2;
    public GameObject y2;
    public GameObject rb2;
    public GameObject lb2;

    public Material mat;


    public GameObject obj;

	// Use this for initialization
	void Start () {

        GameObject tempA = Instantiate(obj);
        tempA.AddComponent<LineRenderer>();
        tempA.transform.position += new Vector3(0, 0, -1);
        SetupLine(tempA.GetComponent<LineRenderer>(), a.transform.position, a2.transform.position);     

        GameObject tempB = Instantiate(obj);
        tempB.AddComponent<LineRenderer>();
        tempB.transform.position += new Vector3(0, 0, -1);
        SetupLine(tempB.GetComponent<LineRenderer>(), b.transform.position, b2.transform.position);

        GameObject tempX = Instantiate(obj);
        tempX.AddComponent<LineRenderer>();
        tempX.transform.position += new Vector3(0, 0, -1);
        SetupLine(tempX.GetComponent<LineRenderer>(), x.transform.position, x2.transform.position);

        GameObject tempY = Instantiate(obj);
        tempY.AddComponent<LineRenderer>();
        tempY.transform.position += new Vector3(0, 0, -1);
        SetupLine(tempY.GetComponent<LineRenderer>(), y.transform.position, y2.transform.position);

        GameObject tempRB = Instantiate(obj);
        tempRB.AddComponent<LineRenderer>();
        tempRB.transform.position += new Vector3(0, 0, -1);
        SetupLine(tempRB.GetComponent<LineRenderer>(), rb.transform.position, rb2.transform.position);

        GameObject tempLB = Instantiate(obj);
        tempLB.AddComponent<LineRenderer>();
        tempLB.transform.position += new Vector3(0, 0, -1);
        SetupLine(tempLB.GetComponent<LineRenderer>(), lb.transform.position, lb2.transform.position);

	}
	
	// Update is called once per frame
	void Update () {
	
            
	}

    void SetupLine(LineRenderer line, Vector3 pos1, Vector3 pos2)
    {
        line.SetVertexCount(2);
        line.SetPosition(0, pos1);
        line.SetPosition(1, pos2);
        line.SetWidth(1.5f, 1.5f);
        line.useWorldSpace = true;
        line.material = mat;
        line.sortingLayerName = "Lines";
        line.sortingOrder = 0;
    }
}
