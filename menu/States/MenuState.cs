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

namespace menu.States{
    public class MenuState : State {
    
        private List<Component> _components;
        private Song musica_fondo;
        private Texture2D _background;
        private GraphicsDevice _graphics;
        private Texture2D _logo;
        public MenuState (Game1 game, GraphicsDevice graphicsDevice, ContentManager content):base(game, graphicsDevice, content){
           _graphics  = graphicsDevice;
            _background = content.Load<Texture2D>("Background/castillo_pie");
            musica_fondo = content.Load<Song>("Music/TheTragicPrince");
            MediaPlayer.Play(musica_fondo);

            _logo = content.Load<Texture2D>("logo2");

            var buttonTexture = _content.Load<Texture2D>("Controls/Button");//consige la textura del boton

            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");//consige el tipo de letra para el boton

            var newGameButton = new Button(buttonTexture, buttonFont) {//crea un boton para un nuevo juego
                Position = new Vector2(300, 200),
                Text = "Nuevo juego",
            };
            newGameButton.Click += NewGameButton_Click;// si se activa el evento Clic manda llmar esa funcion de juego nuevo
            var quitGameButton = new Button(buttonTexture, buttonFont)//crea un boton para salir
            {
                Position = new Vector2(300, 300),
                Text = "Salir",
                
            };

            quitGameButton.Click += QuitGameButton_Click; ;//si se activa el evento Clic se sale del juego

            _components = new List<Component>(){
                newGameButton,
                quitGameButton,
            };
        }
        private void NewGameButton_Click(object sender, EventArgs e){//funcion para nuevo juego
            _game.ChangeState(new HistoryState(_game, _graph, _content));//llama a Chancestate de Game1
        }
        private void QuitGameButton_Click(object sender, EventArgs e){//funcion para salir del juego
            _game.Exit();
        }
       
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch){//
            spriteBatch.Begin();
            spriteBatch.Draw(_background, _graphics.Viewport.Bounds, Color.White);
            spriteBatch.Draw(_logo, new Vector2(225, 70), null, Color.White, 0f, new Vector2(0, 0), 0.3f, SpriteEffects.None, 0f);
            foreach (var component in _components)      //imprime los botones
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }
        public override void PostUpdate(GameTime gameTime){
            // remover los sprite si no son necesarios
             
        }
    
        public override void Update(GameTime gameTime){
            foreach (var component in _components)//actualiza si se a presionado o no el boton
                component.Update(gameTime);

        }
    }
}
