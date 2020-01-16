using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jesus.Cards
{ 
    [CreateAssetMenu(fileName = "General", menuName = "ScriptableObjects/General")]
    public class GeneralSO : ScriptableObject
    {
        public new string name;
        public Sprite image;

        public int buff;
        public string buffDescription;

    }
}
