.gba
.create "out.bin", 0x00000000
.definelabel branch,0x08065b2c
.definelabel function,0x0203ff00


.org	branch
ldr	r1,=function+1
bx	r1
.pool

.org	function

ldr	r1,=branch+9
push	{r1}

ldr	r1,=0x030001f0
ldrb	r1,[r1]
mov	r2,1
and	r1,r2
beq	end

mov	r2,0x50
add	r2,r4,r2
mov	r1,0
strb	r1,[r2]

end:
ldr	r0,[r0]
mov	r1,0x99
lsl	r1,1
add	r0,r0,r1
pop	{pc}

.pool
.close
