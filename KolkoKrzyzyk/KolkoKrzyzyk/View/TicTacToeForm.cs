using System;
using System.Drawing;
using System.Windows.Forms;
using TicTacToe.Model.Canvases;
using TicTacToe.Model.Utility;

namespace TicTacToe.View
{
    public partial class TicTacToeForm : Form
    {
        public event Action<Point, LearnCanvasType> DrawLearnWindowAction;
        public event Action<Point, GameCanvasType> DrawGameWindowAction;
        public event Action LearnAction;
        public event Action CrossAction;
        public event Action CircleAction;
        public event Action<LearnCanvasType> ClearAction;
        public event Action<LearnCanvasType> CopyAction;
        public event Action TestAction;

        public TicTacToeForm()
        {
            InitializeComponent();
        }

        private void canvas_DrawLearnWindow(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PictureBox pictureBox = sender as PictureBox;
                var canvasType = EnumUtil.Parse<LearnCanvasType>(pictureBox?.Name);
                DrawLearnWindowAction?.Invoke(e.Location, canvasType);
            }
        }

        private void canvas_DrawGameWindow(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PictureBox pictureBox = sender as PictureBox;
                var canvasType = EnumUtil.Parse<GameCanvasType>(pictureBox?.Name);
                DrawGameWindowAction?.Invoke(e.Location, canvasType);
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
                ClearAction?.Invoke(LearnCanvasType.Cross);
            }
            if (button.Name == btnCleanCircle.Name)
            {
                ClearAction?.Invoke(LearnCanvasType.Circle);
            }
            if (button.Name == btnCleanBlank.Name)
            {
                ClearAction?.Invoke(LearnCanvasType.Blank);
            }
            if (button.Name == btnCleanTest.Name)
            {
                ClearAction?.Invoke(LearnCanvasType.Test);
            }

        }

        private void btnCoppy_Click(object sender, EventArgs e)
        {

            Button button = sender as Button;
            if (button.Name == btnCoppyCross.Name)
            {
                CopyAction?.Invoke(LearnCanvasType.Cross);
            }
            if (button.Name == btnCoppyCircle.Name)
            {
                CopyAction?.Invoke(LearnCanvasType.Circle);
            }
            if (button.Name == btCoppyBlank.Name)
            {
                CopyAction?.Invoke(LearnCanvasType.Blank);
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
