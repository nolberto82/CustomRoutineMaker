.gba
.create "out.bin", 0x00000000
.definelabel branch,0x08017158
.definelabel function,0x0203ff00


.org	branch
ldr	r0,=function+1
bx	r0
.pool

.org	function

ldr	r0,=branch+9
push	{r0}

mov 	r0,0x94
lsl 	r0,r0,0x02
add 	r2,r5,r0
add 	r0,r5,0x00

push	{r1-r7}

mov	r6,0xb7
lsl	r6,r6,2
ldrb	r1,[r0,r6]
mov	r6,1
and	r1,r6
beq	end

mov	r6,0xe0
lsl	r6,r6,0xa
str	r6,[r2,4]

end:
pop	{r1-r7}

pop	{pc}

.pool
.close
