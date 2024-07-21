namespace RPG.Character {
    public interface INPCCombat : ICombat {
        void Attack();
        void CancelAttack();
    }
}