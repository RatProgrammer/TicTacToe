using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KolkoKrzyzyk
{
    public partial class TicTacToe : Form
    {
        public event Action<Point> StartPaintAction;
        public event Action<Point> StopPaintAction;
        public TicTacToe()
        {
            InitializeComponent();
        }
        private void canvas_StartPaint(MouseEventArgs e)
        {
            StartPaintAction?.Invoke(e.Location);
        }
        private void canvas_StopPaint(MouseEventArgs e)
        {
            StopPaintAction?.Invoke(e.Location);
        }

    }
}
