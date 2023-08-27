using DG.Tweening;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
public class AttackPoint : MonoBehaviour
{
    [SerializeField] private bool IsProjectile;
    [SerializeField] private bool DealsDamageOverTime;
    [SerializeField] private int damageDuration = 5;
    [SerializeField] private GameObject vfx;
    private StatSystem characterStats;


    private void OnEnable()
    {
        if(IsProjectile)
        {
            DOVirtual.DelayedCall(3f, () => Destroy(gameObject)).SetUpdate(false);
            DOVirtual.DelayedCall(.25f,() => Destroy(vfx)).SetUpdate(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == this.gameObject.layer)
            return;

        if (other.TryGetComponent<StatSystem>(out StatSystem stats))
        {
            if(DealsDamageOverTime)
            {
                stats.DealDamageOverTime(characterStats.DealDamage(), damageDuration);
                return;
            }
            stats.UpdateHealth(characterStats.DealDamage());
            if (IsProjectile)
            {
                Destroy(gameObject);
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (IsProjectile)
        {

            Destroy(gameObject);

        }
    }

    public void SetStatsData(StatSystem data)
    {
        characterStats = data;
    }

}
