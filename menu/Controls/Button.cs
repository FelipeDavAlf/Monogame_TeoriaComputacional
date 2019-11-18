using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace menu.Controls
{
    public class Button : Component {
        #region Fields

        private MouseState _currentMouse;
        private SpriteFont _font;
        private bool _isMoving;
        private MouseState _previousMouse;
        private Texture2D _texture;

        #endregion

        #region Propierties

        public event EventHandler Click;
        public bool clicked { get; private set; }
        public float Layer { get; set; }
        public Vector2 Origin{
            get{
                return new Vector2(_texture.Width/2, _texture.Height / 2);
            }
        }
        public Color PenColor { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle Rectangle {
            get {
                return new Rectangle((int)Position.X-(int)Origin.X, (int)Position.Y - (int)Origin.Y, _texture.Width, _texture.Height);
            }
        }
        public string Text { get; set; }

        #endregion

        #region Metodos
        public Button(Texture2D texture,SpriteFont font){//marca la imagen, el tipo de letra y el color del texto del boton
            _texture = texture;
            _font = font;
            PenColor = Color.Black; 
        }

        public override void Draw(GameTime gameTime, SpriteBatch spritBatch){
            var colour = Color.White;//color predefinido del fondo del boton

            if (_isMoving) {//si el mouse se mueve dentro del boton cambia el color
                colour = Color.Black;
                PenColor = Color.White;
            }
            spritBatch.Draw(_texture,Position,null,colour,0f,Origin,1f,SpriteEffects.None,Layer);//imprime el rectangulo

            if (!String.IsNullOrEmpty(Text)) {//si la cadena dentro del boton no es nula, acomoda el  texto en el centro
                var x = (Rectangle.X + (Rectangle.Width / 2)) - (_font.MeasureString(Text).X / 2);
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - (_font.MeasureString(Text).Y / 2);
                
                spritBatch.DrawString(_font, Text, new Vector2(x,y), PenColor, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, Layer+0.01f);//imprime la cadena
            }
        }

        public override void Update(GameTime gameTime){
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y,1,1);//crea un rectangulo en la posicion del mouse

            _isMoving = false;  //variable que captura si no se mueve el mouse dentro del boton
            PenColor = Color.Black;

            if (mouseRectangle.Intersects(Rectangle)) {//si el mouse entra en contacto con algun boton
                _isMoving = true;
                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                    Click?.Invoke(this, new EventArgs());//si se hizo un click se activa un evento
            }

        }
        #endregion
    }
}
