.gba
.create "out.bin", 0x00000000
.definelabel branch,0x080055a8


.org	branch
ldr	r0,=0x0203fe01
bx	r0
.pool

.org	0x0203fe00

ldr	r0,=branch+9
push	{r0}

//ldr	r0,=0x02004cb6
//ldrb	r0,[r0]
//cmp	r0,0
//beq	end

ldr	r1,[r4]
cmp	r1,0
beq	end

mov	r0,0xff
lsl	r0,r0,8
strh	r0,[r1,0x38]
strh	r0,[r1,0x3a]
strh	r0,[r1,0x2c]
strh	r0,[r1,0x2e]

end:
mov	r0,0x14
add	r0,r0,r7
mov 	r8,r0
add	r0,r4,0

pop	{pc}

.pool
.close
