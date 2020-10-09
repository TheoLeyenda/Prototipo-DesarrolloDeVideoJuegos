using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumsPlayers : EnumsCharacter
{

    public enum NumberPlayer
    {
        player1,
        player2,
        Count
    }
    public enum SpecialAttackEquipped
    {
        Nulo,
        DisparoDeCarga,
        GranadaGaseosa,
        ProyectilImparable,
        MagicBust,
        Limusina,
        ProyectilChicle,
        Default
    }
    public SpecialAttackEquipped specialAttackEquipped;
    public NumberPlayer numberPlayer;
}
