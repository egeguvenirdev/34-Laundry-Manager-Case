using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy Type")]

public class EnemyInfos : ScriptableObject
{
    [SerializeField] private CharacterPref[] characterPrefs;

    public CharacterPref[] GetCharacterPrefs { get => characterPrefs; set => characterPrefs = value; }

    [System.Serializable]
    public class CharacterPref
    {
        [Header("Stats")]
        public float speed;
        public float range;
        public float attackDamge;
        public float maxHealth;
    }
}