.nds
.thumb
.create "out.bin", 0x02000000


.org	0x0203efd8
bl	0x02000000

.org	0x02000000

push	{r2-r3}
ldr	r2,=0x440
ldr	r3,=0x1a4
add	r3,r3,r7
ldrh	r3,[r3,2]
and	r2,r3
beq	end
mov	r1,0
mov 	r0,0
str	r1,[r4,0x68]
ldr	r2,=0x5f8+0x60
add	r2,r2,r4
mov	r3,0x40
strb	r3,[r2,3]
end:
pop	{r2-r3}
ldr	r1,[r4,0x68]
bx	r14
.pool
.close
