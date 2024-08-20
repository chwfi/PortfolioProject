using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Task/Action/PositiveCount")]
public class PositiveCount : TaskAction
{
    public override int Run(Task task, int currentSuccess, int successCount)
    {
        return successCount > 0 ? currentSuccess + successCount : currentSuccess;
        //���� �������� 0���� ũ�ٸ� ���� �������� ���� �������� ���ؼ� ��ȯ���ְ�
        //�ƴ϶�� �׳� ���� ������ ��ȯ
    }
}
