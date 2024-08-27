.gba
.create "out.bin", 0x00000000


.org	0x0806fca8
ldr	r3,=0x0203ff01
bx	r3
.pool

.org	0x0203ff00

mov 	r0,lr
add	r0,8
mov	lr,r0

ldrb	r0,[r4]
cmp	r0,0xf0
blt	end
mov	r3,0xf0
and	r3,r0
strb	r3,[r2]
end:
ldr	r3,=0x082d88b8
lsl	r0,r0,2
add	r0,r0,r3

mov	pc,lr

.pool
.close
