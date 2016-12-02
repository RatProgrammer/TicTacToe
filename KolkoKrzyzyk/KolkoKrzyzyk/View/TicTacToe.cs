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
            pbCross.MouseDown += canvas_StartPaint;
            pbCross.MouseUp += canvas_StopPaint;
            pbCross.MouseMove += canvas_MovePaint;
            pbCircle.MouseDown += canvas_StartPaint;
            pbCircle.MouseUp += canvas_StopPaint;
            pbCircle.MouseMove += canvas_MovePaint;
            pbTest.MouseDown += canvas_StartPaint;
            pbTest.MouseUp += canvas_StopPaint;
            pbTest.MouseMove += canvas_MovePaint;
            pbBlank.MouseDown += canvas_StartPaint;
            pbBlank.MouseUp += canvas_StopPaint;
            pbBlank.MouseMove += canvas_MovePaint;
        }

        private void canvas_MovePaint(object sender,MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PictureBox PictureBox = sender as PictureBox;
                canvasType = EnumUtil.Parse<CanvasType>(PictureBox?.Name);
                MovePaintAction?.Invoke(e.Location, canvasType);
            }

        }

        private void canvas_StartPaint(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PictureBox PictureBox = sender as PictureBox;
                canvasType = EnumUtil.Parse<CanvasType>(PictureBox?.Name);
                StartPaintAction?.Invoke(e.Location, canvasType);
            }
        }

        private void canvas_StopPaint(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PictureBox PictureBox = sender as PictureBox;
                canvasType = EnumUtil.Parse<CanvasType>(PictureBox?.Name);
                StopPaintAction?.Invoke(e.Location, canvasType);
            }
        }

        public void UpdateCanvasCross(Bitmap currentBitmap)
        {
            pbCross.Image = currentBitmap;
            pbCross.Invalidate();
            pbCross.Refresh();
        }


        public void UpdateCanvasCircle(Bitmap currentBitmap)
        {
            pbCircle.Image = currentBitmap;
            pbCircle.Invalidate();
            pbCircle.Refresh();
        }

        public void UpdateCanvasTest(Bitmap currentBitmap)
        {
            pbTest.Image = currentBitmap;
            pbTest.Invalidate();
            pbTest.Refresh();
        }

        public void UpdateCanvasBlank(Bitmap currentBitmap)
        {
            pbBlank.Image = currentBitmap;
            pbBlank.Invalidate();
            pbBlank.Refresh();
        }

        public void UpdateCanvasResult(Bitmap currentBitmap)
        {
            pbResult.Image = currentBitmap;
            pbResult.Invalidate();
            pbResult.Refresh();
        }

        private void btnLearn_Click(object sender, EventArgs e)
        {
            LearnAction?.Invoke();
        }

        private void btnCross_Click(object sender, EventArgs e)
        {
            CrossAction?.Invoke();
        }

        private void canvas_Clean(object sender, EventArgs e)
        {
                Button button = sender as Button;
            if (button.Name == btnCleanCross.Name)
            {
                ClearAction?.Invoke(CanvasType.Cross);
            }
            if (button.Name == btnCleanCircle.Name)
            {
                ClearAction?.Invoke(CanvasType.Circle);
            }
            if (button.Name == btnCleanBlank.Name)
            {
                ClearAction?.Invoke(CanvasType.Blank);
            }
            if (button.Name == btnCleanTest.Name)
            {
                ClearAction?.Invoke(CanvasType.Test);
            }

        }

        private void btnCoppy_Click(object sender, EventArgs e)
        {

            Button button = sender as Button;
            if (button.Name == btnCoppyCross.Name)
            {
                CopyAction?.Invoke(CanvasType.Cross);
            }
            if (button.Name == btnCoppyCircle.Name)
            {
                CopyAction?.Invoke(CanvasType.Circle);
            }
            if (button.Name == btCoppyBlank.Name)
            {
                CopyAction?.Invoke(CanvasType.Blank);
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            TestAction?.Invoke();
        }

        private void btnCircle_Click(object sender, EventArgs e)
        {
            CircleAction?.Invoke();
        }

        public void ShowMessage(string text)
        {
            lbInformation.Text = text;
            this.Update();
        }
    }
}
