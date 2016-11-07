using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KolkoKrzyzyk.View
{
    public partial class CanvasControl : PictureBox
    {
        public event Action<MouseEventArgs> StartPaint;
        public event Action<MouseEventArgs> StopPaint;
        public event Action<MouseEventArgs> MovePaint;


        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                OnCanvasStartPaint(e);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Button == MouseButtons.Left)
            {
                OnCanvasMove(e);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            OnCanvasStopPaint(e);
        }

        protected virtual void OnCanvasStartPaint(MouseEventArgs e)
        {
            StartPaint?.Invoke(e);
        }

        protected virtual void OnCanvasStopPaint(MouseEventArgs e)
        {
            StopPaint?.Invoke(e);
        }

        protected virtual void OnCanvasMove(MouseEventArgs e)
        {
            MovePaint?.Invoke(e);
        }

    }
}
