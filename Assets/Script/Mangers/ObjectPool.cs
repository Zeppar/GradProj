using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance = null;
    public Queue<PlayerDash> queue = new Queue<PlayerDash>();
    public PlayerDash prefab;
    public int initCount = 10;

    private void Awake() {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else if(instance != this) {
            Destroy(gameObject);
        }
    }

    private void FillObjectPool() {
        for(int i = 0; i < initCount; i++) {
            PlayerDash pd = Instantiate(prefab);
            pd.transform.SetParent(transform, false);
            pd.gameObject.SetActive(false);
            queue.Enqueue(pd);
        }
    }

    public PlayerDash GetItem() {
        if(queue.Count == 0) {
            FillObjectPool();
        }
        PlayerDash pd = queue.Dequeue();
        pd.gameObject.SetActive(true);
        return pd;
    }

    public void ReturnToPool(PlayerDash pd) {
        pd.gameObject.SetActive(false);
        queue.Enqueue(pd);
    }
}
