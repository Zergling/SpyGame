using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalPage : MonoBehaviour 
{
	[SerializeField] private List<JournalItemUI> _entries;
	public List<JournalItemUI> Entries { get { return _entries; } }
}
