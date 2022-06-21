public class CombatStats
{
    public CombatStats(int strength, int intelligence, int dexterity, int vitality, float moveSpeed)
    {
        Strength = strength;
        Intelligence = intelligence;
        Dexterity = dexterity;
        Vitality = vitality;
        MoveSpeed = moveSpeed;
    }
    public int Strength { get; set; }
    public int Intelligence { get; set; }
    public int Dexterity { get; set; }
    public int Vitality { get; set; }
    public float MoveSpeed { get; set; }
}
