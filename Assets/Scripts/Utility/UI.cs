using UnityEngine.UIElements;

namespace RPG.Utility {
    static class UITools {
        public static void ToggleClass<T>(T element, string className) where T : VisualElement {
            if (element.ClassListContains(className)) {
                element.RemoveFromClassList(className);
            }
            else {
                element.AddToClassList(className);
            }
        }
    }
}