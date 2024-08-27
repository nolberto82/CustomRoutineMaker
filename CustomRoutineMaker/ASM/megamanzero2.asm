.gba
.create "out.bin", 0x00000000
.definelabel branch,0x08006d70
.definelabel function,0x0203fe00


.org	branch
ldr	r2,=function+1
bx	r2
.pool

.org	function

ldr	r2,=branch+9
push	{r2}

cmp	r0,0
beq	end

mov	r2,0xff
lsl	r2,r2,8
strh	r2,[r0,0x2c]
strh	r2,[r0,0x2e]
strh	r2,[r0,0x14]
strh	r2,[r0,0x16]

end:
ldr	r2,=0x614
add	r2,r2,r7
mov	r9,r2
ldr	r1,[r2]

pop	{pc}

.pool
.close
