
using System.Collections.Generic;
using UnityEngine;

public static class Util 
{
    public static class TagCollection {
        public static string playerTag = "Player";
        public static string groundTag = "Ground";
        public static string enemyTag = "Enemy";
    }

    public static class PlayerAnimCollection {
        public static string idle = "Idle";
        public static string jump = "Jump";
        public static string walk = "Walk";
        public static string attack1 = "Attack1";
        public static string attack2 = "Attack2";
        public static string attack3 = "Attack3";
        public static string airAttack1 = "AirAttack1";
        public static string airAttack2 = "AirAttack2";
        public static string airAttack3 = "AirAttack3";
    }

    public static class LayerCollection {
        public static string playerLayer = "Player";
        public static string groundLayer = "Ground";
        public static string enemyLayer = "Enemy";
    }

    public static class EffectCollection {
        public static string playerFireBallExplosion = "PlayerFireBallExplosion";
        public static string enemyFireBallExplosion = "EnemyFireBallExplosion";
    }

    public static class SkillCollection {
        public static string playerFireBall = "PlayerFireBall";
        public static string enemyFireBall = "EnemyFireBall";
    }

    public static class ClipNameCollection {
        public static string attack = "Attack";
        public static string explosion = "Explosion";
    }

}
