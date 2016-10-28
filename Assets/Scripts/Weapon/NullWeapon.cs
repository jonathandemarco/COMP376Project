using UnityEngine;
using System.Collections;

public class NullWeapon : Weapon
{
    public override void PressAttack(ControlButton button)
    {
        Debug.Log("NullWeapon");
    }
    public override void HoldAttack(ControlButton button)
    {
        Debug.Log("NullWeapon");
    }
        public override void ReleaseAttack(ControlButton button)
    {
        Debug.Log("NullWeapon");
    }
}