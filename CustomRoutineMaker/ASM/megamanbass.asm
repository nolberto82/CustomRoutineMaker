.gba
.create "out.bin", 0x00000000
.definelabel branch,0x080121c4
.definelabel function,0x0203ff00


.org	branch
ldr	r2,=function+1
bx	r2
.pool

.org	function

ldr	r2,=branch+9
push	{r2}
ldrb	r2,[r6,0x15]
mov	r0,0x80
and	r0,r2
beq	end

mov	r2,5
lsl	r1,r2,8

end:
strh	r1,[r3,0x2e]
add	r2,r1
add	r0,r3
add	r0,0xb6

pop	{pc}

.pool
.close
