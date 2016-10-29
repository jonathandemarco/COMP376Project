using UnityEngine;
using System.Collections;

interface Weather {
    //TODO: expand this
    Vector3 getCastPoint();
    void setCastPoint(Vector3 castPoint);
    void setRadius(float radius);
}
