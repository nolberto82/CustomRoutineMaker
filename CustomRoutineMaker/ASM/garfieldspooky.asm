.gba
.create "out.bin", 0x00000000
.definelabel branch,0x080133e0
.definelabel function,0x0203ff00


.org	branch
ldr	r3,=function+1
bx	r3
.pool

.org	function

ldr	r3,=branch+9
push	{r3}

ldrh	r3,[r6,0x2e]
cmp	r3,0
beq	end

mov	r3,3
strh	r3,[r6,0x1e]
strb	r3,[r6,0x08]

end:
mov 	r3,0x01
lsr 	r0,r0,0x10
str 	r3,[r4,0x00]
str 	r0,[r4,0x04]

pop	{pc}

.pool
.close
