using System.ComponentModel;
using System.Security.Cryptography;
enum Element
{
    fire,
    grass,
    water,
}

// Create a Pokemon Class for Leafeon
class Leafeon : Pokemon
{

    public Leafeon(string name) : base()
    {
        this.name = name;
        hp = 65;
        speed = 95;
        baseDamageFactor = 1.1;
        description = "Leafeon is a Grass type Pokemon introduced in Generation 4.";
        atk1Name = "Tail Whip";
        atk2Name = "Spray";
        atk3Name = "Flex";
        element = Element.grass;
        atkTurn = false;


    }
    public override int Attack1()
    {
        int damage = 5;
        attack1Left--;
        if (attack1Left <= 0)
        {
            Console.WriteLine("You can't choose that option, you don't have attacks left");
            return 0;
        }
        return damage;
    }

    public override int Attack2()
    {
        int damage = 7;
        attack2Left--;
        if (attack2Left <= 0)
        {
            return 0;
        }
        return damage;
    }
    public override int Attack3()
    {
        int damage = 10;
        attack3Left--;
        if (attack3Left <= 0)
        {
            return 0;
        }
        return damage;
    }
}
//Create a Pokemon class for Vaporeon
class Vaporeon : Pokemon
{
    public Vaporeon(string name) : base()
    {
        this.name = name;
        hp = 130;
        speed = 65;
        baseDamageFactor = 1.2;
        description = "Vaporeon is a Water type Pokemon introduced in Generation 1.";
        atk1Name = "Stomp";
        atk2Name = "Drag";
        atk3Name = "Spit";
        element = Element.water;
        atkTurn = false;
    }
    public override int Attack1()
    {
        int damage = 5;
        attack1Left--;
        if (attack1Left <= 0)
        {
            return 0;
        }
        return damage;
    }

    public override int Attack2()
    {
        int damage = 7;
        attack2Left--;
        if (attack2Left <= 0)
        {
            return 0;
        }
        return damage;
    }
    public override int Attack3()
    {
        int damage = 10;
        attack3Left--;
        if (attack3Left <= 0)
        {
            return 0;
        }
        return damage;
    }
}
//Create a Pokemon Class for Flareon
class Flareon : Pokemon
{
    public Flareon(string name) : base()
    {
        this.name = name;
        hp = 65;
        speed = 65;
        baseDamageFactor = 1.2;
        description = "Flareon is a Fire type Pokemon introduced in Generation 1.";
        atk1Name = "Tease";
        atk2Name = "Charm";
        atk3Name = "FIREBALL";
        element = Element.fire;
        atkTurn = false;
    }
    public override int Attack1()
    {
        int damage = 5;
        attack1Left--;
        if (attack1Left <= 0)
        {
            return 0;
        }
        return damage;
    }
    public override int Attack2()
    {
        int damage = 7;
        attack2Left--;
        if (attack2Left <= 0)
        {
            return 0;
        }
        return damage;
    }
    public override int Attack3()
    {
        int damage = 10;
        attack3Left--;
        if (attack3Left <= 0)
        {
            return 0;
        }
        return damage;
    }
}
// Parent Pokemon Class
abstract class Pokemon
{
    public string? name = "Pokemon";
    public int hp = 1;
    public int speed = 1;
    public double baseDamageFactor = 1.0;
    public string description = "this is a pokemon";
    public string atk1Name = "attack 1 name";
    public string atk2Name = "attack 2 name";
    public string atk3Name = "attack 3 name";
    public int attack1Left = 26;
    public int attack2Left = 12;
    public int attack3Left = 6;
    public bool atkTurn = false;

    public Element element = Element.fire;
    public abstract int Attack1();
    public abstract int Attack2();
    public abstract int Attack3();
    public void hit(int damage)
    {
        hp -= damage;
    }
    public bool isFaint()
    {
        return hp <= 0;
    }
}
class Program
{
    // Define Variables
    public const double ATK_STRONG = 2.0;
    public const double ATK_NEUTRAL = 1.0;
    public const double ATK_WEAK = 0.5;
    //Pokemon Attack Sequence
    public static void doBattle(Pokemon p1, Pokemon p2, int attackNum)
    {
        double baseDamageFactor = p1.baseDamageFactor;
        int atkDamage = 1;

        switch (attackNum)
        {
            case 1:
                atkDamage = p1.Attack1();
                break;
            case 2:
                atkDamage = p1.Attack2();
                break;
            case 3:
                atkDamage = p1.Attack3();
                break;
        }
        double eleDamage = rps(p1.element, p2.element);

        double totalDmg = (baseDamageFactor * atkDamage) * eleDamage;

        p2.hit(Convert.ToInt32(totalDmg));

    }
    //Element checking
    public static double rps(Element e1, Element e2)
    {

        double value = 0.0;

        if (e1 == Element.fire)
        {
            switch (e2)
            {
                case Element.fire:
                    value = ATK_NEUTRAL;
                    break;
                case Element.grass:
                    value = ATK_STRONG;
                    break;
                case Element.water:
                    value = ATK_WEAK;
                    break;
            }
        }
        else if (e1 == Element.water)
        {
            switch (e2)
            {
                case Element.fire:
                    value = ATK_STRONG;
                    break;
                case Element.grass:
                    value = ATK_WEAK;
                    break;
                case Element.water:
                    value = ATK_NEUTRAL;
                    break;
            }
        }
        else if (e1 == Element.grass)
        {
            switch (e2)
            {
                case Element.fire:
                    value = ATK_WEAK;
                    break;
                case Element.grass:
                    value = ATK_NEUTRAL;
                    break;
                case Element.water:
                    value = ATK_STRONG;
                    break;
            }

        }
        return value;
    }
}