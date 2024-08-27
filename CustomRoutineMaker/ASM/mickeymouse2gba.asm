.gba
.create "out.bin", 0x00000000
.definelabel branch,0x0800ec28
.definelabel function,0x0203ff00


.org	branch
ldr	r3,=function+1
bx	r3
.pool

.org	function

ldr	r3,=branch+9
push	{r3}

mov	r3,0xe1
ldrb	r3,[r4,r3]
mov	r2,8
and	r3,r2
beq	end

mov	r0,0
strb	r0,[r4,3]

end:
lsl 	r0,r0,2
add 	r0,r0,r1
ldr 	r1,[r0,0x00]
add 	r0,r4,0

pop	{pc}

.pool
.close
