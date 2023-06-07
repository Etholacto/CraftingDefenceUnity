using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterDB : ScriptableObject
{
    public CharacterSelect[] character;

    public int CharacterCount
    {
        get
        {
            return character.Length;
        }
    }

    public CharacterSelect GetCharacter(int index)
    {
        return character[index];
    }
}
