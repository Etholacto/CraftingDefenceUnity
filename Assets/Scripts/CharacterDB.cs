using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterDB : ScriptableObject
{
    public Resources[] resources;

    public float GetResource(string value)
    {
        if (value.Contains("wood"))
        {
            return resources[0].Value;
        }
        else if (value.Contains("stone"))
        {
            return resources[1].Value;
        }
        else
        {
            return 0;
        }
    }
    
    public void SetResource(string value, float amount)
    {
        if (value.Contains("wood"))
        {
            resources[0].Value += amount;
        }
        else if (value.Contains("stone"))
        {
            resources[1].Value += amount;
        }
    }

    public void resetValues()
    {
        resources[0].Value = 0f;
        resources[1].Value = 0f;
    }
}
