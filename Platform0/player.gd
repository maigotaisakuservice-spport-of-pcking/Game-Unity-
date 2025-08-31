# player.gd
extends CharacterBody3D

# Movement variables
@export var speed = 5.0
@export var sprint_speed = 8.0
#@export var jump_velocity = 4.5

# Mouse look variables
@export var mouse_sensitivity = 0.002 # Radians/pixel

# Get the camera node
@onready var camera = $Camera3D

# Gravity from project settings
var gravity = ProjectSettings.get_setting("physics/3d/default_gravity")

func _ready():
    # Hide and capture the mouse cursor
    Input.set_mouse_mode(Input.MOUSE_MODE_CAPTURED)

func _unhandled_input(event):
    # Handle mouse look
    if event is InputEventMouseMotion:
        # Rotate the player body left/right
        rotate_y(-event.relative.x * mouse_sensitivity)
        # Rotate the camera up/down
        camera.rotate_x(-event.relative.y * mouse_sensitivity)
        # Clamp camera rotation to prevent flipping
        camera.rotation.x = clamp(camera.rotation.x, -1.5708, 1.5708) # -90 to +90 degrees in radians

func _physics_process(delta):
    # Handle gravity
    if not is_on_floor():
        velocity.y -= gravity * delta

    # Handle jump
    # if Input.is_action_just_pressed("ui_accept") and is_on_floor():
    #     velocity.y = jump_velocity

    # Get input direction
    var input_dir = Input.get_vector("move_left", "move_right", "move_forward", "move_backward")
    # Transform direction relative to player's rotation
    var direction = (transform.basis * Vector3(input_dir.x, 0, input_dir.y)).normalized()

    # Determine current speed (sprinting or walking)
    var current_speed = speed
    if Input.is_action_pressed("sprint"):
        current_speed = sprint_speed

    # Apply movement
    if direction:
        velocity.x = direction.x * current_speed
        velocity.z = direction.z * current_speed
    else:
        velocity.x = move_toward(velocity.x, 0, speed)
        velocity.z = move_toward(velocity.z, 0, speed)

    move_and_slide()
