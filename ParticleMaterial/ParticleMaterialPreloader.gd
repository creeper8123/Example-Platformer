extends Node

var materials = [
	preload("res://ParticleMaterial/CoinDoorOpen.tres"),
	preload("res://ParticleMaterial/CollectCoin.tres")
]

func _ready():
	for material in materials:
		var partInst = Particles2D.new()
		partInst.process_material = material
		partInst.one_shot = true
		partInst.modulate = Color(1, 1, 1, 0)
		partInst.emitting = true
		add_child(partInst)
