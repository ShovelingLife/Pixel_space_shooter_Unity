using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// 씬 전환 후 그 전에서 사용했던 것들을 새로운 씬에서 사용하기 위함
[Serializable]
public class ObjectPoolData
{
    // 오브젝트 종류 및 개수
    public GameObject objPool = null;
    public int count = 0;

    // 그 전 씬에 사용했던 오브젝트 및 개수 할당
    public ObjectPoolData(GameObject _obj, int _count)
    {
        objPool = _obj;
        count = _count;
    }
}

public class ObjectPoolingManager : SingletonLocal<ObjectPoolingManager>
{
    // 클래스 타입 - 해당 타입 오브젝트 해시 테이블
    protected Dictionary<Type, Stack<GameObject>> objPools = new Dictionary<Type, Stack<GameObject>>();

    // 사용했던 
    public List<ObjectPoolData> prevObj = new List<ObjectPoolData>();


    private void Awake()
    {
        InitSettingPrevObj();
    }

    // 전 오브젝트 리스트 안에 있는 오브젝트를 초기화 해줌
    protected void InitSettingPrevObj()
    {
        // 강제 등록
        foreach (var item in prevObj)
        {
            Stack<GameObject> container = new Stack<GameObject>();
            objPools.Add(item.objPool.GetType(), container);

            // 오브젝트를 개수만큼 생성 후 스택에 추가
            for (int i = 0; i < item.count; ++i)
            {
                GameObject copyobj = Instantiate(item.objPool);
                objPools.Add(copyobj.GetType(), container);
            }
        }
    }

    // 전 오브젝트들을 초기화 함
    public void InitPrevObj(GameObject _obj, int _count)
    {
        Stack<GameObject> container = new Stack<GameObject>();
        objPools.Add(_obj.GetType(), container);

        // 오브젝트를 개수만큼 생성 후 스택에 넣음
        for (int i = 0; i < _count; ++i)
        {
            GameObject copyObj = Instantiate(_obj);
            copyObj.SetActive(false);
            objPools.Add(copyObj.GetType(), container);
        }
    }

    // 오브젝트 컨테이너 담당하는 오브젝트에 자식으로 추가
    public Transform CreateObj(Type _type, Transform _trans, Transform _parentTrans = null)
    {
        Transform         outTrans     = null;
        Stack<GameObject> tmpContainer = null;

        #region 스택 초기화 부분

        // Type 키가 존재한다면 딕셔너리에서 스택을 갖고옴
        if (objPools.ContainsKey(_type))
            tmpContainer = objPools[_type];

        else // 없으면 스택을 새로 생성해서 넣음
        {
            tmpContainer = new Stack<GameObject>();
            objPools.Add(_type, tmpContainer);
        }

        #endregion

        #region Transform 초기화 부분

        // 스택에 오브젝트가 있고 꺼져있으면 해당 오브젝트 트랜스폼 가져옴
        if (tmpContainer.Count > 0 &&
            !tmpContainer.Peek().activeInHierarchy)
            outTrans = tmpContainer.Pop().transform;

        // 없으면 새로 만들어서 추가
        else
        {
            outTrans = Instantiate(_trans);
            objPools[_type].Push(outTrans.gameObject);
        }

        // 오브젝트 부모 설정
        if (_parentTrans != null)
            outTrans.SetParent(_parentTrans);

        outTrans.gameObject.SetActive(true);

        #endregion

        return outTrans;
    }

    // 템플릿 인자 받아서 오브젝트 생성 (여러 컴포넌트 사용 가능)
    public T CreateObj<T>(Type _type, T _cloneObj, Transform _parentTrans = null) where T : Component
    {
        T out_result = null;
        Stack<GameObject> tmp_stack = null;

        // ------- 스택 초기화 부분 -------

        // Type 키가 존재한다면 딕셔너리에서 스택을 갖고옴
        if (objPools.ContainsKey(_type))
            tmp_stack = objPools[_type];

        else // 없으면 스택을 새로 생성해서 넣음
        {
            tmp_stack = new Stack<GameObject>();
            objPools.Add(_type, tmp_stack);
        }

        // ------- Transform 초기화 부분 -------

        // 스택에 오브젝트가 있고 꺼져있으면 해당 오브젝트 트랜스폼 가져옴
        if (tmp_stack.Count > 0 &&
            !tmp_stack.Peek().activeInHierarchy)
            out_result = tmp_stack.Pop().GetComponent<T>();

        else // 없으면 새로 만들어서 추가
        {
            out_result = GameObject.Instantiate<T>(_cloneObj);
            objPools[_type].Push(out_result.gameObject);
        }
        // 오브젝트 부모 설정
        if (_parentTrans != null)
            out_result.transform.SetParent(_parentTrans);

        out_result.gameObject.SetActive(true);
        return out_result;
    }

    //void RemoveObject()

    // 오브젝트를 제거해주는 코루틴
    IEnumerator IE_RemoveObj(Type _type, Transform _trans, Transform _parentTrans = null, float _remove_sec = 0.0f)
    {
        yield return new WaitForSeconds(_remove_sec);
        RemoveObj(_type, _trans, _parentTrans);
    }

    // 나타났다가 일정 시간 후 사라지는 오브젝트 예)이펙트
    public Transform CreateRemoveDelayedObj(Type _type,
        Transform _trans,
        float _remove_sec,
        Transform _trans_parent = null,
        Transform _trans_remove_parent = null)
    {
        Transform copy_obj = null;

        // 시간이 초과 됐다면 제거
        if (_remove_sec > 0f)
        {
            copy_obj = CreateObj(_type, _trans, _trans_parent);
            StartCoroutine(IE_RemoveObj(_type, copy_obj, _trans_remove_parent, _remove_sec));
        }
        return copy_obj;
    }

    // 오브젝트를 제거
    public void RemoveObj(Type _type, Transform _trans, Transform _parentTrans = null)
    {
        // 컨테이너 트랜스폼이 널이 아니라면 등록
        if (_parentTrans != null)
            _trans.SetParent(_parentTrans);

        // 해시 테이블 안에 클래스 타입이 존재할 시 넣음
        if (objPools.ContainsKey(_type))
        {
            objPools[_type].Push(_trans.gameObject);
            _trans.gameObject.SetActive(false);
        }
        else // 타입이 없으면 해당 컨테이너가 없으므로 지움
        {
            Debug.LogErrorFormat("풀에 데이터가 없음 확인 요망 : {0}", _trans.name);
            Destroy(_trans.gameObject);
        }
    }

    // 컴포넌트 유형을 받아서 오브젝트를 제거함
    public void RemoveObj<T>(Type _type, T _obj, Transform _parentTrans = null) where T : Component
    {
        // 컨테이너 트랜스폼이 널이 아니라면 등록
        if (_parentTrans != null)
            _obj.transform.SetParent(_parentTrans);

        // 해시 테이블 안에 클래스 타입이 존재할 시 넣음
        if (objPools.ContainsKey(_type))
        {
            objPools[_type].Push(_obj.gameObject);
            _obj.gameObject.SetActive(false);
        }
        else // 타입이 없으면 해당 컨테이너가 없으므로 지움
        {
            Debug.LogErrorFormat("풀에 데이터가 없음 확인 요망 2 : {0}", _obj.name);
            Destroy(_obj.gameObject);
        }
    }
}