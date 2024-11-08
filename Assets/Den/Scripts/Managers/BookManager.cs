using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class BookManager : MonoBehaviour
{
    private static bool s_IsShuttingDown = false;

    public static BookManager Instance
    {
        get// set and get just prevent inauthoritized accesses
        {
#if UNITY_EDITOR
            if (s_Instance == null && !s_IsShuttingDown)
            {
                var newInstance = Instantiate(Resources.Load<BookManager>("BookManager"));
                newInstance.Awake();
            }
#endif
            return s_Instance;
        }
        private set => s_Instance = value;
    }

    private static BookManager s_Instance;
    public Book book;

    private void Awake()
    {
        if (s_Instance == this)
        {
            return;
        }

        if (s_Instance == null)
        {
            s_Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (UIManager.Instance != null) UIManager.Instance.OpenBook(book);
        }
    }
}
