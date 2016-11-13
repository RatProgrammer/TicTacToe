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
        private CanvasType canvasType;
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
            CanvasControl canvasControl = sender as CanvasControl;
            canvasType = EnumUtil.ParseEnum<CanvasType>(canvasControl?.Name);
            MovePaintAction?.Invoke(obj.Location, canvasType);
            
        }

        private void canvas_StartPaint(object sender, MouseEventArgs e)
        {
            CanvasControl canvasControl = sender as CanvasControl;
            canvasType = EnumUtil.ParseEnum<CanvasType>(canvasControl?.Name);
            StartPaintAction?.Invoke(e.Location,canvasType);
        }

        private void canvas_StopPaint(object sender, MouseEventArgs e)
        {
            CanvasControl canvasControl = sender as CanvasControl;
            canvasType = EnumUtil.ParseEnum<CanvasType>(canvasControl?.Name);
            StopPaintAction?.Invoke(e.Location,canvasType);
        }

        public void UpdateCanvasX(Bitmap currentBitmap)
        {
            cCX.Image = currentBitmap;
            cCX.Invalidate();
            cCX.Refresh();
        }


        public void UpdateCanvasO(Bitmap currentBitmap)
        {
            cCO.Image = currentBitmap;
            cCO.Invalidate();
            cCO.Refresh();
        }

        public void UpdateCanvasTest(Bitmap currentBitmap)
        {
                    cCTest.Image = currentBitmap;
                    cCTest.Invalidate();
                    cCTest.Refresh();
        }
        public void UpdateCanvasBlank(Bitmap currentBitmap)
        {
                    cCBlank.Image = currentBitmap;
                    cCBlank.Invalidate();
                    cCBlank.Refresh();
        }
    }
}
