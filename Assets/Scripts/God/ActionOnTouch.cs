using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionOnTouch : MonoBehaviour {

    public int Damage;
    public abstract void PlayerTouched(PlayerController player);

}
