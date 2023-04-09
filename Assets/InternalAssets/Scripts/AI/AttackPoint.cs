using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
public class AttackPoint : MonoBehaviour
{
    private StatSystem characterStats;

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
        if(other.TryGetComponent<StatSystem>(out StatSystem stats))
        {
            Debug.Log(other.name);
            stats.TakeDamage(characterStats.DealDamage());
        }
        
    }

    public void SetStatsData(StatSystem data)
    {
        characterStats = data;
    }
  
}