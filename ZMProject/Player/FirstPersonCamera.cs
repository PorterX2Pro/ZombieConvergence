using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZMProject
{
    class Camera
    {
        //Attributes
        private Vector3 cameraPosition;   //position
        private Vector3 cameraRotation;   //rotation
        private Vector3 cameraLookAt;     //look at vector
        private float cameraSpeed;        //camera move speed
        private Vector3 mouseRotationBuffer;    //rotation buffer for mouse movement
        private MouseState prevMouseState;      //previous mouse state
        private MouseState currentMouseState;   //current mouse state

        //Properties

        //Gets or sets our camera's position vector
        public Vector3 Position
        {
            get { return cameraPosition; }
            set
            {
                cameraPosition = value;
                //Must update the look at vector post translation or rotation
                //changes.
                UpdateLookAt();
            }
        }

        public void SetSpeed(float newS)
        {
            cameraSpeed = newS;
        }

        //Gets or sets our camera's rotation vector
        public Vector3 Rotation
        {
            get { return cameraRotation; }
            set
            {
                cameraRotation = value;
                //Must update the look at vector post translation or rotation
                //changes.
                UpdateLookAt();
            }
        }

        public Matrix Projection
        {
            get;
            protected set;
        }

        //Returns our camera's view matrix
        public Matrix View
        {
            get
            {
                return Matrix.CreateLookAt(Position, cameraLookAt, Vector3.Up);
            }
        }


        //Constructor
        public Camera(Game game, Vector3 position, Vector3 rotation, float speed)
        {
            Position = position;
            Rotation = rotation;
            this.cameraSpeed = speed;

            //Setup the projection matrix
            Projection = Matrix.CreatePerspectiveFieldOfView(
                    MathHelper.PiOver4,
                    game.GraphicsDevice.Viewport.AspectRatio,
                    0.05f,
                    1000.0f);

            prevMouseState = Mouse.GetState();
        }

        //Updates the camera's lookAt vector
        private void UpdateLookAt()
        {
            //Calculate a rotation matrix from our camera's rotation, used
            //to orient our look at vector
            Matrix rotationMatrix = Matrix.CreateRotationX(cameraRotation.X) *
                                  Matrix.CreateRotationY(cameraRotation.Y);
            //Create the look at offset vector based on the direction our camera is
            //originally looking and our rotation matrix
            Vector3 lookAtOffset = Vector3.Transform(
                    Vector3.UnitZ, rotationMatrix);
            //Finally, build the camera's look at vector by adding
            //our current position and the look at vector offset.
            cameraLookAt = cameraPosition + lookAtOffset;
        }

        public void Update(GameTime gameTime, Game game)
        {
            //Delta time
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            currentMouseState = Mouse.GetState();

            Vector3 moveVector = Vector3.Zero;
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
            {
                cameraSpeed = 55;
            }
            else
            {
                cameraSpeed = 35;
            }

            //Handle basic key movement
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                moveVector.Z = 1; //cameraSpeed * dt;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                moveVector.Z = -1; //cameraSpeed * dt;
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                moveVector.X = 1;// cameraSpeed* dt;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                moveVector.X = -1;//cameraSpeed * dt;

            //Now if our movement vector is not zero
            if (moveVector != Vector3.Zero)
            {
                //We must normalize the vector (make it of unit length (1))
                moveVector.Normalize();
                //Now add in our camera speed and delta time
                moveVector *= cameraSpeed * dt;

                //This is for checking movement parameters
                //Vector3 newLoc = PreviewMove(moveVector);

                //Now we move the camera using that movement vector
                Move(moveVector);
            }

            //Change in mouse position
            //x and y
            float deltaX;
            float deltaY;

            //Handle mouse movement
            if (currentMouseState != prevMouseState)
            {
                //Get the change in mouse position
                deltaX = Mouse.GetState().X - (game.GraphicsDevice.Viewport.Width / 2);
                deltaY = Mouse.GetState().Y - (game.GraphicsDevice.Viewport.Height / 2);

                //This is used to buffer against use input.
                mouseRotationBuffer.X -= 0.31f * deltaX * dt;
                mouseRotationBuffer.Y -= 0.31f * deltaY * dt;

                if (mouseRotationBuffer.Y < MathHelper.ToRadians(-75.0f))
                    mouseRotationBuffer.Y = mouseRotationBuffer.Y - (mouseRotationBuffer.Y - MathHelper.ToRadians(-75.0f));
                if (mouseRotationBuffer.Y > MathHelper.ToRadians(90.0f))
                    mouseRotationBuffer.Y = mouseRotationBuffer.Y - (mouseRotationBuffer.Y - MathHelper.ToRadians(90.0f));


                Rotation = new Vector3(-MathHelper.Clamp(mouseRotationBuffer.Y, MathHelper.ToRadians(-75.0f),
                    MathHelper.ToRadians(90.0f)), MathHelper.WrapAngle(mouseRotationBuffer.X), 0);

                deltaX = 0;
                deltaY = 0;
            }

            Mouse.SetPosition(game.Window.ClientBounds.Width / 2, game.Window.ClientBounds.Height / 2);

            prevMouseState = currentMouseState;
        }

        //Sets the camera's position and rotation
        public void MoveTo(Vector3 position, Vector3 rotation)
        {
            Position = position;
            Rotation = rotation;
        }

        //Used to move simulate camera movement
        //without actually moving the camera
        //Good for checking collision before allowing player to move
        public Vector3 PreviewMove(Vector3 amount)
        {
            Matrix rotate = Matrix.CreateRotationY(cameraRotation.Y);
            Vector3 movement = new Vector3(amount.X, amount.Y, amount.Z);
            movement = Vector3.Transform(movement, rotate);
            return cameraPosition + movement;
        }

        //Actually moves the camera by the scale factor passed in
        public void Move(Vector3 scale)
        {
            MoveTo(PreviewMove(scale), Rotation);
        }
    }
}
