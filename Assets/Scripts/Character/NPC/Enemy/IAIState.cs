namespace RPG.Character.NPC {

    public interface IAIState {
        public void EnterState();

        public void UpdateState();

        public void ExitState();

    }
}