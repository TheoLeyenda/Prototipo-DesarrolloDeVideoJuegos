using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDHealth<T>
{
    void GiveHealth(T health);
}
public interface IDSeteableHelth<T>
{
    void SetHealth(T health);
}
