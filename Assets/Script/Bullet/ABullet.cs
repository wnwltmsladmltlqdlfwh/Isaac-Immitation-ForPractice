using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABullet : BulletBase
{
    protected override void ShotBullet()
    {
        base.ShotBullet();
        _body2D.AddForce(new Vector2(0f, 2f) * 3);
    }
}
