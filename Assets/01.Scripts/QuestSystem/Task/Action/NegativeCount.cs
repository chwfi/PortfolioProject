using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Task/Action/NegativeCount")]
public class NegativeCount : TaskAction
{
    public override int Run(Task task, int currentSuccess, int successCount)
    {
        return successCount < 0 ? currentSuccess + successCount : currentSuccess;
        //���� �������� 0���� �۴ٸ� ���� �������� ���� �������� ���ؼ� ��ȯ���ְ�
        //�ƴ϶�� �׳� ���� ������ ��ȯ
    }
}
