using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics;

namespace Densha.clustering
{
    class TimeClustering<T> where T: class
    {
        public delegate int GetSpan(T t1, T t2);

        private List<T> _list = null;
        private List<int> _thCands = new List<int>();
        private Dictionary<int, int> _hist = new Dictionary<int, int>();
        private int _datSize = 0;
        private int _maxSpan = int.MinValue;
        private int _minSpan = int.MaxValue;

        public TimeClustering(List<T> list, GetSpan getSpan)
        {
            this._list = list;
            Dictionary<int, int> tmphist = new Dictionary<int, int>();
            List<int> spans = new List<int>();

            lock (_list)
            {
                T t1 = null;
                foreach (T t2 in _list)
                {
                    if (t1 != null)
                    {
                        int ts = getSpan(t1, t2);

                        if (tmphist.ContainsKey(ts))
                            tmphist[ts] += 1;
                        else
                            tmphist.Add(ts, 1);

                        if (ts < _minSpan) _minSpan = ts;
                        if (ts > _maxSpan) _maxSpan = ts;
                        spans.Add(ts);

                        _datSize++;
                    }
                    t1 = t2;
                }
            }
            spans.Sort();

            int w = (int)((_maxSpan - _minSpan) / 50.0);
            int j = 0;
            for (int i = _minSpan; i <= _maxSpan; i += w)
            {
                _hist.Add(i, 0);
                for (; j < spans.Count; j++)
                {
                    if (spans[j] <= i)
                    {
                        _hist[i] += tmphist[spans[j]];
                    }
                    else break;
                }
            }

            _hist.Add(int.MinValue, 0);
            _hist.Add(int.MaxValue, 0);
            _thCands = new List<int>(_hist.Keys);

            _thCands.Sort();
            calcVals();

            _ths = ThresholdKittler(5);
        }

        private List<int> _ths = null;

        public List<int> ThresholdKittler(int clsNum)
        {
            double[,] jtable = new double[clsNum, _thCands.Count];
            List<int> ths = new List<int>();

            for (int m = 1; m < clsNum; m++)
            {
                double minI = double.MaxValue;
                ths.Add(0);
                for (int i = m; i <= _thCands.Count - 1 - clsNum + m + 1; i++)
                {
                    double minJ = double.MaxValue;
                    double l = _thCands[i];
                    for (int j = m; j < i; j++)
                    {
                        double k = _thCands[j];

                        double z = jtable[m - 1, j] + eval(j, i);
                        if (z < minJ)
                        {
                            minJ = z;
                        }
                    }
                    jtable[m, i] = minJ;
                    if (minJ < minI)
                    {
                        minI = minJ;
                        ths[m - 1] = i;
                    }
                }
            }
            return ths;
        }

        private double[] _histSums;
        private double[] _valueSums;
        private void calcVals()
        {
            _histSums = new double[_thCands.Count];
            _valueSums = new double[_thCands.Count];

            for (int i = 0; i < _thCands.Count; i++)
            {
                if (i == 0)
                {
                    _histSums[i] = _hist[_thCands[i]];
                    _valueSums[i] = _hist[_thCands[i]] * _thCands[i];
                }
                else
                {
                    _histSums[i] = _histSums[i - 1] + _hist[_thCands[i]];
                    _valueSums[i] = _valueSums[i - 1] + _hist[_thCands[i]] * _thCands[i];
                }
            }
        }
        /// <summary>
        /// クラス内データ数
        /// </summary>
        private double getSum(int fromIdx, int toIdx) 
        {
            Debug.Assert(fromIdx >= toIdx, "fromがto未満でない");
            // sum(0, 1) -> th_0 < x <= th_1 のデータ
            return _histSums[toIdx] - _histSums[fromIdx];
        }
        /// <summary>
        /// クラス内平均
        /// </summary>
        private double getMean(int fromIdx, int toIdx)
        {
            Debug.Assert(fromIdx >= toIdx, "fromがto未満でない");
            // sum(0, 1) -> th_0 < x <= th_1 のデータ
            return (_valueSums[toIdx] - _valueSums[fromIdx]) / getSum(fromIdx, toIdx);
        }
        /// <summary>
        /// クラス内分散
        /// </summary>
        private double getVariance(int fromIdx, int toIdx)
        {
            Debug.Assert(fromIdx >= toIdx, "fromがto未満でない");
            // sum(0, 1) -> th_0 < x <= th_1 のデータ
            double sum = getSum(fromIdx, toIdx);
            double mean = getMean(fromIdx, toIdx);

            if (sum == 0) return 0;

            double ret = 0;
            for (int i = fromIdx; i <= toIdx; i++)
            {
                ret += Math.Pow(_thCands[i] - mean, 2) * _hist[_thCands[i]];
            }
            return ret / sum;
        }
        /// <summary>
        /// Kittlerの評価関数
        /// </summary>
        private double eval(int fromIdx, int toIdx)
        {
            Debug.Assert(fromIdx >= toIdx, "fromがto未満でない");
            // sum(0, 1) -> th_0 < x <= th_1 のデータ
            double sum = getSum(fromIdx, toIdx);
            double sigma = Math.Sqrt(getVariance(fromIdx, toIdx));
            double ret = sum * Math.Log10(sigma / sum);
            return ret;
        }

        public void PaintHistogram(Graphics g, Rectangle bounds)
        {
            g.FillRectangle(Brushes.White, bounds);
            g.DrawRectangle(Pens.Red, bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);

            int v = 0;
            int w = (int)(bounds.Width / (double)_thCands.Count);

            foreach (int span in _thCands)
            {
                if (_hist.ContainsKey(span))
                {
                    g.FillRectangle(Brushes.Blue,
                        v * w, 0, w, (int)(_hist[span] * 10));
                    if (_ths.IndexOf(v) >= 0)
                    {
                        g.DrawLine(Pens.Green, v * w, 0, v * w, bounds.Height);
                        g.DrawString(TimeSpan.FromSeconds(_thCands[v]).ToString(),
                            SystemFonts.DefaultFont, Brushes.Green,
                            v * w, bounds.Height - 20 * (_ths.IndexOf(v) + 1));
                    }
                    v++;
                }
            }
        }
    }
}
