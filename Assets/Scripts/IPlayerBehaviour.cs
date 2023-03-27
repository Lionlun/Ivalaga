using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerBehaviour
{
    void Enter();
    void Exit();
    void Update();

    void  Shoot<T>(T bulletType) where T: IPlayerBullet;
}
