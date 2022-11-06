using System.Collections;
using System.Collections.Generic;

public interface IDamageable
{
    void DecreaseHealth(int damage, bool isSpecial);
}
