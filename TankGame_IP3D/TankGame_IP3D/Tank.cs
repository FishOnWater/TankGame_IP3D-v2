using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TankGame_IP3D
{
    class Tank
    {
        Model modelTank;

        Matrix world;
        Matrix view;
        Matrix projection;

        float scale;

        ModelBone turretBone;
        ModelBone canonBone;
        ModelBone rEngineBone;
        ModelBone rBackWheelBone;
        ModelBone rSteerBone;
        ModelBone rFrontWheelBone;
        ModelBone lEngineBone;
        ModelBone lBackWheelBone;
        ModelBone lFrontWheelBone;
        ModelBone lSteerBone;
        ModelBone hatchBone;

        Matrix turretTransform;
        Matrix canonTransform;
        Matrix rEngineTransform;
        Matrix rBackWheelTranform;
        Matrix rSteerTransform;
        Matrix rFrontWheelTransform;
        Matrix lEngineTransform;
        Matrix lBackWheelTransform;
        Matrix lFrontWheelTransform;
        Matrix lSteerTransform;
        Matrix hatchTransform;

        float turretAngle = 0.0f;
        float canonAngle = 0.01f;

        Matrix[] boneTransforms;

        ClsBattlefield terreno;

        public Tank(GraphicsDevice device, ContentManager content)
        {
            modelTank = content.Load<Model>("tank");

            world = Matrix.CreateScale(0.005f);

            float aspectRatio = (float)device.Viewport.Width / device.Viewport.Height;
            view = Matrix.CreateLookAt(
                               new Vector3(2.0f, 3.0f, 5.0f),
                              Vector3.Zero, Vector3.Up);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f), aspectRatio, 0.01f, 1000.0f);
            scale = 0.001f;

            turretBone = modelTank.Bones["turret_geo"];
            canonBone = modelTank.Bones["canon_geo"];
            rEngineBone = modelTank.Bones["r_engine_geo"];
            lEngineBone = modelTank.Bones["l_engine_geo"];
            rBackWheelBone = modelTank.Bones["r_back_wheel_geo"];
            lBackWheelBone = modelTank.Bones["l_back_wheel_geo"];
            rSteerBone = modelTank.Bones["r_steer_geo"];
            lSteerBone = modelTank.Bones["l_steer_geo"];
            rFrontWheelBone = modelTank.Bones["r_front_wheel_geo"];
            lFrontWheelBone = modelTank.Bones["l_front_wheel_geo"];
            hatchBone = modelTank.Bones["hatch_geo"]; 

            turretTransform = turretBone.Transform;
            canonTransform = canonBone.Transform;
            rEngineTransform = rEngineBone.Transform;
            lEngineTransform = lEngineBone.Transform;
            rBackWheelTranform = rBackWheelBone.Transform;
            lBackWheelTransform = rBackWheelBone.Transform;
            rSteerTransform = rSteerBone.Transform;
            lSteerTransform = lSteerBone.Transform;
            rFrontWheelTransform = rFrontWheelBone.Transform;
            lFrontWheelTransform = lFrontWheelBone.Transform;
            hatchTransform = hatchBone.Transform;


            boneTransforms = new Matrix[modelTank.Bones.Count];

            terreno = new ClsBattlefield(device, content);
        }

        public void GetTankNormal()
        {
        }

        public void UpdateTankStuff(KeyboardState keyboard)
        {
            if (keyboard.IsKeyDown(Keys.Left))
                turretAngle += MathHelper.ToRadians(1.0f);
            if (keyboard.IsKeyDown(Keys.Right))
                turretAngle -= MathHelper.ToRadians(1.0f);
            if (keyboard.IsKeyDown(Keys.Up))
                canonAngle += MathHelper.ToRadians(1.0f);
            if (keyboard.IsKeyDown(Keys.Down))
                canonAngle -= MathHelper.ToRadians(1.0f);
        }

        public void Draw()
        {
            modelTank.Root.Transform = Matrix.CreateScale(scale) * Matrix.CreateRotationY((float)Math.PI / 2.0f);
            turretBone.Transform = Matrix.CreateRotationY(turretAngle) * turretTransform;
            canonBone.Transform = Matrix.CreateRotationX(canonAngle) * canonTransform;

            modelTank.CopyAbsoluteBoneTransformsTo(boneTransforms);

            foreach (ModelMesh mesh in modelTank.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = boneTransforms[mesh.ParentBone.Index];
                    effect.View = view;
                    effect.Projection = projection;
                    effect.EnableDefaultLighting();
                }
                // Draw each mesh of the model 
                mesh.Draw();
            }
        }
    }
}
