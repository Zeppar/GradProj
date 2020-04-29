using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 设计模式 isDirty 
// 脏数据
// 消息队列
// 单例模式
// 对象池

public class SkillActionManager : MonoBehaviour {
    // TODO cd
    public Dictionary<int, Util.NoParmsCallBack> skillDict = new Dictionary<int, Util.NoParmsCallBack>();
    public Dictionary<KeyCode, GoodInfo> keyCodeDict = new Dictionary<KeyCode, GoodInfo>();
    public Dictionary<int, float> cdDict = new Dictionary<int, float>();
    public List<KeyCode> keyCodeList = new List<KeyCode> { KeyCode.U, KeyCode.I };
    public Queue<SkillInfo> queue = new Queue<SkillInfo>();

    private void Start() {
        StartCoroutine(SkillRoutinue());
    }

    public void InitSkillCallback() {
        foreach (var kv in GameManager.instance.skillManager.skillDict) {
            AddSkillCallBack(kv.Value, () => {
                // TODO 设置callback
                Debug.Log("skill : " + kv.Value.name);
                SendMessage(kv.Value.action, kv.Value);
            });
        }
        for (int i = 0; i < keyCodeList.Count; i++) {
            keyCodeDict[keyCodeList[i]] = null;
        }
        foreach (var kv in GameManager.instance.skillManager.skillDict) {


        }
    }

    public void SetKeyCode(KeyCode keyCode, GoodInfo info) {
        keyCodeDict[keyCode] = info;
    }

    public void AddSkillCallBack(SkillInfo info, Util.NoParmsCallBack callBack) {
        skillDict[info.id] = callBack;
        cdDict[info.id] = 0;
    }

    public void ExecuteSkillAction(SkillInfo info) {
        queue.Enqueue(info);
    }

    public void ExchangeKeyCode(GoodInfo info1, GoodInfo info2) {
        KeyCode key1 = KeyCode.None, key2 = KeyCode.None;
        foreach (var kv in keyCodeDict) {
            if (kv.Value == info1) {
                key1 = kv.Key;
            } else if (kv.Value == info2) {
                key2 = kv.Key;
            }
        }
        keyCodeDict[key1] = info2;
        keyCodeDict[key2] = info1;
    }

    private IEnumerator SkillRoutinue() {
        while (true) {
            if (queue.Count > 0) {
                SkillInfo info = queue.Dequeue();
                if (skillDict.ContainsKey(info.id)) {
                    if (cdDict.ContainsKey(info.id)
                        && (Mathf.Approximately(cdDict[info.id], 0) || Time.time - cdDict[info.id] > info.cd)) {
                        cdDict[info.id] = Time.time;
                        skillDict[info.id]();
                    }

                }
            }
            yield return null;
        }
    }

    private void Fireball(SkillInfo info) {
        GameManager.instance.skillParticleCreator.CreateFireball(GameManager.instance.player.attackPoint.position, new Vector2(GameManager.instance.player.transform.GetComponent<Player>().dir, 0), 0.25f, info.value, Util.FireBallType.Player);
    }
    private void Levelup(SkillInfo info) {
        GameManager.instance.player.GetComponent<SpriteRenderer>().color = Color.green;
        GameManager.instance.StartCoroutine(Util.DelayExecute(0.2f, () => {
            GameManager.instance.player.GetComponent<SpriteRenderer>().color = Color.white;
            GameManager.instance.player.HP += 10;
        }));
    }
    private void ManyFireBalls(SkillInfo info) {
        Debug.Log("ManyFireBalls");
    }
    public void JinhuaBack() {
        GameManager.instance.player.GetComponent<SpriteRenderer>().color = Color.white;
        GameManager.instance.player.HP += 10;
    }

    private void OnGUI() {
        if (Input.anyKeyDown) {
            Event e = Event.current;
            if (e != null && e.isKey && keyCodeDict.ContainsKey(e.keyCode) && keyCodeDict[e.keyCode] != null) {
                ExecuteSkillAction(keyCodeDict[e.keyCode].skillInfo);
            }
        }
    }
}


