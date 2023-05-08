using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AlmanacManager : MonoBehaviour
{
    public List<AlamanacData> data;

    public TMP_Text content;
    public TMP_Text Name;
    public Image profile;
    public TMP_Text Attack;
    public TMP_Text Health;


    int currentIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        content.text = data[currentIndex].Info;
        Name.text = data[currentIndex].Name;
        UpdateHealthAndAttackText();
        profile.sprite = data[currentIndex].characterImage;
        currentIndex++;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnLeftButtonClicked()
    {
        AudioManager.Instance.Play("TurnPage");
        if (currentIndex < 0)
        {
            currentIndex = data.Count - 1;
        }
        if (currentIndex == 0)
        {

        }
        content.text = data[currentIndex].Info;
        Name.text = data[currentIndex].Name;
        profile.sprite = data[currentIndex].characterImage;
        UpdateHealthAndAttackText();
        currentIndex--;
    }
    public void OnRightButtonClicked()
    {
        AudioManager.Instance.Play("TurnPage");
        if (currentIndex >= data.Count-1)
        {
            currentIndex = 0;
        }
        content.text = data[currentIndex].Info;
        Name.text = data[currentIndex].Name;
        profile.sprite = data[currentIndex].characterImage;
        UpdateHealthAndAttackText();
        currentIndex++;
    }
    public void UpdateHealthAndAttackText()
    {
        //Attack String 
        Attack.text = $"Base Attack:{data[currentIndex].characterData.Base_Damage} \r\nTier I Attack:{data[currentIndex].characterData.TierI_AttackDamage}" +
            $"\r\nTier II Attack:{data[currentIndex].characterData.TierII_AttackDamage} \r\nTier III Attack:{data[currentIndex].characterData.TierIII_AttackDamage}";
        //Health String
        Health.text = $"Base Health:{data[currentIndex].characterData.Base_Health} \r\nTier I Health:{data[currentIndex].characterData.TierI_Health}" +
            $"\r\nTier II Health:{data[currentIndex].characterData.TierII_Health} \r\nTier III Health:{data[currentIndex].characterData.TierIII_Health}";
        //
    }
}
