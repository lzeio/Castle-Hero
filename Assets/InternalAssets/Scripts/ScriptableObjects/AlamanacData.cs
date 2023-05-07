using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CharacterInfo", menuName = "Alamanac")]
public class AlamanacData : ScriptableObject
{
    public string Name;
    public string Info;
    public Sprite characterImage;

    public CharacterData characterData;
}
