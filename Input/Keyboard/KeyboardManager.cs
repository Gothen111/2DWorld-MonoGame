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
#endregion

namespace Input.Keyboard
{
    public class KeyboardManager
    {
        public static KeyboardManager keyboardManager = new KeyboardManager();

        public static List<KeyboardListener> keyboardFocus = new List<KeyboardListener>();

        public static bool shiftPressed = false;

        public static bool altPressed = false;

        private List<Keys> keysPressed;

        private KeyboardManager()
        {
            keysPressed = new List<Keys>();
        }

        public void update()
        {
            foreach (Keys key in Microsoft.Xna.Framework.Input.Keyboard.GetState().GetPressedKeys())
            {
                if (!keysPressed.Contains(key))
                {
                    keysPressed.Add(key);
                    notifyKeyboardFocusAboutClickEvent(key);
                }
            }

            List<Keys> keysToRemove = new List<Keys>();
            foreach (Keys key in keysPressed)
            {
                if (!Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(key))
                {
                    keysToRemove.Add(key);
                }
            }

            foreach (Keys key in keysToRemove)
            {
                keysPressed.Remove(key);
                notifyKeyboardFocusAboutReleaseEvent(key);
            }
        }

        private void notifyKeyboardFocusAboutClickEvent(Keys key)
        {
            if (key == Keys.LeftShift)
                KeyboardManager.shiftPressed = true;
            if (key == Keys.LeftAlt)
                KeyboardManager.altPressed = true;
            foreach (KeyboardListener listener in keyboardFocus)
            {
                listener.keyboardButtonClicked(key);
            }
        }

        private void notifyKeyboardFocusAboutReleaseEvent(Keys key)
        {
            if (key == Keys.LeftShift)
                KeyboardManager.shiftPressed = false;
            if (key == Keys.LeftAlt)
                KeyboardManager.altPressed = false;
            foreach (KeyboardListener listener in keyboardFocus)
            {
                listener.keyboardButtonReleased(key);
            }
        }

        public static char TryConvertKey(Keys keys)
        {
            char key = char.MinValue;
            Boolean shift = KeyboardManager.shiftPressed;
            switch (keys)
            {
                    //Alphabet keys
                    case Keys.A: if (shift) { key = 'A'; } else { key = 'a'; } break;
                    case Keys.B: if (shift) { key = 'B'; } else { key = 'b'; } break;
                    case Keys.C: if (shift) { key = 'C'; } else { key = 'c'; } break;
                    case Keys.D: if (shift) { key = 'D'; } else { key = 'd'; } break;
                    case Keys.E: if (shift) { key = 'E'; } else { key = 'e'; } break;
                    case Keys.F: if (shift) { key = 'F'; } else { key = 'f'; } break;
                    case Keys.G: if (shift) { key = 'G'; } else { key = 'g'; } break;
                    case Keys.H: if (shift) { key = 'H'; } else { key = 'h'; } break;
                    case Keys.I: if (shift) { key = 'I'; } else { key = 'i'; } break;
                    case Keys.J: if (shift) { key = 'J'; } else { key = 'j'; } break;
                    case Keys.K: if (shift) { key = 'K'; } else { key = 'k'; } break;
                    case Keys.L: if (shift) { key = 'L'; } else { key = 'l'; } break;
                    case Keys.M: if (shift) { key = 'M'; } else { key = 'm'; } break;
                    case Keys.N: if (shift) { key = 'N'; } else { key = 'n'; } break;
                    case Keys.O: if (shift) { key = 'O'; } else { key = 'o'; } break;
                    case Keys.P: if (shift) { key = 'P'; } else { key = 'p'; } break;
                    case Keys.Q: if (shift) { key = 'Q'; } else { key = 'q'; } break;
                    case Keys.R: if (shift) { key = 'R'; } else { key = 'r'; } break;
                    case Keys.S: if (shift) { key = 'S'; } else { key = 's'; } break;
                    case Keys.T: if (shift) { key = 'T'; } else { key = 't'; } break;
                    case Keys.U: if (shift) { key = 'U'; } else { key = 'u'; } break;
                    case Keys.V: if (shift) { key = 'V'; } else { key = 'v'; } break;
                    case Keys.W: if (shift) { key = 'W'; } else { key = 'w'; } break;
                    case Keys.X: if (shift) { key = 'X'; } else { key = 'x'; } break;
                    case Keys.Y: if (shift) { key = 'Y'; } else { key = 'y'; } break;
                    case Keys.Z: if (shift) { key = 'Z'; } else { key = 'z'; } break;

                    //Decimal keys
                    case Keys.D0: if (shift) { key = '='; } else { key = '0'; } break;
                    case Keys.D1: if (shift) { key = '!'; } else { key = '1'; } break;
                    case Keys.D2: if (shift) { key = '@'; } else { key = '2'; } break;
                    case Keys.D3: if (shift) { key = '#'; } else { key = '3'; } break;
                    case Keys.D4: if (shift) { key = '$'; } else { key = '4'; } break;
                    case Keys.D5: if (shift) { key = '%'; } else { key = '5'; } break;
                    case Keys.D6: if (shift) { key = '&'; } else { key = '6'; } break;
                    case Keys.D7: if (shift) { key = '/'; } else { key = '7'; } break;
                    case Keys.D8: if (shift) { key = '('; } else { key = '8'; } break;
                    case Keys.D9: if (shift) { key = ')'; } else { key = '9'; } break;

                    //Decimal numpad keys
                    case Keys.NumPad0: key = '0'; break;
                    case Keys.NumPad1: key = '1'; break;
                    case Keys.NumPad2: key = '2'; break;
                    case Keys.NumPad3: key = '3'; break;
                    case Keys.NumPad4: key = '4'; break;
                    case Keys.NumPad5: key = '5'; break;
                    case Keys.NumPad6: key = '6'; break;
                    case Keys.NumPad7: key = '7'; break;
                    case Keys.NumPad8: key = '8'; break;
                    case Keys.NumPad9: key = '9'; break;

                    //Special keys
                    case Keys.OemTilde: if (shift) { key = '~'; } else { key = '`'; } break;
                    case Keys.OemSemicolon: if (shift) { key = ':'; } else { key = ';'; } break;
                    case Keys.OemQuotes: if (shift) { key = '"'; } else { key = '\''; } break;
                    case Keys.OemQuestion: if (shift) { key = '?'; } else { key = 'ß'; } break;
                    case Keys.OemPlus: if (shift) { key = '+'; } else { key = '='; } break;
                    case Keys.OemPipe: if (shift) { key = '|'; } else { key = '\\'; } break;
                    case Keys.OemPeriod: if (shift) { key = ':'; } else { key = '.'; } break;
                    case Keys.OemOpenBrackets: if (shift) { key = '{'; } else { key = '['; } break;
                    case Keys.OemCloseBrackets: if (shift) { key = '}'; } else { key = ']'; } break;
                    case Keys.OemMinus: if (shift) { key = '_'; } else { key = '-'; } break;
                    case Keys.OemComma: if (shift) { key = ';'; } else { key = ','; } break;
                    case Keys.Space: key = ' '; break;
                }
                return key;
            }

        }
}
