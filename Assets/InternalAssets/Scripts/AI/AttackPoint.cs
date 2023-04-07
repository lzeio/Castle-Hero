using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
public class AttackPoint : MonoBehaviour
{
    private StatSystem characterStats;
    // Start is called before the first frame update
    void Start()
    {
        characterStats = GetComponentInParent<StatSystem>();    
        Debug.Log(characterStats.ToString());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<StatSystem>(out StatSystem stats))
        {
            stats.TakeDamage(characterStats.DealDamage());
        }
        
    }
  
}
