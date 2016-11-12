using System;
using System.Drawing;
using System.Windows.Forms;
using KolkoKrzyzyk.Model.Utility;
using KolkoKrzyzyk.View;

namespace KolkoKrzyzyk
{
    public partial class TicTacToe : Form
    {
        public event Action<Point, CanvasType> StartPaintAction;
        public event Action<Point, CanvasType> StopPaintAction;
        public event Action<Point, CanvasType> MovePaintAction;
        public TicTacToe()
        {
            InitializeComponent();
            InitCanvas();
        }

        private void InitCanvas()
        {
            cCX.StartPaint += canvas_StartPaint;
            cCX.StopPaint += canvas_StopPaint;
            cCX.MovePaint += canvas_MovePaint;
            cCO.StartPaint += canvas_StartPaint;
            cCO.StopPaint += canvas_StopPaint;
            cCO.MovePaint += canvas_MovePaint;
            cCTest.StartPaint += canvas_StartPaint;
            cCTest.StopPaint += canvas_StopPaint;
            cCTest.MovePaint += canvas_MovePaint;
        }

        private void canvas_MovePaint(object sender,MouseEventArgs obj)
        {
            CanvasType canvasType= CanvasType.None;
            CanvasControl canvasControl = sender as CanvasControl;
            canvasType = EnumUtil.ParseEnum<CanvasType>(canvasControl?.Name);
            MovePaintAction?.Invoke(obj.Location, canvasType);
            
        }

        private void canvas_StartPaint(object sender, MouseEventArgs e)
        {
            CanvasType canvasType = CanvasType.None;
            CanvasControl canvasControl = sender as CanvasControl;
            canvasType = EnumUtil.ParseEnum<CanvasType>(canvasControl?.Name);
            StartPaintAction?.Invoke(e.Location,canvasType);
        }

        private void canvas_StopPaint(object sender, MouseEventArgs e)
        {
            CanvasType canvasType = CanvasType.None;
            CanvasControl canvasControl = sender as CanvasControl;
            canvasType = EnumUtil.ParseEnum<CanvasType>(canvasControl?.Name);
            StopPaintAction?.Invoke(e.Location,canvasType);
        }

        public void UpdateCanvas(CanvasType canvasType, Bitmap currentBitmap)
        {
            switch (canvasType)
            {
                case CanvasType.CCX:
                    cCX.Image = currentBitmap;
                    cCX.Invalidate();
                    cCX.Refresh();
                    break;
                case CanvasType.CCO:
                    cCO.Image = currentBitmap;
                    cCO.Invalidate();
                    cCO.Refresh();
                    break;
                case CanvasType.CCBlank:
                    cCBlank.Image = currentBitmap;
                    cCBlank.Invalidate();
                    cCBlank.Refresh();
                    break;
                case CanvasType.CCTest:
                    cCTest.Image = currentBitmap;
                    cCTest.Invalidate();
                    cCTest.Refresh();
                    break;
                case CanvasType.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(canvasType), canvasType, null);
            }
        }



    }
}
