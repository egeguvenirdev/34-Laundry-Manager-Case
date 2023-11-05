using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level Type")]

public class LevelProps : ScriptableObject
{
    [SerializeField] private LevelPref[] levelPrefs;

    public LevelPref[] GetLevelPrefs { get => levelPrefs; set => levelPrefs = value; }

    [System.Serializable]
    public class LevelPref
    {
        [Header("Stats")]
        public Sprite image;
        public ColorType colorType;
        public ClothType clothType;
    }
}
