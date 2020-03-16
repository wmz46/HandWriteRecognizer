using Microsoft.Ink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HandWriteRecognizer
{
    public class InkAnalyzer
    {
        public String Recognizer(int[][][] strokes, int count)
        {
            RecognizerContext recognizerContext = new Recognizers().GetDefaultRecognizer().CreateRecognizerContext();
            Ink ink = new Ink();
            recognizerContext.Strokes = ink.CreateStrokes();
            foreach (int[][] stroke in strokes)
            {
                Point[] points = new Point[stroke.Length];
                for(int i =0;i<stroke.Length;i++)
                {
                    points[i] =  new Point(stroke[i][0], stroke[i][1]);

                } 
                recognizerContext.Strokes.Add(ink.CreateStroke(points));
            }
            RecognitionStatus recognitionStatus = RecognitionStatus.NoError;
            RecognitionResult recognitionResult = recognizerContext.Recognize(out recognitionStatus);
            var text = "";
            if (recognitionStatus == RecognitionStatus.NoError)
            {
                RecognitionAlternates alts = recognitionResult.GetAlternatesFromSelection();
                for (int i =0;i<alts.Count&& i<count;i++)
                {
                    RecognitionAlternate alt = alts[i];
                    text += alt.ToString() + " ";
                }
            }
            return text.Trim();

        } 
    }
}
