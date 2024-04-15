using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using KoganeUnityLib;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class DialogController : MonoBehaviour
{
    [SerializeField]
    private TMP_Typewriter typewriter;

    [SerializeField]
    private GameObject gameObjectDialogCanvas;

    [SerializeField]
    private TextMeshProUGUI helpText;

    [SerializeField]
    private List<MonsterScriptableObject> monsters;

    private List<string> _textBlocks;

    private int _activeBlockIndex;

    private bool _isTypewritingInProgress = false;

    private Dictionary<string, List<string>> dialogsBeforeFight = new Dictionary<string, List<string>>();

    private Dictionary<string, List<string>> dialogsAfterFight = new Dictionary<string, List<string>>();

    private bool _isDialogClosed = false;

    private void Awake()
    {
        foreach (MonsterScriptableObject monster in monsters)
        {
            dialogsBeforeFight[monster.id] = new List<string>(monster.dialogsBeforeFight);
            dialogsAfterFight[monster.id] = new List<string>(monster.dialogsAfterFight);
        }
    }

    private void CloseDialog()
    {
        _isDialogClosed = true;

        gameObjectDialogCanvas.SetActive(false);
    }

    private void Update()
    {
        if (gameObjectDialogCanvas.activeInHierarchy)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (_isTypewritingInProgress)
                {
                    typewriter.Skip(true);
                }
                else
                {
                    ShowNextBlock();
                }
            }
        }
    }

    private void ShowDialog(string text)
    {
        _isDialogClosed = false;

        _textBlocks = text.Split("\n").ToList();
        _activeBlockIndex = -1;

        if (_textBlocks.Count == 0)
        {
            CloseDialog();
            return;
        }

        ShowNextBlock();
    }

    private void ShowNextBlock()
    {
        _activeBlockIndex++;

        if (_activeBlockIndex == _textBlocks.Count)
        {
            CloseDialog();
            return;
        }

        gameObjectDialogCanvas.SetActive(true);

        helpText.SetText("Click to skip.");

        _isTypewritingInProgress = true;

        typewriter.Play(_textBlocks[_activeBlockIndex], 20.0f, () => {
            helpText.SetText("Click to continue.");

            _isTypewritingInProgress = false;
        });
    }

    public IEnumerator ShowDialogBeforeFight(string monsterId)
    {
        if (!dialogsBeforeFight.ContainsKey(monsterId))
        {
            yield break;
        }

        List<string> lines = dialogsBeforeFight[monsterId];

        if (lines.Count > 0)
        {
            string line = lines[0];
            lines.RemoveAt(0);
            ShowDialog(line);
            while (!_isDialogClosed)
            {
                yield return null;
            }
        }
    }

    public IEnumerator ShowDialogAfterFight(string monsterId)
    {
        if (!dialogsAfterFight.ContainsKey(monsterId))
        {
            yield break;
        }

        List<string> lines = dialogsAfterFight[monsterId];

        if (lines.Count > 0)
        {
            string line = lines[0];            
            lines.RemoveAt(0);
            ShowDialog(line);
            while (!_isDialogClosed)
            {
                yield return null;
            }
        }
    }    
}
