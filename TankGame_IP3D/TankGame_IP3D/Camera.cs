﻿using Microsoft.Xna.Framework;
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
        Vector3 posicao;
        float alturaCam = 5.0f;
        Vector3 direcao;
        Vector3 directionBase = Vector3.UnitX;
        public Matrix view;
        Vector3 speed = new Vector3(1.0f, 0.0f, 0.0f);
        KeyboardState keyboardState = Keyboard.GetState();
        float yaw = 0.01f;
        float pitch = 0.01f;
        Matrix Projection;

        public Camera(GraphicsDevice device)
        {
            posicao = new Vector3(64.0f, alturaCam, 64.0f);
            float aspectRatio = (float)device.Viewport.Width / device.Viewport.Height;
            view = Matrix.CreateLookAt(posicao, direcao, Vector3.Up);
            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f), aspectRatio, 1.0f, 100.0f);
        }

        public void UpdateCameraPosition(ClsBattlefield terreno)
        {
            MouseState mousestate = Mouse.GetState();

            pitch = MathHelper.ToRadians(mousestate.Y * 0.1f);
            Matrix pitchRotation = Matrix.CreateFromYawPitchRoll(yaw, pitch, 0.0f);

            if (keyboardState.IsKeyDown(Keys.NumPad4))
                speed = Vector3.Transform(speed, Matrix.CreateRotationY(yaw));
            if (keyboardState.IsKeyDown(Keys.NumPad6))
                speed = Vector3.Transform(speed, Matrix.CreateRotationY(-yaw));

            Matrix yawRotation = Matrix.CreateRotationY(yaw);
            Vector3 dir = speed;
            dir.Normalize();
            yawRotation.Forward = dir;
            yawRotation.Up = Vector3.UnitY;
            yawRotation.Right = Vector3.Cross(dir, Vector3.UnitY);
            view = yawRotation * Matrix.CreateTranslation(posicao);
            direcao = Vector3.Transform(directionBase, yawRotation);

            if (keyboardState.IsKeyDown(Keys.NumPad8))
            {
                posicao = posicao + speed;
                alturaCam = terreno.Interpolacao(posicao.X, posicao.Z);
            }
            if (keyboardState.IsKeyDown(Keys.NumPad2))
            {
                posicao = posicao - speed;
                alturaCam = terreno.Interpolacao(posicao.X, posicao.Z);
            }

            view = Matrix.CreateLookAt(posicao, direcao, Vector3.Up) * pitchRotation;
        }

    }
}
