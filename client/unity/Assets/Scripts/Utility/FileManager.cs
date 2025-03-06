using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FileManager : MonoBehaviour
{
    private Button AddFile;
    private Button Remove;
    private Button Exit;
    public GameObject targetCanvas;
    public Button start;
    public Button exit;
    public static List<string> SelectedFilePaths { get; private set; } = new List<string>();

    public Transform contentParent; // ScrollView��Content����
    public GameObject fileButtonPrefab; // �ļ���ťԤ����
    private bool isRemovingMode = false; // ����ɾ��ģʽ��־
    private Image removeButtonImage; // ����Image�������
    private Color originalColor = Color.white; // ��ʼ��ɫ

    void Start()
    {
        targetCanvas = GameObject.Find("Canvas/Canvas");
        AddFile = GameObject.Find("Canvas/Canvas/AddFile").GetComponent<Button>();
        Remove = GameObject.Find("Canvas/Canvas/Remove").GetComponent<Button>();
        Exit = GameObject.Find("Canvas/Canvas/Exit").GetComponent<Button>();
        contentParent = GameObject.Find("Canvas/Canvas/Scroll View/Viewport/Content").GetComponentInParent<Transform>();
        fileButtonPrefab = Resources.Load<GameObject>("UI/Buttons/recordButton");
        removeButtonImage = Remove.GetComponent<Image>();
        originalColor = removeButtonImage.color;

        AddFile.onClick.AddListener(() =>
        {
            StartCoroutine(SelectFileAndUpdate());
        });
        Remove.onClick.AddListener(() =>
        {
            isRemovingMode = !isRemovingMode;
            UpdateRemoveButtonColor();
        });
        Exit.onClick.AddListener(() => ExitFileManager());
    }

    IEnumerator SelectFileAndUpdate()
    {
        yield return FileSelect.SelectFile(SelectedFilePaths);
        UpdateFileListUI();
    }

    void UpdateFileListUI()
    {
        // ��վ��б�
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }
        Debug.Log($"the length of FilePaths: {SelectedFilePaths.Count}");
        // �������б�
        foreach (string filePath in SelectedFilePaths)
        {
            GameObject buttonObj = Instantiate(fileButtonPrefab, contentParent);
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            buttonObj.GetComponentInChildren<TMP_Text>().text = fileName;

            // ��ӵ���¼�
            buttonObj.GetComponent<Button>().onClick.AddListener(() =>
            {
                if (isRemovingMode)
                {
                    // ɾ������
                    SelectedFilePaths.Remove(filePath);
                    UpdateFileListUI(); // ����ˢ��UI
                }
                else
                {
                    // ѡ�����
                    OnFileSelected(filePath);
                }
            });
        }
    }
    void UpdateRemoveButtonColor()
    {
        removeButtonImage.color = isRemovingMode ? Color.red : originalColor;
        Remove.OnPointerExit(null); // ����״̬����
    }
    void ExitFileManager()
    {
        targetCanvas.SetActive(false);
        gameObject.SetActive(false);
        start.gameObject.SetActive(true);
        exit.gameObject.SetActive(true);
    }
    void OnFileSelected(string filePath)
    {
        SceneData.FilePath = filePath;
        // �л���test_Game����
        SceneManager.LoadScene("test_Game");
    }
}

public static class SceneData
{
    public static string FilePath { get; set; }
}