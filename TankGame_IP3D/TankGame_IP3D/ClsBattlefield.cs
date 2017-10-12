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
        int vertexCount;
        VertexBuffer vertexBuffer;

        int indexCount;
        short[] indices;
        IndexBuffer indexBuffer;


        BasicEffect effect;
        Matrix matrixTerreno=Matrix.Identity;
        Texture2D alturas;

        Color c;
        float y;

        public ClsBattlefield(GraphicsDevice device, ContentManager content)
        {
            float escala = 0.01f;
            alturas = content.Load<Texture2D>("heightmap");
            int h = alturas.Height;
            int w = alturas.Width;
            Color[] texels = new Color[h*w];
            alturas.GetData<Color>(texels);

            effect = new BasicEffect(device);
            float aspectRatio = (float)device.Viewport.Width / device.Viewport.Height;
            effect.View = Matrix.CreateLookAt(new Vector3(0.5f, 2.0f, 2.0f), Vector3.Zero, Vector3.Up);
            effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f), aspectRatio, 1.0f, 10.0f);
            effect.TextureEnabled = true;
            effect.Texture = alturas;
            effect.VertexColorEnabled = true;
            effect.LightingEnabled = false;
            CreateGeometry(h, w, texels, escala, device);
        }

        public void CreateGeometry(int h, int w, Color[] texels, float escala, GraphicsDevice device)
        {
            vertexCount = texels.Length;
            vertices = new VertexPositionNormalTexture[vertexCount];

            for(int x=0; x<w; x++)
            {
                for (int z = 0; z<h; z++)
                {
                    c = texels[z * w + x];
                    y = c.R * escala;
                    vertices[z * w + x] = new VertexPositionNormalTexture(new Vector3(x, y, z), Vector3.Up, new Vector2(x % 2, z % 2));
                }
            }

            vertexBuffer = new VertexBuffer(device, typeof(VertexPositionNormalTexture), vertices.Length, BufferUsage.None);
            vertexBuffer.SetData<VertexPositionNormalTexture>(vertices);

            //Usaremos strips verticais
            indexCount = h * 2 * (w - 1);
            indices = new short[indexCount];
            for(int ix=0; ix < w - 1; ix++)
            {
                for(int iz=0; iz<h; iz++)
                {
                    indices[2 * iz + 0 + ix * 2 * h] = (short)(iz * w + ix);
                    indices[2 * iz + 1 + ix * 2 * h] = (short)(iz * w + 1 + ix);
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
            device.DrawIndexedPrimitives(PrimitiveType.TriangleStrip, 0, 0, vertexCount, 0, indexCount - 2);
        }
    }
}
