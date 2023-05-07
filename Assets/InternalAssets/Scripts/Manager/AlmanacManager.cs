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
    public TMP_Text Stats;


    int currentIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        content.text = data[currentIndex].Info;
        Name.text = data[currentIndex].Name;
        Stats.text = data[currentIndex].characterData.Base_Damage.ToString();
        profile.sprite = data[currentIndex].characterImage;
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
        Stats.text = data[currentIndex].characterData.Base_Damage.ToString();
        profile.sprite = data[currentIndex].characterImage;
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
        Stats.text = data[currentIndex].characterData.Base_Damage.ToString();
        profile.sprite = data[currentIndex].characterImage;
        currentIndex++;
    }
}
