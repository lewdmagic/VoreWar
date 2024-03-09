/**
 * Rectangle packer
 *
 * Copyright 2012 Ville Koskela. All rights reserved.
 * Ported to Unity by Da Viking Code.
 *
 * Email: ville@villekoskela.org
 * Blog: http://villekoskela.org
 * Twitter: @villekoskelaorg
 *
 * You may redistribute, use and/or modify this source code freely
 * but this copyright statement must not be removed from the source files.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
 * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
 * ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE. *
 *
 */

#region

using System.Collections.Generic;

#endregion

namespace DaVikingCode.RectanglePacking
{
    /**
     * Class used to pack rectangles within container rectangle with close to optimal solution.
     */
    public class RectanglePacker
    {
        public static readonly string Version = "1.3.0";
        private readonly List<IntegerRectangle> _mFreeAreas = new List<IntegerRectangle>();

        private readonly List<IntegerRectangle> _mInsertedRectangles = new List<IntegerRectangle>();

        private readonly List<SortableSize> _mInsertList = new List<SortableSize>();
        private readonly List<IntegerRectangle> _mNewFreeAreas = new List<IntegerRectangle>();

        private readonly IntegerRectangle _mOutsideRectangle;

        private readonly List<IntegerRectangle> _mRectangleStack = new List<IntegerRectangle>();

        private readonly List<SortableSize> _mSortableSizeStack = new List<SortableSize>();
        private int _mHeight;

        private int _mWidth;

        public RectanglePacker(int width, int height, int padding = 0)
        {
            _mOutsideRectangle = new IntegerRectangle(width + 1, height + 1);
            Reset(width, height, padding);
        }

        public int RectangleCount => _mInsertedRectangles.Count;

        public int PackedWidth { get; private set; }

        public int PackedHeight { get; private set; }

        public int Padding { get; private set; } = 8;

        public void Reset(int width, int height, int padding = 0)
        {
            while (_mInsertedRectangles.Count > 0)
            {
                FreeRectangle(_mInsertedRectangles.Pop());
            }

            while (_mFreeAreas.Count > 0)
            {
                FreeRectangle(_mFreeAreas.Pop());
            }

            _mWidth = width;
            _mHeight = height;

            PackedWidth = 0;
            PackedHeight = 0;

            _mFreeAreas.Add(AllocateRectangle(0, 0, _mWidth, _mHeight));

            while (_mInsertList.Count > 0)
            {
                FreeSize(_mInsertList.Pop());
            }

            this.Padding = padding;
        }

        public IntegerRectangle GetRectangle(int index)
        {
            IntegerRectangle rectangle = new IntegerRectangle();
            var inserted = _mInsertedRectangles[index];

            rectangle.X = inserted.X;
            rectangle.Y = inserted.Y;
            rectangle.Width = inserted.Width;
            rectangle.Height = inserted.Height;

            return rectangle;
        }

        public string GetRectangleId(int index)
        {
            var inserted = _mInsertedRectangles[index];
            return inserted.ID;
        }

        public void InsertRectangle(int width, int height, string id)
        {
            var sortableSize = AllocateSize(width, height, id);
            _mInsertList.Add(sortableSize);
        }

        public int PackRectangles(bool sort = true)
        {
            if (sort)
            {
                _mInsertList.Sort((emp1, emp2) => emp1.Width.CompareTo(emp2.Width));
            }

            while (_mInsertList.Count > 0)
            {
                var sortableSize = _mInsertList.Pop();
                var width = sortableSize.Width;
                var height = sortableSize.Height;

                var index = GetFreeAreaIndex(width, height);
                if (index >= 0)
                {
                    var freeArea = _mFreeAreas[index];
                    var target = AllocateRectangle(freeArea.X, freeArea.Y, width, height);
                    target.ID = sortableSize.ID;

                    // Generate the new free areas, these are parts of the old ones intersected or touched by the target
                    GenerateNewFreeAreas(target, _mFreeAreas, _mNewFreeAreas);

                    while (_mNewFreeAreas.Count > 0)
                    {
                        _mFreeAreas.Add(_mNewFreeAreas.Pop());
                    }

                    _mInsertedRectangles.Add(target);

                    if (target.Right > PackedWidth)
                    {
                        PackedWidth = target.Right;
                    }

                    if (target.Bottom > PackedHeight)
                    {
                        PackedHeight = target.Bottom;
                    }
                }

                FreeSize(sortableSize);
            }

            return RectangleCount;
        }

        private void FilterSelfSubAreas(List<IntegerRectangle> areas)
        {
            for (var i = areas.Count - 1; i >= 0; i--)
            {
                var filtered = areas[i];
                for (var j = areas.Count - 1; j >= 0; j--)
                {
                    if (i != j)
                    {
                        var area = areas[j];
                        if (filtered.X >= area.X && filtered.Y >= area.Y && filtered.Right <= area.Right && filtered.Bottom <= area.Bottom)
                        {
                            FreeRectangle(filtered);
                            var topOfStack = areas.Pop();
                            if (i < areas.Count)
                            {
                                // Move the one on the top to the freed position
                                areas[i] = topOfStack;
                            }

                            break;
                        }
                    }
                }
            }
        }

        private void GenerateNewFreeAreas(IntegerRectangle target, List<IntegerRectangle> areas, List<IntegerRectangle> results)
        {
            // Increase dimensions by one to get the areas on right / bottom this rectangle touches
            // Also add the padding here
            var x = target.X;
            var y = target.Y;
            var right = target.Right + 1 + Padding;
            var bottom = target.Bottom + 1 + Padding;

            IntegerRectangle targetWithPadding = null;
            if (Padding == 0)
            {
                targetWithPadding = target;
            }

            for (var i = areas.Count - 1; i >= 0; i--)
            {
                var area = areas[i];
                if (!(x >= area.Right || right <= area.X || y >= area.Bottom || bottom <= area.Y))
                {
                    if (targetWithPadding == null)
                    {
                        targetWithPadding = AllocateRectangle(target.X, target.Y, target.Width + Padding, target.Height + Padding);
                    }

                    GenerateDividedAreas(targetWithPadding, area, results);
                    var topOfStack = areas.Pop();
                    if (i < areas.Count)
                    {
                        // Move the one on the top to the freed position
                        areas[i] = topOfStack;
                    }
                }
            }

            if (targetWithPadding != null && targetWithPadding != target)
            {
                FreeRectangle(targetWithPadding);
            }

            FilterSelfSubAreas(results);
        }

        private void GenerateDividedAreas(IntegerRectangle divider, IntegerRectangle area, List<IntegerRectangle> results)
        {
            var count = 0;

            var rightDelta = area.Right - divider.Right;
            if (rightDelta > 0)
            {
                results.Add(AllocateRectangle(divider.Right, area.Y, rightDelta, area.Height));
                count++;
            }

            var leftDelta = divider.X - area.X;
            if (leftDelta > 0)
            {
                results.Add(AllocateRectangle(area.X, area.Y, leftDelta, area.Height));
                count++;
            }

            var bottomDelta = area.Bottom - divider.Bottom;
            if (bottomDelta > 0)
            {
                results.Add(AllocateRectangle(area.X, divider.Bottom, area.Width, bottomDelta));
                count++;
            }

            var topDelta = divider.Y - area.Y;
            if (topDelta > 0)
            {
                results.Add(AllocateRectangle(area.X, area.Y, area.Width, topDelta));
                count++;
            }

            if (count == 0 && (divider.Width < area.Width || divider.Height < area.Height))
            {
                // Only touching the area, store the area itself
                results.Add(area);
            }
            else
            {
                FreeRectangle(area);
            }
        }

        private int GetFreeAreaIndex(int width, int height)
        {
            var best = _mOutsideRectangle;
            var index = -1;

            var paddedWidth = width + Padding;
            var paddedHeight = height + Padding;

            var count = _mFreeAreas.Count;
            for (var i = count - 1; i >= 0; i--)
            {
                var free = _mFreeAreas[i];
                if (free.X < PackedWidth || free.Y < PackedHeight)
                {
                    // Within the packed area, padding required
                    if (free.X < best.X && paddedWidth <= free.Width && paddedHeight <= free.Height)
                    {
                        index = i;
                        if ((paddedWidth == free.Width && free.Width <= free.Height && free.Right < _mWidth) || (paddedHeight == free.Height && free.Height <= free.Width))
                        {
                            break;
                        }

                        best = free;
                    }
                }
                else
                {
                    // Outside the current packed area, no padding required
                    if (free.X < best.X && width <= free.Width && height <= free.Height)
                    {
                        index = i;
                        if ((width == free.Width && free.Width <= free.Height && free.Right < _mWidth) || (height == free.Height && free.Height <= free.Width))
                        {
                            break;
                        }

                        best = free;
                    }
                }
            }

            return index;
        }

        private IntegerRectangle AllocateRectangle(int x, int y, int width, int height)
        {
            if (_mRectangleStack.Count > 0)
            {
                var rectangle = _mRectangleStack.Pop();
                rectangle.X = x;
                rectangle.Y = y;
                rectangle.Width = width;
                rectangle.Height = height;
                rectangle.Right = x + width;
                rectangle.Bottom = y + height;

                return rectangle;
            }

            return new IntegerRectangle(x, y, width, height);
        }

        private void FreeRectangle(IntegerRectangle rectangle)
        {
            _mRectangleStack.Add(rectangle);
        }

        private SortableSize AllocateSize(int width, int height, string id)
        {
            if (_mSortableSizeStack.Count > 0)
            {
                var size = _mSortableSizeStack.Pop();
                size.Width = width;
                size.Height = height;
                size.ID = id;

                return size;
            }

            return new SortableSize(width, height, id);
        }

        private void FreeSize(SortableSize size)
        {
            _mSortableSizeStack.Add(size);
        }
    }

    internal static class ListExtension
    {
        public static T Pop<T>(this List<T> list)
        {
            var index = list.Count - 1;

            var r = list[index];
            list.RemoveAt(index);
            return r;
        }
    }
}