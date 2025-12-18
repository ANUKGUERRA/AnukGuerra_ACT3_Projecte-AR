using UnityEngine;

public class Projectile : MonoBehaviour
{
    team owningTeam;
    private Transform target;
    public SphereCollider sphereCollider;

    public void SetTarget(Transform target, team OwningTeam)
    {
        owningTeam = OwningTeam;
        this.target = target;
        sphereCollider.enabled = true;
    }

    void Update()
    {
        if (!target) return;
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            1.0f * Time.deltaTime
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        UnitBehaviour unit = other.GetComponent<UnitBehaviour>();
        if (unit == null) return;
        if (unit.unitTeam == owningTeam) return;
        else if (unit.unitTeam != owningTeam)
        {
            unit.takeDamage(10);
            //Agregar efecto de explosion
            Destroy(gameObject);
        }

    }
}
