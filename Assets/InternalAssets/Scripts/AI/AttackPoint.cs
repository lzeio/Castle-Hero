using System.Net;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
public class AttackPoint : MonoBehaviour
{
    [SerializeField] private bool IsProjectile;
    [SerializeField] private bool DealsDamageOverTime;
    private StatSystem characterStats;

    int duration = 5;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == this.gameObject.layer)
            return;

        if (other.TryGetComponent<StatSystem>(out StatSystem stats))
        {
            if(DealsDamageOverTime)
            {
                stats.DealDamageOverTime(characterStats.DealDamage(), duration);
            }
            stats.UpdateHealth(characterStats.DealDamage());
            if(IsProjectile)
            {
                Destroy(gameObject);
            }
        }

    }

    public void SetStatsData(StatSystem data)
    {
        characterStats = data;
    }

}
