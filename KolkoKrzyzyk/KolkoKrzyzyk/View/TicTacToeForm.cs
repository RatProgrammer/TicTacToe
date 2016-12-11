using System;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using TicTacToe.Model.Canvases;
using TicTacToe.Model.Utility;

namespace TicTacToe.View
{
    public partial class TicTacToeForm : Form
    {
        public event Action<Point, CanvasType> DrawAction;
        public event Action LearnAction;
        public event Action CrossAction;
        public event Action CircleAction;
        public event Action<CanvasType> ClearAction;
        public event Action<CanvasType> CopyAction;
        public event Action TestAction;
        public event Action NewGameAction;
        public event Action PlayAction;

        public TicTacToeForm()
        {
            InitializeComponent();
        }   

        private void canvas_Draw(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PictureBox pictureBox = sender as PictureBox;
                var canvasType = EnumUtil.GetFirstEnumElement<CanvasType>(pictureBox?.Name);
                DrawAction?.Invoke(e.Location, canvasType);
            }
        }

        public void UpdatePictureBox(string name, Bitmap bitmap)
        {
            PictureBox box = this.Controls.Find(name, true).FirstOrDefault() as PictureBox;
            box.Image = bitmap;
            box.Invalidate();
            box.Refresh();
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

        private void canvas_NewGame(object sender, EventArgs e)
        {
            NewGameAction?.Invoke();
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

        private void btnPlay_Click(object sender, EventArgs e)
        {
            PlayAction?.Invoke();
        }
    }
}
