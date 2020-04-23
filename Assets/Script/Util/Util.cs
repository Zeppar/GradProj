using System.Collections.Generic;
using UnityEngine;

public static class Util {
    // data
    public static Dictionary<string, int> objectInitCountDict = new Dictionary<string, int> {
        { ObjectItemNameCollection.playerDash, 10},
        { ObjectItemNameCollection.bossFireBall, 5},
        { ObjectItemNameCollection.enemyHpCanvas, 10},
        { ObjectItemNameCollection.getItemEffect, 2},
        { ObjectItemNameCollection.playerShadow, 2},
        { ObjectItemNameCollection.enemyDie, 5},
        { ObjectItemNameCollection.blood, 5},
        { ObjectItemNameCollection.bossFireBallRain, 10},
        { ObjectItemNameCollection.playerFireBall, 5},
        { ObjectItemNameCollection.enemyFireBall, 5},
        { ObjectItemNameCollection.bossFireBallExplosion, 3},
        { ObjectItemNameCollection.enemyFireBallExplosion, 3},
        { ObjectItemNameCollection.playerFireBallExplosion, 3},
        { ObjectItemNameCollection.getItemLightEffect, 3},
        { ObjectItemNameCollection.skillStone, 3}
    };
    //type
    public delegate void NoParmsCallBack();
    public enum FireBallType {
        Player = 0,
        Enemy,
        Boss
    }

    // tag
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

    public static class ClipNameCollection {
        public static string attack = "Attack";
        public static string explosion = "Explosion";
        public static string getItem = "GetItem";
    }

    public static class ObjectItemNameCollection {
        public static string playerDash = "PlayerDash";
        public static string playerFireBall = "PlayerFireBall";
        public static string enemyFireBall = "EnemyFireBall";
        public static string bossFireBall = "BossFireBall";
        public static string enemyHpCanvas = "EnemyHpCanvas";
        public static string getItemEffect = "GetItemEffect";
        public static string playerShadow = "PlayerShadow";
        public static string enemyDie = "EnemyDie";
        public static string blood = "Blood";
        public static string bossFireBallRain = "BossFireBallRain";
        public static string getItemLightEffect = "GetItemLightEffect";
        public static string playerFireBallExplosion = "PlayerFireBallExplosion";
        public static string enemyFireBallExplosion = "EnemyFireBallExplosion";
        public static string bossFireBallExplosion = "BossFireBallExplosion";
        public static string skillStone = "SkillStone";
    }

    // function
    public static System.Collections.IEnumerator DelayExecute(float time, NoParmsCallBack callback) {
        yield return new WaitForSecondsRealtime(time);
        callback?.Invoke();
    }

}
