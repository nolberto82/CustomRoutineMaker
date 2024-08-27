.gba
.create "out.bin", 0x00000000
.definelabel branch,0x08011000
.definelabel function,0x0203ff00


.org	branch
ldr	r0,=function+1
bx	r0
.pool

.org	function

ldr	r0,=branch+9
push	{r0}

ldr	r2,=0x030053f4
mov	r0,1
ldrb	r2,[r2]
and	r0,r2

ldrb	r0,[r1,0x10]
beq	end
mov	r0,0

end:
strb	r0,[r1,0x10]
pop	{pc}

.pool
.close
