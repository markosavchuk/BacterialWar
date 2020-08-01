using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DifficultySettingsProvider : MonoBehaviour
{
    public List<DifficultySettings> DifficultySettingsList
    {
        get
        {
            return gameObject.GetComponents<DifficultySettings>().ToList();
        }
    }
}
