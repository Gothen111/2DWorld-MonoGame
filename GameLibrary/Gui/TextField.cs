#region Using Statements Standard
using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.Runtime.Serialization;
#endregion

#region Using Statements Class Specific
using Input.Keyboard;
using Input.Mouse;
using Input.Mouse.MouseEnum;
#endregion

namespace GameLibrary.Gui
{
    public enum TextAlign
    {
        Left,
        Center,
        Right
    }

    public class TextField : DragAndDrop
    {
        private String text;

        public String Text
        {
            get { return text; }
            set { text = value; }
        }

        private Color foreGroundColor;

        public Color ForeGroundColor
        {
            get { return foreGroundColor; }
            set { foreGroundColor = value; }
        }

        private TextAlign textAlign;

        public TextAlign TextAlign
        {
            get { return textAlign; }
            set { textAlign = value; }
        }

		private bool isTextEditAble;

		public bool IsTextEditAble
		{
			get { return isTextEditAble; }
			set { isTextEditAble = value; }
		}

        public TextField()
            : base()
        {
            this.text = "";
            foreGroundColor = Color.Black;
            this.textAlign = TextAlign.Left;
			this.BackgroundGraphicPath = "Gui/TextField";
			this.isTextEditAble = true;
        }

        public TextField(Rectangle _Bounds)
            : base(_Bounds)
        {
            this.text = "";
            foreGroundColor = Color.Black;
            this.textAlign = TextAlign.Left;
			this.BackgroundGraphicPath = "Gui/TextField";
			this.isTextEditAble = true;
        }

        public override void keyboardButtonClicked(Microsoft.Xna.Framework.Input.Keys buttonPressed)
        {
            base.keyboardButtonClicked(buttonPressed);
			if(this.isTextEditAble)
			{
	            if (this.IsFocused)
	            {
	                if (buttonPressed == Microsoft.Xna.Framework.Input.Keys.Back)
	                {
	                    if(this.text.Length > 0)
	                        this.text = this.text.Substring(0, this.text.Length - 1);
	                }
	                else if (buttonPressed == Microsoft.Xna.Framework.Input.Keys.LeftShift || buttonPressed == Microsoft.Xna.Framework.Input.Keys.LeftAlt)
	                {

	                }
	                else
	                {
	                    this.text += KeyboardManager.TryConvertKey(buttonPressed);
	                }
	            }
			}
        }

        public override void draw(GraphicsDevice _GraphicsDevice, SpriteBatch _SpriteBatch)
        {
            this.draw(_GraphicsDevice, _SpriteBatch, Vector2.Zero);
        }

        public virtual void draw(GraphicsDevice _GraphicsDevice, SpriteBatch _SpriteBatch, Vector2 _TextShiftPosition)
        {
            base.draw(_GraphicsDevice, _SpriteBatch);
            SpriteFont font = Ressourcen.RessourcenManager.ressourcenManager.Fonts["Arial"];
            if (this.textAlign == TextAlign.Left)
            {
                _SpriteBatch.DrawString(font, this.text, (new Vector2(this.Bounds.X + 20, this.Bounds.Y + this.Bounds.Height / 3) + _TextShiftPosition), this.foreGroundColor);
            }
            else if (this.textAlign == TextAlign.Center)
            {
                _SpriteBatch.DrawString(font, this.text, (new Vector2(this.Bounds.X + this.Bounds.Width / 2 - font.MeasureString(this.text).X / 2, this.Bounds.Y + this.Bounds.Height / 3) + _TextShiftPosition), this.foreGroundColor);
            }
            else if (this.textAlign == TextAlign.Right)
            {
                _SpriteBatch.DrawString(font, this.text, (new Vector2(this.Bounds.X + this.Bounds.Width - 20 - font.MeasureString(this.text).X, this.Bounds.Y + this.Bounds.Height / 3) + _TextShiftPosition), this.foreGroundColor);
            }
        }
    }
}
