using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Task/Action/PositiveCount")]
public class PositiveCount : TaskAction
{
    public override int Run(Task task, int currentSuccess, int successCount)
    {
        return successCount > 0 ? currentSuccess + successCount : currentSuccess;
        //들어온 성공값이 0보다 크다면 현재 성공값에 들어온 성공값을 더해서 반환해주고
        //아니라면 그냥 현재 성공값 반환
    }
}
