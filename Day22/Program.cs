Spell[] spells =
[
    Spell.Create("Missile", 53, damage: 4),
    Spell.Create("Drain", 73, damage: 2, heal: 2),
    Spell.Create("Shield", 113, armour: 7, duration: 6),
    Spell.Create("Poison", 173, damage: 3, duration: 6),
    Spell.Create("Recharge", 229, manaRestore: 101, duration: 5)
];

Console.WriteLine($"P1 least amount of mana spent and still won = {Run(hardMode: false)}");
Console.WriteLine($"P2 least amount of mana spent and still won = {Run(hardMode: true)}");

int Run(bool hardMode)
{
    Queue<GameState> battles = new();
    battles.Enqueue(new GameState(hardMode));
    int lowest = int.MaxValue;

    while (battles.Count > 0)
    {
        GameState current = battles.Dequeue();
        if (current.ManaUsed >= lowest)
        {
            continue;
        }

        foreach (Spell spell in spells.Except(current.ActiveEffects.Keys).Where(s => s.ManaCost < current.ManaRemaining))
        {
            GameState newGameState = new(current);
            RoundOutcome outcome = newGameState.RunRound(spell);

            switch (outcome)
            {
                case RoundOutcome.Undecided:
                    battles.Enqueue(newGameState);
                    break;
                case RoundOutcome.Win:
                    lowest = Math.Min(lowest, newGameState.ManaUsed);
                    break;
            }
        }
    }
    
    return lowest;
}


public class Spell
{
    public string Name { get; private init; }
    public int ManaCost { get; private init; }
    public int Damage { get; private init; }
    public int Armour { get; private init; }
    public int Heal { get; private init; }
    public int ManaRestore { get; private init; }
    public int Duration { get; private init; }

    private Spell() { }

    public static Spell Create(string name, int manaCost, int damage = 0, int armour = 0, int heal = 0, int manaRestore = 0, int duration = 0)
    {
        return new Spell
        {
            Name = name,
            ManaCost = manaCost,
            Damage = damage,
            Armour = armour,
            Heal = heal,
            ManaRestore = manaRestore,
            Duration = duration
        };
    }
}


public enum RoundOutcome { Undecided, Loss, Win }

public class GameState
{
    public int ManaRemaining;
    public int ManaUsed;
    private readonly bool isHardMode;
    private int playerHealth;
    private int bossHealth;
    private readonly int bossDamage;
    public readonly Dictionary<Spell, int> ActiveEffects;

    public GameState(bool hardMode)
    {
        isHardMode = hardMode;
        playerHealth = 50 - (hardMode ? 1 : 0);
        ManaRemaining = 500;
        ManaUsed = 0;
        bossHealth = 55;
        bossDamage = 8;
        ActiveEffects = new Dictionary<Spell, int>();
    }

    public GameState(GameState state)
    {
        isHardMode = state.isHardMode;
        playerHealth = state.playerHealth;
        ManaRemaining = state.ManaRemaining;
        ManaUsed = state.ManaUsed;
        bossHealth = state.bossHealth;
        bossDamage = state.bossDamage;
        ActiveEffects = new Dictionary<Spell, int>(state.ActiveEffects);
    }

    public RoundOutcome RunRound(Spell spell)
    {
        //==== Middle of Player Turn  ================================
        // Skip Applying Effects at start because first iteration
        // cannot have effects and this allows spell selection to
        // cast effect on turn it expires.
        UseSpell(spell);
        //==== 'End' of Player Turn  ================================

        //==== Start Boss Turn ======================================
        ApplyEffects();
        if (bossHealth <= 0) return RoundOutcome.Win;
        playerHealth -= Math.Max(bossDamage - ActiveEffects.Sum(e => e.Key.Armour), 1);
        if (playerHealth <= 0) return RoundOutcome.Loss;
        //==== End Boss Turn ========================================
        
        //==== Start of Player Turn  ================================.
        if (isHardMode && --playerHealth == 0) return RoundOutcome.Loss;
        ApplyEffects();
        if (bossHealth <= 0) return RoundOutcome.Win;
        
        return RoundOutcome.Undecided;
    }

    private void UseSpell(Spell spell)
    {
        ManaRemaining -= spell.ManaCost;
        ManaUsed += spell.ManaCost;
        
        if (spell.Duration == 0)
        {
            ApplySpell(spell);
        }
        else
        {
            ActiveEffects.Add(spell, spell.Duration);
        }
    }

    private void ApplyEffects()
    {
        ActiveEffects.ToList().ForEach(item =>
        {
            ApplySpell(item.Key);
            if (item.Value == 1)
            {
                ActiveEffects.Remove(item.Key);
            }
            else
            {
                ActiveEffects[item.Key] -= 1;
            }
        });
    }

    private void ApplySpell(Spell spell)
    {
        bossHealth -= spell.Damage;
        playerHealth += spell.Heal;
        ManaRemaining += spell.ManaRestore;
    }
}