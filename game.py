from ursina import *
from ursina.prefabs.platformer_controller_2d import PlatformerController2d

app = Ursina()
window.fps_counter.visible = False


def update():
    global speed, dx, switch, rb

    red_bar.x = camera.x - size // 2
    green_bar.x = red_bar.x

    if player.intersects(door):
        quit()

    if switch == 1:
        dx += speed * time.dt
        if abs(dx) > 2:
            speed *= -1
            dx = 0
        for enemy in enemies:
            enemy.x += speed * time.dt

            if abs(player.x - enemy.x) < 1 and abs(player.y - enemy.y) < 1:
                player.rotation_z = 90
                green_bar.scale_x = 0
                switch = 0
                rb.y = 0


def reset():
    global switch
    player.rotation_z = 0
    player.x = -5
    green_bar.scale_x = 10
    switch = 1
    rb.y = 1


class HealthBar(Entity):
    def __init__(self, y, z, r, g, b):
        super().__init__()
        self.model = 'quad'
        self.scale = (10, .5)
        self.color = color.rgb(r, g, b)
        self.y = y
        self.z = z
        self.origin = (-.5, -.5)


class Enemy(Entity):
    def __init__(self, x, y):
        super().__init__()
        self.model = 'cube'
        self.texture = 'assets/sprites/sec.png'
        self.color = color.white
        self.x = x
        self.y = y


speed = 1
switch = 1
dx = 0

red_bar = HealthBar(4, 0, 255, 0, 0)
green_bar = HealthBar(4, -0.01, 0, 255, 0)

rb = Button(color=color.rgb(255, 255, 255, 0), scale=(.5, .5), y=1, icon='assets\\restart.png')
rb.on_click = reset

enemies = []

enemy = Enemy(-2.5 - 1, -.5)
enemies.append(enemy)

size = 13
bg = Entity(model='quad', scale=(size, 6), texture='assets/backgrounds/bg0.jpeg', z=1)
player = PlatformerController2d(x=-4.5, y=1, z=-.01, color=color.white,
                                texture='assets\\sprites\\courier.png')
ground = Entity(model='quad', texture='brick', color=color.brown, y=-2.37, scale_x=12.9, collider='box')
border = Entity(model='quad', color=color.black10, x=-6.4, scale_y=3.5, scale_x=0.2, collider='box')
wall = Entity(model='quad', texture='brick', scale=(1, 5), x=5.5, collider='box')
level = Entity(model='quad', texture='brick', color=color.gray, scale=(3, 1), scale_y=3.5, x=3.5, y=-0.15,
               collider='box')
ceiling = Entity(model='quad', texture='brick', color=color.black, scale=(3, 1), x=-2.5, y=-1.4, collider='box')
door = Entity(model='quad', texture='assets/sprites/gates.png', scale=5, x=18 + size, collider='box')

extension = 1
for m in range(extension):
    enemy = Enemy(2.5 - 1 + size * (m + 1), 2.5)
    enemies.append(enemy)

    enemy = Enemy(-2.5 - 1 + size * (m + 1), -.9)
    enemies.append(enemy)

    enemy = Enemy(-2.5 - 1 + size * (m + 2), -.9)
    enemies.append(enemy)

    enemy = Enemy(-2.5 - 1 + size * (m + 1.75), -.9)
    enemies.append(enemy)

    duplicate(bg, x=size * (m + 1), texture='assets/backgrounds/bg1')
    duplicate(bg, x=size * (m + 2), texture='assets/backgrounds/bg2')
    duplicate(ground, x=size)
    duplicate(ground, x=size * (m + 2))

    # duplicate(wall, x=size + 5.5)

    duplicate(level, x=2 + size)

    duplicate(ceiling, x=-2.5 + size, y=1)

camera.add_script(SmoothFollow(target=player, offset=[0, 1, -20], speed=4))

app.run()
