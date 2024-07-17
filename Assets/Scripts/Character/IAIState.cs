namespace RPG.Character {
    public interface IAIState {
        public void EnterState(EnemyController enemy);

        public void UpdateState(EnemyController enemy);
    }
}