using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSwitcher : MonoBehaviour
{

    public int selectedAttack = 0;
    // Start is called before the first frame update
    void Start()
    {
        SelectAttack();
    }

    // Update is called once per frame
    void Update()
    {
        int prevSelectedAttack = selectedAttack;
        if(Input.GetAxis("Mouse ScrollWheel")>0f)
        {
            if(selectedAttack>=transform.childCount-1)
                selectedAttack=0;
            else
            selectedAttack++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") <  0f)
        {
            if (selectedAttack <= 0)
                selectedAttack = transform.childCount - 1;
            else
                selectedAttack--;
        }

        if(prevSelectedAttack!=selectedAttack)
        {
            SelectAttack();
        }
    }
    
    void SelectAttack()
    {
        int i = 0;
        foreach(Transform attack in transform)
        {
            if (selectedAttack == i)
                attack.gameObject.SetActive(true);
            else
                attack.gameObject.SetActive(false);
            i++;
        }
    }
}
