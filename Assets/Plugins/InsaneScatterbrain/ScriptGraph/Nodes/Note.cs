#if UNITY_2020_1_OR_NEWER && UNITY_EDITOR

using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace InsaneScatterbrain.ScriptGraph
{
    [Serializable]
    public class Note
    {
        [SerializeField] private Rect position;
        [SerializeField] private string title;
        [SerializeField] private string contents;
        [SerializeField] private StickyNoteTheme theme;
        [SerializeField] private StickyNoteFontSize fontSize;
        
        public Rect Position
        {
            get => position;
            set => position = value;
        }
        
        public string Title
        {
            get => title;
            set => title = value;
        }
        
        public string Contents
        {
            get => contents;
            set => contents = value;
        }
        
        public StickyNoteTheme Theme
        {
            get => theme;
            set => theme = value;
        }
        
        public StickyNoteFontSize FontSize
        {
            get => fontSize;
            set => fontSize = value;
        }
    }
}

#endif