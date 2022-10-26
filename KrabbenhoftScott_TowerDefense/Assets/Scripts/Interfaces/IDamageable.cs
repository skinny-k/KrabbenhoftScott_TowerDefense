using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void DecreaseHealth(int damage, bool isSpecial);
}
