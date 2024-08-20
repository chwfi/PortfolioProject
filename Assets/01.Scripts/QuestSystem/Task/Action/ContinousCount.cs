using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Task/Action/ContinousCount")]
public class ContinousCount : TaskAction
{
    public override int Run(Task task, int currentSuccess, int successCount)
    {
        return successCount > 0 ? currentSuccess + successCount : 0;
        //들어온 성공값이 0보다 크면 현재 성공값에 들어온 성공값을 더하고 아니면 0을 반환
    }
}
