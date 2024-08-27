.gba
.thumb

.create "out.bin", 0x0

.definelabel pad,0x030007e0
.definelabel addr,0x0800b270

.org	addr
ldr	r2,=0x0203ff01
bx	r2
.pool


.org	0x0203ff00


ldr	r2,=addr+9
push	r2
push	{r0-r3}

ldr	r0,=pad
ldrb	r0,[r0,#2]
mov	r3,1
and	r0,r3
beq	end

ldr	r0,=0xfffffb00
str	r0,[r4,0x64]

end:
pop	{r0-r3}
add	r2,r0
ldr	r1,[r2,0x60]
ldr	r3,[r2,0x50]
cmp	r1,r3
pop	r5
bx	r5
.pool
.close
