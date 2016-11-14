using System;
using System.Drawing;
using System.Windows.Forms;
using TicTacToe.Model.Utility;

namespace TicTacToe.View
{
    public partial class TicTacToe : Form
    {
        public event Action<Point, CanvasType> StartPaintAction;
        public event Action<Point, CanvasType> StopPaintAction;
        public event Action<Point, CanvasType> MovePaintAction;
        public event Action LearnAction;
        public event Action CrossAction;
        public event Action CircleAction;
        public event Action<CanvasType> ClearAction;
        public event Action<CanvasType> CopyAction;
        public event Action TestAction;
        private CanvasType canvasType;
        public TicTacToe()
        {
            InitializeComponent();
            InitCanvas();
        }

        private void InitCanvas()
        {
            pcCross.MouseDown += canvas_StartPaint;
            pcCross.MouseUp += canvas_StopPaint;
            pcCross.MouseMove += canvas_MovePaint;
            pcCircle.MouseDown += canvas_StartPaint;
            pcCircle.MouseUp += canvas_StopPaint;
            pcCircle.MouseMove += canvas_MovePaint;
            pcTest.MouseDown += canvas_StartPaint;
            pcTest.MouseUp += canvas_StopPaint;
            pcTest.MouseMove += canvas_MovePaint;
            pcBlank.MouseDown += canvas_StartPaint;
            pcBlank.MouseUp += canvas_StopPaint;
            pcBlank.MouseMove += canvas_MovePaint;
        }

        private void canvas_MovePaint(object sender,MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PictureBox PictureBox = sender as PictureBox;
                canvasType = EnumUtil.ParseEnum<CanvasType>(PictureBox?.Name);
                MovePaintAction?.Invoke(e.Location, canvasType);
            }

        }

        private void canvas_StartPaint(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PictureBox PictureBox = sender as PictureBox;
                canvasType = EnumUtil.ParseEnum<CanvasType>(PictureBox?.Name);
                StartPaintAction?.Invoke(e.Location, canvasType);
            }
        }

        private void canvas_StopPaint(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PictureBox PictureBox = sender as PictureBox;
                canvasType = EnumUtil.ParseEnum<CanvasType>(PictureBox?.Name);
                StopPaintAction?.Invoke(e.Location, canvasType);
            }
        }

        public void UpdateCanvasCross(Bitmap currentBitmap)
        {
            pcCross.Image = currentBitmap;
            pcCross.Invalidate();
            pcCross.Refresh();
        }


        public void UpdateCanvasCircle(Bitmap currentBitmap)
        {
            pcCircle.Image = currentBitmap;
            pcCircle.Invalidate();
            pcCircle.Refresh();
        }

        public void UpdateCanvasTest(Bitmap currentBitmap)
        {
            pcTest.Image = currentBitmap;
            pcTest.Invalidate();
            pcTest.Refresh();
        }

        public void UpdateCanvasBlank(Bitmap currentBitmap)
        {
            pcBlank.Image = currentBitmap;
            pcBlank.Invalidate();
            pcBlank.Refresh();
        }

        private void btUcz_Click(object sender, EventArgs e)
        {
            LearnAction?.Invoke();
        }

        private void btKrzyzyk_Click(object sender, EventArgs e)
        {
            CrossAction?.Invoke();
        }

        private void canvas_Clean(object sender, EventArgs e)
        {
                Button button = sender as Button;
            if (button.Name == btCzyscKrzyzyk.Name)
            {
                ClearAction?.Invoke(CanvasType.pcCross);
            }
            if (button.Name == btCzyscKolko.Name)
            {
                ClearAction?.Invoke(CanvasType.pcCircle);
            }
            if (button.Name == btCzyscPusty.Name)
            {
                ClearAction?.Invoke(CanvasType.pcBlank);
            }
            if (button.Name == btCzyscTestowanie.Name)
            {
                ClearAction?.Invoke(CanvasType.pcTest);
            }

        }

        private void btKopiuj_Click(object sender, EventArgs e)
        {

            Button button = sender as Button;
            if (button.Name == btKopiujKrzyzyk.Name)
            {
                CopyAction?.Invoke(CanvasType.pcCross);
            }
            if (button.Name == btKopiujKolko.Name)
            {
                CopyAction?.Invoke(CanvasType.pcCircle);
            }
            if (button.Name == btKopiujPusty.Name)
            {
                CopyAction?.Invoke(CanvasType.pcBlank);
            }
        }

        private void btTest_Click(object sender, EventArgs e)
        {
            TestAction?.Invoke();
        }

        private void btKolko_Click(object sender, EventArgs e)
        {
            CircleAction?.Invoke();
        }
    }
}
