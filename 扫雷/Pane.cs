using System;
using System.Collections.Generic;

using System.Windows.Controls;
using System.Windows.Controls.Primitives;
namespace 扫雷
{
    class Pane :Button
    {
        public Image back = new Image();
        public int MaxNoMineNum
        { get; set; }
        public int MaxRow
        { get; set; }
        public int MaxColumn
        { get; set; }
        public bool IsMine
        { get; set; }
        public int MineAround
        { get; set; }
        public bool IsFlagged
        { get; set; }
        public bool IsOpened
        { get; set; }
        public Pane(bool isMine)
        {
            IsMine = isMine;
            
        }
        
    }

    
}
