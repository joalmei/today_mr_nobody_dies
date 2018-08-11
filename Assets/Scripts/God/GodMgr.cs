using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum GodPower {Bomb, Turret, Trap};

public class GodMgr : MonoBehaviour
{
    public KeyCode BombButton = KeyCode.A;
    public KeyCode TurretButton = KeyCode.S;
    public KeyCode TrapButton = KeyCode.D;

    private GodPower CurrentPower = GodPower.Bomb;

    private bool MaySwitchPowers = true;

    // Bomb stuff
    private bool IsDraggingBomb = false;
    private GameObject DraggedBomb = null;

    void Start () {
	}
	
	void Update () {
        if (MaySwitchPowers)
        {
            if (Input.GetKeyDown(BombButton))
            {
                CurrentPower = GodPower.Bomb;
            }
            else if (Input.GetKeyDown(TurretButton))
            {
                CurrentPower = GodPower.Turret;
            }
            else if (Input.GetKeyDown(TrapButton))
            {
                CurrentPower = GodPower.Trap;
            }
        }

        if (CurrentPower == GodPower.Bomb)
        {
            if (Input.GetButtonDown("Fire1"))
            {

            }
            else
            {
                if (IsDraggingBomb)
                {
                    IsDraggingBomb = false;
                }
            }
        }
	}
}
