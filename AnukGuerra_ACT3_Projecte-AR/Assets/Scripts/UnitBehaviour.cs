using DG.Tweening;
using Unity.Behavior;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UnitBehaviour : MonoBehaviour
{
    public team unitTeam;
    private BehaviorGraphAgent agent;
    BlackboardVariable<GameObject> enemy;
    public GameObject projectile;

    public GameObject TargetToAim;
    public GameObject StaffPoint;

    public Canvas healthCanvas;
    public UnityEngine.UI.Slider healthSlider;
    public int maxhealth = 100;
    public int health = 100;

    public Material disolveMaterial;



    private void Awake()
    {
        agent = GetComponent<BehaviorGraphAgent>();
        //agent.SetVariableValue<GameObject>("EnemyTarget", this.gameObject);
    }

    private void Update()
    {
        healthCanvas.gameObject.transform.LookAt(Camera.allCameras[0].transform.position,Camera.main.transform.up);

    }
    public void Attack()
    {
        if (!agent.GetVariable<GameObject>("EnemyTarget", out enemy))
            return;

        GameObject instance = Instantiate(projectile, StaffPoint.transform.position, Quaternion.identity);
        Projectile proj = instance.GetComponent<Projectile>();
        if (proj != null)
        {
            proj.SetTarget(enemy.Value.GetComponent<UnitBehaviour>().TargetToAim.transform, unitTeam);
        }
    }

    public void takeDamage(int damageAmount)
    {
        health -= damageAmount;
        healthSlider.value = (float)health / maxhealth;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        UnitsManager.Instance.units[unitTeam].Remove(gameObject);


        SkinnedMeshRenderer meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        Material mat = meshRenderer.material;
        mat = meshRenderer.material = new Material(disolveMaterial);
        mat.SetColor("_Color", (unitTeam == team.red) ? Color.red : Color.blue * 4f);
        mat.SetFloat("_ClipThreshold", 0f);

        mat
        .DOFloat(1f, "_ClipThreshold", 5f)
        .SetEase(Ease.OutCirc)
        .OnComplete(() =>
        {
            Destroy(gameObject);
            if (UnitsManager.Instance.units[unitTeam].Count == 0)
            {
                Game.Winner = (unitTeam == team.red) ? team.blue : team.red;
                SceneManager.LoadScene("WinMenu");
            }
        });
    }
}
