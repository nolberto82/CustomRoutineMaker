.gba
.create "out.bin", 0x00000000
.definelabel branch,0x0802d254
.definelabel function,0x0203ff00


.org	branch
ldr	r0,=function+1
bx	r0
.pool

.org	function

ldr	r0,=branch+9
push	{r0}

ldr	r0,[r4]
ldr	r3,=0x29ba
add	r0,r0,r3

push	{r0-r7}
ldr	r3,=0x47c1-0xc
add	r1,r6,r3
ldrb	r1,[r1]
mov	r3,2
and	r1,r3
beq	end

mov	r3,4
strb	r3,[r0]
mov	r3,0
strb	r3,[r0,2]
	
end:
pop	{r0-r7}
ldr	r0,[r0]
pop	{pc}

.pool
.close
