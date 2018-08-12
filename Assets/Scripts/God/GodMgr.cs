using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum GodPower {Bomb, Turret, Trap};

public class GodMgr : MonoBehaviour
{
    public KeyCode BombButton = KeyCode.A;
    public KeyCode TurretButton = KeyCode.S;
    public KeyCode TrapButton = KeyCode.D;

    public float BombMaximumSpeed = 1.0f;
    public float PowerCooldown = 4.0f;

    public GameObject PrefabBomb;
    public GameObject PrefabTrap;
    public GameObject PrefabTurret;

    private GodPower CurrentPower = GodPower.Bomb;

    private bool MaySwitchPowers = true;

    // Bomb stuff
    private bool IsDraggingBomb = false;
    private GameObject DraggedBomb = null;
    private float LastBomb = 0.0f;

    // Turret stuff
    private bool IsPlacingTurret = false;
    private GameObject PlacedTurret = null;
    private float LastTurret = 0.0f;

    // Trap stuff
    private float LastTrap = 0.0f;
        
    private Queue<Vector3> LastMousePositions;
    private Queue<float> LastStepsTime;

    private static Vector3 GetMouseWorldPos()
    {
        Vector3 Scale = new Vector3(1, 1, 0);
        Vector3 ZOffset = new Vector3(0, 0, SceneMgr.GlobalZ);
        return Vector3.Scale(
            Camera.main.ScreenToWorldPoint(Input.mousePosition),
            Scale
        ) + ZOffset;
    }

    void Start ()
    {
        LastMousePositions = new Queue<Vector3>();
        LastStepsTime = new Queue<float>();
    }

    void FixedUpdate() {
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
            if (Input.GetButton("Fire1"))
            {
                if (!IsDraggingBomb && Time.time > LastBomb + PowerCooldown)
                {
                    MaySwitchPowers = false;
                    IsDraggingBomb = true;

                    DraggedBomb = Instantiate(
                        PrefabBomb,
                        GetMouseWorldPos(),
                        Quaternion.identity
                    ) as GameObject;
                }
            }
            else
            {
                if (IsDraggingBomb)
                {
                    IsDraggingBomb = false;
                    MaySwitchPowers = true;

                    LastBomb = Time.time;

                    if (DraggedBomb)
                    {
                        Rigidbody BombPhysics = DraggedBomb.GetComponent<Rigidbody>();
                        BombPhysics.useGravity = true;
                        BombPhysics.isKinematic = false;

                        Vector3[] PositionVector = LastMousePositions.ToArray() as Vector3[];
                        float[] TimeArray = LastStepsTime.ToArray() as float[];

                        Vector3 ThrowVector = (
                            (PositionVector[PositionVector.Length - 1] - PositionVector[0]) /
                            (TimeArray[TimeArray.Length - 1] - TimeArray[0])
                        );

                        BombPhysics.velocity = ThrowVector.normalized * Mathf.Clamp(ThrowVector.magnitude, 0, BombMaximumSpeed);
                    }
                }
            }

            if (IsDraggingBomb)
            {
                Rigidbody BombPhysics = DraggedBomb.GetComponent<Rigidbody>();
                BombPhysics.position = GetMouseWorldPos();
                /*
                    Vector3 GoalVector = (GetMouseWorldPos() - BombPhysics.position);
                    BombPhysics.velocity = (
                    BombPhysics.velocity +
                    GoalVector.normalized * Time.fixedDeltaTime * BombAcceleration
                );*/
            }
        } else if (CurrentPower == GodPower.Turret)
        {
            if (Input.GetButton("Fire1"))
            {
                if (!IsPlacingTurret && Time.time > LastTurret + PowerCooldown)
                {
                    MaySwitchPowers = false;
                    IsPlacingTurret = true;

                    PlacedTurret = Instantiate(
                        PrefabTurret,
                        GetMouseWorldPos(),
                        Quaternion.identity
                    ) as GameObject;
                }
            }
            else
            {
                if (IsPlacingTurret)
                {
                    IsPlacingTurret = false;
                    MaySwitchPowers = true;

                    LastTurret = Time.time;

                    PlacedTurret.GetComponent<Turret>().Activate(GetMouseWorldPos() - PlacedTurret.transform.position);
                }
            }
        } else if (CurrentPower == GodPower.Trap)
        {
            if (Input.GetButtonDown("Fire1") && Time.time > LastTrap + PowerCooldown)
            {
                Instantiate(
                    PrefabTrap,
                    GetMouseWorldPos(),
                    Quaternion.identity
                );

                LastTrap = Time.time;
            }
        }

        LastStepsTime.Enqueue(Time.time);
        LastMousePositions.Enqueue(GetMouseWorldPos());
        if (LastMousePositions.Count > 5)
        {
            LastMousePositions.Dequeue();
            LastStepsTime.Dequeue();
        }
    }
}
