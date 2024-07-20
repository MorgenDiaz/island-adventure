namespace RPG.Character.Enemy {

    public interface IAIState {
        public void EnterState(IEnemyController enemy);

        public void UpdateState(IEnemyController enemy);
    }
}