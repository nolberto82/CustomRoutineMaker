.gba
.create "out.bin", 0x00000000


.org	0x08006078
ldr	r7,=0x0203ff00
bx	r7
.pool


.org	0x0203ff00

ldr 	r7,=0x08006078+9
mov	r11,r7

ldr	r6,=0x080055b7
ldr	r7,[sp,0x20]
cmp	r6,r7
bne	end

mov	r7,r11
add	r11,r7,0x0

end:
ldrh	r7,[r5,0x2c]
ldrh	r6,[r5,0x2e]

sub	r1, r2, r1
lsr	r2, r0, #1
add  	r1, r1, r2
cmp 	r1, r0

mov	pc,r11

.pool
.close
