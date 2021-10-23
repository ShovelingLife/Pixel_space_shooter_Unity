using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// 씬 전환 후 그 전에서 사용했던 것들을 새로운 씬에서 사용하기 위함
[Serializable]
public class Object_pool_data
{
    // 오브젝트 종류 및 개수
    public GameObject obj_pool = null;
    public int        count    = 0;

    // 그 전 씬에 사용했던 오브젝트 및 개수 할당
    public Object_pool_data(GameObject _obj, int _count)
    {
        obj_pool = _obj;
        count    = _count;
    }
}

public class Object_pooling_manager : Singleton_local<Object_pooling_manager>
{
    // 클래스 타입 - 해당 타입 오브젝트 해시 테이블
    protected Dictionary<Type, Stack<GameObject>> m_dict_pool_manager = new Dictionary<Type, Stack<GameObject>>();

    // 사용했던 
    public    List<Object_pool_data>              list_prev_obj       = new List<Object_pool_data>();


    private void Awake()
    {
        Init_setting_prev_obj();
    }

    // 전 오브젝트 리스트 안에 있는 오브젝트를 초기화 해줌
    protected void Init_setting_prev_obj()
    {
        // 강제 등록
        foreach (var item in list_prev_obj)
        {
            Stack<GameObject> tmp_stack = new Stack<GameObject>();
            m_dict_pool_manager.Add(item.obj_pool.GetType(), tmp_stack);

            // 오브젝트를 개수만큼 생성 후 스택에 추가
            for (int i = 0; i < item.count; ++i)
            {
                GameObject copyobj = GameObject.Instantiate(item.obj_pool);
                m_dict_pool_manager.Add(copyobj.GetType(), tmp_stack);
            }
        }
    }

    // 전 오브젝트들을 초기화 함
    public void Init_prev_obj(GameObject _obj, int _count)
    {
        Stack<GameObject> tmp_stack = new Stack<GameObject>();
        m_dict_pool_manager.Add(_obj.GetType(), tmp_stack);

        // 오브젝트를 개수만큼 생성 후 스택에 넣음
        for (int i = 0; i < _count; ++i)
        {
            GameObject copy_obj = GameObject.Instantiate(_obj);
            copy_obj.SetActive(false);
            m_dict_pool_manager.Add(copy_obj.GetType(), tmp_stack);
        }
    }

    // 오브젝트 컨테이너 담당하는 오브젝트에 자식으로 추가
    public Transform Create_obj(Type _type, Transform _trans, Transform _trans_parent = null)
    {
        Transform         trans_out = null;
        Stack<GameObject> tmp_stack = null;

        // ------- 스택 초기화 부분 -------

        // Type 키가 존재한다면 딕셔너리에서 스택을 갖고옴
        if (m_dict_pool_manager.ContainsKey(_type))
            tmp_stack = m_dict_pool_manager[_type];

        else // 없으면 스택을 새로 생성해서 넣음
        {
            tmp_stack = new Stack<GameObject>();
            m_dict_pool_manager.Add(_type, tmp_stack);
        }

        // ------- Transform 초기화 부분 -------

        // 스택에 오브젝트가 있고 꺼져있으면 해당 오브젝트 트랜스폼 가져옴
        if (tmp_stack.Count > 0 &&
            !tmp_stack.Peek().activeInHierarchy)
            trans_out = tmp_stack.Pop().transform;

        else // 없으면 새로 만들어서 추가
        {
            trans_out = GameObject.Instantiate(_trans);
            m_dict_pool_manager[_type].Push(trans_out.gameObject);
        }
        // 오브젝트 부모 설정
        if (_trans_parent != null)
            trans_out.SetParent(_trans_parent);

        trans_out.gameObject.SetActive(true);
        return trans_out;
    }

    // 템플릿 인자 받아서 오브젝트 생성 (여러 컴포넌트 사용 가능)
    public T Create_obj<T>(Type _type, T _clone_obj, Transform _trans_parent = null) where T : Component
    {
        T                 out_result = null;
        Stack<GameObject> tmp_stack  = null;

        // ------- 스택 초기화 부분 -------

        // Type 키가 존재한다면 딕셔너리에서 스택을 갖고옴
        if (m_dict_pool_manager.ContainsKey(_type))
            tmp_stack = m_dict_pool_manager[_type];

        else // 없으면 스택을 새로 생성해서 넣음
        {
            tmp_stack = new Stack<GameObject>();
            m_dict_pool_manager.Add(_type, tmp_stack);
        }

        // ------- Transform 초기화 부분 -------

        // 스택에 오브젝트가 있고 꺼져있으면 해당 오브젝트 트랜스폼 가져옴
        if (tmp_stack.Count > 0 &&
            !tmp_stack.Peek().activeInHierarchy)
            out_result = tmp_stack.Pop().GetComponent<T>();

        else // 없으면 새로 만들어서 추가
        {
            out_result = GameObject.Instantiate<T>(_clone_obj);
            m_dict_pool_manager[_type].Push(out_result.gameObject);
        }
        // 오브젝트 부모 설정
        if (_trans_parent != null)
            out_result.transform.SetParent(_trans_parent);

        out_result.gameObject.SetActive(true);
        return out_result;
    }

    //void RemoveObject()

    // 오브젝트를 제거해주는 코루틴
    IEnumerator IE_remove_obj(Type _type, Transform _trans, Transform _trans_parent = null, float _remove_sec = 0f)
    {
        yield return new WaitForSeconds(_remove_sec);

        Remove_obj(_type, _trans, _trans_parent);
    }

    // 나타났다가 일정 시간 후 사라지는 오브젝트 예)이펙트
    public Transform Create_delay_remove_obj(Type _type,
        Transform _trans,
        float     _remove_sec,
        Transform _trans_parent        = null,
        Transform _trans_remove_parent = null)
    {
        Transform copy_obj = null;

        // 시간이 초과 됐다면 제거
        if (_remove_sec > 0f)
        {
            copy_obj = Create_obj(_type, _trans, _trans_parent);
            StartCoroutine(IE_remove_obj(_type, copy_obj, _trans_remove_parent, _remove_sec));
        }
        return copy_obj;
    }

    // 오브젝트를 제거
    public void Remove_obj(Type _type, Transform _trans, Transform _trans_parent = null)
    {
        // 컨테이너 트랜스폼이 널이 아니라면 등록
        if (_trans_parent != null)
            _trans.SetParent(_trans_parent);

        // 해시 테이블 안에 클래스 타입이 존재할 시 넣음
        if (m_dict_pool_manager.ContainsKey(_type))
        {
            m_dict_pool_manager[_type].Push(_trans.gameObject);
            _trans.gameObject.SetActive(false);
        }
        else // 타입이 없으면 해당 컨테이너가 없으므로 지움
        {
            Debug.LogErrorFormat("풀에 데이터가 없음 확인 요망 : {0}", _trans.name);
            GameObject.Destroy(_trans.gameObject);
        }
    }

    // 컴포넌트 유형을 받아서 오브젝트를 제거함
    public void Remove_obj<T>(Type _type, T _obj, Transform _trans_parent = null) where T : Component
    {
        // 컨테이너 트랜스폼이 널이 아니라면 등록
        if (_trans_parent != null)
            _obj.transform.SetParent(_trans_parent);

        // 해시 테이블 안에 클래스 타입이 존재할 시 넣음
        if (m_dict_pool_manager.ContainsKey(_type))
        {
            m_dict_pool_manager[_type].Push(_obj.gameObject);
            _obj.gameObject.SetActive(false);
        }
        else // 타입이 없으면 해당 컨테이너가 없으므로 지움
        {
            Debug.LogErrorFormat("풀에 데이터가 없음 확인 요망 2 : {0}", _obj.name);
            GameObject.Destroy(_obj.gameObject);
        }
    }
}