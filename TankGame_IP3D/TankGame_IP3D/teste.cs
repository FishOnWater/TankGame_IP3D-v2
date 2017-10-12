using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame_IP3D
{
    class teste
    {
        VertexPositionColor[] vertices;
        BasicEffect effect;
        Matrix worldMatrix = Matrix.Identity;

        public teste(GraphicsDevice device)
        {

            effect = new BasicEffect(device);
            float aspectRatio = (float)device.Viewport.Width / device.Viewport.Height;
            effect.View = Matrix.CreateLookAt(new Vector3(2.0f, 2.0f, 2.0f), Vector3.Zero, Vector3.Up);
            effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f), aspectRatio, 1.0f, 10.0f);
            effect.LightingEnabled = false;
            effect.VertexColorEnabled = true;
            CreateGeometry();
        }

        private void CreateGeometry()
        {
            float SquareSide = 1f; 
            int vertexCount = 6;
            vertices = new VertexPositionColor[vertexCount];
            vertices[0] = new VertexPositionColor(new Vector3(-SquareSide, 0.0f, -SquareSide), Color.Red);
            vertices[1] = new VertexPositionColor(new Vector3(+SquareSide, 0.0f, -SquareSide), Color.Red);
            vertices[2] = new VertexPositionColor(new Vector3(-SquareSide, 0.0f, +SquareSide), Color.Red);
            vertices[3] = new VertexPositionColor(new Vector3(+SquareSide, 0.0f, +SquareSide), Color.Red);
            vertices[4] = new VertexPositionColor(new Vector3(-SquareSide, 0.0f, +SquareSide), Color.Red);
            vertices[5] = new VertexPositionColor(new Vector3(+SquareSide, 0.0f, -SquareSide), Color.Red);
        }

        public void Draw(GraphicsDevice device)
        {
            effect.World = worldMatrix;
            effect.CurrentTechnique.Passes[0].Apply();
            device.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, vertices, 0, 2);
        }
    }
}
