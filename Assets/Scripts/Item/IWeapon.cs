namespace RPG.Item {
    public interface IWeapon : IItem {
        int Damage { get; set; }
        string GameTag { get; set; }
        bool IsEquipped { get; set; }
    }
}