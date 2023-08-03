using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    public void StateIdle();
    public void StateRun();
    public void StateMove();
    public void StateAttack();
    public void StateDamaged();
    public void StateReturn();
    public void StateDie();

}
