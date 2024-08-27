.gba
.create "out.bin", 0x00000000
.definelabel branch,0x08009698
.definelabel function,0x0203ff00


.org	branch
ldr	r0,=function+1
bx	r0
.pool

.org	function

ldr	r0,=branch+9
push	{r0}

ldr	r0,=0x03003ed4
ldr	r0,[r0]
mov	r1,1
and	r0,r1
beq	end

ldr	r0,=0xffffc700
str	r0,[r5,0x04]

end:
ldr 	r0,[r5,0x04]
ldr 	r1,[r5,0x0c]
add 	r0,r0,r1
str 	r0,[r5,0x04]

pop	{pc}

.pool
.close
