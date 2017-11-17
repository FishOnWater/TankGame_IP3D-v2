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
        Model modelTankP1;
        Model modelTankP2;

        Matrix world;
        Matrix view;
        Matrix projection;

        float scale;

        ModelBone turretBoneP1;
        ModelBone canonBoneP1;
        ModelBone rEngineBoneP1;
        ModelBone rBackWheelBoneP1;
        ModelBone rSteerBoneP1;
        ModelBone rFrontWheelBoneP1;
        ModelBone lEngineBoneP1;
        ModelBone lBackWheelBoneP1;
        ModelBone lFrontWheelBoneP1;
        ModelBone lSteerBoneP1;
        ModelBone hatchBoneP1;

        Matrix turretTransformP1;
        Matrix canonTransformP1;
        Matrix rEngineTransformP1;
        Matrix rBackWheelTranformP1;
        Matrix rSteerTransformP1;
        Matrix rFrontWheelTransformP1;
        Matrix lEngineTransformP1;
        Matrix lBackWheelTransformP1;
        Matrix lFrontWheelTransformP1;
        Matrix lSteerTransformP1;
        Matrix hatchTransformP1;

        ModelBone turretBoneP2;
        ModelBone canonBoneP2;
        ModelBone rEngineBoneP2;
        ModelBone rBackWheelBoneP2;
        ModelBone rSteerBoneP2;
        ModelBone rFrontWheelBoneP2;
        ModelBone lEngineBoneP2;
        ModelBone lBackWheelBoneP2;
        ModelBone lFrontWheelBoneP2;
        ModelBone lSteerBoneP2;
        ModelBone hatchBoneP2;

        Matrix turretTransformP2;
        Matrix canonTransformP2;
        Matrix rEngineTransformP2;
        Matrix rBackWheelTranformP2;
        Matrix rSteerTransformP2;
        Matrix rFrontWheelTransformP2;
        Matrix lEngineTransformP2;
        Matrix lBackWheelTransformP2;
        Matrix lFrontWheelTransformP2;
        Matrix lSteerTransformP2;
        Matrix hatchTransformP2;

        float turretAngleP1 = 0.0f;
        float canonAngleP1 = 0.01f;
        float steerAngleP1 = 0.01f;
        float wheelAngleP1 = 0.01f;

        float turretAngleP2 = 0.0f;
        float canonAngleP2 = 0.01f;
        float steerAngleP2 = 0.01f;
        float wheelAngleP2 = 0.01f;

        Matrix[] boneTransformsP1;
        Matrix[] bonetransformsP2;

        public Vector3 normalTankP1;
        public Vector3 normalTankP2;
        public float heightTankP1 = 5.0f;
        public float heightTankP2 = 5.0f;
        public Vector3 positionTankP1;
        public Vector3 positionTankP2;
        Vector3 speed = new Vector3(3.0f, 0.0f, 0.0f);
        float yaw = 1.0f;
        float pitch = 1.0f;

        ClsBattlefield terreno;

        public Tank(GraphicsDevice device, ContentManager content)
        {
            modelTankP1 = content.Load<Model>("tank");
            modelTankP2 = content.Load<Model>("tank");

            terreno = new ClsBattlefield(device, content);

            float aspectRatio = (float)device.Viewport.Width / device.Viewport.Height;
            view = Matrix.CreateLookAt(new Vector3(64.0f, 12.0f, 64.0f), new Vector3(64.0f, 0.0f, 0.0f), Vector3.Up);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f), aspectRatio, 0.01f, 10000.0f);
            scale = 0.05f;

            turretBoneP1 = modelTankP1.Bones["turret_geo"];
            canonBoneP1 = modelTankP1.Bones["canon_geo"];
            rEngineBoneP1 = modelTankP1.Bones["r_engine_geo"];
            lEngineBoneP1 = modelTankP1.Bones["l_engine_geo"];
            rBackWheelBoneP1 = modelTankP1.Bones["r_back_wheel_geo"];
            lBackWheelBoneP1 = modelTankP1.Bones["l_back_wheel_geo"];
            rSteerBoneP1 = modelTankP1.Bones["r_steer_geo"];
            lSteerBoneP1 = modelTankP1.Bones["l_steer_geo"];
            rFrontWheelBoneP1 = modelTankP1.Bones["r_front_wheel_geo"];
            lFrontWheelBoneP1 = modelTankP1.Bones["l_front_wheel_geo"];
            hatchBoneP1 = modelTankP1.Bones["hatch_geo"]; 

            turretTransformP1 = turretBoneP1.Transform;
            canonTransformP1 = canonBoneP1.Transform;
            rEngineTransformP1 = rEngineBoneP1.Transform;
            lEngineTransformP1 = lEngineBoneP1.Transform;
            rBackWheelTranformP1 = rBackWheelBoneP1.Transform;
            lBackWheelTransformP1 = rBackWheelBoneP1.Transform;
            rSteerTransformP1 = rSteerBoneP1.Transform;
            lSteerTransformP1 = lSteerBoneP1.Transform;
            rFrontWheelTransformP1 = rFrontWheelBoneP1.Transform;
            lFrontWheelTransformP1 = lFrontWheelBoneP1.Transform;
            hatchTransformP1 = hatchBoneP1.Transform;

            turretBoneP2 = modelTankP1.Bones["turret_geo"];
            canonBoneP2 = modelTankP1.Bones["canon_geo"];
            rEngineBoneP2 = modelTankP1.Bones["r_engine_geo"];
            lEngineBoneP2 = modelTankP1.Bones["l_engine_geo"];
            rBackWheelBoneP2 = modelTankP1.Bones["r_back_wheel_geo"];
            lBackWheelBoneP2 = modelTankP1.Bones["l_back_wheel_geo"];
            rSteerBoneP2 = modelTankP1.Bones["r_steer_geo"];
            lSteerBoneP2 = modelTankP1.Bones["l_steer_geo"];
            rFrontWheelBoneP2 = modelTankP1.Bones["r_front_wheel_geo"];
            lFrontWheelBoneP2 = modelTankP1.Bones["l_front_wheel_geo"];
            hatchBoneP2 = modelTankP1.Bones["hatch_geo"];

            turretTransformP2 = turretBoneP2.Transform;
            canonTransformP2 = canonBoneP2.Transform;
            rEngineTransformP2 = rEngineBoneP2.Transform;
            lEngineTransformP2 = lEngineBoneP2.Transform;
            rBackWheelTranformP2 = rBackWheelBoneP2.Transform;
            lBackWheelTransformP2 = rBackWheelBoneP2.Transform;
            rSteerTransformP2 = rSteerBoneP2.Transform;
            lSteerTransformP2 = lSteerBoneP2.Transform;
            rFrontWheelTransformP2 = rFrontWheelBoneP2.Transform;
            lFrontWheelTransformP2 = lFrontWheelBoneP2.Transform;
            hatchTransformP2 = hatchBoneP1.Transform;

            boneTransformsP1 = new Matrix[modelTankP1.Bones.Count];
            bonetransformsP2 = new Matrix[modelTankP2.Bones.Count];
        }

        public void UpdateTankPosition(ClsBattlefield terreno)
        {
            KeyboardState keyboard = Keyboard.GetState();

            //Controlo da torre e canhão do Jogador 1
            if (keyboard.IsKeyDown(Keys.Left))
                turretAngleP1 += MathHelper.ToRadians(yaw);
            if (keyboard.IsKeyDown(Keys.Right))
                turretAngleP1 -= MathHelper.ToRadians(yaw);
            if (keyboard.IsKeyDown(Keys.Up))
                canonAngleP1 += MathHelper.ToRadians(yaw);
            if (keyboard.IsKeyDown(Keys.Down))
                canonAngleP1 -= MathHelper.ToRadians(yaw);

            //Controlo da torre e canhão do Jogador ~2
            if (keyboard.IsKeyDown(Keys.Left))
                turretAngleP2 += MathHelper.ToRadians(yaw);
            if (keyboard.IsKeyDown(Keys.Right))
                turretAngleP2 -= MathHelper.ToRadians(yaw);
            if (keyboard.IsKeyDown(Keys.Up))
                canonAngleP2 += MathHelper.ToRadians(yaw);
            if (keyboard.IsKeyDown(Keys.Down))
                canonAngleP2 -= MathHelper.ToRadians(yaw);

            //Controlo do tanque do Jogador 1 (steering)
            if (keyboard.IsKeyDown(Keys.A))
                steerAngleP1 += MathHelper.ToRadians(yaw);
            if (keyboard.IsKeyDown(Keys.D))
                steerAngleP1 -= MathHelper.ToRadians(yaw);

            //Controlo do tanque do Jogador 2 (steering)
            if (keyboard.IsKeyDown(Keys.A))
                steerAngleP2 += MathHelper.ToRadians(yaw);
            if (keyboard.IsKeyDown(Keys.D))
                steerAngleP2 -= MathHelper.ToRadians(yaw);

            //Controlo do tanque do Jogador 1 (Movimento)
            if (keyboard.IsKeyDown(Keys.A))
            {
                speed = Vector3.Transform(speed, Matrix.CreateRotationY(yaw));
            }

            if (keyboard.IsKeyDown(Keys.D))
            {
                speed = Vector3.Transform(speed, Matrix.CreateRotationY(-yaw));
            }

            if (keyboard.IsKeyDown(Keys.W))
            {
                positionTankP1 = positionTankP1 + speed;
                heightTankP1 = terreno.Interpolacao(positionTankP1.X, positionTankP1.Z);
            }

            if (keyboard.IsKeyDown(Keys.S))
            {
                positionTankP1 = positionTankP1 - speed;
                heightTankP1 = terreno.Interpolacao(positionTankP1.X, positionTankP1.Z);
            }

            //Controlo do tanque do Jogador 2 (Movimento)
            if (keyboard.IsKeyDown(Keys.J))
            {
                speed = Vector3.Transform(speed, Matrix.CreateRotationY(yaw));
            }

            if (keyboard.IsKeyDown(Keys.L))
            {
                speed = Vector3.Transform(speed, Matrix.CreateRotationY(-yaw));
            }

            if (keyboard.IsKeyDown(Keys.I))
            {
                positionTankP2 = positionTankP2 + speed;
                heightTankP2 = terreno.Interpolacao(positionTankP2.X, positionTankP2.Z);
            }

            if (keyboard.IsKeyDown(Keys.K))
            {
                positionTankP2 = positionTankP2 - speed;
                heightTankP2 = terreno.Interpolacao(positionTankP2.X, positionTankP2.Z);
            }

            //Efeito das rodas a andar do Jogador 1
            if (keyboard.IsKeyDown(Keys.W))
                wheelAngleP1 += MathHelper.ToRadians(pitch);
            if (keyboard.IsKeyDown(Keys.S))
                wheelAngleP1 -= MathHelper.ToRadians(pitch);

            //Efeito das rodas a andar do Jogador 2
            if (keyboard.IsKeyDown(Keys.I))
                wheelAngleP2 += MathHelper.ToRadians(pitch);
            if (keyboard.IsKeyDown(Keys.K))
                wheelAngleP2 -= MathHelper.ToRadians(pitch);

        }

        public void Draw(ClsBattlefield terreno, Matrix camView)
        {
            //Transforms do tanque de jogador 1
            Matrix translacaoP1 = Matrix.CreateTranslation(positionTankP1.X, terreno.Interpolacao(positionTankP1.X, positionTankP1.Z), positionTankP1.Z);

            Vector3 tankNormalP1 = terreno.vectorNormal[(int)positionTankP1.Z * terreno.alturas.Width + (int)positionTankP1.X];
            Matrix rotacaoP1 = Matrix.Identity;
            rotacaoP1.Up = tankNormalP1;
            rotacaoP1.Forward = speed;
            rotacaoP1.Right = Vector3.Cross(tankNormalP1, speed);
            world = rotacaoP1 * translacaoP1;

            modelTankP1.Root.Transform = Matrix.CreateScale(scale) * Matrix.CreateRotationY((float)Math.PI / 2.0f);

            //Controlos da torre
            turretBoneP1.Transform = Matrix.CreateRotationY(turretAngleP1) * turretTransformP1;
            canonBoneP1.Transform = Matrix.CreateRotationX(canonAngleP1) * canonTransformP1;

            //Controlos do moviemnto
            lSteerBoneP1.Transform = Matrix.CreateRotationY(steerAngleP1) * lSteerTransformP1;
            rSteerBoneP1.Transform = Matrix.CreateRotationY(steerAngleP1) * rSteerTransformP1;

            lEngineBoneP1.Transform = Matrix.CreateTranslation(positionTankP1) * lEngineTransformP1;
            rEngineBoneP1.Transform = Matrix.CreateTranslation(positionTankP1) * rEngineTransformP1;

            lBackWheelBoneP1.Transform = Matrix.CreateRotationX(wheelAngleP1) * lBackWheelTransformP1;
            rBackWheelBoneP1.Transform = Matrix.CreateRotationX(wheelAngleP1) * rBackWheelTranformP1;
            lFrontWheelBoneP1.Transform = Matrix.CreateRotationX(wheelAngleP1) * lFrontWheelTransformP1;
            rFrontWheelBoneP1.Transform = Matrix.CreateRotationX(wheelAngleP1) * rFrontWheelTransformP1;

            hatchBoneP1.Transform = Matrix.CreateTranslation(positionTankP1) * hatchTransformP1;
            turretBoneP1.Transform = Matrix.CreateTranslation(positionTankP1) * turretTransformP1;
            canonBoneP1.Transform = Matrix.CreateTranslation(positionTankP1) * canonTransformP1;

            modelTankP1.CopyAbsoluteBoneTransformsTo(boneTransformsP1);

            foreach (ModelMesh mesh in modelTankP1.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = boneTransformsP1[mesh.ParentBone.Index];
                    effect.View = camView;
                    effect.Projection = projection;
                    effect.EnableDefaultLighting();
                }
                // Draw each mesh of the model 
                mesh.Draw();
            }
            //--------------------------------------------------------------------------//----------------------------------------------------------------//

            //Transforms do tanque de jogador 2
            Matrix translacaoP2 = Matrix.CreateTranslation(positionTankP2.X, terreno.Interpolacao(positionTankP2.X, positionTankP2.Z), positionTankP2.Z);

            Vector3 tankNormalP2 = terreno.vectorNormal[(int)positionTankP2.Z * terreno.alturas.Width + (int)positionTankP2.X];
            Matrix rotacaoP2 = Matrix.Identity;
            rotacaoP2.Up = tankNormalP2;
            rotacaoP2.Forward = speed;
            rotacaoP2.Right = Vector3.Cross(tankNormalP2, speed);
            world = rotacaoP2 * translacaoP2;

            modelTankP2.Root.Transform = Matrix.CreateScale(scale) * Matrix.CreateRotationY((float)Math.PI / 2.0f);

            //Controlos da torre
            turretBoneP2.Transform = Matrix.CreateRotationY(turretAngleP2) * turretTransformP2;
            canonBoneP2.Transform = Matrix.CreateRotationX(canonAngleP2) * canonTransformP2;

            //Controlos do moviemnto
            lSteerBoneP2.Transform = Matrix.CreateRotationY(steerAngleP2) * lSteerTransformP2;
            rSteerBoneP2.Transform = Matrix.CreateRotationY(steerAngleP2) * rSteerTransformP2;

            lEngineBoneP2.Transform = Matrix.CreateTranslation(positionTankP2) * lEngineTransformP2;
            rEngineBoneP2.Transform = Matrix.CreateTranslation(positionTankP2) * rEngineTransformP2;

            lBackWheelBoneP2.Transform = Matrix.CreateRotationX(wheelAngleP2) * lBackWheelTransformP2;
            rBackWheelBoneP2.Transform = Matrix.CreateRotationX(wheelAngleP2) * rBackWheelTranformP2;
            lFrontWheelBoneP2.Transform = Matrix.CreateRotationX(wheelAngleP2) * lFrontWheelTransformP2;
            rFrontWheelBoneP2.Transform = Matrix.CreateRotationX(wheelAngleP2) * rFrontWheelTransformP2;

            hatchBoneP2.Transform = Matrix.CreateTranslation(positionTankP2) * hatchTransformP2;
            turretBoneP2.Transform = Matrix.CreateTranslation(positionTankP1) * turretTransformP2;
            canonBoneP2.Transform = Matrix.CreateTranslation(positionTankP2) * canonTransformP2;

            modelTankP2.CopyAbsoluteBoneTransformsTo(bonetransformsP2);

            foreach (ModelMesh mesh in modelTankP2.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = bonetransformsP2[mesh.ParentBone.Index];
                    effect.View = camView;
                    effect.Projection = projection;
                    effect.EnableDefaultLighting();
                }
                // Draw each mesh of the model 
                mesh.Draw();
            }
            //---------------------------------------------------//--------------------------------------//
        }
    }
}
