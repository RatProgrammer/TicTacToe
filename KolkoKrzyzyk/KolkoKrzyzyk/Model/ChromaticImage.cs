namespace TicTacToe.Model
{
    class ChromaticImage
    {
        private double[] _image;

        public ChromaticImage(double[] image)
        {
            _image = image;
        }

        public double[] GetChromaticImage()
        {
            return _image;
        }

    }
}
