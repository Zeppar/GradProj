using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtension {
    public static void SetActiveFast(this GameObject go, bool active) {
        if (go.activeInHierarchy != active)
            go.SetActive(active);
    }
}

public static class Util {
    // data
    public static Dictionary<string, int> objectInitCountDict = new Dictionary<string, int> {
        { ObjectItemNameCollection.playerDash, 10},
        { ObjectItemNameCollection.bossFireBall, 3},
        { ObjectItemNameCollection.enemyHpCanvas, 5},
        { ObjectItemNameCollection.getItemEffect, 2},
        { ObjectItemNameCollection.playerShadow, 2},
        { ObjectItemNameCollection.enemyDie, 2},
        { ObjectItemNameCollection.blood, 5},
        { ObjectItemNameCollection.bossFireBallRain, 10},
        { ObjectItemNameCollection.playerFireBall, 2},
        { ObjectItemNameCollection.enemyFireBall, 3},
        { ObjectItemNameCollection.bossFireBallExplosion, 3},
        { ObjectItemNameCollection.enemyFireBallExplosion, 3},
        { ObjectItemNameCollection.playerFireBallExplosion, 2},
        { ObjectItemNameCollection.getItemLightEffect, 3},
        { ObjectItemNameCollection.skillStone, 3},
        { ObjectItemNameCollection.lightBall, 2},
        { ObjectItemNameCollection.getItemCanvas, 1}
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

    public static class KeyCollection
    {
        public static string Jump = "Jump";
        public static string Dash = "Dash";
        public static string Attack = "Attack";
        public static string OpenBag = "OpenBag";
      //  public static KeyCode Jump = KeyCode.W;
      //  public static KeyCode Dash = KeyCode.K;
      //  public static KeyCode Attack = KeyCode.J;
      //  public static KeyCode OpenBag = KeyCode.B;
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
        public static string attack0 = "Attack0";
        public static string attack1 = "Attack1";
        public static string explosion = "Explosion";
        public static string getItem = "GetItem";
        public static string getLittleLight = "GetLittleLight";
        public static string getMuchLight = "GetMuchLight";
        public static string dash = "Dash";
        public static string jump = "Jump";
        public static string recovery = "Recovery";
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
        public static string lightBall = "LightBall";
        public static string getItemCanvas = "GetItemCanvas";
    }

    // function
    public static System.Collections.IEnumerator DelayExecute(float time, NoParmsCallBack callback) {
        yield return new WaitForSecondsRealtime(time);
        callback?.Invoke();
    }

    public static Color ColorFromString(string s, float a) {
        int R = int.Parse(s.Substring(1, 2),
            System.Globalization.NumberStyles.HexNumber);
        int G = int.Parse(s.Substring(3, 2),
            System.Globalization.NumberStyles.HexNumber);
        int B = int.Parse(s.Substring(5, 2),
            System.Globalization.NumberStyles.HexNumber);
        return new Color(R / 255f, G / 255f, B / 255f, a);
    }

}
