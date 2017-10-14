using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame_IP3D
{
    class Camera
    {
        Vector3 posicao = new Vector3(2.0f, 2.0f, 2.0f);
        Vector3 camera = new Vector3(0.0f, 0.0f, 0.0f);
        Vector3 direcao;
        Matrix cameraMatrix;
        BasicEffect effect;
        float speed = 0.1f;
        KeyboardState keyboardState = Keyboard.GetState();

        public Camera(GraphicsDevice device)
        {
            UpdateCameraPosition(device);

            effect = new BasicEffect(device);
            cameraMatrix = Matrix.Identity;
            float aspectRatio = (float)device.Viewport.Width / device.Viewport.Height;
            effect.View = Matrix.CreateLookAt(posicao, camera, Vector3.Up);
            effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f), aspectRatio, 1.0f, 100.0f);
        }

        public void UpdateCameraPosition(GraphicsDevice device)
        {
            direcao = posicao - camera;
            if (keyboardState.IsKeyDown(Keys.Up))
                posicao = (posicao + direcao) * speed;
            if (keyboardState.IsKeyDown(Keys.Down))
                posicao = (posicao - direcao) *speed;
            if (keyboardState.IsKeyDown(Keys.Right))
                posicao = posicao + direcao;
            if (keyboardState.IsKeyDown(Keys.Left))
                posicao = posicao + direcao;
            direcao = new Vector3(0.0f, 0.0f, 0.0f);
        }
    }
}
