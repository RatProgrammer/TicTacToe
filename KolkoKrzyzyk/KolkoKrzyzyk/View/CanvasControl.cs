using System;
using System.Windows.Forms;

namespace KolkoKrzyzyk.View
{
    public class CanvasControl : PictureBox
    {
        public event Action<object, MouseEventArgs> StartPaint;
        public event Action<object, MouseEventArgs> StopPaint;
        public event Action<object, MouseEventArgs> MovePaint;


        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                OnCanvasStartPaint(this, e);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Button == MouseButtons.Left)
            {
                OnCanvasMove(this, e);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            OnCanvasStopPaint(this, e);
        }

        protected virtual void OnCanvasStartPaint(object sender, MouseEventArgs e)
        {
            StartPaint?.Invoke(sender, e);
        }

        protected virtual void OnCanvasStopPaint(object sender, MouseEventArgs e)
        {
            StopPaint?.Invoke(sender, e);
        }

        protected virtual void OnCanvasMove(object sender, MouseEventArgs e)
        {
            MovePaint?.Invoke(sender, e);
        }

    }
}
