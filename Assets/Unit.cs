using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static Unit;

public class Unit : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private bool UpdateStats;
    [SerializeField] private int DEF;
    [SerializeField] private int DMG;
    [SerializeField] private float FireRate;
    [SerializeField] private float Range;
    [SerializeField] private GunHead UnitType;
    [SerializeField] private Element DamageType;
    [SerializeField] private Element ElementResistance;
    [Header("Opponent Detection")]
    [SerializeField] private bool DoDetection = true;
    [SerializeField] private string OpponentTag;
    [SerializeField] private string FriendTag;
    [SerializeField] private LookAt lookAt;
    [SerializeField] private Spawner spawner;
    [Header("Materials and Models")]
    [SerializeField] private GameObject TurretHead;
    [SerializeField] private GameObject ShotgunHead;
    [SerializeField] private GameObject BarretHead;
    [SerializeField] private GameObject Armor;
    [SerializeField] private Material[] TurretHeadElements;
    [SerializeField] private Material[] ShotgunHeadElements;
    [SerializeField] private Material[] BarretHeadElements;
    [SerializeField] private Material[] Frames;
    [SerializeField] private List<GameObject> Muzzles;
    

    public enum GunHead
    {
        Turret,
        Shotgun,
        Barret
    }

    public enum Element
    {
        Null,
        Physical,
        Fire,
        Ice,
        Electric
    }

    public void SetAttributes(
        int def,
        int dmg,
        float range,
        float fireRate,
        GunHead unitType,
        Element damageType,
        Element elementResistance
        )
    {
        DEF = def;
        DMG = dmg;
        Range = range;
        FireRate = fireRate;
        UnitType = unitType;
        DamageType = damageType;
        ElementResistance = elementResistance;
        UpdateStats = true;
    }

    public void SetBotType(string type)
    {
        switch (type)
        {
            default:
                Debug.Log("Error: No unit type specified.");
                break;
            case "Turret":
                SetAttributes(0, 100, 30, 0.5f, Unit.GunHead.Turret, Unit.Element.Physical, Unit.Element.Null);
                break;
            case "Shotgun":
                SetAttributes(0, 100, 15, 0.7f, Unit.GunHead.Shotgun, Unit.Element.Physical, Unit.Element.Null);
                break;
            case "Barret":
                SetAttributes(0, 700, 70, 1.2f, Unit.GunHead.Barret, Unit.Element.Physical, Unit.Element.Null);
                break;
        }
    }

    void Start()
    {
        SelectionManager.Instance.AllUnits.Add(gameObject);
        Muzzles.Add(TurretHead.transform.GetChild(0).gameObject);
        Muzzles.Add(ShotgunHead.transform.GetChild(0).gameObject);
        Muzzles.Add(BarretHead.transform.GetChild(0).gameObject);
    }

    void OnDestroy()
    {
        SelectionManager.Instance.AllUnits.Remove(gameObject);
        SelectionManager.Instance.SelectedUnits.Remove(gameObject);
    }

    

    private void Update()
    {
        if (spawner.Fired)
        {
            foreach (GameObject muzzle in Muzzles)
            {
                float og_pos = muzzle.transform.localPosition.y;
                Vector3 pos = new(0,1,0);
                muzzle.transform.localPosition = muzzle.transform.localPosition + pos;
                muzzle.transform.LeanMoveLocalY(og_pos, 0.1f);
            }
        }

        spawner.DoDetection = DoDetection;

        if (UpdateStats)
        {
            spawner.spawnrate = FireRate;
            spawner.TagToDamage = OpponentTag;
            spawner.LayerToAvoidDetection = FriendTag;
            spawner.Range = Range;
            spawner.spawnrate = 0.5f;
            spawner.Damage = DMG;

            TurretHead.SetActive(false);
            ShotgunHead.SetActive(false);
            BarretHead.SetActive(false);
            Armor.SetActive(false);
            

            switch (UnitType)
            {
                case GunHead.Turret:
                    TurretHead.SetActive(true);
                    spawner.SpawnMode = Spawner.Mode.GunMode;
                    SetBotType("Turret");
                    switch (DamageType)
                    {
                        case Element.Physical:
                            TurretHead.GetComponent<Renderer>().material = TurretHeadElements[((int)Element.Physical)];
                            break;
                        case Element.Fire:
                            TurretHead.GetComponent<Renderer>().material = TurretHeadElements[((int)Element.Fire)];
                            break;
                        case Element.Ice:
                            TurretHead.GetComponent<Renderer>().material = TurretHeadElements[((int)Element.Ice)];
                            break;
                        case Element.Electric:
                            TurretHead.GetComponent<Renderer>().material = TurretHeadElements[((int)Element.Electric)];
                            break;
                    }
                    break;

                case GunHead.Shotgun:
                    ShotgunHead.SetActive(true);
                    SetBotType("Shotgun");
                    spawner.SpawnMode = Spawner.Mode.ShotgunMode;
                    switch (DamageType)
                    {
                        case Element.Physical:
                            ShotgunHead.GetComponent<Renderer>().material = ShotgunHeadElements[((int)Element.Physical)];
                            break;
                        case Element.Fire:
                            ShotgunHead.GetComponent<Renderer>().material = ShotgunHeadElements[((int)Element.Fire)];
                            break;
                        case Element.Ice:
                            ShotgunHead.GetComponent<Renderer>().material = ShotgunHeadElements[((int)Element.Ice)];
                            break;
                        case Element.Electric:
                            ShotgunHead.GetComponent<Renderer>().material = ShotgunHeadElements[((int)Element.Electric)];
                            break;
                    }
                    break;

                case GunHead.Barret:
                    BarretHead.SetActive(true);
                    SetBotType("Barret");
                    spawner.SpawnMode = Spawner.Mode.GunMode;
                    switch (DamageType)
                    {
                        case Element.Physical:
                            BarretHead.GetComponent<Renderer>().material = BarretHeadElements[((int)Element.Physical)];
                            break;
                        case Element.Fire:
                            BarretHead.GetComponent<Renderer>().material = BarretHeadElements[((int)Element.Fire)];
                            break;
                        case Element.Ice:
                            BarretHead.GetComponent<Renderer>().material = BarretHeadElements[((int)Element.Ice)];
                            break;
                        case Element.Electric:
                            BarretHead.GetComponent<Renderer>().material = BarretHeadElements[((int)Element.Electric)];
                            break;
                    }
                    break;
            }

            switch (ElementResistance)
            {
                case Element.Null:
                    Armor.GetComponent<Renderer>().material = Frames[((int)Element.Physical)];
                    break;
                case Element.Physical:
                    Armor.SetActive(true);
                    Armor.GetComponent<Renderer>().material = Frames[((int)Element.Physical)];
                    break;
                case Element.Fire:
                    Armor.SetActive(true);
                    Armor.GetComponent<Renderer>().material = Frames[((int)Element.Fire)];
                    break;
                case Element.Ice:
                    Armor.SetActive(true);
                    Armor.GetComponent<Renderer>().material = Frames[((int)Element.Ice)];
                    break;
                case Element.Electric:
                    Armor.SetActive(true);
                    Armor.GetComponent<Renderer>().material = Frames[((int)Element.Electric)];
                    break;
            }

            

            
            UpdateStats = false;
        }

        if (DoDetection)
        {
            if (spawner.FoundTarget)
            {
                lookAt.target[0] = spawner.target;
            }
            else
            {
                lookAt.target[0] = null;
            }
        }
    }
}
