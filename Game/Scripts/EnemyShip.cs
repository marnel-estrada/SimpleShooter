using System;
using System.Collections.Generic;

using Godot;

namespace Game {
    class EnemyShip : Node2D {

        [Export]
        public int minSpeed = 100;

        [Export]
        public int maxSpeed = 300;

        private float moveSpeed;

        private static Random random = new Random();

        private const float ROTATE_SPEED = Mathf.PI * 2;

        public override void _Ready() {
            base._Ready();

            this.moveSpeed = random.Next(this.minSpeed, this.maxSpeed);
        }

        public override void _Process(float delta) {
            base._Process(delta);

            // Just move down
            Vector2 position = this.Position;
            position.y += this.moveSpeed * delta;
            SetPosition(position);

            Rotate(ROTATE_SPEED * delta);
        }
		
		private void _OnBodyEntered(Godot.Object body) {
            ((Node)body).QueueFree();
            QueueFree();
		}

    }
}
