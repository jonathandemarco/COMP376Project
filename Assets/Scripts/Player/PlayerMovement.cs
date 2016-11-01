using UnityEngine;
using System.Collections;

[System.Serializable]
public class PlayerMovement
{
    public float dashSpeed;
    public float dashTime;
    public float maxHeldTimeDash;
    public float dashCoolDownTime;
    public float jumpForce;
    public float jumpCooldownTime;
    public float moveSpeed;
    public float groundDragForce;
    public float airDragForce;
    public float airMoveForce;
    public float rotateSpeed;
    public float groundDistance;
    public float frontDistance;
    public float pushbackFactor;
}