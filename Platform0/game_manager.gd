# game_manager.gd
extends Node

var anomaly_present = false

@onready var player = get_node("Player")
@onready var train = get_node("Train")
@onready var anomaly_cone = get_node("Anomaly_Cone")
@onready var light = get_node("DirectionalLight3D")
@onready var flicker_timer = get_node("FlickerTimer")

var start_position: Vector3
var start_rotation: Basis

func _ready():
    start_position = player.global_transform.origin
    start_rotation = player.global_transform.basis
    randomize_anomalies()

func randomize_anomalies():
    # Reset all anomalies to default state first
    anomaly_cone.hide()
    light.show()
    flicker_timer.stop()
    train.hide()

    # Decide if an anomaly should be present
    anomaly_present = [true, false].pick_random()
    print("Anomaly present: ", anomaly_present)

    if anomaly_present:
        var anomaly_type = ["cone", "light"].pick_random()
        print("Anomaly type: ", anomaly_type)
        match anomaly_type:
            "cone":
                anomaly_cone.show()
            "light":
                flicker_timer.start()

func _on_train_zone_body_entered(body):
    if body == player:
        print("Player entered train zone.")
        if not anomaly_present:
            train.show()
            print("Train arrived.")

func _on_loop_zone_body_entered(body):
    if body == player:
        print("Player entered loop zone.")
        if anomaly_present:
            var new_transform = player.global_transform
            new_transform.origin = start_position
            new_transform.basis = start_rotation
            player.global_transform = new_transform
            player.velocity = Vector3.ZERO
            print("Anomaly present. Looping player.")
            # Reroll anomalies for the next loop
            randomize_anomalies()
        else:
            print("No anomaly. You win!")
            get_tree().quit()

func _on_flicker_timer_timeout():
    # Toggle light visibility
    light.visible = not light.visible
