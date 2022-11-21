using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using System;
using System.IO;

namespace AvaloniaApplication14.Controls
{
    public class BlurryImage : Image
    {
        Rect srcRect;
        Rect dstRect;
        MemoryStream stream = new();

        static BlurryImage()
        {
            AffectsRender<BlurryImage>(BlurLevelProperty, SourceProperty, StretchDirectionProperty, StretchProperty);
            AffectsMeasure<BlurryImage>(BlurLevelProperty, SourceProperty, StretchDirectionProperty, StretchProperty);
            AffectsArrange<BlurryImage>(BlurLevelProperty, SourceProperty, StretchDirectionProperty, StretchProperty);

            ClipToBoundsProperty.OverrideDefaultValue<BlurryImage>(true);
        }

        public BlurryImage()
        {
            BoundsProperty.Changed.Subscribe(BoundsChanged);
            SourceProperty.Changed.Subscribe(SourceChanged);
        }

        void SourceChanged(object obj)
        {
            if (Source is not null&& Source is IBitmap bitm)
            {
                bitm.Save(stream);
            }
        }

        void BoundsChanged(object @obj)
        {
            if (Bounds.Width != 0 && Bounds.Height != 0)
            {
                Rect viewPort = new Rect(Bounds.Size);
                Size sourceSize = Source.Size;

                Vector scale = Stretch.CalculateScaling(Bounds.Size, sourceSize, StretchDirection);
                Size scaledSize = sourceSize * scale;
                dstRect = viewPort
                    .CenterRect(new Rect(scaledSize))
                    .Intersect(viewPort);
                srcRect = new Rect(sourceSize)
                    .CenterRect(new Rect(dstRect.Size / scale));
            }
        }

        public override void Render(DrawingContext context)
        {
            context.Custom(new BlurImageRender(stream, dstRect, srcRect, BlurLevel, BlurLevel));
        }

        ///<inheritdoc/>
        protected override Size MeasureOverride(Size availableSize)
        {
            var source = Source;
            var result = new Size();

            if (source != null)
            {
                result = Stretch.CalculateSize(availableSize, source.Size, StretchDirection);
            }

            return result;
        }

        /// <inheritdoc/>
        protected override Size ArrangeOverride(Size finalSize)
        {
            var source = Source;

            if (source != null)
            {
                var sourceSize = source.Size;
                var result = Stretch.CalculateSize(finalSize, sourceSize);
                return result;
            }
            else
            {
                return new Size();
            }
        }

        public float BlurLevel
        {
            get => GetValue(BlurLevelProperty);
            set => SetValue(BlurLevelProperty, value);
        }

        public readonly static StyledProperty<float> BlurLevelProperty =
            AvaloniaProperty.Register<BlurryImage, float>(nameof(BlurLevel), 16);
    }
}