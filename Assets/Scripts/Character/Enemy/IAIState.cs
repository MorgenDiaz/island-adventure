namespace RPG.Character.Enemy {

    public interface IAIState {
        public void EnterState();

        public void UpdateState();

        public void ExitState();

    }
}