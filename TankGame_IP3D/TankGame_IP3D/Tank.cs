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
        ModelBone rFrontWheel;
        ModelBone lEngineBone;
        ModelBone lBackWheel;
        ModelBone lFrontWheel;
        ModelBone lSteerBone;
        ModelBone hatchBone;

        Matrix turretTransform;
        Matrix canonTransform;
        float turretAngle = 0.0f;
        float canonAngle = 0.01f;

        Matrix[] boneTransforms;

        public Tank(GraphicsDevice device, ContentManager content)
        {
            modelTank = content.Load<Model>("tank");

            world = Matrix.CreateScale(0.005f);

            float aspectRatio = (float)device.Viewport.Width / device.Viewport.Height;
            view = Matrix.CreateLookAt(
                               new Vector3(2.0f, 3.0f, 5.0f),
                              Vector3.Zero, Vector3.Up);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f), aspectRatio, 0.01f, 1000.0f);
            scale = 0.0005f;

            turretBone = modelTank.Bones["turret_geo"];
            canonBone = modelTank.Bones["canon_geo"];
            rEngineBone = modelTank.Bones["r_engine_geo"];
            lEngineBone = modelTank.Bones["l_engine_geo"];
            rBackWheelBone = modelTank.Bones["r_back_wheel_geo"];
            lBackWheel = modelTank.Bones["l_back_wheel_geo"];
            rSteerBone = modelTank.Bones["r_steer_geo"];
            lSteerBone = modelTank.Bones["l_steer_geo"];
            rFrontWheel = modelTank.Bones["r_front_wheel_geo"];
            lFrontWheel = modelTank.Bones["l_front_wheel_geo"];
            hatchBone = modelTank.Bones["hatch_geo"]; 

            turretTransform = turretBone.Transform;
            canonTransform = canonBone.Transform;


            boneTransforms = new Matrix[modelTank.Bones.Count];
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
