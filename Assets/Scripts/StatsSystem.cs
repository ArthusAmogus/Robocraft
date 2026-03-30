using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class StatsSystem : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private GameObject AppliedObject;
    [SerializeField] private GameObject HPBar;
    [SerializeField] private TextMeshProUGUI HPNum;
    [SerializeField] private int MaxHP = 1000;
    [SerializeField] [Range(0, 1000)] private int HP = 1000;
    public int DEF;

    [Header("Reference")]
    [SerializeField] private GameObject DeathParticle;
    [SerializeField] private Material DamageMatFX;
    

    [Header("Properties")]
    [SerializeField] private bool DoDamage = true;
    [SerializeField] private bool DoDeath = true;
    [SerializeField] private bool IndicateDamage;

    [Header("Debug")]
    [SerializeField] private bool DamageEntity;

    //Data
    private List<Material> normal_mat = new();
    private List<Renderer> render = new();
    private bool IsDead;

    void Start()
    {
        if (AppliedObject==null)
        {
            AppliedObject = this.gameObject;
        }
        HP = MaxHP;
    }


    void Update()
    {
        if (DamageEntity)
        {
            TakeDamage(100);
            DamageEntity = false;
        }

        //Death Detection
        if (HP <= 0 && !IsDead && DoDeath)
        {
            Instantiate(DeathParticle, transform.position, Quaternion.identity);
            Destroy(AppliedObject, 0.1f);
            IsDead = true;
        }
        

        //HP Bar Display
        if (HPNum!=null) HPNum.text = "HP: "+HP.ToString()+"/"+MaxHP.ToString();
        if (HPBar!=null)
        {
            //Scale
            Vector2 Bar = HPBar.transform.localScale;
            Bar.x = 1 * ((float)HP / (float)MaxHP);
            HPBar.transform.localScale = Bar;
        }
    }


    public void TakeDamage(int damage)
    {
        int actualDamage = Mathf.Max(damage - DEF, 0);
        if (DoDamage)
        {
            HP -= actualDamage;
            StopCoroutine(DamageEffect());
            StartCoroutine(DamageEffect());
        }
        if (IndicateDamage)
        {
            GameObject damageText = new("DamageText");
            //damageText.transform.;
            damageText.AddComponent<Billboard>();
            damageText.AddComponent<TextMeshProUGUI>();
            damageText.GetComponent<TextMeshProUGUI>().text = actualDamage.ToString();
            damageText.AddComponent<LifeSpan>();
            damageText.GetComponent<LifeSpan>().lifespan = 1f;
            damageText.AddComponent<EasedTransform>();
            damageText.GetComponent<EasedTransform>().Location = new Vector3(0, 1, 0);
        }
    }


    IEnumerator DamageEffect()
    {
        //THIS CODE MAY BE UNOPTIMIZED

        //Restore original just incase of a sudden interuption
        for (int i = 0; i < render.Count; i++) render[i].material = normal_mat[i];
        render.Clear();
        normal_mat.Clear();

        //Initialize 
        render = GetComponentsInChildren<Renderer>().ToList();
        if (normal_mat.Count < render.Count) normal_mat.AddRange(new Material[render.Count - normal_mat.Count]);

        //Store original materials for backup
        for (int i = 0; i < render.Count; i++) normal_mat[i] = render[i].material;

        //Apply damage material
        foreach (var r in render) r.material = DamageMatFX;
        yield return new WaitForSeconds(0.1f);

        //Restore original materials
        for (int i = 0; i < render.Count; i++) render[i].material = normal_mat[i];
        render.Clear();
        normal_mat.Clear();
    }
}
