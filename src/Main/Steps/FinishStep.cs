using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace WizardBase
{
    [ToolboxItem(false), Designer(typeof (WizardStepDesigner)), DefaultEvent("Click")]
    public class FinishStep : WizardStep
    {
      private ColorPair pair = new ColorPair();
      private string subtitle = "The wizard in now finished";
      TextAppearence subtitleAppearence = new TextAppearence();
      private string title = "Finish";
      private TextAppearence titleAppearence = new TextAppearence();
      private ColorPair headerPair = new ColorPair();
      private Image bindingImage = Tamu.GeoInnovation.BuildTools.WizardBase.Properties.Resources.Top;

        public FinishStep()
        {
#pragma warning disable DoNotCallOverridableMethodsInConstructor
            Reset();
#pragma warning restore DoNotCallOverridableMethodsInConstructor
        }

        internal override void Reset()
        {
          ResetTitleAppearence();
          ResetSubtitleAppearence();
            BackColor = SystemColors.ControlLightLight;
            BindingImage = Tamu.GeoInnovation.BuildTools.WizardBase.Properties.Resources.back;
            BackgroundImageLayout = ImageLayout.Tile;
        }
        protected virtual void ResetTitleAppearence()
        {
          titleAppearence = new TextAppearence();
        }

        protected virtual void ResetSubtitleAppearence()
        {
          subtitleAppearence = new TextAppearence();
          subtitleAppearence.Font = new Font("Microsoft Sans", 8.25f, GraphicsUnit.Point);
        }

        protected virtual Rectangle HeaderRectangle
        {
          get { return new Rectangle(0, 0, Width, Height); }
        }

        ///<summary>
        ///Raises the <see cref="E:System.Windows.Forms.Control.Paint"></see> event.
        ///</summary>
        ///
        ///<param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs"></see> that contains the event data. </param>
        protected override void OnPaint(PaintEventArgs e)
        {
          base.OnPaint(e);
          Graphics graphics = e.Graphics;
          Rectangle rect = HeaderRectangle;
          Rectangle rectangle;
          RectangleF titleRect;
          RectangleF subtitleRect;
          GetTextBounds(out titleRect, out subtitleRect);
          if (bindingImage != null)
          {
            graphics.DrawImage(bindingImage, rect);
            rectangle = new Rectangle(rect.Left, rect.Bottom, rect.Width, 2);
            ControlPaint.DrawBorder3D(graphics, rectangle);
          }
          else
          {
            using (Brush brush = new LinearGradientBrush(rect, headerPair.BackColor1, headerPair.BackColor2, headerPair.Gradient))
            {
              graphics.FillRectangle(brush, rect);
              rectangle = new Rectangle(rect.Left, rect.Bottom, rect.Width, 2);
              ControlPaint.DrawBorder3D(graphics, rectangle);
            }
          }
          DrawText(graphics, titleRect, title, titleAppearence);
          DrawText(graphics, subtitleRect, subtitle, subtitleAppearence);
        }

        ///<summary>
        ///Raises the <see cref="E:System.Windows.Forms.Control.Paint"></see> event.
        ///</summary>
        ///
        ///<param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs"></see> that contains the event data. </param>
        /*protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (Brush brush = new LinearGradientBrush(ClientRectangle, pair.BackColor1, pair.BackColor2, pair.Gradient))
            {
                e.Graphics.FillRectangle(brush, ClientRectangle);
            }
        }*/

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Image BackgroundImage
        {
            get
            {
                return base.BackgroundImage;
            }
            set
            {
                base.BackgroundImage = value;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
            }
        }

        [Description("Backgroung of the finish step."), Category("Appearance")]
        public Image BindingImage
        {
          get { return bindingImage; }
          set
          {
            if (value != bindingImage)
            {
              bindingImage = value;
              Invalidate();
              OnBindingImageChanged();
            }
          }
            //get { return base.BackgroundImage; }
            //set
            //{
            //    if (value != base.BackgroundImage)
            //    {
            //        base.BackgroundImage = value;
            //        Invalidate();
            //        OnBindingImageChanged();
            //    }
            //}
        }
        [Description("Appearence of body."), Category("Appearance")]
        public ColorPair Pair
        {
            get { return pair; }
            set
            {
                if (pair != value)
                {
                    pair = value;
                    Invalidate();
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; }
        }

        [Description("The title of the step."), DefaultValue("New Wizard step."), Editor(typeof(MultilineStringEditor), typeof(UITypeEditor)), Category("Appearance")]
        public string Title
        {
          get { return title; }
          set
          {
            if (!string.IsNullOrEmpty(title) && value != title)
            {
              Region refreshRegion = GetTextBounds();
              title = value;
              refreshRegion.Union(GetTextBounds());
              Invalidate(refreshRegion);
            }
          }
        }

        [Description("The title text appearence of step."), Category("Appearance")]
        public TextAppearence TitleAppearence
        {
          get { return titleAppearence; }
          set
          {
            if (titleAppearence != value)
            {
              titleAppearence = value;
              Invalidate();
            }
          }
        }

        [Description("The sub title appearence of step."), Category("Appearance")]
        public TextAppearence SubtitleAppearence
        {
          get { return subtitleAppearence; }
          set
          {
            if (subtitleAppearence != value)
            {
              subtitleAppearence = value;
              Invalidate();
            }
          }
        }

        [Category("Appearance"), DefaultValue("Description for the new step."), Description("The subtitle of the step."), Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public string Subtitle
        {
          get { return subtitle; }
          set
          {
            if (!string.IsNullOrEmpty(subtitle) && value != subtitle)
            {
              Region refreshRegion = GetTextBounds();
              subtitle = value;
              refreshRegion.Union(GetTextBounds());
              Invalidate(refreshRegion);
            }
          }
        }

        protected virtual void GetTextBounds(out RectangleF titleRect, out RectangleF subtitleRect, Graphics graphics)
        {
            StringFormat format = new StringFormat(StringFormatFlags.FitBlackBox);
            format.Trimming = StringTrimming.EllipsisCharacter;
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            format.Trimming = StringTrimming.None;
            SizeF sz = graphics.MeasureString(Title, titleAppearence.Font, Width, format);
            titleRect = new RectangleF(subtitleAppearence.Font.SizeInPoints, subtitleAppearence.Font.SizeInPoints, sz.Width, sz.Height);
            SizeF sz1 = graphics.MeasureString(Subtitle, subtitleAppearence.Font, Width, format);
            subtitleRect = new RectangleF(2 * subtitleAppearence.Font.SizeInPoints, titleRect.Height + subtitleAppearence.Font.SizeInPoints, sz1.Width, sz1.Height);
        }

        protected void GetTextBounds(out RectangleF titleRect, out RectangleF subtitleRect)
        {
            Graphics graphics = CreateGraphics();
            try
            {
                GetTextBounds(out titleRect, out subtitleRect, graphics);
            }
            finally
            {
                if (graphics != null)
                {
                    graphics.Dispose();
                }
            }
        }

        protected Region GetTextBounds()
        {
            RectangleF titleRect;
            RectangleF subtitleRect;
            GetTextBounds(out titleRect, out subtitleRect);
            return GetTextBounds(titleRect, subtitleRect);
        }

        protected Region GetTextBounds(RectangleF titleRect, RectangleF subtitleRectangle)
        {
            if (!titleRect.IsEmpty)
            {
                if (!subtitleRectangle.IsEmpty)
                {
                    return new Region(new RectangleF(6f, Width - 12, (Width - 66), (6f + titleRect.Height) + subtitleRectangle.Height));
                }
                else
                {
                    return new Region(titleRect);
                }
            }
            else
            {
                if (!subtitleRectangle.IsEmpty)
                {
                    return new Region(subtitleRectangle);
                }
                return new Region(RectangleF.Empty);
            }
        }

        public bool ShouldSerializeBindingImage()
        {
            return BindingImage != Tamu.GeoInnovation.BuildTools.WizardBase.Properties.Resources.back;
        }

        public void ResetBindingImage()
        {
            BindingImage = Tamu.GeoInnovation.BuildTools.WizardBase.Properties.Resources.back; ;
        }

        public bool ShouldSerializePair()
        {
            return pair != new ColorPair();
        }

        public void ResetPair()
        {
            pair = new ColorPair();
        }
    }
}