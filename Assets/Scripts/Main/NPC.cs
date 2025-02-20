using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private GameObject talk;
    // NPC��ȭ player���� �ݶ��̴��� �ӹ����� �̾߱� Ȱ��ȭ ������ ��Ȱ��ȭ
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            talk.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            talk.SetActive(false);
        }
    }
}
