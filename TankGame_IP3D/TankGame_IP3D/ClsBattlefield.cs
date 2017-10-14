using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace TankGame_IP3D
{
    class ClsBattlefield
    {
        VertexPositionNormalTexture[] vertices;
        VertexPositionNormalTexture v;
        int vertexCount;
        VertexBuffer vertexBuffer;

        int indexCount;
        short[] indices;
        IndexBuffer indexBuffer;

        BasicEffect effect;
        Matrix matrixTerreno = Matrix.Identity;
        Texture2D alturas;

        Color c;
        float y;
        float escala = 0.05f;
        float[] alturasTextura;

        public ClsBattlefield(GraphicsDevice device, ContentManager content)
        {
            alturas = content.Load<Texture2D>("heightmap");

            effect = new BasicEffect(device);
            float aspectRatio = (float)device.Viewport.Width / device.Viewport.Height;
            effect.View = Matrix.CreateLookAt(new Vector3(0.5f, 2.0f, 2.0f), Vector3.Zero, Vector3.Up);
            effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f), aspectRatio, 1.0f, 10.0f);
            effect.TextureEnabled = true;
            effect.Texture = alturas;
            effect.VertexColorEnabled = true;
            effect.LightingEnabled = false;
            CreateGeometry(device);
        }

       public void CreateGeometry(GraphicsDevice device)
        {
            Color[] texels = new Color[alturas.Height * alturas.Width];
            alturas.GetData<Color>(texels);
            alturasTextura = new float[alturas.Height * alturas.Width];
            vertexCount = alturas.Width * alturas.Height;
            vertices = new VertexPositionNormalTexture[vertexCount];

            for (int x = 0; x < alturas.Width; x++)
            {
                for (int z = 0; z < alturas.Height; z++)
                {
                   Color c = texels[z * alturas.Width + x];
                    float y = c.R * escala;
                    v = new VertexPositionNormalTexture(new Vector3(x, y, z), Vector3.Up, new Vector2(x % 2, z % 2));
                    vertices[z * alturas.Width + x] = v;
                    alturasTextura[z * alturas.Width + x] = y;
                }
            }
            vertexBuffer = new VertexBuffer(device, typeof(VertexPositionNormalTexture), vertices.Length, BufferUsage.None);
            vertexBuffer.SetData<VertexPositionNormalTexture>(vertices);

            //Usaremos strips verticais
            indexCount = alturas.Height * 2 * (alturas.Width - 1);
            indices = new short[indexCount];
            for (int ix = 0; ix < alturas.Width - 1; ix++)
            {
                for (int iz = 0; iz < alturas.Height; iz++)
                {
                    indices[2 * iz + 0 + ix * 2 * alturas.Height] = (short)(iz * alturas.Width + ix);
                    indices[2 * iz + 1 + ix * 2 * alturas.Height] = (short)(iz * alturas.Width + 1 + ix);
                }
            }

            indexBuffer = new IndexBuffer(device, typeof(short), indices.Length, BufferUsage.None);
            indexBuffer.SetData<short>(indices);
        }
        public void Draw(GraphicsDevice device)
        {
            effect.World = matrixTerreno;
            effect.CurrentTechnique.Passes[0].Apply();
            device.SetVertexBuffer(vertexBuffer);
            device.Indices = indexBuffer;

            for (int ix = 0; ix < (alturas.Width-1); ix++)
            {
                device.DrawIndexedPrimitives(PrimitiveType.TriangleStrip, 0, ix * alturas.Height * 2, 2 * alturas.Height - 2); //tenta ver este ciclo
            }
        }
    }
}