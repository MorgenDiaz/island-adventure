using UnityEngine;

namespace RPG.Item {
    public interface IKeyItem : IItem {
        /*
          Although key items do not currently introduce any new properties or behavior
          beyond the base item interface, this interface is necessary to maintain
          compatibility with the JSON deserialization process.
      */
    }
}