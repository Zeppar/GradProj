using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillActionManager : MonoBehaviour
{
    //消息队列
    // TODO cd
    public Dictionary<int, Util.NoParmsCallBack> skillDict = new Dictionary<int, Util.NoParmsCallBack>();
    public Dictionary<KeyCode, GoodInfo> keyCodeDict = new Dictionary<KeyCode, GoodInfo>();
    public List<KeyCode> keyCodeList = new List<KeyCode> { KeyCode.U, KeyCode.I};
    public Queue<SkillInfo> queue = new Queue<SkillInfo>();

    private void Start() {
        StartCoroutine(SkillRoutinue());
    }

    public void InitSkillCallback() {
        foreach(var kv in GameManager.instance.skillManager.skillDict) {
            AddSkillCallBack(kv.Value, () => {
                // TODO
                Debug.Log("skill : " + kv.Value.name);
            });
        }
        for(int i = 0;i < keyCodeList.Count; i++) {
            keyCodeDict[keyCodeList[i]] = null;
        }
    }

    public void SetKeyCode(KeyCode keyCode, GoodInfo info) {
        keyCodeDict[keyCode] = info;
    }

    public void AddSkillCallBack(SkillInfo info, Util.NoParmsCallBack callBack) {
        skillDict[info.id] = callBack;
    }

    public void ExecuteSkillAction(SkillInfo info) {
        queue.Enqueue(info);
    }

    private IEnumerator SkillRoutinue() {
        while(queue.Count > 0) {
            SkillInfo info = queue.Dequeue();
            if(skillDict.ContainsKey(info.id)) {
                skillDict[info.id]();
            }
            yield return null;
        }
    }

    private void OnGUI() {
        // A = 97
        //if(Input.anyKeyDown) {
        //    Event e = Event.current;
        //    if(e != null && keyCodeDict.ContainsKey(e.keyCode)) {
        //        ExecuteSkillAction(keyCodeDict[e.keyCode]);
        //    }
        //}
    }

    /*private float lastActionA = 0;
    private float lastActionB = 0;


    // Update is called once per frame
    void Update() {
        if (GameManager.instance.goodManager.goodInfoList[UIManager.instance.quickSkill1.gameObject.GetComponent<BagItem>().index].goodInfo != null) {
            int cdA = GameManager.instance.goodManager.goodInfoList[UIManager.instance.quickSkill1.gameObject.GetComponent<BagItem>().index].goodInfo.skillInfo.cd;
            UIManager.instance.quickSkill1.GetComponentInChildren<GoodItem>().maskImage.fillAmount = 1 - ((Time.time - lastActionA) / cdA);
        }
        if (GameManager.instance.goodManager.goodInfoList[UIManager.instance.quickSkill2.gameObject.GetComponent<BagItem>().index].goodInfo != null) {
            int cdB = GameManager.instance.goodManager.goodInfoList[UIManager.instance.quickSkill2.gameObject.GetComponent<BagItem>().index].goodInfo.skillInfo.cd;
            UIManager.instance.quickSkill2.GetComponentInChildren<GoodItem>().maskImage.fillAmount = 1 - ((Time.time - lastActionB) / cdB);
        }


        if (Input.GetKeyDown(KeyCode.K)) {
            if (GameManager.instance.goodManager.goodInfoList[UIManager.instance.quickSkill1.gameObject.GetComponent<BagItem>().index].goodInfo == null) {
                return;
            }
            int cd = GameManager.instance.goodManager.goodInfoList[UIManager.instance.quickSkill1.gameObject.GetComponent<BagItem>().index].goodInfo.skillInfo.cd;
            if (Time.time - lastActionA > cd) {
                GameManager.instance.skillActionManager.SendMessage(GameManager.instance.goodManager.goodInfoList[UIManager.instance.quickSkill1.gameObject.GetComponent<BagItem>().index].goodInfo.skillInfo.action, GameManager.instance.goodManager.goodInfoList[UIManager.instance.quickSkill1.gameObject.GetComponent<BagItem>().index].goodInfo.skillInfo);
                lastActionA = Time.time;
            } else {
                print("Nono");
            }

        }
        if (Input.GetKeyDown(KeyCode.L)) {
            if (GameManager.instance.goodManager.goodInfoList[UIManager.instance.quickSkill2.gameObject.GetComponent<BagItem>().index].goodInfo == null) {
                return;
            }
            int cd = GameManager.instance.goodManager.goodInfoList[UIManager.instance.quickSkill2.gameObject.GetComponent<BagItem>().index].goodInfo.skillInfo.cd;

            if (Time.time - lastActionB > cd) {
                GameManager.instance.skillActionManager.SendMessage(GameManager.instance.goodManager.goodInfoList[UIManager.instance.quickSkill2.gameObject.GetComponent<BagItem>().index].goodInfo.skillInfo.action, GameManager.instance.goodManager.goodInfoList[UIManager.instance.quickSkill2.gameObject.GetComponent<BagItem>().index].goodInfo.skillInfo);
                lastActionB = Time.time;
            }

        }
    }
    public void Fireball(SkillInfo info) {
        GameManager.instance.skillParticleCreator.CreateFireball(GameManager.instance.player.attackPoint.position, new Vector2(GameManager.instance.player.transform.GetComponent<Player>().dir, 0), 0.5f, Util.SkillCollection.playerFireBall, info.value, Util.TagCollection.enemyTag, Util.EffectCollection.playerFireBallExplosion);
    }
    public void Jinhua(SkillInfo info) {
        GameManager.instance.player.GetComponent<SpriteRenderer>().color = Color.green;
        Invoke("JinhuaBack", 0.2f);
    }
    public void JinhuaBack() {
        GameManager.instance.player.GetComponent<SpriteRenderer>().color = Color.white;
        GameManager.instance.player.HP += 10;
    }*/
}


