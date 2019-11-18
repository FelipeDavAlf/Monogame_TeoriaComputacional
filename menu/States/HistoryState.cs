using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using menu.Controls;
using Microsoft.Xna.Framework.Media;

namespace menu.States
{
    public class HistoryState : State{

        private List<Component> _components;
        private Song musica_fondo;
        private Texture2D _background;
        private GraphicsDevice _graphics;
        private SpriteFont _font;
        private String Texto="Por favor jueguito escribe esto :,(";

        public HistoryState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content){
            MediaPlayer.Stop();
            musica_fondo = content.Load<Song>("Music/DanceOfPales");
            MediaPlayer.Play(musica_fondo);
            _graphics = graphicsDevice;
            _background = content.Load<Texture2D>("Background/marco");

            var buttonTexture = _content.Load<Texture2D>("Controls/Button");//consige la textura del boton

            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");//consige el tipo de letra para el boton

            _font = _content.Load<SpriteFont>("Fonts/History");

            var RegresarButton = new Button(buttonTexture, buttonFont)
            {//crea un boton para un nuevo juego
                Position = new Vector2(100, 450),
                Text = "Regresar",
            };
            RegresarButton.Click += Regresar_Click; ;// si se activa el evento Clic manda llmar esa funcion de juego nuevo
            var JugarButton = new Button(buttonTexture, buttonFont)//crea un boton para salir
            {
                Position = new Vector2(700, 450),
                Text = "Jugar",

            };

            JugarButton.Click += JugarButton_Click; ; ;//si se activa el evento Clic se sale del juego

            _components = new List<Component>(){
                RegresarButton,
                JugarButton,
            };
        
    }

        

        private void JugarButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Regresar_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MenuState(_game, _graph, _content));//llama a Chancestate de Game1
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch){
            spriteBatch.Begin();
            spriteBatch.Draw(_background, _graphics.Viewport.Bounds, Color.White);
            foreach (var component in _components)      //imprime los botones
                component.Draw(gameTime, spriteBatch);

            spriteBatch.DrawString(_font, Texto, new Vector2((_graphics.Viewport.Bounds.Width /3) - (Texto.Length /2), _graphics.Viewport.Bounds.Height / 3), Color.White);
            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime){
            
        }

        public override void Update(GameTime gameTime){
            foreach (var component in _components)//actualiza si se a presionado o no el boton
                component.Update(gameTime);
        }
    }
} 
