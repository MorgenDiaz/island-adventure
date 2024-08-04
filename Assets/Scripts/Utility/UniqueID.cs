using UnityEngine;

namespace RPG.Utility {
    public class UniqueID : MonoBehaviour {
        [SerializeField]
        private string _id;

        public string ID {
            get { return _id; }
            set { _id = value; }
        }
    }
}