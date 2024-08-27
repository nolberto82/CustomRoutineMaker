.gba
.create "out.bin", 0x0
.definelabel branch,0x0203ff00
.definelabel pad,0x030032a8
.definelabel jump,0x030032a8


.org	0x08014784
ldr	r0,=branch
bx	r0
.pool


.org	0x0203ff00


push	{r1-r2}

ldr	r0,[r3,0xc]
add	r0,r1,r0
ldr	r1,=pad
ldrb	r1,[r1]
mov 	r2,1
and	r2,r1
beq	end


ldr	r0,=jump

end:
str	r0,[r2,0x18]
pop	{r1-r2}
bx	lr
.pool
.close
