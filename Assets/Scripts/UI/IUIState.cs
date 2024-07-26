
using UnityEngine;
namespace RPG.UI {

    public interface IUIState {

        void EnterState();
        void SelectButton();
        void ExitState();
    }
}