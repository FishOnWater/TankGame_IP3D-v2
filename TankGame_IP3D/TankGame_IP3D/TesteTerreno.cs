using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace TankGame_IP3D
{
    class TesteTerreno
    {

        BasicEffect effect;
        Matrix worldMatrix;

        IndexBuffer indexBuffer;
        VertexBuffer vertexBuffer;

        VertexPositionNormalTexture[] vertices;//Array de vertices
        VertexPositionNormalTexture v;

        Texture2D texturaAlturas;


        short[] indices;//Array de indices
        float escala = 0.05f; //Escala definida
        float[] texturaAlturasAlturas;//Array de alturas em cada vertice

        public TesteTerreno(GraphicsDevice device, ContentManager content)
        {
            worldMatrix = Matrix.Identity;
            effect = new BasicEffect(device);

            texturaAlturas = content.Load<Texture2D>("heightmap");//Mapas das alturas

            // Calcula a aspectRatio, a view matrix e a projeção
            float aspectRatio = (float)device.Viewport.Width / device.Viewport.Height;
            effect.View = Matrix.CreateLookAt(new Vector3(1.0f, 2.0f, 2.0f), Vector3.Zero, Vector3.Up); //Alterar a view 
            effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(90.0f), aspectRatio, 2.0f, 1000.0f);
            effect.LightingEnabled = false;
            effect.VertexColorEnabled = false;
            effect.TextureEnabled = true;

            // Cria os eixos 3D
            CreateGeometry(device);
        }
        private void CreateGeometry(GraphicsDevice device)
        {
            Color[] texels = new Color[texturaAlturas.Height * texturaAlturas.Width];
            vertices = new VertexPositionNormalTexture[texturaAlturas.Height * texturaAlturas.Width];
            texturaAlturas.GetData<Color>(texels);
            texturaAlturasAlturas = new float[texturaAlturas.Height * texturaAlturas.Width];


            for (int x = 0; x < (texturaAlturas.Width); x++)
            {
                for (int z = 0; z < (texturaAlturas.Height); z++)
                {
                    Color corTexels = texels[z * texturaAlturas.Width + x];
                    float y = corTexels.R * escala; //Buscar a altura de um certo ponto do terreno
                    v = new VertexPositionNormalTexture(new Vector3(x, y, z), Vector3.Up, new Vector2(x % 2, z % 2)); //Vertice criado       
                    vertices[z * texturaAlturas.Width + x] = v;
                    texturaAlturasAlturas[z * texturaAlturas.Width + x] = y;
                }
            }

            vertexBuffer = new VertexBuffer(device, typeof(VertexPositionNormalTexture), vertices.Length, BufferUsage.None);
            vertexBuffer.SetData<VertexPositionNormalTexture>(vertices);

            indices = new short[texturaAlturas.Height * 2 * (texturaAlturas.Width - 1)];

            for (int xi = 0; xi < (texturaAlturas.Width - 1); xi++)
            {
                for (int zi = 0; zi < (texturaAlturas.Height); zi++)
                {
                    indices[2 * zi + (xi * 2 * texturaAlturas.Height) + 0] = (short)(zi * texturaAlturas.Width + xi);
                    indices[2 * zi + (xi * 2 * texturaAlturas.Height) + 1] = (short)(zi * texturaAlturas.Width + 1 + xi);
                }
            }
            indexBuffer = new IndexBuffer(device, typeof(short), indices.Length, BufferUsage.None);
            indexBuffer.SetData<short>(indices);
        }

        public void Draw(GraphicsDevice device)
        {
            // World Matrix
            effect.World = worldMatrix;
            effect.Texture = texturaAlturas;

            // Indica o efeito para desenhar os eixos
            effect.CurrentTechnique.Passes[0].Apply();
            device.SetVertexBuffer(vertexBuffer);
            device.Indices = indexBuffer;
            //effect.View = view;

            for (int i = 0; i < (texturaAlturas.Width - 1); i++)
            {
                device.DrawIndexedPrimitives(PrimitiveType.TriangleStrip, 0, i * texturaAlturas.Height * 2, 2 * texturaAlturas.Height - 2);
            }

        }
    }
}
