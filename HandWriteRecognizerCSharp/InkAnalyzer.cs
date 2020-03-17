using Microsoft.Ink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Text.RegularExpressions;

namespace HandWriteRecognizerCSharp
{
    public class InkAnalyzer
    {
        public String Recognizer(String strokesStr, int count)
        {
            List<List<int[]>> strokes = new List<List<int[]>>();
            var array = Regex.Split(strokesStr, ",eb,");
            foreach (var item in array)
            {
                var stroke = new List<int[]>();
                var array2 = item.Split(',');
                for (var i = 0; i < array2.Length; i = i + 2)
                {
                    int[] point = new int[2];
                    point[0] = int.Parse(array2[i]);
                    point[1] = int.Parse(array2[i + 1]);
                    stroke.Add(point);
                }
                strokes.Add(stroke);

            }

            RecognizerContext recognizerContext = new Recognizers().GetDefaultRecognizer().CreateRecognizerContext();
            Ink ink = new Ink();
            recognizerContext.Strokes = ink.CreateStrokes();
            foreach (List<int[]> stroke in strokes)
            {
                Point[] points = new Point[stroke.Count];
                for (int i = 0; i < stroke.Count; i++)
                {
                    points[i] = new Point(stroke[i][0], stroke[i][1]);

                }
                recognizerContext.Strokes.Add(ink.CreateStroke(points));
            }
            RecognitionStatus recognitionStatus = RecognitionStatus.NoError;
            RecognitionResult recognitionResult = recognizerContext.Recognize(out recognitionStatus);
            var text = "";
            if (recognitionStatus == RecognitionStatus.NoError)
            {
                RecognitionAlternates alts = recognitionResult.GetAlternatesFromSelection();
                for (int i = 0; i < alts.Count && i < count; i++)
                {
                    RecognitionAlternate alt = alts[i];
                    text += alt.ToString() + " ";
                }
            }
            return text.Trim();

        }
    }
}
