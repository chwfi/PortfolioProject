using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    public class FindObjectUtil
    {
        //�θ𿡼� Ư�� Ÿ���� ã�� ����, �� Ư�� Ÿ���� �����ϴ� �θ� ������Ʈ�� ã�������� ���� �ݺ� ������ �Լ�
        public static T FindParent<T>(GameObject go) where T : Object
        {
            if (go == null) return null; //ã�� ���� ������Ʈ�� ������ ���� null

            Transform currentTransform = go.transform; //ã�� ���� ������Ʈ�� ���� Ʈ������ ����

            while (currentTransform != null)
            {
                T component = currentTransform.GetComponent<T>(); //���� Transform���� T���� ã��
                if (component != null) //���� �����Ѵٸ�
                {
                    return component; //���� ����
                }
                currentTransform = currentTransform.parent; //���ٸ� ���� Transform�� �θ� ���� Transform���� �ٲ�

                //�θ� ������Ʈ(currentTransform)�� ���̻� ���ٸ� ���� �Ϸ��Ѱ��̹Ƿ� while�� ����
            }

            return null;
        }
    }
}