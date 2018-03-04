using System;
using System.Collections.Generic;

using Godot;

namespace Game {
    class Laser : Node2D {

        [Export]
        public float moveSpeed = -1500;

        private KinematicBody2D body;

        private Vector2 velocity;

        public override void _Ready() {
            base._Ready();

            this.body = FindNode("Body") as KinematicBody2D;

            this.velocity = new Vector2(0, this.moveSpeed);
        }

        public override void _PhysicsProcess(float delta) {
            base._PhysicsProcess(delta);

            if(this.body.IsQueuedForDeletion()) {
                QueueFree(); // Also deletes the parent controller node
                return;
            }

            if(this.body.GlobalPosition.y < -50) {
                // Reached edge of screen
                QueueFree();
                return;
            }

            this.body.MoveAndSlide(this.velocity);
        }

    }
}
