using System;
using System.Collections.Generic;
using Alchemy.Serialization;
using UnityEngine;
using XTools;

namespace Snake {
    
    [AlchemySerialize]
    public partial class Test  : MonoBehaviour{
        
        [AlchemySerializeField] [NonSerialized]
        public Dictionary<MusicBundleType, MusicBundle> bundles = new();
        
        
        void Test2() {



            
        }
    }
}